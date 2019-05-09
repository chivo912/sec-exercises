namespace UltimateMinesweeper
{
	public class MineField
	{
		private bool[,] minesPresent;

		private bool[,] minesVisible;

		private bool[,] minesFlagged;

		private uint size;

		public bool[,] MinesPresent
		{
			get
			{
				return minesPresent;
			}
			set
			{
				minesPresent = value;
			}
		}

		public bool[,] GarbageCollect
		{
			get
			{
				return minesPresent;
			}
			set
			{
				minesPresent = value;
			}
		}

		public bool[,] MinesVisible
		{
			get
			{
				return minesVisible;
			}
			set
			{
				minesVisible = value;
			}
		}

		public bool[,] MinesFlagged
		{
			get
			{
				return minesFlagged;
			}
			set
			{
				minesFlagged = value;
			}
		}

		public int TotalMines
		{
			get
			{
				int num = 0;
				for (int i = 0; i < Size; i++)
				{
					for (int j = 0; j < Size; j++)
					{
						if (MinesPresent[i, j])
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		public int TotalUnrevealedEmptySquares
		{
			get
			{
				int num = 0;
				for (int i = 0; i < Size; i++)
				{
					for (int j = 0; j < Size; j++)
					{
						if (!MinesPresent[i, j] && !MinesVisible[i, j])
						{
							num++;
						}
					}
				}
				return num;
			}
		}

		public bool BombRevealed
		{
			get
			{
				for (int i = 0; i < Size; i++)
				{
					for (int j = 0; j < Size; j++)
					{
						if (MinesPresent[j, i] && MinesVisible[j, i])
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public uint Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public MineField(uint size)
		{
			Size = size;
			MinesPresent = new bool[size, size];
			MinesVisible = new bool[size, size];
			MinesFlagged = new bool[size, size];
		}
	}
}
