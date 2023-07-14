using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using BackEndSharedLibrary;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using static BackEndSharedLibrary.VizFuncFileDataAndHelpers.VizFuncFileData;

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
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
			MyTestImage01();
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
			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
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