using System;
using Microsoft.AspNetCore.Components;

namespace RosToolbox.Helpers
{
	public class CustomNavManager
	{
		private NavigationManager navManager;

		public CustomNavManager(NavigationManager navManager)
		{
			this.navManager = navManager;
		}

		public void NavigateTo(string uri, bool forceLoad = false, bool replace = false)
		{
			navManager.NavigateTo($"{GitHubPages.BaseUri}{uri}", forceLoad, replace);
		}
    }
}

