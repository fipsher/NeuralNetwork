using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Models
{
    public class NNParameter<TItem>
    {
        public NNParameter(List<TItem> collection)
        {
            Collection = collection;
        }

        public List<TItem> Collection { get; set; }
    }
}
