/* DropboxPuburlAction.cs
 *
 * GNOME Do is the legal property of its developers. Please refer to the
 * COPYRIGHT file distributed with this
 * source distribution.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Mono.Addins;
 
using Do.Universe;
using Do.Universe.Common;
using Do.Platform;


namespace Dropbox
{
	
	
	public class DropboxRevisionsAction : DropboxAbstractAction
	{
				
		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("View revisions");  }
		}
		
		public override string Description {
			get { return AddinManager.CurrentLocalizer.GetString ("Views file history in Dropbox web interface."); }
		}
		
		public override string Icon {
			get { return ("dropbox-revisions.png@") + GetType ().Assembly.FullName; }
		}
		
		public override bool SupportsItem (Item item) 
		{
			string path = GetPath(item);
			
			return File.Exists (path) && 
				(path.StartsWith (Dropbox.BasePath) || 
				HasLink (path));
		}
		
		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			string path = GetPath(items.First ());
			
			if (!path.StartsWith (Dropbox.BasePath)) 
				path = GetLink (path);
			
			string url = Dropbox.GetRevisionsUrl (path);
			
			Services.Environment.OpenUrl (url);
			
			yield break;
		}

	}
}
