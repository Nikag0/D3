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
using Microsoft.Research.DynamicDataDisplay.Filters;
using System.Reflection;
using System.Security.Policy;
using static System.Net.WebRequestMethods;
using System.Net.NetworkInformation;

namespace DynamicDataDisplay1
{
    public class GraphicService
    {
        private List<double[]> listAllPointAmpl = new List<double[]>();
        private int maxLengthPointOfA = 0;
        public string[] NumberOdFiles { get; private set; }
        private double k = 0.0;

        public List<double[]> ListAllPointAmpl { get { return listAllPointAmpl; } private set { listAllPointAmpl = value; } }
        public double[] MassAllPointTime { get; private set; }

        //Заполнение массива значениями по оси Амплитуд.
        public void AmplitudeData(string pathFolder, string nameFile)
        {
            NumberOdFiles = Directory.GetFiles(pathFolder);
            k = 360.0 / (double)NumberOdFiles.Length;

            for (int i = 0; i < NumberOdFiles.Length - 1; i++)
            {
                string[] MassPointAStr = System.IO.File.ReadAllLines($@"{pathFolder}\{i}{nameFile}");
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

        //Вторая версия метода FindInfexOfArray.
        //Возвращает лист индексов массивов, где значение элементов массива больше или равно strob и кол-во элементов больше numArraySequence.
        public List<int> FindIndexOfArray(List<double[]> arraysList, double strob, int numArraySequence)
        {
            int num = 0;
            List<int> indexOfArray = new List<int>();

            for (int i = 0; i < arraysList.Count; i++)
            {
                for (int j = 630; j < 840; j++)
                {
                    if (arraysList[i][j] >= strob)
                    {
                        num++;
                    }
                }

                if (num > numArraySequence)
                {
                    indexOfArray.Add(i);
                }

                num = 0;
            }
            return indexOfArray;
        }

        //Выписывает из numbers  первый и последний индекс последовательности.
        //Эта последовательность должна состояить из 5 чисел, т.к. разница между первым и последним элементом равна аргументу sequenceLength.
        public void FindSequence(List<int> numbers, int sequenceLength)
        {
            string filePath = "Defects.txt";
            List<int> angles = new List<int>();

            for (int i = 0; i <= numbers.Count - sequenceLength; i++)
            {
                if (numbers[i + sequenceLength - 1] - numbers[i] == sequenceLength - 1)
                {
                    angles.Add(numbers[i]);
                }
            }

            int n = 0;

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i <= angles.Count - 1; i++)
                {

                    if (i + 1 < angles.Count && angles[i + 1] - angles[i] == 1)
                    {
                        n++;
                    }
                    else
                    {
                        writer.WriteLine($"Дефекты на углах {(angles[i] - n) * k} - {(angles[i] + sequenceLength - 1) * k}, массивы с {angles[i] - n} по {(angles[i] + sequenceLength - 1)}");
                        n = 0;
                    }

                }
            }
        }
    }
}
