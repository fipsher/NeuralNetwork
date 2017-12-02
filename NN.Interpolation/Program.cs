using NeuralNetwork.Implementation;
using NeuralNetwork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NeuralNetwork.Console.Utils;

namespace NeuralNetwork.Console
{
    class Program
    {
        public static List<T> GetData<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<T> items = JsonConvert.DeserializeObject<List<T>>(json);
                return items;
            }
        }

        public static double Func(double x)
        {
            return x + x;
        }


        static void Main(string[] args)
        {

            int inputsCount = 1;

            var dict = new Dictionary<long, Func<double, double>>
            {
                {1, (x) => x },
                {2, (x) => x > 0 ? 1 : 0 }
            };
            var linear = GetData<LinearLayerModel>(@"D:\Projects\NeuralNetwork-master\NN.Interpolation\source1.json");
            var layers1 = LayerModelToBaseLayer.MapLinear(linear, dict);
            var neuralNetwork = new Implementation.NeuralNetwork(layers1, inputsCount);
            var result = neuralNetwork.Run(new NNParameter<double>(new List<double> { 1 }));
            
            System.Console.WriteLine(string.Join(",", result.Collection.Select(i => i.ToString()).ToArray()));

            var input = new List<NNParameter<double>>();
            var output = new List<NNParameter<double>>();
            for (double i = 0; i < 20; i+= 0.1)
            {
                input.Add(new NNParameter<double>(new List<double>() { i }));
                output.Add(new NNParameter<double>(new List<double>() { Func(i) }));
            }

            neuralNetwork.Train(input, output);


            for (double i = 20; i < 40; i+=0.1)
            {
                result = neuralNetwork.Run(new NNParameter<double>(new List<double> { i }));
                var aprox = result.Collection.Single();
                var exact = Func(i);
                System.Console.WriteLine($"{aprox} //// {exact} //// {Math.Abs(aprox - exact)}");
            }

            System.Console.WriteLine(Func(1));


            System.Console.ReadKey();
        }
    }
}
