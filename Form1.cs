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

        SerialPort currentSerialPort;   // serial port which the user is currently using
        bool output_paused = false;     // used for when the user pauses the output

        public Form1()
        {
            InitializeComponent();                          // Initialize the components (found in Form1.Designer.cs)
            currentSerialPort = new SerialPort();           // Create the Serial Port
            string[] ports = SerialPort.GetPortNames();     // Get all the ports that are accessable
            
            // If there are COM ports available, add all the available ports to the PortChoiceBox object
            if (ports.Length != 0)
            {
                this.PortChoiceBox.Items.AddRange(ports);
                this.PortChoiceBox.SelectedIndex = 0;       // Set the current port equal to the first port available
            }

            this.currentSerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); // Create an event handler for when data is received
            this.currentSerialPort.ErrorReceived += CurrentSerialPort_ErrorReceived;    // Create an event handler for when an error occurs with the Serial Port
            if (this.currentSerialPort.IsOpen) this.currentSerialPort.Close();
            try // Try to open the port, an error should be expected if no ports were detected
            {
                this.currentSerialPort.PortName = this.PortChoiceBox.Text.ToString();       // Assign the port name
                this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);   // Assign the Baud Rate
                this.currentSerialPort.DataBits = 8;            // Assign the Data Bits
                this.currentSerialPort.StopBits = StopBits.Two; // Assign the Stop Bits
            
                this.currentSerialPort.Open();  // Open the port
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208); // Set the color of the disconnect/reconnect button to cyan
            }
            catch (System.UnauthorizedAccessException e) // Another program is currently using the serial port
            {
                this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
                Console.WriteLine("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable.");
            }
            catch (System.IO.IOException e) // No port was detected, or the port opening does not exist.
            {
                Console.WriteLine("[[[ !!! ]]] No port was detected. Waiting for ports... (Press the refresh button to update)");
                this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Waiting for ports... (Press the refresh button to update)" + Environment.NewLine);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("[[[ !!! ]]] No port was detected. Waiting for ports... (Press the refresh button to update)");
                this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Waiting for ports... (Press the refresh button to update)" + Environment.NewLine);
            }
        }

        // Handles the errors with the Serial Port
        private void CurrentSerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            this.OutputBox.AppendText("[[ An ERROR has occurred with the Serial Port]]" + Environment.NewLine);
            Console.WriteLine("[[ An ERROR has occurred with the Serial Port]]");
            Console.WriteLine("Error message below:");
            Console.WriteLine(e);
        }

        // Pauses or resumes the output from the serial port
        private void PauseOutputButton_Click(object sender, EventArgs e)
        {
            output_paused = !output_paused;
            Console.WriteLine("Output status set to: " + output_paused);
        }

        // When the port receives data
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs args)
        {
            string data = this.currentSerialPort.ReadExisting(); // Store the data into a string
            if (!output_paused) // if the output is not paused, proceed to output data, otherwise ignore.
            {
                this.OutputBox.AppendText(data);
                Console.WriteLine(data);
            }
        }

        // When the InputBox is in focus, and the user presses a key
        private void InputBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // If the user hits the Enter key
            {
                if (this.currentSerialPort.IsOpen) // If the Serial Port is open, allow to send input, and clear the InputBox of text.
                {
                    this.currentSerialPort.WriteLine(this.InputBox.Text);
                    this.InputBox.Clear();
                }
            }
        }


        private void BaudRateTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            // User wishes to assign the Baud Rate
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
            // User hits the backspace
            else if (e.KeyCode == Keys.Back)
                BaudRateTextBox.Text.Remove(BaudRateTextBox.TextLength - 1);

            // User can only enter a number
            else if (e.KeyValue >= '0' || e.KeyValue <= '9')
                BaudRateTextBox.Text += (char)e.KeyValue;
            else;
        }

        // Refreshes the list of ports in the Dropdown Box for PortChoiceBox
        private void RefreshPortBoxButton_Click(object sender, EventArgs e)
        {
            this.PortChoiceBox.Items.Clear();   // clear the dropdown box of all elements
            string[] ports = SerialPort.GetPortNames(); // get all the COM ports
            if (ports.Length >= 1)  // There is at least one port available
            {
                this.PortChoiceBox.Items.AddRange(ports);   // Add all the ports to the dropdown box
                this.PortChoiceBox.SelectedIndex = 0;       // Set the first port available
            }
        }

        // When the value inside the PortChoiceBox has changed
        private void PortChoiceBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.currentSerialPort.IsOpen)
                    this.currentSerialPort.Close();
                this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
                this.currentSerialPort.DataBits = 8;
                this.currentSerialPort.StopBits = StopBits.Two;
                this.currentSerialPort.PortName = this.PortChoiceBox.Text.ToString();


                this.currentSerialPort.Open();
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208);
            }
            catch (System.UnauthorizedAccessException) // Failed to connect, as another program may be using the port
            {
                Console.WriteLine("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable.");
                this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("[[[ !!! ]]] No Port can be found...");
                this.OutputBox.AppendText("[[[ !!! ]]] No Port can be found...");
            }
            catch (System.IO.IOException) // Honestly, can't remember why this is here, but I will figure it out later
            {
            }
        }

        // Handles the DisconnectReconnect Button
        private void DisconnectReconnectButton_Click(object sender, EventArgs e)
        {
            // Check to see if the Serial Port is not open, if it is not, proceed to open the port.
            if (!this.currentSerialPort.IsOpen)
            {
                try
                {
                    if (this.PortChoiceBox.Items.Count >= 1)
                        this.currentSerialPort.PortName = this.PortChoiceBox.Text.ToString();
                    else
                        this.currentSerialPort.PortName = String.Empty;
                    this.currentSerialPort.BaudRate = Int32.Parse(this.BaudRateTextBox.Text);
                    this.currentSerialPort.DataBits = 8;
                    this.currentSerialPort.StopBits = StopBits.Two;
                    this.currentSerialPort.Open();
                    this.DisconnectReconnectButton.BackColor = Color.FromArgb(76, 223, 208);
                }
                catch (System.UnauthorizedAccessException) // Another program may be using the port
                {
                    Console.WriteLine("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable.");
                    this.OutputBox.AppendText("[[[ !!! ]]] PORT " + this.PortChoiceBox.SelectedItem.ToString() + " is unaccessable." + Environment.NewLine);
                }
                catch (System.IO.IOException) // Failed to reconnect
                {
                    Console.WriteLine("[[[ !!! ]]] No port was detected. Failed to re-open connection...");
                    this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Failed to re-open connection..." + Environment.NewLine);
                }
                catch (System.ArgumentException)
                {
                    Console.WriteLine("[[[ !!! ]]] No port was detected. Failed to re-open connection...");
                    this.OutputBox.AppendText("[[[ !!! ]]] No port was detected. Failed to re-open connection..." + Environment.NewLine);
                }

            }
            else // The Serial Port is open. Proceed to close the connection.
            {
                this.DisconnectReconnectButton.BackColor = Color.FromArgb(255, 80, 80);
                this.currentSerialPort.Close();
                this.OutputBox.AppendText(Environment.NewLine + "[[ *** ]] User Disconnected from " + this.currentSerialPort.PortName + Environment.NewLine);
                Console.WriteLine("\n[[ *** ]] User Disconnected from " + this.currentSerialPort.PortName);
                this.currentSerialPort.Dispose();
            }
        }

        // Clear the output box when the ClearOutput Button is clicked
        private void ClearOutputButton_Click(object sender, EventArgs e)
        {
            this.OutputBox.Clear();
        }

        // Sends the input from the InputBox, if the connection is live
        private void SendInputButton_Click(object sender, EventArgs e)
        {
            if (this.currentSerialPort.IsOpen)
            {
                this.currentSerialPort.WriteLine(this.InputBox.Text);
                this.InputBox.Clear();
            }
        }

        // Will modify later, as resizing does not move everything else.
        private void Form1_Resize(object sender, EventArgs e)
        {
        }

        // When the form is closed
        private void Form1_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            this.currentSerialPort.Close();
            this.currentSerialPort.Dispose();
        }

    }
}
