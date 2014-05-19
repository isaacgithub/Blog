using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WebTron.Resources;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WebTron
{
    public partial class MainPage : PhoneApplicationPage
    {
        private WebSocket4Net.WebSocket _websocket;
        private int[,] _matriz;
        private bool _mostrarCanvas = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _matriz = new int[150, 150];
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string ip;
            string jogar;

            NavigationContext.QueryString.TryGetValue("ip", out ip);
            NavigationContext.QueryString.TryGetValue("jogar", out jogar);

            string conexao = string.Concat("ws://", ip, ":8080/TronWebsocket/tron");

            _mostrarCanvas = Convert.ToBoolean(jogar);
            if (!_mostrarCanvas)
            {
                ConfigurarVisualizacaoBotao(btnTop);
                ConfigurarVisualizacaoBotao(btnBottom);
                ConfigurarVisualizacaoBotao(btnLeft);
                ConfigurarVisualizacaoBotao(btnRight);
            }

            _websocket = new WebSocket4Net.WebSocket(conexao);

            //Caso queira implementar demais eventos
            //_websocket.Opened += websocket_Opened;
            //_websocket.Error += websocket_Error;
            //_websocket.Closed += websocket_Closed;

            _websocket.MessageReceived += websocket_MessageReceived;
            _websocket.Open();
        }

        public void ConfigurarVisualizacaoBotao(Button button)
        {
            button.BorderBrush = new SolidColorBrush(Colors.White);
            button.BorderThickness = new Thickness(1);
        }

        //VALUE
        // 0
        // 1 e 2 jogador
        // 3 parede
        void websocket_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (e.Message.Contains("winner"))
                {
                    Jogador jogador = Newtonsoft.Json.JsonConvert.DeserializeObject<Jogador>(e.Message);
                    FalarMensagem(jogador);
                }

                else if (_mostrarCanvas)
                {
                    DesenharCanvas(e);
                }
            });

        }

        private async void FalarMensagem(Jogador jogador)
        {
            Windows.Phone.Speech.Synthesis.SpeechSynthesizer fala = new Windows.Phone.Speech.Synthesis.SpeechSynthesizer();
            await fala.SpeakTextAsync("Player " + jogador.Winner + " wins the match.");
        }

        private void DesenharCanvas(WebSocket4Net.MessageReceivedEventArgs e)
        {
            List<Element> elements = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Element>>(e.Message);
            if (elements.Count > 1000)
            {
                ReiniciarPartida();
            }
            foreach (var element in elements.Where(element => element.Value != 0))
            {
                DesenharPersonagens(element);
            }
        }

        private void DesenharPersonagens(Element element)
        {
            Rectangle rec = new Rectangle();
            rec.Height = 3;
            rec.Width = 3;
            Canvas.SetLeft(rec, element.X * 3);
            Canvas.SetTop(rec, element.Y * 3);
            Color cor = Colors.Black;
            switch (element.Value)
            {
                case 1:
                    cor = Colors.Red;
                    break;
                case 2:
                    cor = Colors.Green;
                    break;
                case 3:
                    cor = Colors.White;
                    break;
            }
            rec.Fill = new SolidColorBrush(cor);
            canvas.Children.Add(rec);
        }

        private void ReiniciarPartida()
        {
            ContentPanel.Children.Remove(canvas);
            canvas = new Canvas();
            Grid.SetRow(canvas, 2);
            canvas.Margin = new Thickness(0);
            canvas.Width = 450;
            canvas.Height = 450;
            Grid.SetRowSpan(canvas, 4);
            Grid.SetColumnSpan(canvas, 4);
            ContentPanel.Children.Insert(0, canvas);
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Button btn = sender as Button;
            _websocket.Send(btn.Tag.ToString());
        }
    }
}
