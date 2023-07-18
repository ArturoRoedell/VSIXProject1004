namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;

public class FileContentsClass
{
	public static string ReadFileContents(string filepath)
	{
		string contents;
		FileUtilitiesXT fileUtilitiesXt = new();
		contents = fileUtilitiesXt.ReadFromFile(filepath);
		return contents;
	}
}