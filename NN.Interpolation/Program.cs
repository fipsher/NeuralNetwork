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
            return 5 * x;
        }


        static void Main(string[] args)
        {

            int inputsCount = 1;

            var dict = new Dictionary<int, Func<double, double>>
            {
                {1, (x) =>  x },
                {2, (x) =>  x * x },
                {3, (x) => x > 0 ? 1 : 0 }
            };
            var linear = GetData<LinearLayerModel>(@"D:\Projects\NeuralNetwork-master\NN.Interpolation\source1.json");
            var layers1 = LayerModelToBaseLayer.MapLinear(linear, dict);
            var neuralNetwork = new Implementation.NeuralNetworkImplementation(layers1, inputsCount);

            var input = new List<double>();
            var output = new List<double>();
            for (double i = 0; i < 20; i+= 1)
            {
                input.Add(i);
                output.Add(Func(i));
            }

            neuralNetwork.Train(input, output);


            for (double i = 0; i < 30; i += 1)
            {
                var result = neuralNetwork.Run(i);
                var exact = Func(i);
                System.Console.WriteLine($"i={i} :==={result} //// {exact} //// {Math.Abs(result - exact)}");
            }

            System.Console.WriteLine(Func(1));


            System.Console.ReadKey();
        }
    }
}
