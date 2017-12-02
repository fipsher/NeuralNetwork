namespace NeuralNetwork.Models
{
    public class Dendrite<T>
    {
        public Dendrite(double weight, Neuron<T> previousNeuron = null)
        {
            Weight = weight;
            PreviousNeuron = previousNeuron;
        }

        public Neuron<T> PreviousNeuron { get; set; }

        public double Weight { get; set; }
    }
}
