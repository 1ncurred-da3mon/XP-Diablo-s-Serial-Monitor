using System;
using System.Drawing;
using System.IO.Ports;

namespace XP_Diablo_s_Serial_Monitor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.InputBox = new System.Windows.Forms.TextBox();
            this.SendInputButton = new System.Windows.Forms.Button();
            this.BaudRateTextBox = new System.Windows.Forms.TextBox();
            this.PortChoiceBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OutputBox.Location = new System.Drawing.Point(12, 12);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.Size = new System.Drawing.Size(776, 338);
            this.OutputBox.TabIndex = 0;
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            // 
            // InputBox
            // 
            this.InputBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InputBox.Location = new System.Drawing.Point(13, 366);
            this.InputBox.Name = "InputBox";
            this.InputBox.Size = new System.Drawing.Size(655, 29);
            this.InputBox.TabIndex = 1;
            this.InputBox.KeyDown += InputBox_KeyDown;
            // 
            // SendInputButton
            // 
            this.SendInputButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SendInputButton.Location = new System.Drawing.Point(689, 365);
            this.SendInputButton.Name = "SendInputButton";
            this.SendInputButton.Size = new System.Drawing.Size(99, 30);
            this.SendInputButton.TabIndex = 2;
            this.SendInputButton.Text = "Send Input";
            this.SendInputButton.UseVisualStyleBackColor = true;
            this.SendInputButton.Click += SendInputButton_Click;
            // 
            // BaudRateTextBox
            // 
            this.BaudRateTextBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BaudRateTextBox.Location = new System.Drawing.Point(13, 401);
            this.BaudRateTextBox.Name = "BaudRateTextBox";
            this.BaudRateTextBox.Size = new System.Drawing.Size(73, 29);
            this.BaudRateTextBox.TabIndex = 3;
            this.BaudRateTextBox.Text = "9600";
            // 
            // PortChoiceBox
            // 
            this.PortChoiceBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PortChoiceBox.FormattingEnabled = true;
            this.PortChoiceBox.Location = new System.Drawing.Point(667, 401);
            this.PortChoiceBox.Name = "PortChoiceBox";
            this.PortChoiceBox.Size = new System.Drawing.Size(121, 29);
            this.PortChoiceBox.TabIndex = 4;
            this.PortChoiceBox.SelectedValueChanged += PortChoiceBox_SelectedValueChanged;
            this.PortChoiceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            //
            // RefreshPortBoxButton
            //
            this.RefreshPortBoxButton = new System.Windows.Forms.Button();
            this.RefreshPortBoxButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RefreshPortBoxButton.Left = this.PortChoiceBox.Left - 50;
            this.RefreshPortBoxButton.Top = this.PortChoiceBox.Top;
            this.RefreshPortBoxButton.Text = "⭯";
            this.RefreshPortBoxButton.Size = new System.Drawing.Size(38, 38);
            this.RefreshPortBoxButton.UseVisualStyleBackColor = true;
            this.RefreshPortBoxButton.Click += RefreshPortBoxButton_Click;

            //
            // Pause Output Button
            //
            this.PauseOutputButton = new System.Windows.Forms.Button();
            this.PauseOutputButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PauseOutputButton.Left = this.RefreshPortBoxButton.Left - 100;
            this.PauseOutputButton.Top = this.PortChoiceBox.Top - 1;
            this.PauseOutputButton.Text = "⏯️";
            this.PauseOutputButton.Size = new System.Drawing.Size(42, 42);
            this.PauseOutputButton.UseVisualStyleBackColor = true;
            this.PauseOutputButton.Click += PauseOutputButton_Click;

            //
            // DisconnectReconnectButton
            //
            this.DisconnectReconnectButton = new System.Windows.Forms.Button();
            this.DisconnectReconnectButton.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DisconnectReconnectButton.Left = this.PauseOutputButton.Left - 200;
            this.DisconnectReconnectButton.Top = this.PortChoiceBox.Top - 1;
            this.DisconnectReconnectButton.Text = "Disconnect/\nConnect";
            this.DisconnectReconnectButton.Size = new System.Drawing.Size(100, 42);
            this.DisconnectReconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectReconnectButton.BackColor = System.Drawing.Color.FromArgb(255, 80, 80);
            this.DisconnectReconnectButton.Click += DisconnectReconnectButton_Click;

            //
            // ClearOutputButton
            //
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.ClearOutputButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ClearOutputButton.Left = this.DisconnectReconnectButton.Left - 100;
            this.ClearOutputButton.Top = this.PortChoiceBox.Top - 1;
            this.ClearOutputButton.Text = "Clear";
            this.ClearOutputButton.Size = new System.Drawing.Size(80, 42);
            this.ClearOutputButton.UseVisualStyleBackColor = true;
            this.ClearOutputButton.Click += ClearOutputButton_Click;

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(93, 402);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "baud";


            // 
            // Form1
            // 


            this.Resize += Form1_Resize;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PortChoiceBox);
            this.Controls.Add(this.BaudRateTextBox);
            this.Controls.Add(this.SendInputButton);
            this.Controls.Add(this.InputBox);
            this.Controls.Add(this.OutputBox);
            this.Controls.Add(this.RefreshPortBoxButton);
            this.Controls.Add(this.PauseOutputButton);
            this.Controls.Add(this.DisconnectReconnectButton);
            this.Controls.Add(this.ClearOutputButton);
            this.Name = "Form1";
            this.Text = "XP Diablo's Serial Monitor";
            this.FormClosed += Form1_FormClosed;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

       





        #endregion

        private System.Windows.Forms.TextBox OutputBox;
        private System.Windows.Forms.TextBox InputBox;
        private System.Windows.Forms.Button SendInputButton;
        private System.Windows.Forms.TextBox BaudRateTextBox;
        private System.Windows.Forms.ComboBox PortChoiceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button RefreshPortBoxButton;
        private System.Windows.Forms.Button PauseOutputButton;
        private System.Windows.Forms.Button DisconnectReconnectButton;
        private System.Windows.Forms.Button ClearOutputButton;
    }
}

