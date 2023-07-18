using System.Threading.Tasks;
using VizFuncTypes;
using System;

namespace DEBUGER_472
{
	internal class DEBUGER_472
	{
		public static async Task Main(string[] args)
		{
			Console.WriteLine("start");
			
			
			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			string ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			VizFuncFileData Mc = new VizFuncFileData(ActiveCode);
			Mc.CodeFullFilePath = ActiveCodeFilePath;
			VizFuncFileData.PascalSearch.RefineResultOptions refineOpt = new VizFuncFileData.PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;

			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				Console.WriteLine(newPascalSearch.SearchTermComplete);
				Console.WriteLine(newPascalSearch.FinalWordCount);
				Console.WriteLine(newPascalSearch.MethodClassName);
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					Console.WriteLine(newPascalSearch.MultiWordSearchTerms[i]);
					Console.WriteLine(newPascalSearch.ImageResultsUrlOneD[i]);
					Console.WriteLine(newPascalSearch.searchWordRefinedSearch[i]);
				}
				//await PascalImagesToScreen(newPascalSearch, newPascalSearch.FinalWordCount);
			}

			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
			
			
			Console.WriteLine("end");
			return;;
		}
	}
}