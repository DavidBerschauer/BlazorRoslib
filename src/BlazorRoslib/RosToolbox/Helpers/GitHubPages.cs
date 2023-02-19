using System;
namespace RosToolbox.Helpers
{
	public static class GitHubPages
	{
		public static string BaseUri { get
			{
#if GITHUB_PAGES
				return "/BlazorRoslib/";
#else
                return "";
#endif
			}
		}
	}
}

