namespace NN.Presentation.Form
{
    partial class ShowPoints
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
            this.resultRTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // resultRTB
            // 
            this.resultRTB.Location = new System.Drawing.Point(113, 24);
            this.resultRTB.Name = "resultRTB";
            this.resultRTB.Size = new System.Drawing.Size(630, 366);
            this.resultRTB.TabIndex = 6;
            this.resultRTB.Text = "";
            // 
            // ShowPoints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 421);
            this.Controls.Add(this.resultRTB);
            this.Name = "ShowPoints";
            this.Text = "ShowPoints";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox resultRTB;
    }
}