using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltimateMinesweeper
{
	public class SuccessPopup : Form
	{
		private IContainer components;

		private PictureBox pictureBox1;

		private TextBox textBox1;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		public SuccessPopup(string key)
		{
			InitializeComponent();
			textBox1.Text = key;
		}

		private void SuccessPopup_Load(object sender, EventArgs e)
		{
			Task.Delay(1000);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltimateMinesweeper.SuccessPopup));
			pictureBox1 = new System.Windows.Forms.PictureBox();
			textBox1 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(1, 2);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(456, 533);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			textBox1.Font = new System.Drawing.Font("Courier New", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBox1.Location = new System.Drawing.Point(463, 300);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new System.Drawing.Size(344, 30);
			textBox1.TabIndex = 1;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Comic Sans MS", 19.8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
			label1.Location = new System.Drawing.Point(511, 67);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(269, 45);
			label1.TabIndex = 2;
			label1.Text = "Congratulations!";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Comic Sans MS", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(547, 131);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(185, 29);
			label2.TabIndex = 3;
			label2.Text = "You have won the";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Comic Sans MS", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.ForeColor = System.Drawing.Color.Blue;
			label3.Location = new System.Drawing.Point(465, 170);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(364, 29);
			label3.TabIndex = 4;
			label3.Text = "Ultimate Minesweeper Championship";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Comic Sans MS", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(540, 199);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(192, 29);
			label4.TabIndex = 5;
			label4.Text = "and nobody cares.";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Comic Sans MS", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(514, 268);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(203, 29);
			label5.TabIndex = 6;
			label5.Text = "Here is your prize:";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Comic Sans MS", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(375, 494);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(454, 29);
			label6.TabIndex = 7;
			label6.Text = "And remember kids, Winners don't do drugs";
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.Color.White;
			base.ClientSize = new System.Drawing.Size(828, 532);
			base.Controls.Add(label6);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBox1);
			base.Controls.Add(pictureBox1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SuccessPopup";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Winner's Circle";
			base.Load += new System.EventHandler(SuccessPopup_Load);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
