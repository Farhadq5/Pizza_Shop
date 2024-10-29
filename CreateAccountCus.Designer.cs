namespace Pizza_Shop
{
    partial class CreateAccountCus
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtusernameCreate = new System.Windows.Forms.TextBox();
            this.txtpasswordCreate = new System.Windows.Forms.TextBox();
            this.txtconfiermpasswordCreate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 228);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Confierm Password";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtusernameCreate
            // 
            this.txtusernameCreate.Location = new System.Drawing.Point(122, 110);
            this.txtusernameCreate.Name = "txtusernameCreate";
            this.txtusernameCreate.Size = new System.Drawing.Size(216, 20);
            this.txtusernameCreate.TabIndex = 3;
            this.txtusernameCreate.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtpasswordCreate
            // 
            this.txtpasswordCreate.Location = new System.Drawing.Point(122, 171);
            this.txtpasswordCreate.Name = "txtpasswordCreate";
            this.txtpasswordCreate.Size = new System.Drawing.Size(216, 20);
            this.txtpasswordCreate.TabIndex = 4;
            // 
            // txtconfiermpasswordCreate
            // 
            this.txtconfiermpasswordCreate.Location = new System.Drawing.Point(122, 225);
            this.txtconfiermpasswordCreate.Name = "txtconfiermpasswordCreate";
            this.txtconfiermpasswordCreate.Size = new System.Drawing.Size(216, 20);
            this.txtconfiermpasswordCreate.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(69, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(217, 31);
            this.label4.TabIndex = 6;
            this.label4.Text = "Create Accouhnt";
            // 
            // btnCreateUser
            // 
            this.btnCreateUser.Location = new System.Drawing.Point(122, 275);
            this.btnCreateUser.Name = "btnCreateUser";
            this.btnCreateUser.Size = new System.Drawing.Size(216, 47);
            this.btnCreateUser.TabIndex = 7;
            this.btnCreateUser.Text = "create";
            this.btnCreateUser.UseVisualStyleBackColor = true;
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);
            // 
            // CreateAccountCus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 378);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtconfiermpasswordCreate);
            this.Controls.Add(this.txtpasswordCreate);
            this.Controls.Add(this.txtusernameCreate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CreateAccountCus";
            this.Text = "CreateAccountCus";
            this.Load += new System.EventHandler(this.CreateAccountCus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtusernameCreate;
        private System.Windows.Forms.TextBox txtpasswordCreate;
        private System.Windows.Forms.TextBox txtconfiermpasswordCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCreateUser;
    }
}