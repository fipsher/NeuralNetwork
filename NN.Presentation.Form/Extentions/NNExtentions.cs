using NeuralNetwork.Implementation;
using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NN.Presentation.Form.Extentions
{
    public static class NNExtentions
    {
        public static void ToNN(this TreeView t, NeuralNetworkImplementation nn, Dictionary<int, Func<double, double>> funcDictionary)
        {
            t.Nodes.Clear();

            TreeNode root = new TreeNode("NeuralNetwork");
            int layerNum = 1;
            nn.Layers.ForEach((layer) =>
            {
                TreeNode lnode = new TreeNode($"Layer[{layerNum}]");
                lnode.Nodes.Add($"[{funcDictionary.First(f => f.Value == layer.ActivationFunction).Key.ToString()}] Activation function");
                layerNum++;
                int neuronNum = 1;
                layer.Neurons.ForEach((neuron) =>
                {
                    TreeNode nnode = new TreeNode($"Neuron[{neuronNum}]");
                    neuronNum++;
                    nnode.Nodes.Add("Bias: " + layer.Bias.ToString());

                    neuron.Dendrites.ForEach((dendrite) =>
                    {
                        TreeNode dnode = new TreeNode("Dendrite");
                        dnode.Nodes.Add("Weight: " + dendrite.Weight.ToString());

                        nnode.Nodes.Add(dnode);
                    });

                    lnode.Nodes.Add(nnode);
                });

                root.Nodes.Add(lnode);
            });

            t.Nodes.Add(root);
        }

        public static void ToNN(this PictureBox p, NeuralNetworkImplementation nn, int X, int Y)
        {
            int neuronWidth = 30;
            int neuronDistance = 50;
            int layerDistance = 50;
            int fontSize = 8;

            Bitmap b = new Bitmap(p.Width, p.Height);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(Brushes.White, g.ClipBounds);

            int y = Y;

            for (int l = 0; l < nn.Layers.Count; l++)
            {
                Layer<double, double> layer = nn.Layers[l];

                int x = X - (neuronDistance * (layer.Neurons.Count / 2));

                for (int n = 0; n < layer.Neurons.Count; n++)
                {
                    Neuron<double> neuron = layer.Neurons[n];

                    for (int d = 0; d < neuron.Dendrites.Count; d++)
                    {
                        // TO DO: optionally draw dendrites between neurons
                    };

                    g.FillEllipse(Brushes.WhiteSmoke, x, y, neuronWidth, neuronWidth);
                    g.DrawEllipse(Pens.Gray, x, y, neuronWidth, neuronWidth);
                    //g.DrawString(neuron.Akson().ToString("0.00"), new Font("Arial", fontSize), Brushes.Black, x + 2, y + (neuronWidth / 2) - 5);
                    g.DrawString("", new Font("Arial", fontSize), Brushes.Black, x + 2, y + (neuronWidth / 2) - 5);
                    x += neuronDistance;
                };

                y += layerDistance;
            };

            p.Image = b;
        }
    }
}