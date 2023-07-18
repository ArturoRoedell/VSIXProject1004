using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using FileUtilitiesXTUtil;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace BackEndSharedLibrary
{
	public class MainMethods
	{
		public static void ReadCodefileSaveVizFuncData(string activeCodeFilePath)
		{
			string activeCode = FileContentsClass.ReadFileContents(activeCodeFilePath);
			VizFuncFileData Mc = new VizFuncFileData(activeCode);
			VizFuncFileData.PascalSearch.RefineResultOptions refineOpt = new VizFuncFileData.PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;
			#region DEBUG ONLY Show_urls
			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					printl(newPascalSearch.ImageResultsUrl[i, 0]);
				}
			}
			#endregion // For Testing Porpuses only
			SubMethods.SaveAllVizfuncDataToFile(Mc);
		}

		public class SubMethods
		{
			public static VizFuncFileData LoadAndDeserialize(string fileContentPath)
			{
				FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
				string fileVisFuncData = fileUtilitiesXT.ReadFromFile(fileContentPath);
				List<VizFuncFileData> vizFuncFileDataList = new List<VizFuncFileData>();
				vizFuncFileDataList = fileUtilitiesXT.DeserializeJsonStringReturnList<VizFuncFileData>(fileVisFuncData);
				VizFuncFileData vizFuncFileData = vizFuncFileDataList[0];
				return vizFuncFileData;
				//FIX: Change to Newtonsoft json to deserialize
			} //BUG; Not Working; Think it is the json file and Deserialization; CurrentLy not being Called

			public static void SaveAllVizfuncDataToFile(VizFuncFileData Mc)
			{
				List<VizFuncFileData> vizFuncFileDataList = new List<VizFuncFileData>();
				vizFuncFileDataList.Add(Mc);
				foreach (var pascal in Mc.PascalSearchList)
				{
					foreach (var element in pascal.MultiWordSearchTerms)
					{
						printl("This is New: " + element);
					}
				}
				string fullFilePath = VizFuncFileData.imageFileDirPath + @"\Test01A.json";
				SerializeSaveFileClass.SerializeSaveFile<VizFuncFileData>(vizFuncFileDataList, fullFilePath);
			}
		}
	}
}