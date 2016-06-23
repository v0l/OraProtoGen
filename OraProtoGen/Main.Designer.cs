namespace OraProtoGen
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.connstringInput = new System.Windows.Forms.TextBox();
            this.schemaInput = new System.Windows.Forms.ComboBox();
            this.tableInput = new System.Windows.Forms.ComboBox();
            this.outTxt = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.protoBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.csBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Connection String";
            // 
            // connstringInput
            // 
            this.connstringInput.Location = new System.Drawing.Point(12, 25);
            this.connstringInput.Name = "connstringInput";
            this.connstringInput.Size = new System.Drawing.Size(628, 20);
            this.connstringInput.TabIndex = 1;
            this.connstringInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // schemaInput
            // 
            this.schemaInput.Enabled = false;
            this.schemaInput.FormattingEnabled = true;
            this.schemaInput.Location = new System.Drawing.Point(12, 51);
            this.schemaInput.Name = "schemaInput";
            this.schemaInput.Size = new System.Drawing.Size(293, 21);
            this.schemaInput.TabIndex = 2;
            this.schemaInput.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tableInput
            // 
            this.tableInput.Enabled = false;
            this.tableInput.FormattingEnabled = true;
            this.tableInput.Location = new System.Drawing.Point(311, 51);
            this.tableInput.Name = "tableInput";
            this.tableInput.Size = new System.Drawing.Size(248, 21);
            this.tableInput.TabIndex = 3;
            // 
            // outTxt
            // 
            this.outTxt.Enabled = false;
            this.outTxt.Location = new System.Drawing.Point(12, 78);
            this.outTxt.Multiline = true;
            this.outTxt.Name = "outTxt";
            this.outTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outTxt.Size = new System.Drawing.Size(709, 517);
            this.outTxt.TabIndex = 4;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(646, 23);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 5;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // protoBtn
            // 
            this.protoBtn.Enabled = false;
            this.protoBtn.Location = new System.Drawing.Point(565, 49);
            this.protoBtn.Name = "protoBtn";
            this.protoBtn.Size = new System.Drawing.Size(75, 23);
            this.protoBtn.TabIndex = 6;
            this.protoBtn.Text = ".PROTO";
            this.protoBtn.UseVisualStyleBackColor = true;
            this.protoBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(12, 601);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(146, 23);
            this.saveBtn.TabIndex = 7;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // csBtn
            // 
            this.csBtn.Enabled = false;
            this.csBtn.Location = new System.Drawing.Point(646, 49);
            this.csBtn.Name = "csBtn";
            this.csBtn.Size = new System.Drawing.Size(75, 23);
            this.csBtn.TabIndex = 8;
            this.csBtn.Text = ".CS";
            this.csBtn.UseVisualStyleBackColor = true;
            this.csBtn.Click += new System.EventHandler(this.csBtn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 629);
            this.Controls.Add(this.csBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.protoBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.outTxt);
            this.Controls.Add(this.tableInput);
            this.Controls.Add(this.schemaInput);
            this.Controls.Add(this.connstringInput);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Main";
            this.Text = "Ora Proto Gen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox connstringInput;
        private System.Windows.Forms.ComboBox schemaInput;
        private System.Windows.Forms.ComboBox tableInput;
        private System.Windows.Forms.TextBox outTxt;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button protoBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button csBtn;
    }
}

