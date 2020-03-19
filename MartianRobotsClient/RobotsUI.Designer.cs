namespace MartianRobotsClient
{
    partial class RobotsUI
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
            this.LogBox = new System.Windows.Forms.ListBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.MapBox = new System.Windows.Forms.TextBox();
            this.GetMapPositionButton = new System.Windows.Forms.Button();
            this.ExecuteMoveButton = new System.Windows.Forms.Button();
            this.NumericX = new System.Windows.Forms.NumericUpDown();
            this.NumericY = new System.Windows.Forms.NumericUpDown();
            this.CommandBox = new System.Windows.Forms.TextBox();
            this.NorthButton = new System.Windows.Forms.Button();
            this.SouthButton = new System.Windows.Forms.Button();
            this.EastButton = new System.Windows.Forms.Button();
            this.WestButton = new System.Windows.Forms.Button();
            this.BlindModeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NumericX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericY)).BeginInit();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.FormattingEnabled = true;
            this.LogBox.Location = new System.Drawing.Point(12, 380);
            this.LogBox.Name = "LogBox";
            this.LogBox.Size = new System.Drawing.Size(803, 108);
            this.LogBox.TabIndex = 0;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(12, 12);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(107, 23);
            this.ConnectButton.TabIndex = 1;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // MapBox
            // 
            this.MapBox.Location = new System.Drawing.Point(289, 12);
            this.MapBox.Multiline = true;
            this.MapBox.Name = "MapBox";
            this.MapBox.Size = new System.Drawing.Size(526, 340);
            this.MapBox.TabIndex = 3;
            // 
            // GetMapPositionButton
            // 
            this.GetMapPositionButton.Location = new System.Drawing.Point(12, 41);
            this.GetMapPositionButton.Name = "GetMapPositionButton";
            this.GetMapPositionButton.Size = new System.Drawing.Size(107, 23);
            this.GetMapPositionButton.TabIndex = 5;
            this.GetMapPositionButton.Text = "Get Map Position";
            this.GetMapPositionButton.UseVisualStyleBackColor = true;
            this.GetMapPositionButton.Click += new System.EventHandler(this.GetMapPositionButton_Click);
            // 
            // ExecuteMoveButton
            // 
            this.ExecuteMoveButton.Location = new System.Drawing.Point(12, 70);
            this.ExecuteMoveButton.Name = "ExecuteMoveButton";
            this.ExecuteMoveButton.Size = new System.Drawing.Size(107, 23);
            this.ExecuteMoveButton.TabIndex = 6;
            this.ExecuteMoveButton.Text = "Execute Move";
            this.ExecuteMoveButton.UseVisualStyleBackColor = true;
            this.ExecuteMoveButton.Click += new System.EventHandler(this.ExecuteMoveButton_Click);
            // 
            // NumericX
            // 
            this.NumericX.Location = new System.Drawing.Point(125, 44);
            this.NumericX.Name = "NumericX";
            this.NumericX.Size = new System.Drawing.Size(35, 20);
            this.NumericX.TabIndex = 10;
            // 
            // NumericY
            // 
            this.NumericY.Location = new System.Drawing.Point(175, 44);
            this.NumericY.Name = "NumericY";
            this.NumericY.Size = new System.Drawing.Size(35, 20);
            this.NumericY.TabIndex = 11;
            // 
            // CommandBox
            // 
            this.CommandBox.Enabled = false;
            this.CommandBox.Location = new System.Drawing.Point(125, 72);
            this.CommandBox.Name = "CommandBox";
            this.CommandBox.Size = new System.Drawing.Size(100, 20);
            this.CommandBox.TabIndex = 12;
            this.CommandBox.TextChanged += new System.EventHandler(this.CommandBox_TextChanged);
            // 
            // NorthButton
            // 
            this.NorthButton.Location = new System.Drawing.Point(85, 99);
            this.NorthButton.Name = "NorthButton";
            this.NorthButton.Size = new System.Drawing.Size(75, 23);
            this.NorthButton.TabIndex = 13;
            this.NorthButton.Text = "North";
            this.NorthButton.UseVisualStyleBackColor = true;
            this.NorthButton.Click += new System.EventHandler(this.NorthButton_Click);
            // 
            // SouthButton
            // 
            this.SouthButton.Location = new System.Drawing.Point(85, 155);
            this.SouthButton.Name = "SouthButton";
            this.SouthButton.Size = new System.Drawing.Size(75, 23);
            this.SouthButton.TabIndex = 14;
            this.SouthButton.Text = "South";
            this.SouthButton.UseVisualStyleBackColor = true;
            this.SouthButton.Click += new System.EventHandler(this.SouthButton_Click);
            // 
            // EastButton
            // 
            this.EastButton.Location = new System.Drawing.Point(159, 127);
            this.EastButton.Name = "EastButton";
            this.EastButton.Size = new System.Drawing.Size(75, 23);
            this.EastButton.TabIndex = 15;
            this.EastButton.Text = "East";
            this.EastButton.UseVisualStyleBackColor = true;
            this.EastButton.Click += new System.EventHandler(this.EastButton_Click);
            // 
            // WestButton
            // 
            this.WestButton.Location = new System.Drawing.Point(12, 127);
            this.WestButton.Name = "WestButton";
            this.WestButton.Size = new System.Drawing.Size(75, 23);
            this.WestButton.TabIndex = 16;
            this.WestButton.Text = "West";
            this.WestButton.UseVisualStyleBackColor = true;
            this.WestButton.Click += new System.EventHandler(this.WestButton_Click);
            // 
            // BlindModeCheckBox
            // 
            this.BlindModeCheckBox.AutoSize = true;
            this.BlindModeCheckBox.Location = new System.Drawing.Point(203, 17);
            this.BlindModeCheckBox.Name = "BlindModeCheckBox";
            this.BlindModeCheckBox.Size = new System.Drawing.Size(76, 17);
            this.BlindModeCheckBox.TabIndex = 17;
            this.BlindModeCheckBox.Text = "BlindMode";
            this.BlindModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // RobotsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 500);
            this.Controls.Add(this.BlindModeCheckBox);
            this.Controls.Add(this.WestButton);
            this.Controls.Add(this.EastButton);
            this.Controls.Add(this.SouthButton);
            this.Controls.Add(this.NorthButton);
            this.Controls.Add(this.CommandBox);
            this.Controls.Add(this.NumericY);
            this.Controls.Add(this.NumericX);
            this.Controls.Add(this.ExecuteMoveButton);
            this.Controls.Add(this.GetMapPositionButton);
            this.Controls.Add(this.MapBox);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.LogBox);
            this.Name = "RobotsUI";
            this.Text = "MartianRobotsClient";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NumericX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LogBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TextBox MapBox;
        private System.Windows.Forms.Button GetMapPositionButton;
        private System.Windows.Forms.Button ExecuteMoveButton;
        private System.Windows.Forms.NumericUpDown NumericX;
        private System.Windows.Forms.NumericUpDown NumericY;
        private System.Windows.Forms.TextBox CommandBox;
        private System.Windows.Forms.Button NorthButton;
        private System.Windows.Forms.Button SouthButton;
        private System.Windows.Forms.Button EastButton;
        private System.Windows.Forms.Button WestButton;
        private System.Windows.Forms.CheckBox BlindModeCheckBox;
    }
}

