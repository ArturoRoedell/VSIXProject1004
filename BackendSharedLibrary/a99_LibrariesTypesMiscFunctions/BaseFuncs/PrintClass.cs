using System;
using System.Diagnostics;

namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs
{
	public class PrintClass
	{
		public static void print(string aString = "")
		{
			Console.Write(aString);
			Debug.Write(aString);
		}

		public static void print(int aNumber)
		{
			Console.Write(aNumber);
			Debug.Write(aNumber);
		}

		public static void printl(string aString = "")
		{
			Console.WriteLine(aString);
			Debug.WriteLine(aString);
		}

		public static void printl(int aNumber)
		{
			Console.WriteLine(aNumber);
			Debug.WriteLine(aNumber);
		}

		public static void print(bool Boolean)
		{
			Console.Write(Boolean);
			Debug.Write(Boolean);
		}

		public static void printl(bool Boolean)
		{
			Console.WriteLine(Boolean);
			Debug.WriteLine(Boolean);
		}
	}
}

