using System.IO;
using BackEndSharedLibrary;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using VizFuncTypes;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;
using System.Text.Json;
using System.Threading.Tasks;

namespace VizFuncMajorTasks;

public class VizFuncMajorTasksClass
{
	public static async Task Main(string[] args = null)
	{
		string CopiedFileName = "CopyOfActiveCode.txt";
		//string CopiedFilefullPath = @"..\..\..\..\..\Viz_func_Images\" + CopiedFileName;
		string CopiedFilefullPath = @"C:\Users\ARTURO 001\source\repos\VSIXProject1004\Viz_func_Images\CopyOfActiveCode.txt";
		//string CopiedFilefullPath = @"..\..\..\..\Viz_func_Images\" + CopiedFileName;
		//string CopiedFilefullPath = @"..\..\..\Viz_func_Images\" + CopiedFileName;
		
		printl("VizFuncMajorTasksClass: ");
		printl(Path.GetFullPath(CopiedFilefullPath) + " vizfuncMajorClass");
		

		
		FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
		string ActiveCode = fileUtilitiesXt.ReadFromFile(CopiedFilefullPath);
		VizFuncFileData Mc = new VizFuncFileData(ActiveCode);
		VizFuncFileData.PascalSearch.RefineResultOptions refineOpt = new VizFuncFileData.PascalSearch.RefineResultOptions();
		Mc._RefineResultForPascal = refineOpt;
		await Mc.AllocatePascalSearchListAndGetUrls();
		//Task.Run(async () => { await Mc.AllocatePascalSearchListAndGetUrls(); }).Wait();



		await MainMethods.SubMethods.DownloadAllImagesFourEach(Mc);
		//Task.Run(async () => { await MainMethods.SubMethods.DownloadAllImagesFourEach(Mc); }).Wait();
		
		
		MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
		
		#region Debug_Only_Test_VizFuncData
		foreach (var newPascalSearch in Mc.PascalSearchList)
		{
			printl();
			printl("===================================");
			printl(newPascalSearch.SearchTermComplete);
			printl(newPascalSearch.FinalWordCount);
			printl(newPascalSearch.MethodClassName);
			for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
			{
				printl("-----------------------------------------");
				printl(newPascalSearch.MultiWordSearchTerms[i]);
				printl(newPascalSearch.ImageResultsUrlOneD[i]);
				printl(newPascalSearch.ImageResultsUrl[i,0]);
				printl(newPascalSearch.searchWordRefinedSearch[i]);
			}
		}
		#endregion
	}
}