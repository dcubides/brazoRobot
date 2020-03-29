namespace brazoRobot
{
    partial class View
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGripper = new System.Windows.Forms.Button();
            this.tbAxis3 = new System.Windows.Forms.TrackBar();
            this.tbAxis2 = new System.Windows.Forms.TrackBar();
            this.tbAxis1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.btnGripper);
            this.groupBox1.Controls.Add(this.tbAxis3);
            this.groupBox1.Controls.Add(this.tbAxis2);
            this.groupBox1.Controls.Add(this.tbAxis1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(284, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Brazo";
            // 
            // btnGripper
            // 
            this.btnGripper.Location = new System.Drawing.Point(83, 164);
            this.btnGripper.Name = "btnGripper";
            this.btnGripper.Size = new System.Drawing.Size(75, 23);
            this.btnGripper.TabIndex = 17;
            this.btnGripper.Text = "Pinza";
            this.btnGripper.UseVisualStyleBackColor = true;
            // 
            // tbAxis3
            // 
            this.tbAxis3.Location = new System.Drawing.Point(83, 103);
            this.tbAxis3.Maximum = 90;
            this.tbAxis3.Minimum = -90;
            this.tbAxis3.Name = "tbAxis3";
            this.tbAxis3.Size = new System.Drawing.Size(174, 45);
            this.tbAxis3.TabIndex = 16;
            // 
            // tbAxis2
            // 
            this.tbAxis2.Location = new System.Drawing.Point(83, 66);
            this.tbAxis2.Maximum = 90;
            this.tbAxis2.Minimum = -90;
            this.tbAxis2.Name = "tbAxis2";
            this.tbAxis2.Size = new System.Drawing.Size(174, 45);
            this.tbAxis2.TabIndex = 15;
            // 
            // tbAxis1
            // 
            this.tbAxis1.Location = new System.Drawing.Point(83, 19);
            this.tbAxis1.Maximum = 90;
            this.tbAxis1.Minimum = -90;
            this.tbAxis1.Name = "tbAxis1";
            this.tbAxis1.Size = new System.Drawing.Size(174, 45);
            this.tbAxis1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Eje 3:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Eje 2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Eje 1:";
            // 
            // pbGraph
            // 
            this.pbGraph.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbGraph.Location = new System.Drawing.Point(317, 13);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(448, 425);
            this.pbGraph.TabIndex = 1;
            this.pbGraph.TabStop = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbGraph);
            this.Controls.Add(this.groupBox1);
            this.Name = "View";
            this.Text = "Brazo Robot";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbAxis1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.TrackBar tbAxis1;
        private System.Windows.Forms.TrackBar tbAxis2;
        private System.Windows.Forms.TrackBar tbAxis3;
        private System.Windows.Forms.Button btnGripper;
    }
}

