using System;
using System.Collections.Generic;

namespace NeuralNetwork.Models
{
    public class Layer<TInput, TOutput>
    {
        public Layer(int neuronsCount, Func<TInput, TOutput> activationFunction, TOutput bias)
        {
            Neurons = new List<Neuron<TOutput>>(neuronsCount);
            ActivationFunction = activationFunction;
            Bias = bias;
        }

        public Func<TInput, TOutput> ActivationFunction { get; set; }

        public List<Neuron<TOutput>> Neurons { get; set; }

        public TOutput Bias;
    }
}
