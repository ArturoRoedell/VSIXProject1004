using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;
using GScraper;
using BackEndSharedLibrary.Types;
using GScraper.Google;
using static BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods.SearchStringHelpers;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers
{



	public partial class VizFuncFileData
	{
		//Important Proj Notes: VizFuncFileData class outer parent class has the function to look though code file and find methods and classes
		//Important Proj Notes: SeparatePascalCaseString is an Important Key method that separates the NName into searchable words;
		//Important Proj Notes: This subclass only separates already found classes or method names into individual words then searches for url images
		public class PascalSearch
		{
			public static PascalSearch asyncReturnPascalNameSearch(string inputString)
			{
				static string[] ModifyInputstring(string intputStringOne, out string WholeSearchTerm)
				{
					string[] stringSeparated = SeparatePascalCaseString(intputStringOne); //Key Method
					string stringRebuilt = RebuildString(stringSeparated);
					string fillerWordsRemoved = RemoveFillerWords(stringRebuilt, _Refine.fillerWords);
					WholeSearchTerm = RemoveExtraSpaces(fillerWordsRemoved); //Proj Notes: We remove any unwanted fillerwords Here
					string[] OneOfManyList;
					string[] OneOfManyListGetTotal = SplitStringToList(WholeSearchTerm); //Find total words for next step
					if (OneOfManyListGetTotal.Length > 1) //Chooses
					{
						OneOfManyList = ImageSpreadOptionChooser(WholeSearchTerm, _Refine._imageSpreadOption);
					}
					else
					{
						OneOfManyList = SplitStringToList(WholeSearchTerm);
					}
					return OneOfManyList;
				}
				static string RefineSearchTerm(string SearchTerm)
				{
					printl("Search Term: " + SearchTerm);
					string singleSearchWord = SearchTerm;
					string refineSearch = "";
					refineSearch = refineSearch + _Refine.refineSearch;
					string searchWordRefinedSearch = $"{singleSearchWord} {refineSearch}";
					printl("searchWordRefinedSearch: " + searchWordRefinedSearch);
					return searchWordRefinedSearch;
				}

				PascalSearch piSearch = new PascalSearch(_Refine);
				piSearch.MethodClassName = inputString;
				string WholeSearchTerm;
				//Key Method: ModifyInputstring Most Important Method in this class because it parses each Methodor class into searchable words
				string[] TransferMultiWordSearchTerms = ModifyInputstring(inputString, out WholeSearchTerm);
				piSearch.MultiWordSearchTerms = new string[TransferMultiWordSearchTerms.Length];
				piSearch.searchWordRefinedSearch = new string[TransferMultiWordSearchTerms.Length];
				piSearch.MultiWordSearchTerms = TransferMultiWordSearchTerms;
				piSearch.SearchTermComplete = WholeSearchTerm;
				piSearch.FinalWordCount = piSearch.MultiWordSearchTerms.Length;
				for (int i = 0; i < piSearch.MultiWordSearchTerms.Length; i++)
				{
					//printl(_Refine.TestDebugStr);
					piSearch.searchWordRefinedSearch[i] = RefineSearchTerm(piSearch.MultiWordSearchTerms[i]);
				}
				return piSearch;
			}

			static string[] ImageSpreadOptionChooser(string wholeSearchTerm, ImageSpreadOption userPref)
			{
				string[] OneOfManyList = null;
				string[] transferArray = SplitStringToList(wholeSearchTerm);
				switch (userPref)
				{
					case ImageSpreadOption.DefaultThreeSingleVizFUncs:
						OneOfManyList = transferArray;
						break;
					case ImageSpreadOption.TwoWordsPairs:
					case ImageSpreadOption.TwoWordsPlusEntireSearch: //
						OneOfManyList = ImageSpreadOptionTwoWordsPairsFunction(transferArray, userPref, wholeSearchTerm);
						break;
					case ImageSpreadOption.DefaultPlusEntireSearch:
						OneOfManyList = PlusEntireSearch(transferArray, wholeSearchTerm);
						break;
				}
				return OneOfManyList;
			}

			static string[] ImageSpreadOptionTwoWordsPairsFunction(string[] transferArray, ImageSpreadOption userPref, string wholeSearchTerm)
			{
				string[] OneOfManyList = null;
				printl(transferArray.Length);
				int totalCap = (transferArray.Length + 1) / 2; // Round Up the to fit uneven words 
				string[] OneOfManyListTwoWordPairs = new string[totalCap];
				printl("pair array length: " + OneOfManyListTwoWordPairs.Length);
				int transferArrayIndex = 0;
				for (int i = 0; i < totalCap - 1; i++)
				{
					OneOfManyListTwoWordPairs[i] = transferArray[transferArrayIndex] + " " + transferArray[++transferArrayIndex];
					transferArrayIndex++;
				}
				//Notes: Newbie Notes - always minus 1 on index  when using .Length
				OneOfManyListTwoWordPairs[totalCap - 1] = transferArray[transferArray.Length - 2] + " " + transferArray[transferArray.Length - 1];

				printl("----------------------");
				printl();
				printl(wholeSearchTerm);
				printl(OneOfManyListTwoWordPairs[totalCap - 1]);
				printl();
				printl("----------------------");

				if (userPref == ImageSpreadOption.TwoWordsPlusEntireSearch) //Proj Notes: Adds Entire searchterm at the end 
				{
					OneOfManyList = PlusEntireSearch(OneOfManyListTwoWordPairs, wholeSearchTerm);
					printl("OneOfManyList0777: " + OneOfManyList.Length);
				}
				else
				{
					OneOfManyList = OneOfManyListTwoWordPairs;
				}
				printl("----------------------");
				printl();
				foreach (var element in OneOfManyList)
				{
					printl("Pairs: " + element);
				}
				printl();
				printl("----------------------");
				return OneOfManyList;
			}

			static string[] PlusEntireSearch(string[] transferArray, string wholeSearchTerm)
			{
				printl("EntireSearch Transfer array: " + transferArray.Length);
				string[] OneOfManyList = null;
				if (transferArray.Length < 4)
				{
					OneOfManyList = new string[transferArray.Length + 1]; // Adding an extra slot for fourth image
					printl("EntireSearch int: " + OneOfManyList.Length);
					for (int i = 0; i < transferArray.Length; i++)
					{
						OneOfManyList[i] = transferArray[i];
					}
					OneOfManyList[OneOfManyList.Length - 1] = wholeSearchTerm;
				}
				else
				{
					OneOfManyList = transferArray;
					OneOfManyList[3] = wholeSearchTerm; //Add whole search term image to slot 4
				}
				return OneOfManyList;
			}
			
			public PascalSearch(RefineResultOptions _refine = null) // = default Proj Notes - Setting initial values for RefineResultOptions search options
			{
				if (_refine != null)
				{
					_Refine = _refine;
				}
			}

			#region public atributes
			public string MethodClassName { get; set; }
			public string[,] ImageResultsUrl; // Proj Notes - Adding Gets set to this breaks things getting runtime error when running json serialization
			public string[] ImageResultsUrlOneD { get; set; } // Proj Notes -  Created This in order to serialize to json
			public string SearchTermComplete { get; set; } // PROJ Notes - This Will be the Cleaned search term which is optional search query
			public int FinalWordCount { get; set; }
			public string[] MultiWordSearchTerms { get; set; }
			public string[] searchWordRefinedSearch { get; set; }
			public static RefineResultOptions _Refine; // = new RefineResultOptions();
			private const int imageCap = 5;
			#endregion

			public class RefineResultOptions
			{
				public string[] fillerWords { get; set; } = new string[]
				{
					"And", "The", "A", "An", "Is", "Are", "Was", "Were", "As",
					"In", "On", "At", "Of", "For", "With", "here", "there", "If", "Then", "Or"
				}; // removes These filler words 
				public string refineSearch = ""; // Empty But a reminder that it is there to use 
				public ImageSpreadOption _imageSpreadOption = ImageSpreadOption.DefaultThreeSingleVizFUncs; //Default DefaultThreeSingleVizFUncs
				public GoogleImageSize _GoogleImageSize = GoogleImageSize.Icon; //Proj Notes: Icon sized Image
				public bool ImageMoreRandom = false; //Default is False
			}
		}
	}
}
