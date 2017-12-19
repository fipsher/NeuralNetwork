using System;
using System.Collections.Generic;

namespace NeuralNetwork.Models
{
    public class Layer<TIn, TOut>
    {
        public Layer(int amountOfNeurones, Func<TIn, TOut> activationFunction, TOut bias)
        {
            Neurons = new List<Neuron<TOut>>(amountOfNeurones);
            ActivationFunction = activationFunction;
            Bias = bias;
        }

        public Func<TIn, TOut> ActivationFunction { get; set; }

        public List<Neuron<TOut>> Neurons { get; set; }

        public TOut Bias;
    }
}
