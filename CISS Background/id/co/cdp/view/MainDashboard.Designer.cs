namespace CISS_Background
{
    partial class MainDashboard
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDashboard));
            this.btnStart = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_p1_monitoring_db = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_securos_db = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_io_configuration = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_socket_configuration = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_api_configuration = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eKioskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_dummy_decision = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_p1_status = new System.Windows.Forms.Label();
            this.group_securos_db = new System.Windows.Forms.GroupBox();
            this.label_securos_db = new System.Windows.Forms.Label();
            this.group_ciss_db = new System.Windows.Forms.GroupBox();
            this.label_ciss_db = new System.Windows.Forms.Label();
            this.grp_ciss_db = new System.Windows.Forms.GroupBox();
            this.label_ciss_db_status = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_status = new System.Windows.Forms.Label();
            this.tbl_transaction_monitoring = new System.Windows.Forms.DataGridView();
            this.gate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.line_type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rfid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plate_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.container_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.securos_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tr_status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.master = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountTapping = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.txt_csv = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.group_securos_db.SuspendLayout();
            this.group_ciss_db.SuspendLayout();
            this.grp_ciss_db.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_transaction_monitoring)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(6, 29);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(95, 32);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(922, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.menu_io_configuration,
            this.menu_socket_configuration,
            this.menu_api_configuration});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_p1_monitoring_db,
            this.menu_securos_db});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.databaseToolStripMenuItem.Text = "Database Configuration";
            // 
            // menu_p1_monitoring_db
            // 
            this.menu_p1_monitoring_db.Name = "menu_p1_monitoring_db";
            this.menu_p1_monitoring_db.Size = new System.Drawing.Size(160, 22);
            this.menu_p1_monitoring_db.Text = "CISS Monitoring";
            this.menu_p1_monitoring_db.Click += new System.EventHandler(this.menu_p1_monitoring_db_Click);
            // 
            // menu_securos_db
            // 
            this.menu_securos_db.Name = "menu_securos_db";
            this.menu_securos_db.Size = new System.Drawing.Size(160, 22);
            this.menu_securos_db.Text = "Securos";
            this.menu_securos_db.Click += new System.EventHandler(this.menu_securos_db_Click);
            // 
            // menu_io_configuration
            // 
            this.menu_io_configuration.Name = "menu_io_configuration";
            this.menu_io_configuration.Size = new System.Drawing.Size(199, 22);
            this.menu_io_configuration.Text = "I/O File Configuration";
            this.menu_io_configuration.Click += new System.EventHandler(this.menu_io_configuration_Click);
            // 
            // menu_socket_configuration
            // 
            this.menu_socket_configuration.Name = "menu_socket_configuration";
            this.menu_socket_configuration.Size = new System.Drawing.Size(199, 22);
            this.menu_socket_configuration.Text = "Socket Configuration";
            this.menu_socket_configuration.Click += new System.EventHandler(this.menu_socket_configuration_Click);
            // 
            // menu_api_configuration
            // 
            this.menu_api_configuration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transactionToolStripMenuItem,
            this.eKioskToolStripMenuItem});
            this.menu_api_configuration.Name = "menu_api_configuration";
            this.menu_api_configuration.Size = new System.Drawing.Size(199, 22);
            this.menu_api_configuration.Text = "API Configuration";
            this.menu_api_configuration.Click += new System.EventHandler(this.menu_api_configuration_Click);
            // 
            // transactionToolStripMenuItem
            // 
            this.transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            this.transactionToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.transactionToolStripMenuItem.Text = "Transaction";
            this.transactionToolStripMenuItem.Click += new System.EventHandler(this.transactionToolStripMenuItem_Click);
            // 
            // eKioskToolStripMenuItem
            // 
            this.eKioskToolStripMenuItem.Name = "eKioskToolStripMenuItem";
            this.eKioskToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.eKioskToolStripMenuItem.Text = "E-Kiosk";
            this.eKioskToolStripMenuItem.Click += new System.EventHandler(this.eKioskToolStripMenuItem_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(107, 29);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(95, 32);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_dummy_decision);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.group_securos_db);
            this.groupBox1.Controls.Add(this.group_ciss_db);
            this.groupBox1.Controls.Add(this.grp_ciss_db);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(874, 119);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Transaction Connection";
            // 
            // cb_dummy_decision
            // 
            this.cb_dummy_decision.AutoSize = true;
            this.cb_dummy_decision.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_dummy_decision.ForeColor = System.Drawing.Color.Black;
            this.cb_dummy_decision.Location = new System.Drawing.Point(285, 77);
            this.cb_dummy_decision.Name = "cb_dummy_decision";
            this.cb_dummy_decision.Size = new System.Drawing.Size(186, 24);
            this.cb_dummy_decision.TabIndex = 2;
            this.cb_dummy_decision.Text = "Dummy Transaction";
            this.cb_dummy_decision.UseVisualStyleBackColor = true;
            this.cb_dummy_decision.CheckedChanged += new System.EventHandler(this.cb_dummy_decision_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_p1_status);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(352, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 52);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "P1 CONNECTION STATUS";
            // 
            // label_p1_status
            // 
            this.label_p1_status.AutoSize = true;
            this.label_p1_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_p1_status.ForeColor = System.Drawing.Color.Red;
            this.label_p1_status.Location = new System.Drawing.Point(6, 16);
            this.label_p1_status.Name = "label_p1_status";
            this.label_p1_status.Size = new System.Drawing.Size(206, 29);
            this.label_p1_status.TabIndex = 0;
            this.label_p1_status.Text = "DISCONNECTED";
            // 
            // group_securos_db
            // 
            this.group_securos_db.Controls.Add(this.label_securos_db);
            this.group_securos_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_securos_db.Location = new System.Drawing.Point(717, 19);
            this.group_securos_db.Name = "group_securos_db";
            this.group_securos_db.Size = new System.Drawing.Size(146, 80);
            this.group_securos_db.TabIndex = 1;
            this.group_securos_db.TabStop = false;
            this.group_securos_db.Text = "SECUROS []";
            // 
            // label_securos_db
            // 
            this.label_securos_db.AutoSize = true;
            this.label_securos_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_securos_db.ForeColor = System.Drawing.Color.Red;
            this.label_securos_db.Location = new System.Drawing.Point(40, 34);
            this.label_securos_db.Name = "label_securos_db";
            this.label_securos_db.Size = new System.Drawing.Size(50, 29);
            this.label_securos_db.TabIndex = 0;
            this.label_securos_db.Text = "NO";
            // 
            // group_ciss_db
            // 
            this.group_ciss_db.Controls.Add(this.label_ciss_db);
            this.group_ciss_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_ciss_db.Location = new System.Drawing.Point(574, 19);
            this.group_ciss_db.Name = "group_ciss_db";
            this.group_ciss_db.Size = new System.Drawing.Size(138, 80);
            this.group_ciss_db.TabIndex = 1;
            this.group_ciss_db.TabStop = false;
            this.group_ciss_db.Text = "CISS DB []";
            // 
            // label_ciss_db
            // 
            this.label_ciss_db.AutoSize = true;
            this.label_ciss_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ciss_db.ForeColor = System.Drawing.Color.Red;
            this.label_ciss_db.Location = new System.Drawing.Point(40, 36);
            this.label_ciss_db.Name = "label_ciss_db";
            this.label_ciss_db.Size = new System.Drawing.Size(50, 29);
            this.label_ciss_db.TabIndex = 0;
            this.label_ciss_db.Text = "NO";
            // 
            // grp_ciss_db
            // 
            this.grp_ciss_db.Controls.Add(this.label_ciss_db_status);
            this.grp_ciss_db.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_ciss_db.Location = new System.Drawing.Point(574, 19);
            this.grp_ciss_db.Name = "grp_ciss_db";
            this.grp_ciss_db.Size = new System.Drawing.Size(138, 52);
            this.grp_ciss_db.TabIndex = 1;
            this.grp_ciss_db.TabStop = false;
            this.grp_ciss_db.Text = "CISS DB [10.0.3.8]";
            // 
            // label_ciss_db_status
            // 
            this.label_ciss_db_status.AutoSize = true;
            this.label_ciss_db_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ciss_db_status.ForeColor = System.Drawing.Color.Red;
            this.label_ciss_db_status.Location = new System.Drawing.Point(40, 16);
            this.label_ciss_db_status.Name = "label_ciss_db_status";
            this.label_ciss_db_status.Size = new System.Drawing.Size(50, 29);
            this.label_ciss_db_status.TabIndex = 0;
            this.label_ciss_db_status.Text = "NO";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_status);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(208, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 52);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MONITORING STATUS";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.ForeColor = System.Drawing.Color.Red;
            this.label_status.Location = new System.Drawing.Point(3, 16);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(115, 29);
            this.label_status.TabIndex = 0;
            this.label_status.Text = "OFFLINE";
            // 
            // tbl_transaction_monitoring
            // 
            this.tbl_transaction_monitoring.AllowUserToAddRows = false;
            this.tbl_transaction_monitoring.AllowUserToDeleteRows = false;
            this.tbl_transaction_monitoring.AllowUserToResizeColumns = false;
            this.tbl_transaction_monitoring.AllowUserToResizeRows = false;
            this.tbl_transaction_monitoring.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tbl_transaction_monitoring.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gate,
            this.line,
            this.line_type,
            this.rfid,
            this.plate_no,
            this.container_no,
            this.securos_id,
            this.tr_status,
            this.master,
            this.amountTapping});
            this.tbl_transaction_monitoring.Location = new System.Drawing.Point(12, 360);
            this.tbl_transaction_monitoring.Name = "tbl_transaction_monitoring";
            this.tbl_transaction_monitoring.RowTemplate.Height = 50;
            this.tbl_transaction_monitoring.Size = new System.Drawing.Size(874, 237);
            this.tbl_transaction_monitoring.TabIndex = 3;
            // 
            // gate
            // 
            this.gate.HeaderText = "GATE NAME";
            this.gate.Name = "gate";
            this.gate.Width = 50;
            // 
            // line
            // 
            this.line.FillWeight = 50F;
            this.line.HeaderText = "LINE";
            this.line.Name = "line";
            this.line.Width = 50;
            // 
            // line_type
            // 
            this.line_type.HeaderText = "LINE TYPE";
            this.line_type.Name = "line_type";
            this.line_type.Width = 50;
            // 
            // rfid
            // 
            this.rfid.HeaderText = "RFID";
            this.rfid.Name = "rfid";
            // 
            // plate_no
            // 
            this.plate_no.HeaderText = "PLATE NO";
            this.plate_no.Name = "plate_no";
            // 
            // container_no
            // 
            this.container_no.HeaderText = "CONTAINER_NO";
            this.container_no.Name = "container_no";
            // 
            // securos_id
            // 
            this.securos_id.HeaderText = "SECUROS ID";
            this.securos_id.Name = "securos_id";
            // 
            // tr_status
            // 
            this.tr_status.HeaderText = "TR STATUS";
            this.tr_status.Name = "tr_status";
            this.tr_status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.tr_status.Width = 95;
            // 
            // master
            // 
            this.master.HeaderText = "MASTER STATUS";
            this.master.Name = "master";
            // 
            // amountTapping
            // 
            this.amountTapping.HeaderText = "AMOUNT TAPPING";
            this.amountTapping.Name = "amountTapping";
            this.amountTapping.Width = 65;
            // 
            // notify
            // 
            this.notify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notify.BalloonTipText = "Monitoring CDP Container Truck Transaction";
            this.notify.BalloonTipTitle = "CISS Background";
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "CISS Background Application";
            this.notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // txt_csv
            // 
            this.txt_csv.BackColor = System.Drawing.Color.LimeGreen;
            this.txt_csv.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txt_csv.Location = new System.Drawing.Point(12, 152);
            this.txt_csv.Multiline = true;
            this.txt_csv.Name = "txt_csv";
            this.txt_csv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_csv.Size = new System.Drawing.Size(874, 202);
            this.txt_csv.TabIndex = 4;
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 613);
            this.Controls.Add(this.txt_csv);
            this.Controls.Add(this.tbl_transaction_monitoring);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainDashboard";
            this.Text = "CISS Background Space v.0.3.0";
            this.Resize += new System.EventHandler(this.MainDashboard_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.group_securos_db.ResumeLayout(false);
            this.group_securos_db.PerformLayout();
            this.group_ciss_db.ResumeLayout(false);
            this.group_ciss_db.PerformLayout();
            this.grp_ciss_db.ResumeLayout(false);
            this.grp_ciss_db.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbl_transaction_monitoring)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripMenuItem menu_io_configuration;
        private System.Windows.Forms.ToolStripMenuItem menu_socket_configuration;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_p1_monitoring_db;
        private System.Windows.Forms.ToolStripMenuItem menu_securos_db;
        private System.Windows.Forms.ToolStripMenuItem menu_api_configuration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.DataGridView tbl_transaction_monitoring;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_p1_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn gate;
        private System.Windows.Forms.DataGridViewTextBoxColumn line;
        private System.Windows.Forms.DataGridViewTextBoxColumn line_type;
        private System.Windows.Forms.DataGridViewTextBoxColumn rfid;
        private System.Windows.Forms.DataGridViewTextBoxColumn plate_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn container_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn securos_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tr_status;
        private System.Windows.Forms.DataGridViewTextBoxColumn master;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountTapping;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.GroupBox group_securos_db;
        private System.Windows.Forms.Label label_securos_db;
        private System.Windows.Forms.GroupBox group_ciss_db;
        private System.Windows.Forms.Label label_ciss_db;
        private System.Windows.Forms.GroupBox grp_ciss_db;
        private System.Windows.Forms.Label label_ciss_db_status;
        private System.Windows.Forms.TextBox txt_csv;
        private System.Windows.Forms.CheckBox cb_dummy_decision;
        private System.Windows.Forms.ToolStripMenuItem transactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eKioskToolStripMenuItem;
    }
}

