using System.Collections.Generic;

namespace NeuralNetwork.Interfaces
{
    public interface ITrainable<TInput, TOutput>
    {
        void Train(List<TInput> input, List<TOutput> output);
    }
}
