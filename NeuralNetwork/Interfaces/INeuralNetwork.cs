namespace NeuralNetwork.Interfaces
{
    public interface INeuralNetwork<TNeuralInptuParameter, TNeuralOutputParameter> : IRunnable<TNeuralInptuParameter, TNeuralOutputParameter>,
                                                                                     ITrainable<TNeuralInptuParameter, TNeuralOutputParameter>
    {
    }
}