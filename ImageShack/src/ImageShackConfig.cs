// ImageShackConfig.cs
// 
// GNOME Do is the legal property of its developers, whose names are too
// numerous to list here.  Please refer to the COPYRIGHT file distributed with
// this source distribution.
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
using System.Text.RegularExpressions;

using Do.Platform;

namespace ImageShack
{
	public partial class ImageShackConfig : Gtk.Bin
	{
		private static IPreferences prefs;
		private readonly static string RegistrationUrl = "http://profile.imageshack.us/registration/";
		
		public ImageShackConfig()
		{
			this.Build();
			
			if (!String.IsNullOrEmpty (RegistrationCode))
				RegistrationCodeEntry.Text = RegistrationCode;
		}
		
		static ImageShackConfig()
		{
			prefs = Services.Preferences.Get<ImageShackConfig> ();
		}
		
		public static string RegistrationCode {
			get { return prefs.Get<string> ("RegistrationCode",""); }
			set { prefs.Set<string> ("RegistrationCode",value); }
		}

		protected virtual void OnRegistrationButtonClicked (object sender, System.EventArgs e)
		{
			Services.Environment.OpenUrl (RegistrationUrl);
		}

		protected virtual void OnRegistrationCodeEntryChanged (object sender, System.EventArgs e)
		{
			string regcode = RegistrationCodeEntry.Text.Trim ();
			Regex regKeyPattern = new Regex ("^[a-z0-9]{32}$");
			if (regKeyPattern.IsMatch (regcode))
				RegistrationCode = regcode;
		}
	}
}
