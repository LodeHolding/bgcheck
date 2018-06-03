namespace BLEScanner
{
    partial class frmMain
    {
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.btnStopScan = new System.Windows.Forms.Button();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lnkCopyToClipboard = new System.Windows.Forms.LinkLabel();
            this.lnkChoosePort = new System.Windows.Forms.LinkLabel();
            this.btnReset = new System.Windows.Forms.Button();
            this.toolTipChoosePort = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // cmbPorts
            // 
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.Location = new System.Drawing.Point(80, 5);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(100, 21);
            this.cmbPorts.TabIndex = 4;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(185, 5);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(100, 23);
            this.btnOpenPort.TabIndex = 5;
            this.btnOpenPort.Text = "&Open Port";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // btnStopScan
            // 
            this.btnStopScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopScan.Location = new System.Drawing.Point(672, 426);
            this.btnStopScan.Name = "btnStopScan";
            this.btnStopScan.Size = new System.Drawing.Size(100, 23);
            this.btnStopScan.TabIndex = 1;
            this.btnStopScan.Text = "Sto&p Scanning";
            this.btnStopScan.UseVisualStyleBackColor = true;
            this.btnStopScan.Click += new System.EventHandler(this.btnStopScan_Click);
            // 
            // btnStartScan
            // 
            this.btnStartScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartScan.Location = new System.Drawing.Point(566, 426);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(100, 23);
            this.btnStartScan.TabIndex = 2;
            this.btnStartScan.Text = "&Start Scanning";
            this.btnStartScan.UseVisualStyleBackColor = true;
            this.btnStartScan.Click += new System.EventHandler(this.btnStartScan_Click);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(12, 32);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(760, 388);
            this.txtLog.TabIndex = 3;
            // 
            // lnkCopyToClipboard
            // 
            this.lnkCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkCopyToClipboard.Location = new System.Drawing.Point(12, 426);
            this.lnkCopyToClipboard.Name = "lnkCopyToClipboard";
            this.lnkCopyToClipboard.Size = new System.Drawing.Size(100, 23);
            this.lnkCopyToClipboard.TabIndex = 6;
            this.lnkCopyToClipboard.TabStop = true;
            this.lnkCopyToClipboard.Text = "Copy to Clipboard";
            this.lnkCopyToClipboard.Click += new System.EventHandler(this.lnkCopyToClipboard_Click);
            // 
            // lnkChoosePort
            // 
            this.lnkChoosePort.AccessibleDescription = "";
            this.lnkChoosePort.AutoSize = true;
            this.lnkChoosePort.Location = new System.Drawing.Point(9, 8);
            this.lnkChoosePort.Name = "lnkChoosePort";
            this.lnkChoosePort.Size = new System.Drawing.Size(67, 13);
            this.lnkChoosePort.TabIndex = 7;
            this.lnkChoosePort.TabStop = true;
            this.lnkChoosePort.Text = "Choose port:";
            this.lnkChoosePort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkChoosePort.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkChoosePort_LinkClicked);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(672, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset Dongle";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // toolTipChoosePort
            // 
            this.toolTipChoosePort.ToolTipTitle = "Click to rescan COM ports.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lnkChoosePort);
            this.Controls.Add(this.lnkCopyToClipboard);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnStartScan);
            this.Controls.Add(this.btnStopScan);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "Blue Giga BLE Scan Test";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStopScan;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.LinkLabel lnkCopyToClipboard;
        private System.Windows.Forms.LinkLabel lnkChoosePort;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ToolTip toolTipChoosePort;
        private System.ComponentModel.IContainer components;
    }
}

