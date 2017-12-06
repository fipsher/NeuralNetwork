using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Console.Utils
{
    public class LayerModelToBaseLayer
    {
        public static List<Layer<double, double>> MapLinear(List<LinearLayerModel> source, Dictionary<int, Func<double, double>> funcDictionary)
        {
            var result = new List<Layer<double, double>>();

            result.Add(new Layer<double, double>(source[0].References.Length, funcDictionary[source[0].Function], source[0].Bias));

            for (int j = 0; j < source[0].References.Length; j++)
            {
                var neurone = new Neuron<double>
                {
                    Dendrites = new List<Dendrite<double>>()
                };
                for (int k = 0; k < source[0].References[j].Length; k++)
                {
                    neurone.Dendrites.Add(new Dendrite<double>(source[0].References[j][k]));
                }
                result[0].Neurons.Add(neurone);

            }

            for (int i = 1; i < source.Count; i++)
            {
                result.Add(new Layer<double, double>(source[i].References.Length, funcDictionary[source[i].Function], source[i].Bias));

                for (int j = 0; j < source[i].References.Length; j++)
                {
                    var neurone = new Neuron<double>
                    {
                        Dendrites = new List<Dendrite<double>>()
                    };
                    for (int k = 0; k < source[i].References[j].Length; k++)
                    {
                        neurone.Dendrites.Add(new Dendrite<double>(source[i].References[j][k], result[i-1].Neurons[k] ));
                    }
                    result[i].Neurons.Add(neurone);
                }
            }

            return result;
        }

        public static List<Layer<double, double>> MapNonLinear(List<NonLinearLayerModel> source, Dictionary<long, Func<double, double>> funcDictionary)
        {
            var result = new List<Layer<double, double>>();

            result.Add(new Layer<double, double>(source[0].References.Length, funcDictionary[source[0].Function], source[0].Bias));

            // add all needed neurones
            foreach (var item in source[0].References)
            {
                result[0].Neurons.Add(new Neuron<double>
                {
                    Dendrites = new List<Dendrite<double>>()
                });
            }
            // [[[1, 2], [3, 4]]] ==> [Neurone(1d,2d). Neurone(3d,4d)] // 1d,2d... - weight
            for (int j = 0; j < source[0].References.Length; j++)
            {                
                for (int k = 0; k < source[0].References[j][0].Length; k++)
                {
                    result[0].Neurons[j].Dendrites.Add(new Dendrite<double>(source[0].References[j][0][k]));
                }
            }

            for (int i = 1; i < source.Count; i++)
            {
                result.Add(new Layer<double, double>(source[i].References[0].Length, funcDictionary[source[i].Function], source[i].Bias));
               
                foreach (var item in source[i].References[0])
                {
                    result[i].Neurons.Add(new Neuron<double>
                    {
                        Dendrites = new List<Dendrite<double>>()
                    });
                }
                for (int j = 0; j < source[i].References.Length; j++)
                {
                    var layerToLayerRef = source[i].References[j];
                    for (int t = 0; t < layerToLayerRef.Count(); t++)
                    {
                        for (int neuroneIndex = 0; neuroneIndex < layerToLayerRef[t].Count(); neuroneIndex++)
                        {
                            result[i].Neurons[t].Dendrites.Add(new Dendrite<double>(layerToLayerRef[t][neuroneIndex], result[j].Neurons[neuroneIndex]));

                        }
                    }
                }
            }

            return result;
        }
    }
}
