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
using OxyPlot.Wpf;
using System.Timers;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.Research.DynamicDataDisplay.Maps.DeepZoom;
using System.Net.Http;

namespace DynamicDataDisplay1
{
    public class ViewModelMainWindow
    {
        private GraphicService graphicService = new GraphicService();
        private int step = 0;
        private List<DataPoint> points = new List<DataPoint>();
        
        public PlotModel PlotModel { get; private set; }
        private List<double[]> yValuesList;
        private int currentYArrayIndex = 0;
        private Timer timer;
        private LineSeries lineSeries;

        public ViewModelMainWindow()
        {
            graphicService.AmplitudeData(pathFolder: "C:\\Users\\Пользователь\\Desktop\\Data2ForD3", nameFile: "_CH-2_OnWr-2.txt");
            graphicService.TimeData();
            graphicService.FindSequence(graphicService.FindIndexOfArray(graphicService.ListAllPointAmpl, strob: 20, numArraySequence: 2), sequenceLength: 5);
            // Инициализация модели графика
            PlotModel = new PlotModel();

            // Создаем и добавляем серию данных
            lineSeries = new LineSeries
            {
                MarkerSize = 2,
                MarkerStroke = OxyColors.Blue
            };

            PlotModel.Series.Add(lineSeries);

            timer = new Timer(50);
            timer.Elapsed += OnTimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

            // Первоначальное обновление графика
            UpdatePlot();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {

            if (currentYArrayIndex >= graphicService.NumberOdFiles.Length)
            {
                currentYArrayIndex = 0;
            }
            else
            {
                currentYArrayIndex++;
            }

            UpdatePlot();
        }

        private async Task UpdatePlot()
        {
            //await Task.Run(() =>
            //{
                lineSeries.Points.RemoveAll(p => true);

                for (int i = 0; i <graphicService.MassAllPointTime.Length; i++) // Децимация для отображения
                {
                    points.Add(new DataPoint(i, graphicService.ListAllPointAmpl[currentYArrayIndex][i]));
                }

                lineSeries.Points.AddRange(points);
                PlotModel.Title = $"{currentYArrayIndex}";
                //Обновление графика.
                PlotModel.InvalidatePlot(true);
                points.Clear();
            //});
        }

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
