/* CopyAction.cs
 *
 * GNOME Do is the legal property of its developers. Please refer to the
 * COPYRIGHT file distributed with this source distribution.
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

using Mono.Unix;

using Do.Universe;
using Do.Platform;

namespace Do.FilesAndFolders
{
	
	class CopyAction : AbstractFileAction
	{

		public override string Name {
			get { return Catalog.GetString ("Copy to..."); }
		}
		
		public override string Description {
			get { return Catalog.GetString ("Copies a file or folder to another location."); }
		}
		
		public override string Icon {
			get { return "gtk-copy"; }
		}

		public override bool SupportsModifierItemForItems (IEnumerable<Item> items, Item modItem)
		{
			return Directory.Exists (GetPath (modItem));
		}

		protected override IEnumerable<Item> Perform (string source, string destination)
		{
			PerformOnThread (() => {
				FileTransformation copy = new FileTransformation (CopyTransform);
				try {
					copy.Transform (source, destination);
				} catch (Exception e) {
					Log.Error ("Could not copy {0} to {1}: {2}", source, destination, e.Message);
					Log.Debug (e.StackTrace);
				}
			});
			
			yield break;
		}

		void CopyTransform (string source, string destination)
		{
			if (Directory.Exists (source) && !Directory.Exists (destination))
				Directory.CreateDirectory (destination);
			else
				File.Copy (source, destination, true);
		}
	}
}