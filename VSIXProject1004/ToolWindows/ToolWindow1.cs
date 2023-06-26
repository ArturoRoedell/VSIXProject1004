using Microsoft.VisualStudio.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace VSIXProject1004
{
	public class ToolWindow1 : BaseToolWindow<ToolWindow1>
	{
		public override string GetTitle(int toolWindowId) => "ToolWindow1";

		public override Type PaneType => typeof(Pane);

		public override Task<FrameworkElement> CreateAsync(int toolWindowId, CancellationToken cancellationToken)
		{
			return Task.FromResult<FrameworkElement>(new ToolWindow1Control());
		}

		[Guid("5d93a9e3-5d1b-478a-9805-28761bfb03b0")]
		internal class Pane : ToolkitToolWindowPane
		{
			public Pane()
			{
				BitmapImageMoniker = KnownMonikers.ToolWindow;
			}
		}
	}
}
