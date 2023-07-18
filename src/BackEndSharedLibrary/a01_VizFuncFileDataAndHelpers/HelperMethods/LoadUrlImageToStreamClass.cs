using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace BackEndSharedLibrary.VizFuncFileDataAndHelpers.HelperMethods;

public class LoadUrlImageToStreamClass
{
	//Proj Notes: This Method loads the image into ram into System.Drawing.Image object
	public static async Task<Image> LoadUrlImageToStream(string imageUrl)
	{
		Image image = null;
		try
		{
			System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
			webRequest.AllowWriteStreamBuffering = true;
			System.Net.WebResponse webResponse = webRequest.GetResponse();
			System.IO.Stream stream = webResponse.GetResponseStream();
			image = System.Drawing.Image.FromStream(stream);
			webResponse.Close();
		}
		catch (Exception ex)
		{
			printl("Exception: Something went wrong with LoadUrlImageToStream Web Request");
			return null;
		}
		return image;
	}
}