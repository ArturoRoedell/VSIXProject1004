using System.Threading.Tasks;
using BackEndSharedLibrary;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace DEBUGER_472
{
	internal class DEBUGER_472
	{
		public static async Task Main(string[] args)
		{
			printl("start");
			
			
			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			string ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			VizFuncFileData Mc = new VizFuncFileData(ActiveCode);
			Mc.CodeFullFilePath = ActiveCodeFilePath;
			VizFuncFileData.PascalSearch.RefineResultOptions refineOpt = new VizFuncFileData.PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;
			await Mc.AllocatePascalSearchListAndGetUrls();
			await MainMethods.SubMethods.DownloadAllImagesFourEach(Mc);
			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				printl(newPascalSearch.SearchTermComplete);
				printl(newPascalSearch.FinalWordCount);
				printl(newPascalSearch.MethodClassName);
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					printl(newPascalSearch.MultiWordSearchTerms[i]);
					printl(newPascalSearch.ImageResultsUrlOneD[i]);
					printl(newPascalSearch.searchWordRefinedSearch[i]);
				}
				//await PascalImagesToScreen(newPascalSearch, newPascalSearch.FinalWordCount);
			}

			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
			
			
			printl("end");
			return;;
		}
	}
}