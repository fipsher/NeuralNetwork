namespace NN.Presentation.Form
{
    partial class NN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.NNChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.NNRun = new System.Windows.Forms.Button();
            this.NNTrain = new System.Windows.Forms.Button();
            this.NNBox = new System.Windows.Forms.PictureBox();
            this.NNTree = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.NNChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NNBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NNChart
            // 
            chartArea2.Name = "CurrentChartArea";
            this.NNChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.NNChart.Legends.Add(legend2);
            this.NNChart.Location = new System.Drawing.Point(498, 10);
            this.NNChart.Margin = new System.Windows.Forms.Padding(2);
            this.NNChart.Name = "NNChart";
            this.NNChart.Size = new System.Drawing.Size(491, 510);
            this.NNChart.TabIndex = 0;
            this.NNChart.Text = "NN Chart";
            // 
            // NNRun
            // 
            this.NNRun.Location = new System.Drawing.Point(126, 10);
            this.NNRun.Margin = new System.Windows.Forms.Padding(2);
            this.NNRun.Name = "NNRun";
            this.NNRun.Size = new System.Drawing.Size(82, 42);
            this.NNRun.TabIndex = 1;
            this.NNRun.Text = "Run NN";
            this.NNRun.UseVisualStyleBackColor = true;
            this.NNRun.Click += new System.EventHandler(this.NNRun_Click);
            // 
            // NNTrain
            // 
            this.NNTrain.Location = new System.Drawing.Point(23, 10);
            this.NNTrain.Margin = new System.Windows.Forms.Padding(2);
            this.NNTrain.Name = "NNTrain";
            this.NNTrain.Size = new System.Drawing.Size(78, 42);
            this.NNTrain.TabIndex = 2;
            this.NNTrain.Text = "Train NN";
            this.NNTrain.UseVisualStyleBackColor = true;
            this.NNTrain.Click += new System.EventHandler(this.NNTrain_Click);
            // 
            // NNBox
            // 
            this.NNBox.Location = new System.Drawing.Point(9, 57);
            this.NNBox.Margin = new System.Windows.Forms.Padding(2);
            this.NNBox.Name = "NNBox";
            this.NNBox.Size = new System.Drawing.Size(199, 463);
            this.NNBox.TabIndex = 3;
            this.NNBox.TabStop = false;
            // 
            // NNTree
            // 
            this.NNTree.Location = new System.Drawing.Point(212, 10);
            this.NNTree.Margin = new System.Windows.Forms.Padding(2);
            this.NNTree.Name = "NNTree";
            this.NNTree.Size = new System.Drawing.Size(282, 511);
            this.NNTree.TabIndex = 4;
            // 
            // NN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 544);
            this.Controls.Add(this.NNTree);
            this.Controls.Add(this.NNBox);
            this.Controls.Add(this.NNTrain);
            this.Controls.Add(this.NNRun);
            this.Controls.Add(this.NNChart);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NN";
            this.Text = "NN";
            ((System.ComponentModel.ISupportInitialize)(this.NNChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NNBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button NNRun;
        private System.Windows.Forms.Button NNTrain;
        private System.Windows.Forms.DataVisualization.Charting.Chart NNChart;
        private System.Windows.Forms.PictureBox NNBox;
        private System.Windows.Forms.TreeView NNTree;
    }
}