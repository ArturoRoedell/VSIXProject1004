﻿using System.Collections;
using System.Collections.Generic;

namespace VizFuncTypes
{
	public class iPascalSearchDataSimple
	{
		public string MethodClassName { get; set; }
		public string SearchTermComplete { get; set; } // PROJ Notes - This Will be the Cleaned search term which is optional search query
		public int FinalWordCount { get; set; }
		public string[] ImageResultsUrlOneD{ get; set; } // Proj Notes -  Created This in order to serialize to json
		public string[] MultiWordSearchTerms { get; set; }
		public string[] searchWordRefinedSearch { get; set; }
	}

	/// <summary>
	/// Use this class to serialize and deserialize to json
	/// </summary>
	public class VizFuncDataSimple
	{
		#region NonList
		//public string ActiveCode { get; set; }
		public string CodeFullFilePath { get; set; }
		public string FileNameCodeFile { get; set; }
		#endregion
	
		#region Lists
		public List<string>? MethodsList { get;  set; }
		public List<string>? ClassesList { get; set; }
		public List<string>? MethodsAndClassesList { get; set; }
		public List<LineNumberPascalNamePair>? LineNumberPascalNamePairList { get; set; }
		public List<iPascalSearchDataSimple> PascalSearchDataSimpleList { get; set; }
		#endregion
	}
}


