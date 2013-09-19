using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectUtil
{
    public class KinectInitializer
    {
        private KinectSensorChooser _kinectChooser;
        private Action<KinectSensor> _initializeResourcesMethod;

        public KinectSensor Kinect
        { get { return _kinectChooser.Kinect; } }

        public void Initialize(Action<KinectSensor> initializeResourcesMethod)
        {
            _initializeResourcesMethod = initializeResourcesMethod;

            _kinectChooser = new KinectSensorChooser();
            _kinectChooser.KinectChanged += KinectChooser_KinectChanged;
            _kinectChooser.Start();
        }

        private void KinectChooser_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            KinectSensor oldSensor = e.OldSensor;
            if (oldSensor != null)
            {
                try
                {
                    if (oldSensor.ColorStream.IsEnabled)
                        oldSensor.ColorStream.Disable();

                    if (oldSensor.DepthStream.IsEnabled)
                        oldSensor.DepthStream.Disable();

                    if (oldSensor.SkeletonStream.IsEnabled)
                        oldSensor.SkeletonStream.Disable();

                }
                catch (InvalidOperationException ex)
                {
                    //Caso o sensor entre em um estado inválido ele cairá nesta exceção
                }
            }

            if (e.NewSensor != null)
            {
                if (_initializeResourcesMethod != null)
                    _initializeResourcesMethod(e.NewSensor);
            }
        }

    }
}
