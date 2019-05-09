using System;
using System.Text;
using System.Collections.Generic;

public class Program
{
	private static uint VALLOC_NODE_LIMIT = 30u;

	private static uint VALLOC_TYPE_HEADER_PAGE = 4294966400u;

	private static uint VALLOC_TYPE_HEADER_POOL = 4294966657u;

	private static uint VALLOC_TYPE_HEADER_RESERVED = 4294967026u;

	private static uint[] VALLOC_TYPES = new uint[3]
	{
		VALLOC_TYPE_HEADER_PAGE,
		VALLOC_TYPE_HEADER_POOL,
		VALLOC_TYPE_HEADER_RESERVED
	};
	private static uint DeriveVallocType(uint r, uint c)
	{
		return ~(r * VALLOC_NODE_LIMIT + c);
	}
	public static void Main()
	{
		List<uint> revealedCells = new List<uint>();
		for (uint num = 0u; num < VALLOC_NODE_LIMIT; num++)
		{
			for (uint num2 = 0u; num2 < VALLOC_NODE_LIMIT; num2++)
			{
				uint r = num + 1;
				uint c = num2 + 1;
				uint a = ~(r * VALLOC_NODE_LIMIT + c);
				if (a == VALLOC_TYPE_HEADER_PAGE || a == 4294966657u || a==4294967026u)
				{
					revealedCells.Add((r-1) * VALLOC_NODE_LIMIT + (c-1));
					Console.WriteLine(r+"--"+c);
				}
			}
		}


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
		Console.WriteLine(Encoding.ASCII.GetString(array2));
	}
}
