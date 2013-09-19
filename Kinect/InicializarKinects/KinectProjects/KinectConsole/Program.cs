using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            KinectUtil.KinectInitializer initializer = new KinectUtil.KinectInitializer();
            initializer.Initialize(
                (kinect) =>
                {
                    kinect.ElevationAngle = 10;
                }
                );
        }

    }
}
