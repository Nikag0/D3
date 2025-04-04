using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace DynamicDataDisplay1
{
    public class ViewModelMainWindow
    {
        private GraphicService graphicService = new GraphicService();
        private int step = 0;

        public PlotModel MyPlotModel { get; private set; }

        public ViewModelMainWindow()
        {
            MyPlotModel = new PlotModel { Title = "Пример графика с привязкой" };

            LineSeries series = new LineSeries
            { 
                Title = "Данные",
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            // Добавляем точки данных
            var dataPoints = new List<DataPoint>();
            for (double x = 0; x < 10; x += 0.1)
            {
                dataPoints.Add(new DataPoint(x, Math.Sin(x)));
            }
            series.ItemsSource = dataPoints;

            // Добавляем серию в модель
            MyPlotModel.Series.Add(series);

        }

      
        private void UpdateCyclyeGraphic()
        {
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50) };

            timer.Tick += (sender, e) =>

            {
            };

            timer.Start();
        }

        //Метод вызывает все методы, необходимые для построения цикличного графика.
        private void DrawCycleGraphic()
        {
            graphicService.AmplitudeData(pathFolder: "C:\\Users\\Пользователь\\Desktop\\Data2ForD3", nameFile: "_CH-2_OnWr-2.txt");
            graphicService.TimeData();
            graphicService.FindSequence(graphicService.FindIndexOfArray(graphicService.ListAllPointAmpl, strob: 20, numArraySequence: 2), sequenceLength: 5);
            //UpdateCyclyeGraphic();
        }

        private void DrawStaticGraphic()
        {
            string path = @"C:\Users\Пользователь\Desktop\DataForD3\0_CH-1_OnWr-2.txt";
            string[] dataString = File.ReadAllLines(path);

            double[] xValues = new double[dataString.Length];
            double[] yValues = new double[dataString.Length];

            for (int i = 0; i < dataString.Length; i++)
            {
                xValues[i] = i / 100.0;
                yValues[i] = double.Parse(dataString[i]);
            }
        }

        private void DrawSineGraphic()
        {
            const int pointCount = 100;
            const double xMin = 0;
            const double xMax = 2 * Math.PI;

            double[] xValues = new double[pointCount];
            double[] yValues = new double[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                double x = xMin + (xMax - xMin) * i / (pointCount - 1);
                xValues[i] = x;
                yValues[i] = Math.Sin(x);
            }
        }
    }
}
