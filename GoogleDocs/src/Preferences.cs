// Preferences.cs
// 
// Copyright (C) 2009 GNOME Do
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Mono.Addins;
using Do.Platform;

namespace GDocs
{
	public class GDocsPreferences
	{
		const string UsernameKey = "Username";
		const string PasswordKey = "Password";
		
		IPreferences prefs;
		
		public GDocsPreferences ()
		{
			prefs = Services.Preferences.Get <GDocsPreferences> ();
		}
		
		public string Username {
			get { return prefs.Get <string> (UsernameKey, ""); }
			set { prefs.Set <string> (UsernameKey, value); }
		}
		
		public string Password {
			get { return prefs.GetSecure <string> (PasswordKey, ""); }
			set { prefs.SetSecure <string> (PasswordKey, value); }
		}
	}
}
