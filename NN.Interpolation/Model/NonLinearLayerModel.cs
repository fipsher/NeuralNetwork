﻿using NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NeuralNetwork.Console.Utils
{
    public class NonLinearLayerModel
    {
        [JsonProperty("Bias")]
        public double Bias { get; set; }

        [JsonProperty("Function")]
        public long Function { get; set; }

        [JsonProperty("References")]
        public double[][][] References { get; set; }
    }
}
