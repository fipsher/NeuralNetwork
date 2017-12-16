using NeuralNetwork.Console.Utils;
using NeuralNetwork.Implementation;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using NN.Presentation.Form.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

using WindowsForm = System.Windows.Forms.Form;

namespace NN.Presentation.Form
{
    public partial class NN : WindowsForm
    {
        NeuralNetworkImplementation _neuralNetwork;
        Func<double, double> _function = (x) => 5 * x + 3;
        Dictionary<int, Func<double, double>> funcDictionary;

        public static List<T> GetData<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
                return items;
            }
        }

        public NN()
        {
            InitializeComponent();
            funcDictionary = new Dictionary<int, Func<double, double>>
            {
                {1, (x) =>  x },
                {2, (x) =>  x * x },
                {3, (x) => x > 0 ? 1 : 0 }
            };
            var linear = GetData<LinearLayerModel>(@"D:\Projects\NeuralNetwork-master\NN.Interpolation\source1.json");
            var layers1 = LayerModelToBaseLayer.MapLinear(linear, funcDictionary);
            _neuralNetwork = new NeuralNetworkImplementation(layers1, 1);
            VisualiseNN();
        }

        private void NNRun_Click(object sender, EventArgs e)
        {
            string actual = "Actual";
            string expected = "Expected";
            string currentChartArea = "CurrentChartArea";



            if (NNChart.Series.Any(s => s.Name == actual))
            {
                NNChart.Series.Remove(NNChart.Series[actual]);
            }
            if (NNChart.Series.Any(s => s.Name == expected))
            {
                NNChart.Series.Remove(NNChart.Series[expected]);
            }

            NNChart.Series.Add(actual);
            NNChart.Series.Add(expected);
            NNChart.Series[actual].ChartType = SeriesChartType.Line;
            NNChart.Series[expected].ChartType = SeriesChartType.FastLine;
            NNChart.Series[actual].BorderWidth = 3;
            NNChart.Series[expected].BorderWidth = 3;
            NNChart.Series[actual].ChartArea = currentChartArea;
            NNChart.Series[expected].ChartArea = currentChartArea;


            StringBuilder values = new StringBuilder();
            for (double i = 0; i < 3; i+= 0.01)
            {
                NNParameter<double> actualResult = _neuralNetwork.Run(new NNParameter<double>(i));

                var actualValue = actualResult.Collection.First();
                var exactValue = _function(i);

                NNChart.Series[actual].Points.AddXY(i, actualValue);
                NNChart.Series[expected].Points.AddXY(i, exactValue);

                values.AppendLine($"X: {i}; \r Actual:{actualValue}; \r Exact:{exactValue}; \r Error:{Math.Abs(actualValue - exactValue)}");
                values.AppendLine();
            }
            ShowPoints p = new ShowPoints(values.ToString());
            p.Show();
        }

        private void NNTrain_Click(object sender, EventArgs e)
        {
            var linear = GetData<LinearLayerModel>(@"D:\Projects\NeuralNetwork-master\NN.Interpolation\source1.json");
            var layers1 = LayerModelToBaseLayer.MapLinear(linear, funcDictionary);
            _neuralNetwork = new NeuralNetworkImplementation(layers1, 1);

            List<NNParameter<double>> inputs = new List<NNParameter<double>>();
            List<NNParameter<double>> outputs = new List<NNParameter<double>>();

            for (double i = 0; i < 1; i += 0.01)
            {
                inputs.Add(new NNParameter<double>(Enumerable.Repeat(i, _neuralNetwork.InputsCount).ToArray()));
                outputs.Add(new NNParameter<double>(Enumerable.Repeat(_function(i), _neuralNetwork.InputsCount).ToArray()));
            }
            _neuralNetwork.Train(inputs, outputs);
            VisualiseNN();
        }

        private void VisualiseNN()
        {
            NNBox.ToNN(_neuralNetwork, 110, 10);
            NNTree.ToNN(_neuralNetwork, funcDictionary);
            NNTree.ExpandAll();
        }
    }
}
