
 /*
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
using System.Collections.Generic;

using Do.Universe;

namespace FilePlugin {
	class RenameToAction : IAction {
		
		public string Name { get { return "Rename to..."; } }
		public string Description { get { return "Renames a file."; } }
		public string Icon { get { return "forward"; } }
		
		public Type [] SupportedItemTypes {
			get {
				return new Type [] {
					typeof (IFileItem),
				};
			}
		}
		
		public Type [] SupportedModifierItemTypes {
			get {
				return new Type [] {
					typeof (ITextItem),
				};
			}
		}
		
		public bool ModifierItemsOptional {
			get { return false; }
		}
		
		public bool SupportsItem (IItem item)
		{
			return true;
		}
		
		public bool SupportsModifierItemForItems (IItem[] items, IItem modItem)
		{
			return true;
		}
		
		public IItem [] DynamicModifierItemsForItem (IItem item)
		{
			return null;
		}
		
		public IItem [] Perform (IItem [] items, IItem [] modItems)
		{
			string dest;
			List<string> seenPaths;

			dest = (modItems [0] as ITextItem).Text;
			seenPaths = new List<string> ();
			if (dest.IndexOf('/') != -1) {
				Console.Error.WriteLine ("If you want to move a file, please use move.");
				return null;
			}
			foreach (FileItem src in items) {
				if (seenPaths.Contains (src.Path)) continue;
				try {
					dest = src.Path.Substring(0,src.Path.LastIndexOf("/")) + "/" + dest;
					File.Move (src.Path, dest);
					seenPaths.Add (src.Path);
				} catch (Exception e) {
					Console.Error.WriteLine ("MoveToAction could not move "+
							src.Path + " to " + dest + ": " + e.Message);
				}
			}
			return null;
		}
	}
}
