using NeuralNetwork.Interfaces;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NeuralNetwork.Implementation
{
    public class NeuralNetwork : BaseNeuralNetwork<double, double>, INeuralNetwork<NNParameter<double>, NNParameter<double>>
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

        public NeuralNetwork(List<Layer<double, double>> layers, int inputsCount)
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
            double leftBound = 0.6d;
            double rightBound = 1d;

            double startPoint = 0.8d;
            double n = 100;



            Layers.ForEach(layer =>
            {
                layer.Neurons.ForEach(neurone =>
                {
                    var localLeft = leftBound;
                    var localRigth = rightBound;
                    double step = (localRigth - localLeft) / n;

                    neurone.Dendrites.ForEach(dendrite =>
                    {
                        var currPos = (rightBound + leftBound) / 2;
                        neurone.Dendrites.First().Weight = currPos;
                        double minEnergy = Energy(input, output);

                        neurone.Dendrites.First().Weight = currPos - step;
                        var energy1 = Energy(input, output);

                        neurone.Dendrites.First().Weight = currPos + step;
                        var energy2 = Energy(input, output);

                        if (minEnergy < energy1 && minEnergy < energy2)
                        {
                            // ignore
                        }
                        else
                        {
                            if (energy1 < energy2)
                            {
                                step *= -1;
                            }

                            double currWeight = currPos;
                            while (true)
                            {
                                dendrite.Weight = currWeight;
                                var currEnergy = Energy(input, output);

                                if (currEnergy < minEnergy)
                                {
                                    minEnergy = currEnergy;
                                    dendrite.Weight = currWeight;
                                }
                                else
                                {
                                    break;
                                }
                                currWeight += step;
                            }
                            //for (double currWeight = localLeft; currWeight <= localRigth; currWeight += step)
                            //{
                            //    dendrite.Weight = currWeight;
                            //    var currEnergy = Energy(input, output);

                            //    if (currEnergy < minEnergy)
                            //    {
                            //        minEnergy = currEnergy;
                            //        dendrite.Weight = currWeight;
                            //    }
                            //    else
                            //    {
                            //        break;
                            //    }
                            //}
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
