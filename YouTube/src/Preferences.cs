using System;
using Mono.Unix;
using Do.Platform;

namespace Do.Universe
{	
	public class YouTubePreferences
	{
		const string UsernameKey = "Username";
		const string PasswordKey = "Password";
		
		IPreferences prefs;
		
		public YouTubePreferences()
		{
			prefs = Services.Preferences.Get<YouTubePreferences> ();
		}

		public string Username {
			get { return prefs.Get<string> (UsernameKey, ""); }
			set { prefs.Set<string> (UsernameKey, value); }
		}

		public string Password {
			get { return prefs.GetSecure (PasswordKey, ""); }
			set { prefs.SetSecure (PasswordKey, value); }
		}
	}
}