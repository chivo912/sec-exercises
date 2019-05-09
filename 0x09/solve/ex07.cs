private void check()
{
	if (Array.IndexOf(state, value: true) >= 0)
	{
		return;
	}
	MessageBox.Show("Congratulations!");
	int[] array = new int[8]
	{1,	7,	16,	11,	14,	19,	20,	18};
	bool flag = true;
	for (int i = 0; i < 8; i++)
	{
		if (hist[i] != array[i])
		{
			flag = false;
		}
	}
	if (flag)
	{
		int[] array2 = new int[33]{85,	111,	117,	43,	104,	127,	117,	117,	33,	110,	99,	43,	72,	95,	85,	85,	94,	66,	120,	98,	79,	117,	68,	83,	64,	94,	39,	65,	73,	32,	65,	72,	51};
		string text = "";
		for (int j = 0; j < array2.Length; j++)
		{
			text += (char)(array2[j] ^ array[j % array.Length]);
		}
		MessageBox.Show(text);
	}
}


// Phần giải :
using System;

public class Program
{
	public static void Main()
	{
		int[] array = new int[8]{1,	7,	16,	11,	14,	19,	20,	18};
		int[] array2 = new int[33]{85,	111,	117,	43,	104,	127,	117,	117,	33,	110,	99,	43,	72,	95,	85,	85,	94,	66,	120,	98,	79,	117,	68,	83,	64,	94,	39,	65,	73,	32,	65,	72,	51};
		string text = "";
		for (int j = 0; j < array2.Length; j++)
		{
			text += (char)(array2[j] ^ array[j % array.Length]);
		}
		Console.WriteLine(text);
	}
}

// Kết quả : The flag is FLAG_EhiAfPAAY7JG3UZ2
