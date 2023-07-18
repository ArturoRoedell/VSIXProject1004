using System;
using System.Collections;
using System.Collections.Generic;
using BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods;
using static BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods.RegexMethods;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using BackEndSharedLibrary.Types;
//using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Imaging;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers
{
	public partial class VizFuncFileData
	{
		public VizFuncFileData(string activeCode)
		{
			this.ActiveCode = activeCode;
			this.FindPascalClasses();
		}
		
		void FindPascalClasses()
		{
			string ClassPattern = @"(?<=(struct|class)\s*)\b[a-zA-Z_0-9]+\b";
			this.ClassesList = FindPatternRegex(this.ActiveCode, ClassPattern);
			foreach (var VARIABLE in this.ClassesList) //Debug
			{
				printl(VARIABLE); //Debug
			}
		}
		
		public void AllocatePascalSearchList()
		{
			PascalSearch newPascalSearch = new PascalSearch(_RefineResultForPascal);
			PascalSearch PascalSearchWithUrls = new PascalSearch(_RefineResultForPascal);
			List<PascalSearch> TransferPascal = new List<PascalSearch>();
			foreach (var methodOrClass in this.MethodsAndClassesList)
			{//BUG fix IS BELOW; I believe the interaction vsix and debug_await
				//BUG: The Code Fails at any debug_await. Needs Fixing whole document
				newPascalSearch = PascalSearch.asyncReturnPascalNameSearch(methodOrClass);
				printl("Method_Or_Class_Name" + methodOrClass);
			}
		}
		
		public static string ImageFileFullPath(string ImageName, int imageIndex = 1)
		{
			string fullFilepath = imageFileDirPath + ImageName + "_0" + imageIndex + ".png";
			return fullFilepath;
		}

		internal List<LineNumberPascalNamePair> SortLineNumberNamesList()
		{
			int[] lineNumbers = new int[this.LineNumberKeyword.Count];
			string[] namesPascal = new string[this.LineNumberKeyword.Count];
			List<LineNumberPascalNamePair> numberPascalNamesList = new List<LineNumberPascalNamePair>();
			int i = 0;
			foreach (DictionaryEntry element in this.LineNumberKeyword)
			{
				try
				{
					int trydis = Convert.ToInt32(element.Key);
					printl(trydis);
					lineNumbers[i] = trydis;
				}
				catch
				{
				}
				namesPascal[i] = element.Value.ToString();
				LineNumberPascalNamePair numberPascalNamesListTransfer = new LineNumberPascalNamePair();
				numberPascalNamesListTransfer.lineNumber = Convert.ToInt32(element.Key);
				numberPascalNamesListTransfer.pascalNames = element.Value.ToString();
				numberPascalNamesList.Add(numberPascalNamesListTransfer);
			}
			List<LineNumberPascalNamePair> sortedNumberPascalNamesList = numberPascalNamesList.OrderBy(x => x.lineNumber).ToList();
			return sortedNumberPascalNamesList;
		}

		#region Public_Attributes
		//Proj Notes: important to fix relative path when Migrating library
		//public static string imageFileDirPath { get; set; } = @"..\..\..\..\Viz_func_Images\";
		public static string imageFileDirPath { get; set; } = @"C:\Users\ARTURO 001\source\repos\VSIXProject1004\Viz_func_Images\";
		public string ActiveCode { get; set; }
		public List<string> MethodsList { get; set; }
		public List<string> ClassesList { get; set; }
		public List<string> MethodsAndClassesList
		{
			get { return UnionOfListsClass.UnionOfLists(MethodsList, ClassesList); }
		}
		public Hashtable LineNumberKeyword { get; set; }
		public List<LineNumberPascalNamePair> LineNumberPascalNamePairList
		{
			get { return SortLineNumberNamesList(); }
		}
		public List<PascalSearch> PascalSearchList { get; set; }
		public string CodeFullFilePath { get; set; }
		public string FileNameCodeFile
		{
			get { return Path.GetFileName(CodeFullFilePath); }
		}
		public PascalSearch.RefineResultOptions _RefineResultForPascal;
		#endregion
	}
}
