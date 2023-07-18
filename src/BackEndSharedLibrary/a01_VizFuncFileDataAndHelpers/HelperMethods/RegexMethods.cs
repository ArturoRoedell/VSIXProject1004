using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods
{
	public class RegexMethods
	{
		public static List<string> FindPatternRegex(string activeCode, string classPattern)
		{
			HashSet<string> hashtable = new HashSet<string>();
			MatchCollection matches = Regex.Matches(activeCode, classPattern);
			foreach (Match match in matches)
			{
				try
				{
					hashtable.Add(match.Value);
				}
				catch
				{
				}
			} //Proj Notes - This part of the code  helps remove duplicates
			List<string> codeSnippsList = hashtable.Cast<string>().ToList();
			return codeSnippsList;
		}
	}
}