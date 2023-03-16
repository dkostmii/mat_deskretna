
namespace mat_deskretna
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.text = new YOUR_NAMESPACE_HERE.CueTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Go_button = new System.Windows.Forms.Button();
            this.panal_p_q_r = new System.Windows.Forms.Panel();
            this.q_label = new System.Windows.Forms.Label();
            this.r_label = new System.Windows.Forms.Label();
            this.p_label = new System.Windows.Forms.Label();
            this.down = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rezalt_panal = new System.Windows.Forms.Panel();
            this.rezalt_text = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panal_p_q_r.SuspendLayout();
            this.down.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.rezalt_panal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // text
            // 
            this.text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text.Cue = "Wprowadź tekst";
            this.text.Location = new System.Drawing.Point(300, 160);
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(400, 27);
            this.text.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(806, 160);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 200);
            this.panel1.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(12, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "wtedy i tylko wtedy";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(12, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "jeśli … to …";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(12, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "… i …";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "… lub …";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "nie";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Słownik :";
            // 
            // Go_button
            // 
            this.Go_button.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Go_button.Location = new System.Drawing.Point(450, 200);
            this.Go_button.Name = "Go_button";
            this.Go_button.Size = new System.Drawing.Size(100, 29);
            this.Go_button.TabIndex = 3;
            this.Go_button.Text = "Go";
            this.Go_button.UseVisualStyleBackColor = false;
            this.Go_button.Click += new System.EventHandler(this.Go_button_Click);
            // 
            // panal_p_q_r
            // 
            this.panal_p_q_r.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panal_p_q_r.Controls.Add(this.q_label);
            this.panal_p_q_r.Controls.Add(this.r_label);
            this.panal_p_q_r.Controls.Add(this.p_label);
            this.panal_p_q_r.Location = new System.Drawing.Point(300, 248);
            this.panal_p_q_r.Name = "panal_p_q_r";
            this.panal_p_q_r.Size = new System.Drawing.Size(400, 80);
            this.panal_p_q_r.TabIndex = 4;
            this.panal_p_q_r.Visible = false;
            // 
            // q_label
            // 
            this.q_label.AutoSize = true;
            this.q_label.Location = new System.Drawing.Point(14, 29);
            this.q_label.Name = "q_label";
            this.q_label.Size = new System.Drawing.Size(52, 20);
            this.q_label.TabIndex = 2;
            this.q_label.Text = "q = \" \"";
            // 
            // r_label
            // 
            this.r_label.AutoSize = true;
            this.r_label.Location = new System.Drawing.Point(14, 49);
            this.r_label.Name = "r_label";
            this.r_label.Size = new System.Drawing.Size(48, 20);
            this.r_label.TabIndex = 1;
            this.r_label.Text = "r = \" \"";
            // 
            // p_label
            // 
            this.p_label.AutoSize = true;
            this.p_label.Location = new System.Drawing.Point(14, 9);
            this.p_label.Name = "p_label";
            this.p_label.Size = new System.Drawing.Size(52, 20);
            this.p_label.TabIndex = 0;
            this.p_label.Text = "p = \" \"";
            // 
            // down
            // 
            this.down.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.down.Controls.Add(this.label7);
            this.down.Location = new System.Drawing.Point(0, 608);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(982, 45);
            this.down.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(420, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "© Rostyslav K. in 2023";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 167);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // rezalt_panal
            // 
            this.rezalt_panal.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rezalt_panal.Controls.Add(this.rezalt_text);
            this.rezalt_panal.Location = new System.Drawing.Point(375, 350);
            this.rezalt_panal.Name = "rezalt_panal";
            this.rezalt_panal.Size = new System.Drawing.Size(250, 40);
            this.rezalt_panal.TabIndex = 7;
            this.rezalt_panal.Visible = false;
            // 
            // rezalt_text
            // 
            this.rezalt_text.AutoSize = true;
            this.rezalt_text.Location = new System.Drawing.Point(3, 10);
            this.rezalt_text.Name = "rezalt_text";
            this.rezalt_text.Size = new System.Drawing.Size(39, 20);
            this.rezalt_text.TabIndex = 0;
            this.rezalt_text.Text = "w(...)";
            this.rezalt_text.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(300, 432);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(300, 188);
            this.dataGridView1.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.rezalt_panal);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.down);
            this.Controls.Add(this.panal_p_q_r);
            this.Controls.Add(this.Go_button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.text);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 700);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "Form1";
            this.Text = "Zut Matematyka dyskretna";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panal_p_q_r.ResumeLayout(false);
            this.panal_p_q_r.PerformLayout();
            this.down.ResumeLayout(false);
            this.down.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.rezalt_panal.ResumeLayout(false);
            this.rezalt_panal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private YOUR_NAMESPACE_HERE.CueTextBox text;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Go_button;
        private System.Windows.Forms.Panel panal_p_q_r;
        private System.Windows.Forms.Label q_label;
        private System.Windows.Forms.Label r_label;
        private System.Windows.Forms.Label p_label;
        private System.Windows.Forms.Panel down;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel rezalt_panal;
        private System.Windows.Forms.Label rezalt_text;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

