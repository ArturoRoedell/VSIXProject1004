using System.Text;
using System.Text.RegularExpressions;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods;

public class SearchStringHelpers
{
	// Prof Notes - The Method Below will return a set of words from a single input of a string this will be used for the image search
	public static string[] ReturnCleanedWordList(string inputString)
	{
		string[] mystrlist = SeparatePascalCaseString(inputString);
		inputString = RebuildString(mystrlist);
		inputString = RemoveFillerWords(inputString);
		inputString = RemoveExtraSpaces(inputString);
		string[] result = SplitStringToList(inputString);
		return result;
	}

	public static string RemoveFillerWords(string MyStr, string[] fillerWords = null)
	{
		for (int i = 0; i < fillerWords.Length; i++) MyStr = Regex.Replace(MyStr, @$"\b{fillerWords[i]}\b", "", RegexOptions.IgnoreCase);
		return MyStr;
	}

	public static string RebuildString(string[] strlist)
	{
		StringBuilder sb = new();
		foreach (string element in strlist)
		{
			sb.Append(element);
			sb.Append(" ");
		}
		string result = sb.ToString();
		return result;
	}

	public static string[] SeparatePascalCaseString(string inputString, string myRegex = "")
	{
		string[] splitString = null;
		string pattern = @"[A-Z]+[a-z_.0-9<>]*"; //This is Good Enough.Produces Some Two letter words and one letter words at the end of string

		if (myRegex == "")
		{
			splitString = System.Text.RegularExpressions.Regex
				.Matches(inputString, pattern)
				.OfType<System.Text.RegularExpressions.Match>()
				.Select(match => match.Value)
				.ToArray();
		}
		else
		{
			splitString = Regex.Split(inputString, myRegex);
		}
		return splitString;
	}

	public static string[] SplitStringToList(string inputString)
	{
		string[] result = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
		return result;
	}

	public static string RemoveExtraSpaces(string input)
	{
		string underScore = "_";
		string replacement01 = " ";
		string removedUnderscores = Regex.Replace(input, underScore, replacement01);
		string pattern = "\\s+";
		string replacement = " ";
		string result = Regex.Replace(removedUnderscores, pattern, replacement);
		return result;
	}
	
	public static string[] FilterListReturnArray(List<string> CodeSnippsList, string[] exclusionArray)
	{
		HashSet<string> nonMethodsHashSet = exclusionArray.ToHashSet();//Important to make later things easier
		int count = CodeSnippsList.Count(element => !nonMethodsHashSet.Contains(element));
		string[] FileredArray = new string[count];
		int index = 0;
		foreach (string element in CodeSnippsList)
		{
			if (!nonMethodsHashSet.Contains(element))
			{
				FileredArray[index++] = element;
			}
		}
		return FileredArray;
	}
	
}