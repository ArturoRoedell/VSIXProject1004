namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;

public static class PopulateArrays
{
	public static void Populate2dArray<T>(this T[,] arr, T value )
	{
		for ( int i = 0; i < arr.GetLength(0);i++ ) 
		{
			for (int j = 0; j < arr.GetLength(1); j++)
			{
				arr[i, j] = value;
			}
		}
	}
}