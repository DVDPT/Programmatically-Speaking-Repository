namespace ClientGui
{
    partial class Client
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
            this.@__FileNameTextBox = new System.Windows.Forms.TextBox();
            this.@__ServerGetFileButton = new System.Windows.Forms.Button();
            this.@__ServerSearchContainer = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.@__FileDownloadingGroup3 = new System.Windows.Forms.GroupBox();
            this.@__FileDownloadingStateLabel3 = new System.Windows.Forms.Label();
            this.@__FileDownloadingRemoveButton3 = new System.Windows.Forms.Button();
            this.@__FileDownloadingCancelButton3 = new System.Windows.Forms.Button();
            this.@__FileDownloadingProgressBar3 = new System.Windows.Forms.ProgressBar();
            this.@__FileDownloadingGroup2 = new System.Windows.Forms.GroupBox();
            this.@__FileDownloadingStateLabel2 = new System.Windows.Forms.Label();
            this.@__FileDownloadingRemoveButton2 = new System.Windows.Forms.Button();
            this.@__FileDownloadingCancelButton2 = new System.Windows.Forms.Button();
            this.@__FileDownloadingProgressBar2 = new System.Windows.Forms.ProgressBar();
            this.@__FileDownloadingGroup1 = new System.Windows.Forms.GroupBox();
            this.@__FileDownloadingStateLabel1 = new System.Windows.Forms.Label();
            this.@__FileDownloadingRemoveButton1 = new System.Windows.Forms.Button();
            this.@__FileDownloadingCancelButton1 = new System.Windows.Forms.Button();
            this.@__FileDownloadingProgressBar1 = new System.Windows.Forms.ProgressBar();
            this.@__FileDownloadingGroup0 = new System.Windows.Forms.GroupBox();
            this.@__FileDownloadingStateLabel0 = new System.Windows.Forms.Label();
            this.@__FileDownloadingRemoveButton0 = new System.Windows.Forms.Button();
            this.@__FileDownloadingCancelButton0 = new System.Windows.Forms.Button();
            this.@__FileDownloadingProgressBar0 = new System.Windows.Forms.ProgressBar();
            this.@__ServerGetHttpPageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.@__ServerSearchContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.@__FileDownloadingGroup3.SuspendLayout();
            this.@__FileDownloadingGroup2.SuspendLayout();
            this.@__FileDownloadingGroup1.SuspendLayout();
            this.@__FileDownloadingGroup0.SuspendLayout();
            this.SuspendLayout();
            // 
            // __FileNameTextBox
            // 
            this.@__FileNameTextBox.Location = new System.Drawing.Point(197, 18);
            this.@__FileNameTextBox.Name = "__FileNameTextBox";
            this.@__FileNameTextBox.Size = new System.Drawing.Size(100, 20);
            this.@__FileNameTextBox.TabIndex = 0;
            // 
            // __ServerGetFileButton
            // 
            this.@__ServerGetFileButton.Location = new System.Drawing.Point(6, 16);
            this.@__ServerGetFileButton.Name = "__ServerGetFileButton";
            this.@__ServerGetFileButton.Size = new System.Drawing.Size(75, 23);
            this.@__ServerGetFileButton.TabIndex = 1;
            this.@__ServerGetFileButton.Text = "GetFile";
            this.@__ServerGetFileButton.UseVisualStyleBackColor = true;
            this.@__ServerGetFileButton.Click += new System.EventHandler(this.@__ServerGetFileButton_Click);
            // 
            // __ServerSearchContainer
            // 
            this.@__ServerSearchContainer.Controls.Add(this.label1);
            this.@__ServerSearchContainer.Controls.Add(this.@__ServerGetHttpPageButton);
            this.@__ServerSearchContainer.Controls.Add(this.@__ServerGetFileButton);
            this.@__ServerSearchContainer.Controls.Add(this.@__FileNameTextBox);
            this.@__ServerSearchContainer.Location = new System.Drawing.Point(12, 12);
            this.@__ServerSearchContainer.Name = "__ServerSearchContainer";
            this.@__ServerSearchContainer.Size = new System.Drawing.Size(558, 52);
            this.@__ServerSearchContainer.TabIndex = 2;
            this.@__ServerSearchContainer.TabStop = false;
            this.@__ServerSearchContainer.Text = "Server Search";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.@__FileDownloadingGroup3);
            this.groupBox1.Controls.Add(this.@__FileDownloadingGroup2);
            this.groupBox1.Controls.Add(this.@__FileDownloadingGroup1);
            this.groupBox1.Controls.Add(this.@__FileDownloadingGroup0);
            this.groupBox1.Location = new System.Drawing.Point(18, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 237);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Files Downloading";
            // 
            // __FileDownloadingGroup3
            // 
            this.@__FileDownloadingGroup3.Controls.Add(this.@__FileDownloadingStateLabel3);
            this.@__FileDownloadingGroup3.Controls.Add(this.@__FileDownloadingRemoveButton3);
            this.@__FileDownloadingGroup3.Controls.Add(this.@__FileDownloadingCancelButton3);
            this.@__FileDownloadingGroup3.Controls.Add(this.@__FileDownloadingProgressBar3);
            this.@__FileDownloadingGroup3.Location = new System.Drawing.Point(6, 181);
            this.@__FileDownloadingGroup3.Name = "__FileDownloadingGroup3";
            this.@__FileDownloadingGroup3.Size = new System.Drawing.Size(546, 48);
            this.@__FileDownloadingGroup3.TabIndex = 7;
            this.@__FileDownloadingGroup3.TabStop = false;
            // 
            // __FileDownloadingStateLabel3
            // 
            this.@__FileDownloadingStateLabel3.AutoSize = true;
            this.@__FileDownloadingStateLabel3.Location = new System.Drawing.Point(478, 28);
            this.@__FileDownloadingStateLabel3.Name = "__FileDownloadingStateLabel3";
            this.@__FileDownloadingStateLabel3.Size = new System.Drawing.Size(35, 13);
            this.@__FileDownloadingStateLabel3.TabIndex = 11;
            this.@__FileDownloadingStateLabel3.Text = "label1";
            // 
            // __FileDownloadingRemoveButton3
            // 
            this.@__FileDownloadingRemoveButton3.Location = new System.Drawing.Point(403, 18);
            this.@__FileDownloadingRemoveButton3.Name = "__FileDownloadingRemoveButton3";
            this.@__FileDownloadingRemoveButton3.Size = new System.Drawing.Size(69, 23);
            this.@__FileDownloadingRemoveButton3.TabIndex = 10;
            this.@__FileDownloadingRemoveButton3.Text = "Remove";
            this.@__FileDownloadingRemoveButton3.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingCancelButton3
            // 
            this.@__FileDownloadingCancelButton3.Location = new System.Drawing.Point(346, 16);
            this.@__FileDownloadingCancelButton3.Name = "__FileDownloadingCancelButton3";
            this.@__FileDownloadingCancelButton3.Size = new System.Drawing.Size(57, 25);
            this.@__FileDownloadingCancelButton3.TabIndex = 6;
            this.@__FileDownloadingCancelButton3.Text = "Cancel";
            this.@__FileDownloadingCancelButton3.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingProgressBar3
            // 
            this.@__FileDownloadingProgressBar3.Location = new System.Drawing.Point(6, 19);
            this.@__FileDownloadingProgressBar3.Name = "__FileDownloadingProgressBar3";
            this.@__FileDownloadingProgressBar3.Size = new System.Drawing.Size(336, 17);
            this.@__FileDownloadingProgressBar3.TabIndex = 5;
            // 
            // __FileDownloadingGroup2
            // 
            this.@__FileDownloadingGroup2.Controls.Add(this.@__FileDownloadingStateLabel2);
            this.@__FileDownloadingGroup2.Controls.Add(this.@__FileDownloadingRemoveButton2);
            this.@__FileDownloadingGroup2.Controls.Add(this.@__FileDownloadingCancelButton2);
            this.@__FileDownloadingGroup2.Controls.Add(this.@__FileDownloadingProgressBar2);
            this.@__FileDownloadingGroup2.Location = new System.Drawing.Point(6, 127);
            this.@__FileDownloadingGroup2.Name = "__FileDownloadingGroup2";
            this.@__FileDownloadingGroup2.Size = new System.Drawing.Size(546, 48);
            this.@__FileDownloadingGroup2.TabIndex = 7;
            this.@__FileDownloadingGroup2.TabStop = false;
            // 
            // __FileDownloadingStateLabel2
            // 
            this.@__FileDownloadingStateLabel2.AutoSize = true;
            this.@__FileDownloadingStateLabel2.Location = new System.Drawing.Point(478, 28);
            this.@__FileDownloadingStateLabel2.Name = "__FileDownloadingStateLabel2";
            this.@__FileDownloadingStateLabel2.Size = new System.Drawing.Size(35, 13);
            this.@__FileDownloadingStateLabel2.TabIndex = 10;
            this.@__FileDownloadingStateLabel2.Text = "label1";
            // 
            // __FileDownloadingRemoveButton2
            // 
            this.@__FileDownloadingRemoveButton2.Location = new System.Drawing.Point(403, 16);
            this.@__FileDownloadingRemoveButton2.Name = "__FileDownloadingRemoveButton2";
            this.@__FileDownloadingRemoveButton2.Size = new System.Drawing.Size(69, 23);
            this.@__FileDownloadingRemoveButton2.TabIndex = 9;
            this.@__FileDownloadingRemoveButton2.Text = "Remove";
            this.@__FileDownloadingRemoveButton2.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingCancelButton2
            // 
            this.@__FileDownloadingCancelButton2.Location = new System.Drawing.Point(346, 16);
            this.@__FileDownloadingCancelButton2.Name = "__FileDownloadingCancelButton2";
            this.@__FileDownloadingCancelButton2.Size = new System.Drawing.Size(57, 25);
            this.@__FileDownloadingCancelButton2.TabIndex = 6;
            this.@__FileDownloadingCancelButton2.Text = "Cancel";
            this.@__FileDownloadingCancelButton2.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingProgressBar2
            // 
            this.@__FileDownloadingProgressBar2.Location = new System.Drawing.Point(6, 19);
            this.@__FileDownloadingProgressBar2.Name = "__FileDownloadingProgressBar2";
            this.@__FileDownloadingProgressBar2.Size = new System.Drawing.Size(336, 17);
            this.@__FileDownloadingProgressBar2.TabIndex = 5;
            // 
            // __FileDownloadingGroup1
            // 
            this.@__FileDownloadingGroup1.Controls.Add(this.@__FileDownloadingStateLabel1);
            this.@__FileDownloadingGroup1.Controls.Add(this.@__FileDownloadingRemoveButton1);
            this.@__FileDownloadingGroup1.Controls.Add(this.@__FileDownloadingCancelButton1);
            this.@__FileDownloadingGroup1.Controls.Add(this.@__FileDownloadingProgressBar1);
            this.@__FileDownloadingGroup1.Location = new System.Drawing.Point(6, 73);
            this.@__FileDownloadingGroup1.Name = "__FileDownloadingGroup1";
            this.@__FileDownloadingGroup1.Size = new System.Drawing.Size(546, 48);
            this.@__FileDownloadingGroup1.TabIndex = 7;
            this.@__FileDownloadingGroup1.TabStop = false;
            // 
            // __FileDownloadingStateLabel1
            // 
            this.@__FileDownloadingStateLabel1.AutoSize = true;
            this.@__FileDownloadingStateLabel1.Location = new System.Drawing.Point(478, 28);
            this.@__FileDownloadingStateLabel1.Name = "__FileDownloadingStateLabel1";
            this.@__FileDownloadingStateLabel1.Size = new System.Drawing.Size(35, 13);
            this.@__FileDownloadingStateLabel1.TabIndex = 9;
            this.@__FileDownloadingStateLabel1.Text = "label1";
            // 
            // __FileDownloadingRemoveButton1
            // 
            this.@__FileDownloadingRemoveButton1.Location = new System.Drawing.Point(403, 16);
            this.@__FileDownloadingRemoveButton1.Name = "__FileDownloadingRemoveButton1";
            this.@__FileDownloadingRemoveButton1.Size = new System.Drawing.Size(69, 23);
            this.@__FileDownloadingRemoveButton1.TabIndex = 8;
            this.@__FileDownloadingRemoveButton1.Text = "Remove";
            this.@__FileDownloadingRemoveButton1.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingCancelButton1
            // 
            this.@__FileDownloadingCancelButton1.Location = new System.Drawing.Point(346, 16);
            this.@__FileDownloadingCancelButton1.Name = "__FileDownloadingCancelButton1";
            this.@__FileDownloadingCancelButton1.Size = new System.Drawing.Size(57, 25);
            this.@__FileDownloadingCancelButton1.TabIndex = 6;
            this.@__FileDownloadingCancelButton1.Text = "Cancel";
            this.@__FileDownloadingCancelButton1.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingProgressBar1
            // 
            this.@__FileDownloadingProgressBar1.Location = new System.Drawing.Point(6, 19);
            this.@__FileDownloadingProgressBar1.Name = "__FileDownloadingProgressBar1";
            this.@__FileDownloadingProgressBar1.Size = new System.Drawing.Size(336, 17);
            this.@__FileDownloadingProgressBar1.TabIndex = 5;
            // 
            // __FileDownloadingGroup0
            // 
            this.@__FileDownloadingGroup0.Controls.Add(this.@__FileDownloadingStateLabel0);
            this.@__FileDownloadingGroup0.Controls.Add(this.@__FileDownloadingRemoveButton0);
            this.@__FileDownloadingGroup0.Controls.Add(this.@__FileDownloadingCancelButton0);
            this.@__FileDownloadingGroup0.Controls.Add(this.@__FileDownloadingProgressBar0);
            this.@__FileDownloadingGroup0.Location = new System.Drawing.Point(6, 19);
            this.@__FileDownloadingGroup0.Name = "__FileDownloadingGroup0";
            this.@__FileDownloadingGroup0.Size = new System.Drawing.Size(546, 48);
            this.@__FileDownloadingGroup0.TabIndex = 4;
            this.@__FileDownloadingGroup0.TabStop = false;
            // 
            // __FileDownloadingStateLabel0
            // 
            this.@__FileDownloadingStateLabel0.AutoSize = true;
            this.@__FileDownloadingStateLabel0.Location = new System.Drawing.Point(478, 23);
            this.@__FileDownloadingStateLabel0.Name = "__FileDownloadingStateLabel0";
            this.@__FileDownloadingStateLabel0.Size = new System.Drawing.Size(35, 13);
            this.@__FileDownloadingStateLabel0.TabIndex = 8;
            this.@__FileDownloadingStateLabel0.Text = "label1";
            // 
            // __FileDownloadingRemoveButton0
            // 
            this.@__FileDownloadingRemoveButton0.Location = new System.Drawing.Point(403, 17);
            this.@__FileDownloadingRemoveButton0.Name = "__FileDownloadingRemoveButton0";
            this.@__FileDownloadingRemoveButton0.Size = new System.Drawing.Size(69, 23);
            this.@__FileDownloadingRemoveButton0.TabIndex = 7;
            this.@__FileDownloadingRemoveButton0.Text = "Remove";
            this.@__FileDownloadingRemoveButton0.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingCancelButton0
            // 
            this.@__FileDownloadingCancelButton0.Location = new System.Drawing.Point(346, 16);
            this.@__FileDownloadingCancelButton0.Name = "__FileDownloadingCancelButton0";
            this.@__FileDownloadingCancelButton0.Size = new System.Drawing.Size(57, 25);
            this.@__FileDownloadingCancelButton0.TabIndex = 6;
            this.@__FileDownloadingCancelButton0.Text = "Cancel";
            this.@__FileDownloadingCancelButton0.UseVisualStyleBackColor = true;
            // 
            // __FileDownloadingProgressBar0
            // 
            this.@__FileDownloadingProgressBar0.Location = new System.Drawing.Point(6, 19);
            this.@__FileDownloadingProgressBar0.Name = "__FileDownloadingProgressBar0";
            this.@__FileDownloadingProgressBar0.Size = new System.Drawing.Size(336, 17);
            this.@__FileDownloadingProgressBar0.TabIndex = 5;
            // 
            // __ServerGetHttpPageButton
            // 
            this.@__ServerGetHttpPageButton.Location = new System.Drawing.Point(320, 16);
            this.@__ServerGetHttpPageButton.Name = "__ServerGetHttpPageButton";
            this.@__ServerGetHttpPageButton.Size = new System.Drawing.Size(104, 23);
            this.@__ServerGetHttpPageButton.TabIndex = 4;
            this.@__ServerGetHttpPageButton.Text = "Get Http Page";
            this.@__ServerGetHttpPageButton.UseVisualStyleBackColor = true;
            this.@__ServerGetHttpPageButton.Click += new System.EventHandler(this.@__ServerGetHttpPageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "FileName or URI:";
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 332);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.@__ServerSearchContainer);
            this.Name = "Client";
            this.Text = "Client Gui";
            this.@__ServerSearchContainer.ResumeLayout(false);
            this.@__ServerSearchContainer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.@__FileDownloadingGroup3.ResumeLayout(false);
            this.@__FileDownloadingGroup3.PerformLayout();
            this.@__FileDownloadingGroup2.ResumeLayout(false);
            this.@__FileDownloadingGroup2.PerformLayout();
            this.@__FileDownloadingGroup1.ResumeLayout(false);
            this.@__FileDownloadingGroup1.PerformLayout();
            this.@__FileDownloadingGroup0.ResumeLayout(false);
            this.@__FileDownloadingGroup0.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox __FileNameTextBox;
        private System.Windows.Forms.Button __ServerGetFileButton;
        private System.Windows.Forms.GroupBox __ServerSearchContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox __FileDownloadingGroup0;
        private System.Windows.Forms.ProgressBar __FileDownloadingProgressBar0;
        private System.Windows.Forms.Button __FileDownloadingCancelButton0;
        private System.Windows.Forms.GroupBox __FileDownloadingGroup3;
        private System.Windows.Forms.Button __FileDownloadingCancelButton3;
        private System.Windows.Forms.ProgressBar __FileDownloadingProgressBar3;
        private System.Windows.Forms.GroupBox __FileDownloadingGroup2;
        private System.Windows.Forms.Button __FileDownloadingCancelButton2;
        private System.Windows.Forms.ProgressBar __FileDownloadingProgressBar2;
        private System.Windows.Forms.GroupBox __FileDownloadingGroup1;
        private System.Windows.Forms.Button __FileDownloadingCancelButton1;
        private System.Windows.Forms.ProgressBar __FileDownloadingProgressBar1;
        private System.Windows.Forms.Button __FileDownloadingRemoveButton3;
        private System.Windows.Forms.Button __FileDownloadingRemoveButton2;
        private System.Windows.Forms.Button __FileDownloadingRemoveButton1;
        private System.Windows.Forms.Button __FileDownloadingRemoveButton0;
        private System.Windows.Forms.Label __FileDownloadingStateLabel3;
        private System.Windows.Forms.Label __FileDownloadingStateLabel2;
        private System.Windows.Forms.Label __FileDownloadingStateLabel1;
        private System.Windows.Forms.Label __FileDownloadingStateLabel0;
        private System.Windows.Forms.Button __ServerGetHttpPageButton;
        private System.Windows.Forms.Label label1;

    }
}

