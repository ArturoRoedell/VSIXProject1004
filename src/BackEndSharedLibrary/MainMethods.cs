using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using VizFuncTypes;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace BackEndSharedLibrary;

public class MainMethods
{
	public static async Task ReadCodefileDownloadSaveVizFuncData(string activeCodeFilePath)
	{
		string activeCode = FileContentsClass.ReadFileContents(activeCodeFilePath);
		VizFuncFileData Mc = new(activeCode);
		VizFuncFileData.PascalSearch.RefineResultOptions refineOpt = new VizFuncFileData.PascalSearch.RefineResultOptions();
		Mc._RefineResultForPascal = refineOpt;
		await Mc.AllocatePascalSearchListAndGetUrls();
		#region DEBUG ONLY Show_urls
		foreach (var newPascalSearch in Mc.PascalSearchList)
		{
			for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
			{
				printl(newPascalSearch.ImageResultsUrlOneD[i]);
			}
		}
		#endregion// For Testing Porpuses only
		await SubMethods.DownloadAllImagesFourEach(Mc);
		SubMethods.SaveAllVizfuncDataToFile(Mc);
	}
	
	public class SubMethods
	{
		public static VizFuncDataSimple LoadAndDeserialize(string fileContentPath)
		{
			/*FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
			string fileVisFuncData = fileUtilitiesXT.ReadFromFile(fileContentPath);
			List<VizFuncFileData> vizFuncFileDataList = new List<VizFuncFileData>();
			//------------Work Below ---------------------
			vizFuncFileDataList = fileUtilitiesXT.DeserializeJsonStringReturnList<VizFuncFileData>(fileVisFuncData);
			VizFuncFileData vizFuncFileData = vizFuncFileDataList[0];
			//--------------Work Above ----------------------
			return vizFuncFileData;*/
			FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
			string fileVisFuncData = fileUtilitiesXT.ReadFromFile(fileContentPath);
			List<VizFuncDataSimple> vizFuncFileDataList = new List<VizFuncDataSimple>();
			vizFuncFileDataList = fileUtilitiesXT.DeserializeJsonStringReturnList<VizFuncDataSimple>(fileVisFuncData);
			VizFuncDataSimple vizFuncFileData = vizFuncFileDataList[0];
			return vizFuncFileData;
		}//BUG; Not Working; Think it is the json file and Deserialization; CurrentLy not being Called
		
		public static void SaveAllVizfuncDataToFile(VizFuncFileData Mc)
		{
			//List<VizFuncFileData> vizFuncFileDataList = new List<VizFuncFileData>();
			List<VizFuncDataSimple> vizFuncFileDataSimpleList = new List<VizFuncDataSimple>();

			#region Create_Method_To_ReturnVizfuncDataSimple_from_VizfuncData
			VizFuncDataSimple vizFuncDataSimple = new VizFuncDataSimple();
			//vizFuncDataSimple.ActiveCode = Mc.ActiveCode;
			vizFuncDataSimple.MethodsList = Mc.MethodsList;
			vizFuncDataSimple.ClassesList = Mc.ClassesList;
			vizFuncDataSimple.MethodsAndClassesList = Mc.MethodsAndClassesList;
			vizFuncDataSimple.LineNumberPascalNamePairList = Mc.LineNumberPascalNamePairList;
			vizFuncDataSimple.PascalSearchDataSimpleList = Mc.PascalSearchDataSimpleList;
			vizFuncDataSimple.CodeFullFilePath = Mc.CodeFullFilePath;
			vizFuncDataSimple.FileNameCodeFile = Mc.FileNameCodeFile;
			#endregion
			//vizFuncFileDataList.Add(Mc);
			
			vizFuncFileDataSimpleList.Add(vizFuncDataSimple);
			
			#region Debug_printl
			foreach (var pascal in Mc.PascalSearchList)
			{
				foreach (var element in pascal.MultiWordSearchTerms)
				{
					printl("This is New: " + element);
				}
			}
			#endregion
			string fullFilePath = VizFuncFileData.imageFileDirPath + @"\Test01A.json";
			SerializeSaveFileClass.SerializeSaveFile<VizFuncDataSimple>(vizFuncFileDataSimpleList,fullFilePath);
		}
		
		public static async Task DownloadAllImagesFourEach(VizFuncFileData Mc)
		{
			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					/*await VizFuncFileData.DownloadSingleImage(newPascalSearch.ImageResultsUrlOneD[i]
						, newPascalSearch.MultiWordSearchTerms[i]);*/
					for (int j = 0; j < 4; j++)
					{
						string name = newPascalSearch.MultiWordSearchTerms[i];
						FileUtilitiesXT fileUtilitiesXt = new FileUtilitiesXT();
						string path = VizFuncFileData.imageFileDirPath + @"\001_URLs\" + name + "_01.json";
						string urlslistJson =  fileUtilitiesXt.ReadFromFile(path);
						List<string[]> urlListArrLST = new List<string[]>();
						urlListArrLST = fileUtilitiesXt.DeserializeJsonStringReturnList<string[]>(urlslistJson);
						string[] urlListArr;// = new string[100];
						urlListArr = urlListArrLST[0];
						await VizFuncFileData.DownloadSingleImage(urlListArr[j]
							,newPascalSearch.MultiWordSearchTerms[i] ,j);
					}
				}
			}
		}
		
		public static async Task DownloadAllImages(VizFuncFileData Mc)
		{
			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					await VizFuncFileData.DownloadSingleImage(newPascalSearch.ImageResultsUrlOneD[i]
						, newPascalSearch.MultiWordSearchTerms[i]);
				}
			}
		}
	}
}