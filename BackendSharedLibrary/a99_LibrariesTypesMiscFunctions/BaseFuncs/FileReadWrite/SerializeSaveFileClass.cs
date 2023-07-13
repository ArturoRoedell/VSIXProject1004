using System.Collections.Generic;

namespace BackEndSharedLibrary.OtherMiscFuncs.BaseFuncs
{
	public class SerializeSaveFileClass
	{
		public static void SerializeSaveFile<T>(List<T> listData, string fullFilePath)
		{
			FileUtilitiesXT fileUtilitiesXT = new FileUtilitiesXT();
			string SaveThis = fileUtilitiesXT.SerializeJsonDataReturnString(listData);
			fileUtilitiesXT.FastCreateWriteFile(SaveThis, fullFilePath);
		}
	}
}