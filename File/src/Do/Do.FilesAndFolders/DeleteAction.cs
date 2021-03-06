// DeleteAction.cs
//
// GNOME Do is the legal property of its developers. Please refer to the
// COPYRIGHT file distributed with this source distribution.
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
//

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Mono.Addins;

using Do.Universe;
using Do.Platform;

namespace Do.FilesAndFolders
{
	
	class DeleteAction : AbstractFileAction
	{

		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("Delete File"); }
		}
		
		public override string Description { 
			get { return AddinManager.CurrentLocalizer.GetString ("Deletes a file or folder."); }
		}
		
		public override string Icon {
			get { return "gtk-delete"; }
		}

		public override IEnumerable<Type> SupportedModifierItemTypes {
			get { yield break; }
		}

		protected override IEnumerable<Item> Perform (string source)
		{
			Services.Application.RunOnThread (() => Delete (source));
			yield break;
		}

		void Delete (string path)
		{
			if (Directory.Exists (path)) {
				Directory.GetFileSystemEntries (path).ForEach (Delete);
				Directory.Delete (path);
			} else {
				File.Delete (path);
			}
		}
	}
}
