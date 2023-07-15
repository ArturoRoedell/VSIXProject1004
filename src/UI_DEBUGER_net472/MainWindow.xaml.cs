using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BackEndSharedLibrary;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using Path = System.Windows.Shapes.Path;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;
using static BackEndSharedLibrary.VizFuncFileDataAndHelpers.VizFuncFileData;

namespace UI_DEBUGER_net472
{
	public partial class MainWindow : Window
	{
		#region Attributes
		private string VisFuncDataFilePath;
		private string ActiveCodeFilePath;
		private string ActiveCode;
		private string OutputWindowBuffer = "";
		#endregion

		public MainWindow()
		{
			ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			VisFuncDataFilePath = imageFileDirPath + @"\Test01A.json"; //This shold match the name of the codefile with _VizFuncSuffix
			InitializeComponent();
			CodeViewWindow.Text = ActiveCode;
		}

		async Task PrintOutputWindow(string appendThis = "")
		{
			OutputWindowBuffer = OutputWindowBuffer + "\n" + appendThis;
			outputTextBox.Text = OutputWindowBuffer;
			printl(appendThis);
		}

		private void TestImageWindow_Button_Click(object sender, RoutedEventArgs e)
		{
			TestImageWindow();
		}

		private async Task TestImageWindow()
		{

			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			string ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			VizFuncFileData Mc = new VizFuncFileData(ActiveCode);
			Mc.CodeFullFilePath = ActiveCodeFilePath;
			PascalSearch.RefineResultOptions refineOpt = new PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;
			await Mc.AllocatePascalSearchListAndGetUrls();
			await MainMethods.SubMethods.DownloadAllImagesFourEach(Mc);
			foreach (var newPascalSearch in Mc.PascalSearchList)
			{
				await PascalImagesToScreen(newPascalSearch, newPascalSearch.FinalWordCount);
			}

			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);


			/*imageFileDirPath = @"C:\Users\ARTURO 001\source\repos\VsixVisualizeMethodsAndClasses\TestFolder002\";
			VizFuncFileData Mc = new(ActiveCode);
			Mc.CodeFullFilePath = ActiveCodeFilePath;
			PascalSearch.RefineResultOptions refineOpt = new PascalSearch.RefineResultOptions();
			Mc._RefineResultForPascal = refineOpt;
			await Mc.AllocatePascalSearchListAndGetUrls();
			await MainMethods.SubMethods.DownloadAllImagesFourEach(Mc);
			foreach ( var newPascalSearch in Mc.PascalSearchList)
			{
				PrintOutputWindow();
				PrintOutputWindow("############################################");
				PrintOutputWindow("NAME: " + newPascalSearch.MethodClassName);
				PrintOutputWindow("############################################");
				PrintOutputWindow();
				await PascalImagesToScreen(newPascalSearch, newPascalSearch.FinalWordCount);
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					PrintOutputWindow(newPascalSearch.ImageResultsUrl[i,0]);
				}
			}
			foreach (var PairList in Mc.LineNumberPascalNamePairList)
			{
				PrintOutputWindow(PairList.pascalNames);
				PrintOutputWindow("Line_NUmber: #" + PairList.lineNumber.ToString());
				PrintOutputWindow("----------------------------------------------------------");
			}
			
			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);*/
		}

		private async Task PascalImagesToScreen(PascalSearch pascalSearch, int cap)
		{
			WideWindowUC01 UsiMy = new();
			PrintOutputWindow("cap = " + cap);
			int i = 0;
			for (int j = 0; j < cap; j++) //Proj Notes - This Loop is capped twice. This line contains the hard cap which should not exceed four
			{
				string ImagePath = ImageFileFullPath(pascalSearch.MultiWordSearchTerms[j]);
				string fullPath = ReturnFullPathClass.ReturnFullPath(ImagePath);
				if (i >= 4)
				{
					break;
				}
				i++;
				try
				{
					ImageWindowUC01 What = new();
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
	}
}