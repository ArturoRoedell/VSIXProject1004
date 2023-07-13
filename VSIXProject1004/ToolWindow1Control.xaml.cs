using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;


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
using System.Threading.Tasks;


namespace VSIXProject1004
{
	/// <summary>
	/// Interaction logic for ToolWindow1Control.
	/// </summary>
	public partial class ToolWindow1Control : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
		/// </summary>
		public ToolWindow1Control()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Handles click on the button by displaying a message box.
		/// </summary>
		/// <param name="sender">The event sender.</param>
		/// <param name="e">The event args.</param>
		[SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
		[SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//string testthis = FileContentsClass.ReadFileContents(ActiveCodeFilePath);;
			TestImageWindow();
			
			MessageBox.Show(
				string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
				"ToolWindow1");
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
			foreach ( var newPascalSearch in Mc.PascalSearchList)
			{
				printl();
				printl("############################################");
				printl("NAME: " + newPascalSearch.MethodClassName);
				printl("############################################");
				printl();
				for (int i = 0; i < newPascalSearch.FinalWordCount; i++)
				{
					printl(newPascalSearch.ImageResultsUrl[i,0]);
				}
			}
			foreach (var PairList in Mc.LineNumberPascalNamePairList)
			{
				printl(PairList.pascalNames);
				printl("Line_NUmber: #" + PairList.lineNumber.ToString());
				printl("----------------------------------------------------------");
			}
		
			MainMethods.SubMethods.SaveAllVizfuncDataToFile(Mc);
			
			
			
			// this.Image01.Source = new BitmapImage(
			// 	new Uri(fullPath, UriKind.RelativeOrAbsolute));
			
			
			
		}
		
	}
	

	
}