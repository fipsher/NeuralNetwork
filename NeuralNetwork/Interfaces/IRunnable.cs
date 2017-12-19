namespace NeuralNetwork.Interfaces
{
    public interface IRunnable<TInput, TOutput>
    {
        TOutput Run(TInput input);     
    }
}
