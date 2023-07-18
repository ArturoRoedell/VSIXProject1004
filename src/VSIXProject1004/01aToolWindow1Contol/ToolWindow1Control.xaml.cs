using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using VizFuncTypes;
using FileUtilitiesXTUtil;
using Newtonsoft.Json;

namespace VSIXProject1004
{
	public partial class ToolWindow1Control : UserControl
	{
		public ToolWindow1Control()
		{
			this.InitializeComponent();
			this.DataContext = this;
		}
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
		private async void button1_Click(object sender, RoutedEventArgs e)
		{
			//MyTestImage01();
			//MyTestImage01();

			//TestImageWindow();

			await TestImageWindow();

			//var t = Task.Run(() => TestImageWindow());
			//t.Wait();


			/*MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();*/
		}

		private async Task Middle()
		{
			try { await TestImageWindow(); }
			catch { }
		}
		
		private async Task TestImageWindow()
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
			
			#region Debug_Loaded_File_deserialized data_Debug_only
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("##############################");
			Console.WriteLine("START: From File VizFuncDataSimple");
			Console.WriteLine("##############################");
			Console.WriteLine(savedVizFunc.CodeFullFilePath);
			Console.WriteLine(savedVizFunc.FileNameCodeFile);
			foreach (var element in savedVizFunc.LineNumberPascalNamePairList)
			{
				Console.WriteLine("LineNumber : " + element.lineNumber);
				Console.WriteLine("PascalNames : " + element.pascalNames);
			}
			foreach (var element in savedVizFunc.MethodsAndClassesList)
			{
				Console.WriteLine("MethodsAndClasses : " + element);
			}
			foreach (var element in savedVizFunc.MethodsList)
			{
				Console.WriteLine("Methods : " + element);
			}
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine("-----------------==================---------------");
			Console.WriteLine();
			Console.WriteLine("Pascal List total" + savedVizFunc.PascalSearchDataSimpleList.Count);
			foreach (var pascal in savedVizFunc.PascalSearchDataSimpleList)
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
			
			#region DrawEveryImageInFile
			foreach (var pascal in savedVizFunc.PascalSearchDataSimpleList)
			{
				PascalImagesToScreen(pascal.MultiWordSearchTerms, pascal.FinalWordCount);
			}
			#endregion
			
			#region DrawwImagesInfileSequentialByLineNuber
			
			foreach (var LineNumberPascalName in savedVizFunc.LineNumberPascalNamePairList)
			{
				//string pascalPath = BackEndMethods.imageFileDirPath + @"\001_URLs\" + LineNumberPascalName.pascalNames + "_01.json";
				
				int pascalIndex = 0;
				foreach (var pascalNameMC in savedVizFunc.MethodsAndClassesList)
				{
					if ( pascalNameMC == LineNumberPascalName.pascalNames)
					{
						PascalImagesToScreen
							(
								savedVizFunc.PascalSearchDataSimpleList[pascalIndex].MultiWordSearchTerms,
								savedVizFunc.PascalSearchDataSimpleList[pascalIndex].FinalWordCount
							);
					}//FIX: WORK IN PROGRESS!!! needs line number spacing three lines eaquals one box image
					//FIX ....therefore add spaces with one third size empty spacers usre control object;
					pascalIndex++;
				}
				
				
				
				
				//savedVizFunc.MethodsAndClassesList;
				//BackEndMethods.imageFileDirPath
				//PascalImagesToScreen( ,);
			}
			#endregion

		}

		private void PascalImagesToScreen(string[] multiWordSearchTerms, int cap)
		{
			WideWindowUC01 UsiMy = new WideWindowUC01();
			int i = 0;
			for (int j = 0; j < cap; j++) //Proj Notes - This Loop is capped twice. This line contains the hard cap which should not exceed four
			{
				string ImagePath = BackEndMethods.ImageFileFullPath(multiWordSearchTerms[j]);
				string fullPath = BackEndMethods.ReturnFullPath(ImagePath);
				fullPath = fullPath; //DEBUG ONLY 
				if (i >= 4)
				{
					break;
				}
				i++;
				try
				{
					ImageWindowUC01 What = new ImageWindowUC01();
					What.Image01.Source = new BitmapImage(
						new Uri(fullPath, UriKind.RelativeOrAbsolute));
					UsiMy.ContainerHorizontal01.Children.Add(What);
				}
				catch (Exception e)
				{
				}
			}

			try
			{
				ScrollingViewer01.Children.Add(UsiMy);
			}
			catch (Exception e)
			{

			}
		}

		private async void MyTestImage01(string TestUrl = "")
		{
			WideWindowUC01 UsiMy = new WideWindowUC01();

			int i = 0;
			for (int j = 0; j < 5; j++) //Proj Notes - This Loop is capped twice. This line contains the hard cap which should not exceed four
			{
				string fullPath = @"C:\Users\ARTURO 001\source\repos\VSIXProject1004\TestingResources\Json_02.png";
				if (i >= 4)
				{
					break;
				}
				i++;
				try
				{
					ImageWindowUC01 What = new ImageWindowUC01();
					What.Image01.Source = new BitmapImage(
						new Uri(fullPath, UriKind.RelativeOrAbsolute));
					UsiMy.ContainerHorizontal01.Children.Add(What);
				}
				catch (Exception e)
				{
				}
			}

			try
			{
				ScrollingViewer01.Children.Add(UsiMy);
			}
			catch (Exception e)
			{

			}
		}

		#region LegacyCodeEraseLater
		/*private async Task TestImageWindowLegacy()
		{
			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
			//string ActiveCode = fileUtilitiesXT.ReadFromFile(ActiveCodeFilePath);


			string ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			
			
			VizFuncDataSimple Mc = new VizFuncDataSimple(ActiveCode);
			Mc.CodeFullFilePath = ActiveCodeFilePath;
			PascalSearch.RefineResultOptions refineOpt = new PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;
			
			Mc.AllocatePascalSearchList();

			
			
			int DEbug02 = Mc.MethodsAndClassesList.Count;
			DEbug02 = DEbug02;
			foreach ( var newPascalSearch in Mc.PascalSearchList)
			{
				string Debug01 = newPascalSearch.MethodClassName;
				Debug01 = Debug01;
				PascalImagesToScreen(newPascalSearch, newPascalSearch.FinalWordCount);
			}
			//MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
		}

		private void PascalImagesToScreenLegacy(PascalSearch pascalSearch, int cap)
		{
			WideWindowUC01 UsiMy = new WideWindowUC01();
			int i = 0;
			for (int j = 0; j < cap; j++) //Proj Notes - This Loop is capped twice. This line contains the hard cap which should not exceed four
			{
				string ImagePath = ImageFileFullPath(pascalSearch.MultiWordSearchTerms[j]);
				string fullPath = ReturnFullPathClass.ReturnFullPath(ImagePath);
				fullPath = fullPath; //DEBUG ONLY 
				if (i >= 4)
				{
					break;
				}
				i++;
				try
				{
					ImageWindowUC01 What = new ImageWindowUC01();
					What.Image01.Source = new BitmapImage(
						new Uri(fullPath, UriKind.RelativeOrAbsolute));
					UsiMy.ContainerHorizontal01.Children.Add(What);
				}
				catch (Exception e)
				{
				}
			}

			try
			{
				ScrollingViewer01.Children.Add(UsiMy);
			}
			catch (Exception e)
			{

			}
		}*/
		#endregion
	}
}