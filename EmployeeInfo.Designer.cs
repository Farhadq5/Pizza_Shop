namespace Pizza_Shop
{
    partial class EmployeeInfo
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtphone = new System.Windows.Forms.TextBox();
            this.txtlastname = new System.Windows.Forms.TextBox();
            this.txtemail = new System.Windows.Forms.TextBox();
            this.txtfirstname = new System.Windows.Forms.TextBox();
            this.Insertcustomer = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(233, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "PhoneNo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(233, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 27;
            this.label3.Text = "Lastname";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Firstname";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(110, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 31);
            this.label1.TabIndex = 25;
            this.label1.Text = "Your Information";
            // 
            // txtphone
            // 
            this.txtphone.Location = new System.Drawing.Point(236, 174);
            this.txtphone.Name = "txtphone";
            this.txtphone.Size = new System.Drawing.Size(155, 20);
            this.txtphone.TabIndex = 24;
            // 
            // txtlastname
            // 
            this.txtlastname.Location = new System.Drawing.Point(236, 114);
            this.txtlastname.Name = "txtlastname";
            this.txtlastname.Size = new System.Drawing.Size(155, 20);
            this.txtlastname.TabIndex = 23;
            // 
            // txtemail
            // 
            this.txtemail.Location = new System.Drawing.Point(54, 174);
            this.txtemail.Name = "txtemail";
            this.txtemail.Size = new System.Drawing.Size(154, 20);
            this.txtemail.TabIndex = 22;
            // 
            // txtfirstname
            // 
            this.txtfirstname.Location = new System.Drawing.Point(54, 114);
            this.txtfirstname.Name = "txtfirstname";
            this.txtfirstname.Size = new System.Drawing.Size(154, 20);
            this.txtfirstname.TabIndex = 20;
            // 
            // Insertcustomer
            // 
            this.Insertcustomer.Location = new System.Drawing.Point(54, 227);
            this.Insertcustomer.Name = "Insertcustomer";
            this.Insertcustomer.Size = new System.Drawing.Size(337, 51);
            this.Insertcustomer.TabIndex = 19;
            this.Insertcustomer.Text = "save";
            this.Insertcustomer.UseVisualStyleBackColor = true;
            this.Insertcustomer.Click += new System.EventHandler(this.Insertcustomer_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Email";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // EmployeeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 318);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtphone);
            this.Controls.Add(this.txtlastname);
            this.Controls.Add(this.txtemail);
            this.Controls.Add(this.txtfirstname);
            this.Controls.Add(this.Insertcustomer);
            this.Name = "EmployeeInfo";
            this.Text = "EmployeeInfo";
            this.Load += new System.EventHandler(this.EmployeeInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtphone;
        private System.Windows.Forms.TextBox txtlastname;
        private System.Windows.Forms.TextBox txtemail;
        private System.Windows.Forms.TextBox txtfirstname;
        private System.Windows.Forms.Button Insertcustomer;
        private System.Windows.Forms.Label label4;
    }
}