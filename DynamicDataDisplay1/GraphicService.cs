using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using Microsoft.Research.DynamicDataDisplay.Maps.DeepZoom;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay.Charts.NewLine;
using System.Threading;
using System.Timers;
using System.Runtime.InteropServices;
using System.Windows.Media.Media3D;
using DynamicDataDisplay.Markers.DataSources;

namespace DynamicDataDisplay1
{
    public class GraphicService
    {
        private int numberOfFiles = 1056;
        private List<double[]> listAllPointAmpl = new List<double[]>();

        public List<double[]> ListAllPointAmpl { get { return listAllPointAmpl; } private set { listAllPointAmpl = value; } }
        public double[] MassAllPointTime { get; private set; }

        //Заполнение массива значениями по оси Амплитуд.
        public void AmplitudeData()
        {
            for (int i = 0; i < numberOfFiles; i++)
            {
                string[] MassPointAStr = File.ReadAllLines($@"C:\Users\Пользователь\Desktop\DataForD3\{i}_CH-1_OnWr-2.txt");
                double[] MassPointADouble = new double[MassPointAStr.Length];

                for (int j = 0; j < MassPointAStr.Length; j++)
                {
                    MassPointADouble[j] = double.Parse(MassPointAStr[j]);
                }

                listAllPointAmpl.Add(MassPointADouble);
            }
        }

        //Заполнение массива значениями по оси времени.
        public void TimeData()
        {
            int maxLengthPointOfA = 0;

            foreach (double[] item in listAllPointAmpl)
            {
                if (item.Length > maxLengthPointOfA)
                {
                    maxLengthPointOfA = item.Length;
                }
            }

            MassAllPointTime = new double[maxLengthPointOfA];

            for (int i = 0; i < maxLengthPointOfA; i++)
            {
                MassAllPointTime[i] = i / 100.0;
            }

        } 
    }
}
