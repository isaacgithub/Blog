using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ImageTools.IO.Gif;
using System.Windows.Media;

namespace WebTron
{
    public partial class OpenPage : PhoneApplicationPage
    {
        public OpenPage()
        {
            InitializeComponent();
        }

        private void Jogar_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Button btn = sender as Button;
            IniciarJogo(Convert.ToBoolean(btn.Tag), ip.Text);
        }

        private void IniciarJogo(bool mostrarCanvas, string ip)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?jogar={0}&ip={1}", mostrarCanvas, ip), UriKind.Relative));
        }
    }
}