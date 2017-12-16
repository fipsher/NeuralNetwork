using System;
using System.Collections.Generic;

namespace NeuralNetwork.Models
{
    public class Neuron<T>
    {
        public List<Dendrite<T>> Dendrites { get; set; }

        public Func<List<T>, T> Function { get; set; }
    }
}
