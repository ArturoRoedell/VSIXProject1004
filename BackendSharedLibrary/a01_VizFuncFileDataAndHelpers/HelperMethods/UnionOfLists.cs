using System.Collections.Generic;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods
{



	public class UnionOfListsClass
	{
		public static List<string> UnionOfLists(List<string> listA, List<string> listB)
		{
			listA.AddRange(listB);
			List<string> listFinal = listA;
			return listFinal;
		}
	}
}