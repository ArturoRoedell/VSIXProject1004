using System.Collections.Generic;
using System.Linq;
using PCRE;
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

		public static List<string> FindPatternPcre2Regex(string ActiveCode, string pattern)
		{
			HashSet<string> hashtable = new HashSet<string>();
			foreach (PcreMatch match in PcreRegex.Matches(ActiveCode, pattern, PcreOptions.MultiLine))
			{
				try
				{
					hashtable.Add(match.Value);
				}
				catch
				{
				}
			} //Proj Notes - This part of the code  helps remove duplicates
			List<string> CodeSnippsList = hashtable.Cast<string>().ToList();
			return CodeSnippsList;
		}
	}
}