using System.IO;

namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs
{
	public class ReturnFullPathClass
	{
		public static string ReturnFullPath(string relativePath) //There was a reason I did this... Know I dont remember the reason
		{
			string fullPath = Path.GetFullPath(relativePath);
			return fullPath;
		}
	}
}