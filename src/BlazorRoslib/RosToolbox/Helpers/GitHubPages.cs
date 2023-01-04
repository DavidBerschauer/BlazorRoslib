using System;
namespace RosToolbox.Helpers
{
	public static class GitHubPages
	{
		public static string BaseUri { get
			{
#if DEBUG
				return "";
#else
				return "/BlazorRoslib/";
#endif
			}
		}
	}
}

