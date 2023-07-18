using System.IO;
using FileUtilitiesXTUtil;

namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs
{
	public class FileContentsClass
	{
		public static string ReadFileContents(string filepath)
		{
			string contents;
			FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
			contents = fileUtilitiesXt.ReadFromFile(filepath);
			return contents;
		}
	}
}