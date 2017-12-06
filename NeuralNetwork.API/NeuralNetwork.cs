using NeuralNetwork.Interfaces;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork.Implementation
{
    public class NeuralNetworkImplementation : BaseNeuralNetwork<double, double>, INeuralNetwork<NNParameter<double>, NNParameter<double>>
    {
        private int _inputsCount { get; set; }

        public override List<Layer<double, double>> Layers { get; set; }

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

        public NeuralNetworkImplementation(List<Layer<double, double>> layers, int inputsCount)
        {
            Layers = layers;
            InputsCount = inputsCount;
        }

        public NNParameter<double> Run(NNParameter<double> input)
        {

            if (InputsCount != input.Collection.Count)
            {
                throw new InvalidDataException($"Dimension missmatch. Current inputs length {input.Collection.Count}, expected count {InputsCount}");
            }

            Layer<double, double> firstLayer = Layers.First();

            firstLayer.Neurons.ForEach(neuron =>
            {
                neuron.Function = () =>
                {
                    double soma = 0;
                    // Sum(xi * w)
                    for (int i = 0; i < InputsCount; i++)
                    {
                        soma += input.Collection[i] * neuron.Dendrites[i].Weight;
                    }
                    
                    return firstLayer.ActivationFunction(soma + firstLayer.Bias);
                };
            });

            // Going thru layers from second
            Layers.Skip(1).ToList().ForEach(layer =>
            {
                layer.Neurons.ForEach(neuron =>
                {
                    neuron.Function = () =>
                    {
                        // Sum(x * w)
                        double soma = neuron.Dendrites.Select(c => c.PreviousNeuron.Function() * c.Weight).Sum();
                        
                        return layer.ActivationFunction(soma + layer.Bias);
                    };
                });
            });

            return new NNParameter<double>(Layers.Last().Neurons.Select(neuron => neuron.Function()).ToList());
        }

        public void Train(List<NNParameter<double>> input, List<NNParameter<double>> output)
        {
            double startPoint = 0.0370651d;
            double step = 0.1;

            Layers.ForEach(layer =>
            {
                layer.Neurons.ForEach(neurone =>
                {
                    neurone.Dendrites.ForEach(dendrite =>
                    {
                        var localStep = step;

                        neurone.Dendrites.First().Weight = startPoint;
                        double minEnergy = Energy(input, output);

                        neurone.Dendrites.First().Weight = startPoint - localStep;
                        var energy1 = Energy(input, output);

                        neurone.Dendrites.First().Weight = startPoint + localStep;
                        var energy2 = Energy(input, output);

                        double currWeight = startPoint;

                        if (minEnergy < energy1 && minEnergy < energy2)
                        {
                            dendrite.Weight = currWeight;
                        }
                        else
                        {
                            if (energy1 < energy2)
                            {
                                localStep *= -1;
                                minEnergy = energy1;
                            }
                            else
                            {
                                minEnergy = energy2;
                            }
                            currWeight = startPoint + localStep;

                            double resultWeight = currWeight;
                            while (true)
                            {
                                dendrite.Weight = currWeight + localStep;
                                var currEnergy = Energy(input, output);

                                if (currEnergy < minEnergy)
                                {
                                    minEnergy = currEnergy;
                                    resultWeight = currWeight;
                                    currWeight += localStep;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            dendrite.Weight = resultWeight;
                        }

                    });
                });
            });
        }


        private double Energy(List<NNParameter<double>> input, List<NNParameter<double>> output)
        {
            double energy = 0;
            for (int i = 0; i < input.Count; i++)
            {
                double element = output[i].Collection.Zip(Run(input[i]).Collection, (a, b) => a - b).First();
                energy += Math.Pow(element, 2);
            }

            return energy;
        }
    }
}
