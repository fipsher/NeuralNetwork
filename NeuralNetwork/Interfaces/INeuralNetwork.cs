using System.Collections.Generic;

namespace NeuralNetwork.Interfaces
{
    public interface INeuralNetwork<TIn, TOut>
    {
        TOut Run(TIn input);

        void Train(List<TIn> input, List<TOut> output);
    }
}