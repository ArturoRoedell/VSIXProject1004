using Downloader;
using static BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs.PrintClass;

namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs;

public class DownloadFileClass
{
	public static async Task DownloadFile(string imageUrl, string fullFilepath)
	{
		printl("Null?? " + imageUrl);
		try
		{
			var download = DownloadBuilder.New()
				.WithUrl(imageUrl)
				.WithFileLocation(fullFilepath)
				.Build();
			await download.StartAsync();
		}
		catch (Exception e)
		{
			printl(e.ToString());
			//throw;
		}
	}
}