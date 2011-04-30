namespace AsyncDemo
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._stopBut = new System.Windows.Forms.Button();
            this._startBut = new System.Windows.Forms.Button();
            this._runningOperationsBox = new System.Windows.Forms.GroupBox();
            this._op3Box = new System.Windows.Forms.GroupBox();
            this._op3StopButton = new System.Windows.Forms.Button();
            this._op3Label = new System.Windows.Forms.Label();
            this._op3ProgressBar = new System.Windows.Forms.ProgressBar();
            this._op2Box = new System.Windows.Forms.GroupBox();
            this._op2StopButton = new System.Windows.Forms.Button();
            this._op2Label = new System.Windows.Forms.Label();
            this._op2ProgressBar = new System.Windows.Forms.ProgressBar();
            this._op1Box = new System.Windows.Forms.GroupBox();
            this._op1StopButton = new System.Windows.Forms.Button();
            this._op1Label = new System.Windows.Forms.Label();
            this._op1ProgressBar = new System.Windows.Forms.ProgressBar();
            this._op0Box = new System.Windows.Forms.GroupBox();
            this._op0StopButton = new System.Windows.Forms.Button();
            this._op0Label = new System.Windows.Forms.Label();
            this._op0ProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this._runningOperationsBox.SuspendLayout();
            this._op3Box.SuspendLayout();
            this._op2Box.SuspendLayout();
            this._op1Box.SuspendLayout();
            this._op0Box.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._stopBut);
            this.groupBox1.Controls.Add(this._startBut);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 66);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Laucher";
            // 
            // _stopBut
            // 
            this._stopBut.Location = new System.Drawing.Point(142, 28);
            this._stopBut.Name = "_stopBut";
            this._stopBut.Size = new System.Drawing.Size(75, 23);
            this._stopBut.TabIndex = 1;
            this._stopBut.Text = "Stop";
            this._stopBut.UseVisualStyleBackColor = true;
            this._stopBut.Click += new System.EventHandler(this._stopBut_Click);
            // 
            // _startBut
            // 
            this._startBut.Location = new System.Drawing.Point(36, 28);
            this._startBut.Name = "_startBut";
            this._startBut.Size = new System.Drawing.Size(75, 23);
            this._startBut.TabIndex = 0;
            this._startBut.Text = "Start";
            this._startBut.UseVisualStyleBackColor = true;
            this._startBut.Click += new System.EventHandler(this._startBut_Click);
            // 
            // _runningOperationsBox
            // 
            this._runningOperationsBox.Controls.Add(this._op3Box);
            this._runningOperationsBox.Controls.Add(this._op2Box);
            this._runningOperationsBox.Controls.Add(this._op1Box);
            this._runningOperationsBox.Controls.Add(this._op0Box);
            this._runningOperationsBox.Location = new System.Drawing.Point(12, 84);
            this._runningOperationsBox.Name = "_runningOperationsBox";
            this._runningOperationsBox.Size = new System.Drawing.Size(286, 216);
            this._runningOperationsBox.TabIndex = 1;
            this._runningOperationsBox.TabStop = false;
            this._runningOperationsBox.Text = "Running Operations";
            // 
            // _op3Box
            // 
            this._op3Box.Controls.Add(this._op3StopButton);
            this._op3Box.Controls.Add(this._op3Label);
            this._op3Box.Controls.Add(this._op3ProgressBar);
            this._op3Box.Location = new System.Drawing.Point(7, 157);
            this._op3Box.Name = "_op3Box";
            this._op3Box.Size = new System.Drawing.Size(273, 42);
            this._op3Box.TabIndex = 5;
            this._op3Box.TabStop = false;
            // 
            // _op3StopButton
            // 
            this._op3StopButton.Location = new System.Drawing.Point(178, 10);
            this._op3StopButton.Name = "_op3StopButton";
            this._op3StopButton.Size = new System.Drawing.Size(75, 23);
            this._op3StopButton.TabIndex = 2;
            this._op3StopButton.Text = "Stop";
            this._op3StopButton.UseVisualStyleBackColor = true;
            // 
            // _op3Label
            // 
            this._op3Label.AutoSize = true;
            this._op3Label.Location = new System.Drawing.Point(113, 20);
            this._op3Label.Name = "_op3Label";
            this._op3Label.Size = new System.Drawing.Size(35, 13);
            this._op3Label.TabIndex = 1;
            this._op3Label.Text = "label3";
            // 
            // _op3ProgressBar
            // 
            this._op3ProgressBar.Location = new System.Drawing.Point(6, 19);
            this._op3ProgressBar.Name = "_op3ProgressBar";
            this._op3ProgressBar.Size = new System.Drawing.Size(100, 17);
            this._op3ProgressBar.TabIndex = 0;
            // 
            // _op2Box
            // 
            this._op2Box.Controls.Add(this._op2StopButton);
            this._op2Box.Controls.Add(this._op2Label);
            this._op2Box.Controls.Add(this._op2ProgressBar);
            this._op2Box.Location = new System.Drawing.Point(7, 109);
            this._op2Box.Name = "_op2Box";
            this._op2Box.Size = new System.Drawing.Size(273, 42);
            this._op2Box.TabIndex = 4;
            this._op2Box.TabStop = false;
            // 
            // _op2StopButton
            // 
            this._op2StopButton.Location = new System.Drawing.Point(178, 13);
            this._op2StopButton.Name = "_op2StopButton";
            this._op2StopButton.Size = new System.Drawing.Size(75, 23);
            this._op2StopButton.TabIndex = 2;
            this._op2StopButton.Text = "Stop";
            this._op2StopButton.UseVisualStyleBackColor = true;
            // 
            // _op2Label
            // 
            this._op2Label.AutoSize = true;
            this._op2Label.Location = new System.Drawing.Point(113, 20);
            this._op2Label.Name = "_op2Label";
            this._op2Label.Size = new System.Drawing.Size(57, 13);
            this._op2Label.TabIndex = 1;
            this._op2Label.Text = "_op2Label";
            // 
            // _op2ProgressBar
            // 
            this._op2ProgressBar.Location = new System.Drawing.Point(6, 19);
            this._op2ProgressBar.Name = "_op2ProgressBar";
            this._op2ProgressBar.Size = new System.Drawing.Size(100, 17);
            this._op2ProgressBar.TabIndex = 0;
            // 
            // _op1Box
            // 
            this._op1Box.Controls.Add(this._op1StopButton);
            this._op1Box.Controls.Add(this._op1Label);
            this._op1Box.Controls.Add(this._op1ProgressBar);
            this._op1Box.Location = new System.Drawing.Point(7, 61);
            this._op1Box.Name = "_op1Box";
            this._op1Box.Size = new System.Drawing.Size(273, 42);
            this._op1Box.TabIndex = 3;
            this._op1Box.TabStop = false;
            // 
            // _op1StopButton
            // 
            this._op1StopButton.Location = new System.Drawing.Point(178, 10);
            this._op1StopButton.Name = "_op1StopButton";
            this._op1StopButton.Size = new System.Drawing.Size(75, 23);
            this._op1StopButton.TabIndex = 2;
            this._op1StopButton.Text = "Stop";
            this._op1StopButton.UseVisualStyleBackColor = true;
            // 
            // _op1Label
            // 
            this._op1Label.AutoSize = true;
            this._op1Label.Location = new System.Drawing.Point(113, 20);
            this._op1Label.Name = "_op1Label";
            this._op1Label.Size = new System.Drawing.Size(35, 13);
            this._op1Label.TabIndex = 1;
            this._op1Label.Text = "label1";
            // 
            // _op1ProgressBar
            // 
            this._op1ProgressBar.Location = new System.Drawing.Point(6, 19);
            this._op1ProgressBar.Name = "_op1ProgressBar";
            this._op1ProgressBar.Size = new System.Drawing.Size(100, 17);
            this._op1ProgressBar.TabIndex = 0;
            // 
            // _op0Box
            // 
            this._op0Box.Controls.Add(this._op0StopButton);
            this._op0Box.Controls.Add(this._op0Label);
            this._op0Box.Controls.Add(this._op0ProgressBar);
            this._op0Box.Location = new System.Drawing.Point(7, 19);
            this._op0Box.Name = "_op0Box";
            this._op0Box.Size = new System.Drawing.Size(273, 42);
            this._op0Box.TabIndex = 0;
            this._op0Box.TabStop = false;
            // 
            // _op0StopButton
            // 
            this._op0StopButton.Location = new System.Drawing.Point(178, 15);
            this._op0StopButton.Name = "_op0StopButton";
            this._op0StopButton.Size = new System.Drawing.Size(75, 23);
            this._op0StopButton.TabIndex = 2;
            this._op0StopButton.Text = "Stop";
            this._op0StopButton.UseVisualStyleBackColor = true;
            // 
            // _op0Label
            // 
            this._op0Label.AutoSize = true;
            this._op0Label.Location = new System.Drawing.Point(113, 20);
            this._op0Label.Name = "_op0Label";
            this._op0Label.Size = new System.Drawing.Size(35, 13);
            this._op0Label.TabIndex = 1;
            this._op0Label.Text = "label1";
            // 
            // _op0ProgressBar
            // 
            this._op0ProgressBar.Location = new System.Drawing.Point(6, 19);
            this._op0ProgressBar.Name = "_op0ProgressBar";
            this._op0ProgressBar.Size = new System.Drawing.Size(100, 17);
            this._op0ProgressBar.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 314);
            this.Controls.Add(this._runningOperationsBox);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "AsyncDemo";
            this.groupBox1.ResumeLayout(false);
            this._runningOperationsBox.ResumeLayout(false);
            this._op3Box.ResumeLayout(false);
            this._op3Box.PerformLayout();
            this._op2Box.ResumeLayout(false);
            this._op2Box.PerformLayout();
            this._op1Box.ResumeLayout(false);
            this._op1Box.PerformLayout();
            this._op0Box.ResumeLayout(false);
            this._op0Box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button _stopBut;
        private System.Windows.Forms.Button _startBut;
        private System.Windows.Forms.GroupBox _runningOperationsBox;
        private System.Windows.Forms.GroupBox _op3Box;
        private System.Windows.Forms.Button _op3StopButton;
        private System.Windows.Forms.Label _op3Label;
        private System.Windows.Forms.ProgressBar _op3ProgressBar;
        private System.Windows.Forms.GroupBox _op2Box;
        private System.Windows.Forms.Button _op2StopButton;
        private System.Windows.Forms.Label _op2Label;
        private System.Windows.Forms.ProgressBar _op2ProgressBar;
        private System.Windows.Forms.GroupBox _op1Box;
        private System.Windows.Forms.Button _op1StopButton;
        private System.Windows.Forms.Label _op1Label;
        private System.Windows.Forms.ProgressBar _op1ProgressBar;
        private System.Windows.Forms.GroupBox _op0Box;
        private System.Windows.Forms.Button _op0StopButton;
        private System.Windows.Forms.Label _op0Label;
        private System.Windows.Forms.ProgressBar _op0ProgressBar;
    }
}

