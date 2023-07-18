using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using VizFuncTypes;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DEBUGER_472
{
	internal class DEBUGER_472
	{
		
		public static async Task Main(string[] args)
		{
			Console.WriteLine("START: DEBUGER472");
			MatchSingleWordsToLinenumber();
			//await TestMajorMethods();
			//await TestOnlyLoadAndDeserialize();
			Console.WriteLine("END: DEBUGER472");
		}

		public static async Task TestMajorMethods()
		{
			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			string ActiveCode;
			
			#region readfile
			if (!(File.Exists(ActiveCodeFilePath)))
			{
				ActiveCode = "";
				Console.WriteLine("File does not exist");
			}
			else
			{
				ActiveCode = "";
				var fileInfo = new FileInfo(ActiveCodeFilePath);
				if (fileInfo.Length == 0)
				{
					Console.WriteLine("Error Empty File");
				}
				else
				{
					ActiveCode = File.ReadAllText(ActiveCodeFilePath);
				}
			}
			#endregion
			
			string CopyFileName = "CopyOfActiveCode.txt";
			string copyToFilePath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\Viz_func_Images\" + CopyFileName;
			
			#region CopyFileWrite
			string dirpath = Path.GetDirectoryName(copyToFilePath);
			if (!(Directory.Exists(dirpath)))
			{
				Directory.CreateDirectory(dirpath);
			}
			
			if (!(File.Exists(copyToFilePath)))
			{
				FileStream fileStream = File.Create(copyToFilePath);
				fileStream.Close();
			}
			using (StreamWriter streamWriter = new StreamWriter(copyToFilePath))
			{
				streamWriter.Write(ActiveCode);
				streamWriter.Close();
			}
			#endregion
			
			string fullFilePath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\src\VizFuncMajorTasks\bin\Debug\net6.0\VizFuncMajorTasks.exe";
			string currentPath = Directory.GetCurrentDirectory();
			Console.WriteLine("Does exe exist in filepath: " + File.Exists(fullFilePath));
			Console.WriteLine(currentPath);
			Console.WriteLine(fullFilePath);
			System.Diagnostics.Process process = System.Diagnostics.Process.Start(fullFilePath);
			string jsonVizDataSimpleJsonPath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\Viz_func_Images\Test01A.json";
			VizFuncDataSimple Load_VFDS = new VizFuncDataSimple();
			List<VizFuncDataSimple> vizFuncFileDataList = new List<VizFuncDataSimple>();
			
			#region readfile
			string fileVisFuncData;
			if (!(File.Exists(ActiveCodeFilePath)))
			{
				fileVisFuncData = "";
				Console.WriteLine("File does not exist");
			}
			else
			{
				fileVisFuncData = "";
				var fileInfo = new FileInfo(ActiveCodeFilePath);
				if (fileInfo.Length == 0)
				{
					Console.WriteLine("Error Empty File");
				}
				else
				{
					fileVisFuncData = File.ReadAllText(ActiveCodeFilePath);
				}
			}
			#endregion
			
			Console.WriteLine("Serialized File string: " + File.Exists(jsonVizDataSimpleJsonPath));
			Console.WriteLine(fileVisFuncData);
			
			#region Deserialize
			List<VizFuncDataSimple> FileDataList = new List<VizFuncDataSimple>();
			try
			{
				vizFuncFileDataList = JsonConvert.DeserializeObject<List<VizFuncDataSimple>>(fileVisFuncData);
			}
			catch
			{
			}
			#endregion
			
			Console.WriteLine(vizFuncFileDataList.Count);
			int countvf = vizFuncFileDataList.Count;
			countvf = countvf;
			Load_VFDS = vizFuncFileDataList[0];
			
			#region Debug_Loaded_File_deserialized data_Debug_only
			Console.WriteLine("##############################");
			Console.WriteLine("START: From File VizFuncDataSimple");
			Console.WriteLine("##############################");
			Console.WriteLine(Load_VFDS.CodeFullFilePath);
			Console.WriteLine(Load_VFDS.FileNameCodeFile);
			foreach (var element in Load_VFDS.LineNumberPascalNamePairList)
			{
				Console.WriteLine("LineNumber : " + element.lineNumber);
				Console.WriteLine("PascalNames : " + element.pascalNames);
			}
			foreach (var element in Load_VFDS.MethodsAndClassesList)
			{
				Console.WriteLine("MethodsAndClasses : " + element);
			}
			foreach (var element in Load_VFDS.MethodsList)
			{
				Console.WriteLine("Methods : " + element);
			}
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine();
			Console.WriteLine("Pascal List total" + Load_VFDS.PascalSearchDataSimpleList.Count);
			foreach (var pascal in Load_VFDS.PascalSearchDataSimpleList)
			{
				Console.WriteLine("------------------------------------");
				Console.WriteLine("Name: "+ pascal.MethodClassName);
				Console.WriteLine("Word count"+pascal.FinalWordCount);
				
				foreach (var element in pascal.MultiWordSearchTerms)
				{
					Console.WriteLine("MultiWordSearchTerms: " + element);
				}
				
				foreach (var element in pascal.ImageResultsUrlOneD)
				{
					Console.WriteLine("ImageResultsUrlOneD: " + element);
				}
				
				foreach (var element in pascal.searchWordRefinedSearch)
				{
					Console.WriteLine("searchWordRefinedSearch: " + element);
				}
			}
			Console.WriteLine();
			Console.WriteLine("##############################");
			Console.WriteLine("END: From File VizFuncDataSimple");
			Console.WriteLine("##############################");
			Console.WriteLine();
			#endregion
		}

		public static async Task TestOnlyLoadAndDeserialize()
		{
			string jsonVizDataSimpleJsonPath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\Viz_func_Images\Test01A.json";
			VizFuncDataSimple Load_VFDS = new VizFuncDataSimple();
			#region readfile
			string jsonFileVisFuncData;
			if (!(File.Exists(jsonVizDataSimpleJsonPath)))
			{
				jsonFileVisFuncData = "";
				Console.WriteLine("File does not exist");
			}
			else
			{
				jsonFileVisFuncData = "";
				var fileInfo = new FileInfo(jsonVizDataSimpleJsonPath);
				if (fileInfo.Length == 0)
				{
					Console.WriteLine("Error Empty File");
				}
				else
				{
					jsonFileVisFuncData = File.ReadAllText(jsonVizDataSimpleJsonPath);
				}
			}
			#endregion
			List<VizFuncDataSimple> vizFuncFileDataList = new List<VizFuncDataSimple>();
			#region Deserialize
			List<VizFuncDataSimple> FileDataList = new List<VizFuncDataSimple>();
			try
			{
				vizFuncFileDataList = JsonConvert.DeserializeObject<List<VizFuncDataSimple>>(jsonFileVisFuncData);
			}
			catch
			{
			}
			#endregion
			Load_VFDS = vizFuncFileDataList[0];
			#region Debug_Loaded_File_deserialized data_Debug_only
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("START: From File VizFuncDataSimple");
			Console.WriteLine("##############################");
			Console.WriteLine(Load_VFDS.CodeFullFilePath);
			Console.WriteLine(Load_VFDS.FileNameCodeFile);
			foreach (var element in Load_VFDS.LineNumberPascalNamePairList)
			{
				Console.WriteLine("LineNumber : " + element.lineNumber);
				Console.WriteLine("PascalNames : " + element.pascalNames);
			}
			foreach (var element in Load_VFDS.MethodsAndClassesList)
			{
				Console.WriteLine("MethodsAndClasses : " + element);
			}
			foreach (var element in Load_VFDS.MethodsList)
			{
				Console.WriteLine("Methods : " + element);
			}
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine();
			Console.WriteLine("Pascal List total" + Load_VFDS.PascalSearchDataSimpleList.Count);
			foreach (var pascal in Load_VFDS.PascalSearchDataSimpleList)
			{
				Console.WriteLine("------------------------------------??????????????@@@@????????????");
				Console.WriteLine("Name: "+ pascal.MethodClassName);
				Console.WriteLine("Word count"+pascal.FinalWordCount);
				
				foreach (var element in pascal.MultiWordSearchTerms)
				{
					Console.WriteLine("MultiWordSearchTerms: " + element);
				}
				
				foreach (var element in pascal.ImageResultsUrlOneD)
				{
					Console.WriteLine("ImageResultsUrlOneD: " + element);
				}
				
				foreach (var element in pascal.searchWordRefinedSearch)
				{
					Console.WriteLine("searchWordRefinedSearch: " + element);
				}
			}
			Console.WriteLine();
			Console.WriteLine("##############################");
			Console.WriteLine("END: From File VizFuncDataSimple ????????????$$$ $$$$$#&&&# ");
			Console.WriteLine("##############################");
			Console.WriteLine();
			#endregion
			Console.WriteLine("DEbuger 472 end");
		}

		public static void MatchSingleWordsToLinenumber()
		{
			
			string jsonVizDataSimpleJsonPath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\Viz_func_Images\Test01A.json";
			VizFuncDataSimple savedVizFunc = new VizFuncDataSimple();
			
			#region readfile
			string jsonFileVisFuncData;
			if (!(File.Exists(jsonVizDataSimpleJsonPath)))
			{
				jsonFileVisFuncData = "";
				Console.WriteLine("File does not exist");
			}
			else
			{
				jsonFileVisFuncData = "";
				var fileInfo = new FileInfo(jsonVizDataSimpleJsonPath);
				if (fileInfo.Length == 0)
				{
					Console.WriteLine("Error Empty File");
				}
				else
				{
					jsonFileVisFuncData = File.ReadAllText(jsonVizDataSimpleJsonPath);
				}
			}
			#endregion
			
			List<VizFuncDataSimple> vizFuncFileDataList = new List<VizFuncDataSimple>();
			
			#region Deserialize
			List<VizFuncDataSimple> FileDataList = new List<VizFuncDataSimple>();
			try
			{
				vizFuncFileDataList = JsonConvert.DeserializeObject<List<VizFuncDataSimple>>(jsonFileVisFuncData);
			}
			catch
			{
			}
			#endregion
			
			savedVizFunc = vizFuncFileDataList[0];
			
			#region DrawwImagesInfileSequentialByLineNuber

			int LineMethodIndex = 0;
			foreach (var LineNumberPascalName in savedVizFunc.LineNumberPascalNamePairList)
			{
				
				
				/*Console.WriteLine("Pair Name:" + LineNumberPascalName.pascalNames);
				Console.WriteLine
				(
					"Total Pascal List: " +
					savedVizFunc.PascalSearchDataSimpleList.Count
				);*/
				
				int pascalIndex = 0;
				foreach (var pascalNameMC in savedVizFunc.PascalSearchDataSimpleList)
				{
					if ( pascalNameMC.MethodClassName == LineNumberPascalName.pascalNames)
					{
						Console.WriteLine("Line: #" + LineNumberPascalName.lineNumber);
						Console.WriteLine(LineNumberPascalName.pascalNames);
						foreach (var ImageWords in pascalNameMC.MultiWordSearchTerms)
						{
							Console.Write(" + " + ImageWords);
						}
						
						Console.WriteLine("\n----------------------------------------");
						/*Console.WriteLine
							(
								
								savedVizFunc.PascalSearchDataSimpleList[pascalIndex].MultiWordSearchTerms.Length
							);
						
						Console.WriteLine
							(
								savedVizFunc.PascalSearchDataSimpleList[pascalIndex].FinalWordCount
							);*/
						/*PascalImagesToScreen
						(
							savedVizFunc.PascalSearchDataSimpleList[pascalIndex].MultiWordSearchTerms,
							savedVizFunc.PascalSearchDataSimpleList[pascalIndex].FinalWordCount
						);*/
					}//FIX: WORK IN PROGRESS!!! needs line number spacing three lines eaquals one box image
					//FIX ....therefore add spaces with one third size empty spacers usre control object;
					pascalIndex++;
				}
				Console.WriteLine("=================================================");
			}
			#endregion
		}
		
		#region LegacyCode_TEMP_Archive_Then Earse
		/*public static async Task Main(string[] args)
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
		}*/
		#endregion
	}
}