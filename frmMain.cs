// Bluegiga BGLib C# interface library
// 2013-01-15 by Jeff Rowberg <jeff@rowberg.net>
// Updates should (hopefully) always be available at https://github.com/jrowberg/bglib

/* ============================================
BGLib C# interface library code is placed under the MIT license
Copyright (c) 2013 Jeff Rowberg

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
===============================================
*/

using Bluegiga;
using Bluegiga.BLE.Events.System;
using Bluegiga.BLE.Responses.GAP;
using Bluegiga.BLE.Responses.System;
using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;

namespace BLEScanner
{
    public partial class frmMain : Form
    {
        private SerialPort serialPort;
        private BGLib bglib = new BGLib();

        public frmMain()
        {
            InitializeComponent();

            bglib.BLEResponseSystemHello += SystemHelloHandler;
            bglib.BLEResponseSystemGetInfo += SystemGetInfoHandler;
            bglib.BLEEventSystemBoot += SystemBootHandler;
            bglib.BLEResponseSystemAddressGet += SystemAddressGetHandler;
            bglib.BLEResponseSystemGetConnections += SystemGetConnectionsHandler;
            bglib.BLEResponseSystemReset += SystemResetHandler;

            bglib.BLEResponseGAPDiscover += GAPDiscoverHandler;
            bglib.BLEResponseGAPEndProcedure += GAPEndProcedureHandler;
            bglib.BLEEventGAPScanResponse += GAPScanResponseHandler;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (serialPort != null))
            {
                serialPort.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Thread-safe operations from event handlers
        // I love StackOverflow: http://stackoverflow.com/q/782274
        private void ThreadSafeDelegate(MethodInvoker method)
        {
            if (InvokeRequired)
                BeginInvoke(method);
            else
                method.Invoke();
        }

        private void WriteTxtLog(string log)
        {
            if (string.IsNullOrEmpty(log))
                return;
            log += Environment.NewLine;
            Console.Write(log);
            ThreadSafeDelegate(delegate { txtLog.AppendText(log); });
        }

        private bool CheckPortOpened()
        {
            if (serialPort == null)
            {
                WriteTxtLog("choose port ...");
                return false;
            }
            if (!serialPort.IsOpen)
            {
                WriteTxtLog("port closed!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Send command to serialPort.
        /// </summary>
        private void SendCommand(byte[] cmd)
        {
            if (!CheckPortOpened())
                return;
            try
            {
                bglib.SendCommand(serialPort, cmd);
            }
            catch (Exception ex)
            {
                WriteTxtLog($"Serial port write failed: {ex.Message}");
            }
        }

        /// <summary>
        /// Convert int result for human.
        /// </summary>
        private static string ResultHuman(int result)
        {
            return result == 0 ? "ok" : $"error {result}";
        }

        private void SystemHelloHandler(object sender, HelloEventArgs e)
        {
            WriteTxtLog("hello: ok");
            AfterHello();
        }

        private void SystemGetInfoHandler(object sender, GetInfoEventArgs e)
        {
            string log = string.Format("get_info: version: {0}.{1}.{2}.{3}, ll_version: {4}, protocol_version: {5}, hw: {6}",
                e.major,
                e.minor,
                e.patch,
                e.build,
                e.ll_version,
                e.protocol_version,
                e.hw
                );
            WriteTxtLog(log);
        }

        private void SystemBootHandler(object sender, BootEventArgs e)
        {
            string log = string.Format("boot: version: {0}.{1}.{2}.{3}, ll_version: {4}, protocol_version: {5}, hw: {6}",
                 e.major,
                 e.minor,
                 e.patch,
                 e.build,
                 e.ll_version,
                 e.protocol_version,
                 e.hw
                 );
            WriteTxtLog(log);
        }

        private void SystemAddressGetHandler(object sender, AddressGetEventArgs e)
        {
            WriteTxtLog("BLE address: " + ByteArrayToHexString(e.address));
        }

        private void SystemGetConnectionsHandler(object sender, GetConnectionsEventArgs e)
        {
            WriteTxtLog($"max connections: {e.maxconn}");
        }

        private void SystemResetHandler(object sender, ResetEventArgs e)
        {
            WriteTxtLog("reset: ok");
        }

        private void GAPDiscoverHandler(object sender, DiscoverEventArgs e)
        {
            string log = $"ble_scan_start: {ResultHuman(e.result)}";
            WriteTxtLog(log);
        }

        private void GAPEndProcedureHandler(object sender, EndProcedureEventArgs e)
        {
            string log = $"ble_scan_end: {ResultHuman(e.result)}";
            WriteTxtLog(log);
        }

        private void GAPScanResponseHandler(object sender, Bluegiga.BLE.Events.GAP.ScanResponseEventArgs e)
        {
            String log = String.Format("ble_evt_gap_scan_response:" + Environment.NewLine + "\trssi: {0}, packet_type: {1}, bd_addr: [{2}], address_type={3}, bond={4}, data=[{5}]",
                e.rssi,
                e.packet_type,
                ByteArrayToHexString(e.sender),
                e.address_type,
                e.bond,
                ByteArrayToHexString(e.data)
                );
            WriteTxtLog(log);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadPorts();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            Byte[] inData = new Byte[sp.BytesToRead];

            // read all available bytes from serial port in one chunk
            try
            {
                sp.Read(inData, 0, sp.BytesToRead);
            }
            catch (Exception ex)
            {
                WriteTxtLog($"Serial port read failed: {ex}");
                return;
            }

            // DEBUG: display bytes read
            Console.WriteLine("<= RX ({0}) [{1}]", inData.Length, ByteArrayToHexString(inData));

            // parse all bytes read through BGLib parser
            for (int i = 0; i < inData.Length; i++)
            {
                bglib.Parse(inData[i]);
            }
        }

        private void ErrorReceivedHandler(object sender, SerialErrorReceivedEventArgs e)
        {
            WriteTxtLog($"Serial port error: {e.EventType}");
        }

        private void LoadPorts()
        {
            // fill cbPorts
            string[] portNames = SerialPort.GetPortNames();
            cmbPorts.Items.AddRange(portNames);
            // often, the BlueGiga is at COM3, otherwise select the first port
            if (portNames.Length > 0)
            {
                int index = Array.IndexOf(portNames, "COM3");
                if (index < 0)
                    index = 0;
                cmbPorts.SelectedIndex = index;
            }
        }

        private void lnkChoosePort_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadPorts();
        }

        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            string portName = cmbPorts.SelectedItem as string;
            if (portName == null)
                return;

            if (serialPort != null)
            {
                try
                {
                    serialPort.Dispose();
                }
                catch (Exception ex)
                {
                    WriteTxtLog($"{serialPort.PortName} dispose failed. {ex}");
                }
            }

            serialPort = new SerialPort(portName);
            serialPort.Handshake = Handshake.RequestToSend;
            serialPort.BaudRate = 115200;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Parity = Parity.None;
            serialPort.WriteTimeout = 1000;
            serialPort.DataReceived += DataReceivedHandler;
            serialPort.ErrorReceived += ErrorReceivedHandler;
            try
            {
                serialPort.Open();
            }
            catch (Exception ex)
            {
                WriteTxtLog($"Port open failed: {portName}: {ex}");
            }
            WriteTxtLog($"Port opened: {portName}");

            SendCommand(bglib.BLECommandSystemHello());
        }

        /// <summary>
        /// Continue after hello is OK.
        /// </summary>
        private void AfterHello()
        {
            SendCommand(bglib.BLECommandSystemGetInfo());
            SendCommand(bglib.BLECommandSystemAddressGet());
            SendCommand(bglib.BLECommandSystemGetConnections());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // reset to normal mode
            SendCommand(bglib.BLECommandSystemReset(0));
        }

        private void btnStartScan_Click(object sender, EventArgs e)
        {
            SendCommand(bglib.BLECommandGAPDiscover(1));
        }

        private void btnStopScan_Click(object sender, EventArgs e)
        {
            SendCommand(bglib.BLECommandGAPEndProcedure());
        }

        private void lnkCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtLog.Text);
        }

        public static string ByteArrayToHexString(Byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }
    }
}