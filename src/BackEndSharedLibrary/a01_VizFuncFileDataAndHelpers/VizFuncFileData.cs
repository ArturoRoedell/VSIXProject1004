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
			this.FindMethodsAndClasses();
		}

		void FindMethodsAndClasses()
		{
			FindPascalClasses();
			FindPascalMethods();
			CreateLineNumberKeywordPair();
		}

		void CreateLineNumberKeywordPair()
		{
			printl("CreateLineNumberKeywordPair Start");
			string[] linesOfCodeArray = this.ActiveCode.Split('\n');
			HashSet<string> MethodsHashSet = new HashSet<string>(this.MethodsList);
			Hashtable TransferTable = new Hashtable();

			int index = 1;
			foreach (string codeLine in linesOfCodeArray)
			{
				int i = 1;
				foreach (var methodElement in MethodsHashSet)
				{
					if (codeLine.Contains(methodElement))
					{
						try
						{
							TransferTable.Add(index, methodElement);
						}
						catch
						{
						}
					}
					i++;
				}
				index++;
			}
			this.LineNumberKeyword = TransferTable;
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

		void FindPascalMethods()
		{
			string LooksLikeMethodsPattern = @"^t*\s*[a-zA-Z_.\s0-9<>]+\((?:[^)(]+|(?R))*+\)\s*((\/\/)(.*?)\n)*\s*\{"; //ProjNotes - Key Feature
			string[] RemoveTheseNonMethodsArray = new[] { "if", "for", "foreach", "else", "while", "do", "switch" };
			List<string> LooksLikeMeThodsList = FindPatternPcre2Regex(this.ActiveCode, LooksLikeMethodsPattern);
			string[] filteredMethods = SearchStringHelpers.FilterListReturnArray(LooksLikeMeThodsList, RemoveTheseNonMethodsArray);
			string MethodNamePattern = @"\b[_a-zA-Z0-9]+\b\(()";
			string filteredMethodsJoined = string.Join("", filteredMethods);
			List<string> FinalMethodList = FindPatternPcre2Regex(filteredMethodsJoined, MethodNamePattern);
			List<string> Transferlist = new List<string>();
			foreach (var VARIABLE in FinalMethodList)
			{
				Transferlist.Add(VARIABLE.TrimEnd('('));
			}
			foreach (var VARIABLE in Transferlist) //Debug
			{
				printl(VARIABLE);
			}
			this.MethodsList = Transferlist;
		}

		public async Task AllocatePascalSearchListAndGetUrls()
		{
			PascalSearch newPascalSearch = new PascalSearch(_RefineResultForPascal);
			PascalSearch PascalSearchWithUrls = new PascalSearch(_RefineResultForPascal);
			List<PascalSearch> TransferPascal = new List<PascalSearch>();
			foreach (var methodOrClass in this.MethodsAndClassesList)
			{//BUG fix IS BELOW; I believe the interaction vsix and await
				//BUG: The Code Fails at any await. Needs Fixing whole document
				newPascalSearch = PascalSearch.asyncReturnPascalNameSearch(methodOrClass);
				PascalSearchWithUrls = await Task.Run(() => PascalSearch.LoopThorughWordsGetImages(newPascalSearch)); //.WaitAsync(TimeSpan.FromMinutes(10));
				TransferPascal.Add(PascalSearchWithUrls);
				printl("Method_Or_Class_Name" + methodOrClass);
			}
			this.PascalSearchList = TransferPascal;
		}

		public static async Task DownloadSingleImage(string imageUrl, string fileName, int imageIndex = 0)
		{
			printl("inside Downloads method");
			imageIndex++; //Default is one
			string fullFilepath = ImageFileFullPath(fileName, imageIndex);
			printl(fullFilepath);
			if (!File.Exists(fullFilepath)) // (!File.Exists(fullFilepath)) // Default
			{
				//MediaTypeNames.Image image;
				printl(imageUrl);
				try
				{
					if (imageUrl.EndsWith(".jpeg"))
					{
						/*image = await LoadUrlImageToStreamClass.LoadUrlImageToStream(imageUrl);
						image.Save(fullFilepath, ImageFormat.Png);
						image.Dispose();*/
					}
					else
					{
						await DownloadFileClass.DownloadFile(imageUrl, fullFilepath);
					}
				}
				catch (Exception e)
				{
					printl(e.ToString());
					//throw;
				}
			}
		}

		public static string ImageFileFullPath(string ImageName, int imageIndex = 1)
		{
			string fullFilepath = imageFileDirPath + ImageName + "_0" + imageIndex + ".png";
			return fullFilepath;
		}//FIX MakeC conver to Absolute

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
