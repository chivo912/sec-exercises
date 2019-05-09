using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UltimateMinesweeper
{
	public class MineFieldControl : UserControl
	{
		public delegate void FirstClickHandler();

		public delegate void SquareRevealedHandler(uint column, uint row);

		public delegate void MineFlaggedHandler(bool addedFlag);

		private bool firstClickHappened;

		private MineField dataSource;

		private int cellDepth;

		private int cellSize;

		private IContainer components;

		private ImageList imageList;

		private PictureBox pictureBox1;

		public MineField DataSource
		{
			get
			{
				return dataSource;
			}
			set
			{
				dataSource = value;
			}
		}

		[Description("3D depth (in pixels) of each cell")]
		[Category("Layout")]
		public int CellDepth
		{
			get
			{
				return cellDepth;
			}
			set
			{
				cellDepth = value;
			}
		}

		[Description("Size (in pixels) of each cell")]
		[Category("Layout")]
		public int CellSize
		{
			get
			{
				return cellSize;
			}
			set
			{
				cellSize = value;
			}
		}

		public event FirstClickHandler FirstClick;

		public event SquareRevealedHandler SquareRevealed;

		public event MineFlaggedHandler MineFlagged;

		public MineFieldControl()
		{
			InitializeComponent();
		}

		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
		}

		private void MineFieldControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.DrawImageUnscaled(pictureBox1.Image, 0, 0, 600, 600);
			for (int i = 0; i < DataSource.Size; i++)
			{
				for (int j = 0; j < DataSource.Size; j++)
				{
					if (DataSource.MinesVisible[j, i])
					{
						if (DataSource.MinesPresent[j, i])
						{
							graphics.DrawImageUnscaled(imageList.Images[1], j * cellSize, i * cellSize);
						}
						else
						{
							graphics.DrawImageUnscaled(imageList.Images[4], j * cellSize, i * cellSize);
						}
					}
					else if (DataSource.MinesFlagged[j, i])
					{
						graphics.DrawImageUnscaled(imageList.Images[3], j * cellSize, i * cellSize);
					}
				}
			}
		}

		private void MineFieldControl_MouseClick(object sender, MouseEventArgs e)
		{
			uint num = Convert.ToUInt32(e.Y / CellSize);
			uint num2 = Convert.ToUInt32(e.X / CellSize);
			if (e.Button == MouseButtons.Right)
			{
				bool flag = DataSource.MinesFlagged[num2, num];
				DataSource.MinesFlagged[num2, num] = !flag;
				Invalidate();
				if (this.MineFlagged != null)
				{
					this.MineFlagged((!flag) ? true : false);
				}
			}
			else if (e.Button == MouseButtons.Left)
			{
				bool num3 = DataSource.MinesVisible[num2, num];
				DataSource.MinesVisible[num2, num] = true;
				Invalidate();
				if (!num3 && this.SquareRevealed != null)
				{
					this.SquareRevealed(num2, num);
				}
			}
			if (!firstClickHappened && this.FirstClick != null && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
			{
				firstClickHappened = true;
				this.FirstClick();
			}
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltimateMinesweeper.MineFieldControl));
			imageList = new System.Windows.Forms.ImageList(components);
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "BlankField.bmp");
			imageList.Images.SetKeyName(1, "bomb.bmp");
			imageList.Images.SetKeyName(2, "hidden.bmp");
			imageList.Images.SetKeyName(3, "flag.bmp");
			imageList.Images.SetKeyName(4, "safe.bmp");
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(0, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(600, 600);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			pictureBox1.Visible = false;
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.SystemColors.ControlDark;
			base.Controls.Add(pictureBox1);
			base.Name = "MineFieldControl";
			base.Paint += new System.Windows.Forms.PaintEventHandler(MineFieldControl_Paint);
			base.MouseClick += new System.Windows.Forms.MouseEventHandler(MineFieldControl_MouseClick);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
