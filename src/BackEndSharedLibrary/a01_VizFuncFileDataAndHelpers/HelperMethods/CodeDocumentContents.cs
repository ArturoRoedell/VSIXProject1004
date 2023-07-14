using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods
{
	public class CodeDocumentContentsClass
	{
		public static string CodeDocumentContents(string filepath)
		{
			string contents = FileContentsClass.ReadFileContents(filepath);
			return contents;
		} // Read a document of code and return contents to string
	}
}

