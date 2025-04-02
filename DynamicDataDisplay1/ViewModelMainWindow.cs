using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;
namespace DynamicDataDisplay1
{
    public class ViewModelMainWindow
    {
        private ChartPlotter plotter = new ChartPlotter();
        private GraphicService graphicService = new GraphicService();
        private int step = 0;

        public ViewModelMainWindow(ChartPlotter plotter)
        {
            this.plotter = plotter;
            DrawCycleGraphic();
        }

        //Метод строит график на основе принимаемых аргументов.
        private void PlotGraphic(IEnumerable<double> xValues, IEnumerable<double> yValues)
        {
            plotter.Viewport.Visible = new Rect(0, -40, 40, 80);
            IPointDataSource compositeDataSource = xValues.AsXDataSource().Join(yValues.AsYDataSource());
            plotter.AddLineGraph(compositeDataSource, Colors.Blue, 2);
        }

        //Метод уадяет график и строит новый каждый тик тиаймера.
        private void UpdateCyclyeGraphic()
        {
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(50)};

            timer.Tick += (sender, e) =>

            {
                if (step == 1056)
                {
                    step = 0;
                }

                plotter.Children.Clear();
                PlotGraphic(graphicService.MassAllPointTime, graphicService.ListAllPointAmpl[step]);
                step++;
            };

            timer.Start();
        }

        //Метод вызывает все методы, необходимые для построения цикличного графика.
        private void DrawCycleGraphic()
        {
            graphicService.AmplitudeData(pathFolder: "C:\\Users\\Пользователь\\Desktop\\DataForD3", nameFile: "_CH-1_OnWr-2.txt");
            graphicService.TimeData();
            graphicService.FindSequence(graphicService.FindIndexOfArray(graphicService.ListAllPointAmpl, strob: 7, numArraySequence: 2), sequenceLength: 5);
            UpdateCyclyeGraphic();
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

            PlotGraphic(xValues, yValues);
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
