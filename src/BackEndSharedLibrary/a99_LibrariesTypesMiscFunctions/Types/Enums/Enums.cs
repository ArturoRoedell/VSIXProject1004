namespace BackEndSharedLibrary.Types
{
	public enum ImageType // Proj Notes: Easiest ways to mitigate small image files either png or jpeg
	{
		png,
		jpeg
	}

	public enum ImageDelivery
	{
		Individual,
		FirsLastTwo,
		MultipleThenWhole
	}

	public enum ImageSpreadOption
	{
		DefaultThreeSingleVizFUncs,
		TwoWordsPairs,
		DefaultPlusEntireSearch,
		TwoWordsPlusEntireSearch
	}
}

