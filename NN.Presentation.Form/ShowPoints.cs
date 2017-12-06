using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsForm = System.Windows.Forms.Form;

namespace NN.Presentation.Form
{
    public partial class ShowPoints : WindowsForm
    {
        public ShowPoints(string values)
        {
            InitializeComponent();
            resultRTB.Text = values;
        }
    }
}
