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

namespace DynamicDataDisplay1
{
    public class GraphicService
    {
        private int numberOfFiles = 1056;
        private List<double[]> listAllPointAmpl = new List<double[]>();
        int maxLengthPointOfA = 0;
        //Лист indexOfArray используется для первой версии метода FindInfexOfArray.
        //private readonly List<double> indexOfArray = new List<double>();

        public List<double[]> ListAllPointAmpl { get { return listAllPointAmpl; } private set { listAllPointAmpl = value; } }
        public double[] MassAllPointTime { get; private set; }

        //Заполнение массива значениями по оси Амплитуд.
        public void AmplitudeData(string pathFolder, string nameFile)
        {
            for (int i = 0; i < numberOfFiles; i++)
            {
                string[] MassPointAStr = File.ReadAllLines($@"{pathFolder}\{i}{nameFile}");
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
        public List<double> FindIndexOfArray(List<double[]> arraysList, double strob, int numArraySequence)
        {
            int num = 0;
            List<double> indexOfArray = new List<double>();

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

        //Первая версия метода FindInfexOfArray.
        //Записывает индексы массивов, подходящие под условия.
        /*public void FindInfexOfArray(double strob, int numArraySequence)
        {
            int num = 0;

            for (int i = 0; i < listAllPointAmpl.Count; i++)
            {
                for (int j = 630; j < 840; j++)
                {
                    //Пропускает только точки больше или равные 7.
                    if (listAllPointAmpl[i][j] >= strob)
                    {
                        num++;
                    }
                }

                //Если таких массивов идёт подряд больше, чем 2, добавляет индекс массивов в лист indexOfArray.
                if (num > numArraySequence)
                {
                    indexOfArray.Add(i);
                }

                num = 0;
            }
            FindSequence(indexOfArray, 4);
        }*/


        //Выписывает из numbers  первый и последний индекс последовательности.
        //Эта последовательность должна состояить из 5 чисел, т.к. разница между первым и последним элементом равна аргументу sequenceLength.
        public void FindSequence(List<double> numbers, int sequenceLength)
        {
            string filePath = "Defects.txt";

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i <= numbers.Count - sequenceLength; i++)
                {
                    if (numbers[i + sequenceLength - 1] - numbers[i] == sequenceLength - 1)
                    {
                        writer.WriteLine($"Дефекты на углах {numbers[i] * 0.34058} - {numbers[i + sequenceLength -1 ] * 0.34058}, массивы с {numbers[i]} по {numbers[i + sequenceLength - 1]}");
                        i += sequenceLength - 1;
                    }
                }
            }
        }
    }
}
