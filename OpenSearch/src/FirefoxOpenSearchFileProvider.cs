//  FirefoxOpenSearchFileProvider.cs
//
//  GNOME Do is the legal property of its developers, whose names are too numerous
//  to list here.  Please refer to the COPYRIGHT file distributed with this
//  source distribution.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Collections.Generic;

namespace Do.Plugins.OpenSearch
{
	public class FirefoxOpenSearchFileProvider : IOpenSearchFileProvider
	{	
		private List<string> openSearchFilePaths;
		
		public FirefoxOpenSearchFileProvider ()
		{		
			System.Diagnostics.Process locate = new System.Diagnostics.Process ();
			locate.StartInfo.FileName = "locate";
			locate.StartInfo.Arguments = @"-r ^.*firefox.*/searchplugins/.*\.xml$";
			locate.StartInfo.RedirectStandardOutput = true;
			locate.StartInfo.UseShellExecute = false;
			try {
				locate.Start ();
			} catch {
				Console.Error.WriteLine ("OpenSearchAction error: The program 'locate' could not be found.");
				return;
			}
			
			List<string> potentialPaths = new List<string> ();
			string path;
			while (null != (path = locate.StandardOutput.ReadLine ())) {	
				potentialPaths.Add (path.Remove (path.IndexOf ("searchplugins/") + "searchplugins/".Length));
			}
			
			string firefoxDefaultPath = GetFirefoxDefaultProfilePath ();
			if(firefoxDefaultPath != null)
				potentialPaths.Add(firefoxDefaultPath);
			
			Dictionary<string,int> unique = new Dictionary<string,int> ();
			openSearchFilePaths = new List<string>();
			foreach(string potentialPath in potentialPaths)
			{
				if (!unique.ContainsKey(potentialPath)) {
					unique.Add(potentialPath,0);
					openSearchFilePaths.Add(potentialPath);			
				}
			}			
		}
		
		public List<string> OpenSearchFilePaths
		{
			get { return openSearchFilePaths; }
		}
		
		public string GetFirefoxDefaultProfilePath ()
		{
			try
			{
				string BeginProfileName = "Path=";
				string BeginDefaultProfile = "Default=1";
				string profile = null;
				string home = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
				string profilePath = Path.Combine (home, ".mozilla/firefox/profiles.ini");
				
				using(StreamReader reader = File.OpenText (profilePath))
				{
					for (string line = reader.ReadLine (); line != null; line = reader.ReadLine ()) 
					{
						if (line.StartsWith (BeginDefaultProfile)) break;
						if (line.StartsWith (BeginProfileName)) {
							line = line.Trim ();
							line = line.Substring (BeginProfileName.Length);
							profile = line;
						}
					}
				}
					
				if(profile != null)
				{
					string path = Path.Combine (home, ".mozilla/firefox");
					path = Path.Combine (path, profile);
					path = Path.Combine (path, "searchplugins");
					
					return path;
				}
			}
			catch
			{
				// just return null if we've got problems
			}
				
			return null;
		}
	}
}

