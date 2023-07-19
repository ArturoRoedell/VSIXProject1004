using System.IO;

namespace VSIXProject1004
{
	public class BackEndMethods
	{
		public static string ImageFileFullPath(string ImageName,int imageIndex = 1)
		{
			string fullFilepath = imageFileDirPath + ImageName + "_0" + imageIndex + ".png";
			return fullFilepath;
		}
		
		public static string ReturnFullPath(string relativePath)//There was a reason I did this... Know I dont remember the reason
		{
			string fullPath = Path.GetFullPath(relativePath);
			return fullPath;
		}
		
		public static string imageFileDirPath { get; set; } = @"C:\Users\ARTURO 001\source\repos\VSIXProject1004\Viz_func_Images\";
	}
}