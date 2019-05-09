using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace UltimateMinesweeper
{
	public class MainForm : Form
	{
		private MineField mineField;

		private Stopwatch stopwatch;

		private int flagsRemaining;

		private static uint VALLOC_NODE_LIMIT = 30u;

		private static uint VALLOC_TYPE_HEADER_PAGE = 4294966400u;

		private static uint VALLOC_TYPE_HEADER_POOL = 4294966657u;

		private static uint VALLOC_TYPE_HEADER_RESERVED = 4294967026u;

		private uint[] VALLOC_TYPES = new uint[3]
		{
			VALLOC_TYPE_HEADER_PAGE,
			VALLOC_TYPE_HEADER_POOL,
			VALLOC_TYPE_HEADER_RESERVED
		};

		private List<uint> RevealedCells;

		private IContainer components;

		private MineFieldControl mineFieldControl;

		private Label label1;

		private TextBox stopwatchTextBox;

		private System.Windows.Forms.Timer stopwatchTimer;

		private Label label2;

		private Label label4;

		private TextBox flagsRemainingTextBox;

		private Label label3;

		internal MineField MineField
		{
			get
			{
				return mineField;
			}
			set
			{
				mineField = value;
			}
		}

		public int FlagsRemaining
		{
			get
			{
				return flagsRemaining;
			}
			set
			{
				flagsRemaining = value;
				flagsRemainingTextBox.Text = flagsRemaining.ToString();
			}
		}

		public MainForm()
		{
			InitializeComponent();
			MineField = new MineField(VALLOC_NODE_LIMIT);
			AllocateMemory(MineField);
			mineFieldControl.DataSource = MineField;
			mineFieldControl.SquareRevealed += SquareRevealedCallback;
			mineFieldControl.FirstClick += FirstClickCallback;
			stopwatch = new Stopwatch();
			FlagsRemaining = MineField.TotalMines;
			mineFieldControl.MineFlagged += MineFlaggedCallback;
			RevealedCells = new List<uint>();
		}

		private void AllocateMemory(MineField mf)
		{
			for (uint num = 0u; num < VALLOC_NODE_LIMIT; num++)
			{
				for (uint num2 = 0u; num2 < VALLOC_NODE_LIMIT; num2++)
				{
					bool flag = true;
					uint r = num + 1;
					uint c = num2 + 1;
					if (VALLOC_TYPES.Contains(DeriveVallocType(r, c)))
					{
						flag = false;
					}
					mf.GarbageCollect[num2, num] = flag;
				}
			}
		}

		private uint DeriveVallocType(uint r, uint c)
		{
			return ~(r * VALLOC_NODE_LIMIT + c);
		}

		private void SquareRevealedCallback(uint column, uint row)
		{
			if (MineField.BombRevealed)
			{
				stopwatch.Stop();
				Application.DoEvents();
				Thread.Sleep(1000);
				new FailurePopup().ShowDialog();
				Application.Exit();
			}
			RevealedCells.Add(row * VALLOC_NODE_LIMIT + column);
			if (MineField.TotalUnrevealedEmptySquares == 0)
			{
				stopwatch.Stop();
				Application.DoEvents();
				Thread.Sleep(1000);
				new SuccessPopup(GetKey(RevealedCells)).ShowDialog();
				Application.Exit();
			}
		}

		private string GetKey(List<uint> revealedCells)
		{
			revealedCells.Sort();
			Random random = new Random(Convert.ToInt32((revealedCells[0] << 20) | (revealedCells[1] << 10) | revealedCells[2]));
			byte[] array = new byte[32];
			byte[] array2 = new byte[32]
			{
				245,
				75,
				65,
				142,
				68,
				71,
				100,
				185,
				74,
				127,
				62,
				130,
				231,
				129,
				254,
				243,
				28,
				58,
				103,
				179,
				60,
				91,
				195,
				215,
				102,
				145,
				154,
				27,
				57,
				231,
				241,
				86
			};
			random.NextBytes(array);
			for (uint num = 0u; num < array2.Length; num++)
			{
				array2[num] ^= array[num];
			}
			return Encoding.ASCII.GetString(array2);
		}

		private void FirstClickCallback()
		{
			stopwatch.Start();
			stopwatchTimer.Start();
		}

		private void MineFlaggedCallback(bool added)
		{
			if (added)
			{
				FlagsRemaining--;
			}
			else
			{
				FlagsRemaining++;
			}
		}

		private void stopwatchTimer_Tick(object sender, EventArgs e)
		{
			TimeSpan elapsed = stopwatch.Elapsed;
			int num = (elapsed.Hours * 60 + elapsed.Minutes) * 60 + elapsed.Seconds;
			string text = $"{num}.{elapsed.Milliseconds:000}";
			stopwatchTextBox.Text = text;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltimateMinesweeper.MainForm));
			label1 = new System.Windows.Forms.Label();
			stopwatchTextBox = new System.Windows.Forms.TextBox();
			stopwatchTimer = new System.Windows.Forms.Timer(components);
			label2 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			flagsRemainingTextBox = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			mineFieldControl = new UltimateMinesweeper.MineFieldControl();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 80);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(567, 20);
			label1.TabIndex = 1;
			label1.Text = "Instructions: Left Click to reveal a square, Right Click to flag as Dangerous";
			stopwatchTextBox.BackColor = System.Drawing.Color.Black;
			stopwatchTextBox.Font = new System.Drawing.Font("Courier New", 16.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			stopwatchTextBox.ForeColor = System.Drawing.Color.Red;
			stopwatchTextBox.Location = new System.Drawing.Point(310, 39);
			stopwatchTextBox.Name = "stopwatchTextBox";
			stopwatchTextBox.ReadOnly = true;
			stopwatchTextBox.Size = new System.Drawing.Size(302, 38);
			stopwatchTextBox.TabIndex = 2;
			stopwatchTextBox.Text = "--";
			stopwatchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			stopwatchTimer.Interval = 10;
			stopwatchTimer.Tick += new System.EventHandler(stopwatchTimer_Tick);
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(306, 16);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(162, 20);
			label2.TabIndex = 3;
			label2.Text = "Championship Time:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(110, 108);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(273, 20);
			label4.TabIndex = 6;
			label4.Text = "Reveal all Non-Mine squares to win";
			flagsRemainingTextBox.BackColor = System.Drawing.Color.Black;
			flagsRemainingTextBox.Font = new System.Drawing.Font("Courier New", 16.2f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			flagsRemainingTextBox.ForeColor = System.Drawing.Color.Red;
			flagsRemainingTextBox.Location = new System.Drawing.Point(16, 39);
			flagsRemainingTextBox.Name = "flagsRemainingTextBox";
			flagsRemainingTextBox.ReadOnly = true;
			flagsRemainingTextBox.Size = new System.Drawing.Size(139, 38);
			flagsRemainingTextBox.TabIndex = 4;
			flagsRemainingTextBox.Text = "-";
			flagsRemainingTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 9);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(143, 20);
			label3.TabIndex = 5;
			label3.Text = "Mines Remaining:";
			mineFieldControl.BackColor = System.Drawing.SystemColors.ControlDark;
			mineFieldControl.CellDepth = 4;
			mineFieldControl.CellSize = 20;
			mineFieldControl.DataSource = null;
			mineFieldControl.Location = new System.Drawing.Point(12, 131);
			mineFieldControl.Name = "mineFieldControl";
			mineFieldControl.Size = new System.Drawing.Size(600, 600);
			mineFieldControl.TabIndex = 0;
			mineFieldControl.Tag = "";
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			BackColor = System.Drawing.SystemColors.ActiveCaption;
			base.ClientSize = new System.Drawing.Size(628, 749);
			base.Controls.Add(label3);
			base.Controls.Add(flagsRemainingTextBox);
			base.Controls.Add(label4);
			base.Controls.Add(label2);
			base.Controls.Add(stopwatchTextBox);
			base.Controls.Add(label1);
			base.Controls.Add(mineFieldControl);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "MainForm";
			Text = "Ultimate Minesweeper - [Championship Edition]";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
