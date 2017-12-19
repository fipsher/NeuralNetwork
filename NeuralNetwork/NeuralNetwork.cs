using NeuralNetwork.Interfaces;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork.Implementation
{
    public class KV
    {
        public List<double> W { get; set; }
        public double E { get; set; }
    }

    public class NeuralNetworkImplementation : INeuralNetwork<double, double>
    {
        private int _inputsCount { get; set; }

        public List<Layer<double, double>> Layers { get; set; }

        List<double> input;
        List<double> output;
        double start = -3;
        double end = 3;
        double step = 1;
        KV res;

        public int InputsCount
        {
            get => _inputsCount;
            set
            {
                if (Layers.First().Neurons.Select(n => n.Dendrites.Count).Distinct().Single() != value)
                {
                    throw new InvalidDataException("Input dimension missmatch");
                }
                _inputsCount = value;
            }
        }

        public NeuralNetworkImplementation(
            List<Layer<double, double>> layers, 
            int inputsCount)
        {
            Layers = layers;
            InputsCount = inputsCount;

            Layer<double, double> firstLayer = Layers.First();

            firstLayer.Neurons.ForEach(neuron =>
            {
                neuron.Function = (collection) =>
                {
                    double soma = 0;
                    // Sum(xi * w)
                    for (int i = 0; i < InputsCount; i++)
                    {
                        soma += collection[i] * neuron.Dendrites[i].Weight;
                    }

                    return firstLayer.ActivationFunction(soma + firstLayer.Bias);
                };
            });

            Layers.Skip(1).ToList().ForEach(layer =>
            {
                layer.Neurons.ForEach(neuron =>
                {
                    neuron.Function = (collection) =>
                    {
                        // Sum(x * w)
                        double soma = neuron.Dendrites.Select(c => c.PreviousNeuron.Function(collection) * c.Weight).Sum();

                        return layer.ActivationFunction(soma + layer.Bias);
                    };
                });
            });
        }

        public double Run(double input)
        {
            return Layers
                .Last()
                .Neurons
                .Select(neuron => neuron.Function(new List<double> { input }))
                .First();
        }

        public void Train(
            List<double> input, 
            List<double> output)
        {
            this.input = input;
            this.output = output;

            List<Dendrite<double>> dendrides = Layers.SelectMany(layer => layer.Neurons.SelectMany(neurone => neurone.Dendrites)).ToList();
            dendrides.ForEach(d =>
            {
                d.Weight = start;
            });
            var weights = dendrides.Select(el => el.Weight).ToList();
            res = new KV
            {
                W = weights,
                E = Energy(input, output)
            };

            MonteKarlo(dendrides);

            for (int i = 0; i < dendrides.Count; i++)
            {
                dendrides[i].Weight = res.W[i];
            }
        }

        public void DoSmth(List<Dendrite<double>> dendrides, int i)
        {
            var dendride = dendrides[i];
            if (dendride.Weight < end)
            {
                dendride.Weight += step;

                //var currEnergy = Energy(input, output);
                //if (res == null || res.E > currEnergy)
                //{
                //    var weights = dendrides.Select(el => el.Weight).ToList();
                //    res.W.Clear();
                //    res.W = weights;
                //    res.E = currEnergy;
                //}


                if (i != 0)
                {
                    dendrides.GetRange(0, i).ForEach(d =>
                    {
                        d.Weight = start;
                    });
                }
                DoSmth(dendrides, 0);

            }
            else if (i != dendrides.Count - 1)
            {
                dendride.Weight = start;
                DoSmth(dendrides, i + 1);
            }
        }

        public void MonteKarlo(List<Dendrite<double>> dendrides)
        {
            for (int i = 0; i < dendrides.Count; i++)
            {
                var dendride = dendrides[i];
                dendride.Weight += step;

                var currE = Energy(input, output);


                Console.WriteLine(string.Join(" ", dendrides.Select(el => el.Weight)));

                if (currE < res.E)
                {
                    res.W.Clear();

                    var weights = dendrides.Select(el => el.Weight).ToList();
                    res.E = currE;
                    res.W = weights;
                }

                if (i == 0 && dendride.Weight < end)
                {
                    i = -1;
                }
                else if (i == 0)
                {
                    dendride.Weight = start;
                }
                else if (dendride.Weight > end)
                {
                    dendrides.GetRange(0, i + 1).ForEach(d =>
                    {
                        d.Weight = start;
                    });
                }
                else
                {
                    i = -1;
                }
            }
        }

        private double Energy(List<double> input, List<double> output)
        {
            double energy = 0;
            for (int i = 0; i < input.Count; i++)
            {
                var runned = Run(input[i]);
                double element = Math.Pow(output[i] - runned, 2);
                energy += Math.Pow(element, 2);
            }

            return energy;
        }
    }
}
