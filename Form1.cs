using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Text.RegularExpressions;


namespace XP_Diablo_s_Serial_Monitor
{
    public partial class Form1 : Form
    {

        SerialPort currentSerialPort;
        bool output_paused = false;

        public Form1()
        {
            InitializeComponent();
            currentSerialPort = new SerialPort();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length != 0)
            {
                this.PortChoiceBox.Items.AddRange(ports);
                this.PortChoiceBox.SelectedIndex = 0;
                this.currentSerialPort.PortName = this.PortChoiceBox.Text.ToString();
            }
            this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
            this.currentSerialPort.DataBits = 8;
            this.currentSerialPort.StopBits = StopBits.Two;
            this.currentSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            this.currentSerialPort.ErrorReceived += CurrentSerialPort_ErrorReceived;
            try
            {
                this.currentSerialPort.Open();
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208);
            }
            catch (System.UnauthorizedAccessException e)
            {
                this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
            }
            catch (System.IO.IOException e)
            {
                this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Waiting for ports... (Press the refresh button to update)" + Environment.NewLine);
            }
        }

        private void CurrentSerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.OutputBox.AppendText("[[ An ERROR has occurred with the Serial Port]]" + Environment.NewLine);
            Console.WriteLine(e);
        }

        private void PauseOutputButton_Click(object sender, EventArgs e)
        {
            output_paused = !output_paused;   
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs args)
        {
            string data = this.currentSerialPort.ReadExisting();
            if (!output_paused)
            {
                this.OutputBox.AppendText(data);
            }
        }

        private void InputBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.currentSerialPort.IsOpen)
                {
                    this.currentSerialPort.WriteLine(this.InputBox.Text);
                    this.InputBox.Clear();
                }
            }
        }

        private void BaudRateTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Regex.Match(this.BaudRateTextBox.Text, @"[\D]").Success) ;
                else
                {
                    this.currentSerialPort.Close();
                    this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
                    this.currentSerialPort.Open();
                }
            }
        }

        private void RefreshPortBoxButton_Click(object sender, EventArgs e)
        {
            this.PortChoiceBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length >= 1)
            {
                this.PortChoiceBox.Items.AddRange(ports);
                this.PortChoiceBox.SelectedIndex = 0;
            }
        }

        private void PortChoiceBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
                this.currentSerialPort.DataBits = 8;
                this.currentSerialPort.StopBits = StopBits.Two;
                this.currentSerialPort.Open();
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208);
            }
            catch (System.UnauthorizedAccessException)
            {
                this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
            }
            catch (System.IO.IOException)
            {
            }
        }

        private void DisconnectReconnectButton_Click(object sender, EventArgs e)
        {
            if (!this.currentSerialPort.IsOpen)
            {
                try
                {
                    this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
                    this.currentSerialPort.DataBits = 8;
                    this.currentSerialPort.StopBits = StopBits.Two;
                    this.currentSerialPort.Open();
                    this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208);
                }
                catch (System.UnauthorizedAccessException)
                {
                    this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
                }
                catch (System.IO.IOException)
                {
                    this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Failed to re-open connection..." + Environment.NewLine);
                }

            }
            else
            {
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(255, 80, 80);
                this.currentSerialPort.Close();
                this.OutputBox.AppendText(Environment.NewLine + "[[ *** ]] User Disconnected from " + this.currentSerialPort.PortName + Environment.NewLine);
                this.currentSerialPort.Dispose();
            }
        }

        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            this.OutputBox.Clear();
        }

        private void SendInputButton_Click(object sender, EventArgs e)
        {
            if (this.currentSerialPort.IsOpen)
            {
                this.currentSerialPort.WriteLine(this.InputBox.Text);
                this.InputBox.Clear();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
        }

    }
}
