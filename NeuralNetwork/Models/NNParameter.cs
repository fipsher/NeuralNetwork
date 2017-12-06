using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork.Models
{
    public class NNParameter<TItem>
    {
        public NNParameter(IEnumerable<TItem> collection)
        {
            Collection = new List<TItem>(collection);
        }

        public NNParameter(TItem item)
        {
            Collection = new List<TItem> { item };
        }

        public List<TItem> Collection { get; set; }
    }
}
