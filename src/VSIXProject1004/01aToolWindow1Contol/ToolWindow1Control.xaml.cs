using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BackEndSharedLibrary;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using static BackEndSharedLibrary.VizFuncFileDataAndHelpers.VizFuncFileData;
using FileUtilitiesXTUtil;

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
			string TestOnly  = VizFuncFileData.imageFileDirPath;
			TestOnly  = VizFuncFileData.imageFileDirPath;
			TestOnly = TestOnly;
			string getAbsolute = Path.GetFullPath(TestOnly);
			getAbsolute = Path.GetFullPath(TestOnly);
			getAbsolute = getAbsolute;
			string ActiveCodeFilePath = @"C:\Users\ARTURO 001\source\repos\001ScratchCode\001ReadCodeFile\TestCodeFile.cs";
			FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
			//string ActiveCode = fileUtilitiesXT.ReadFromFile(ActiveCodeFilePath);


			string ActiveCode = FileContentsClass.ReadFileContents(ActiveCodeFilePath);
			
			
			
			VizFuncFileData Mc = new VizFuncFileData(ActiveCode);
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

		private void PascalImagesToScreen(PascalSearch pascalSearch, int cap)
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
		

		
		
	}
}