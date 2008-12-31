// ScreenItemSource.cs
//
//GNOME Do is the legal property of its developers. Please refer to the
//COPYRIGHT file distributed with this
//source distribution.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//
//

using System;
using System.Collections.Generic;

using Do.Universe;
using Wnck;
using Mono.Unix;

namespace WindowManager
{
	
	public class ScreenItemSource : ItemSource
	{
		public override string Name {
			get {
				return Catalog.GetString ("Window Screen Items");
			}
		}

		public override string Description {
			get {
				return Catalog.GetString ("Actions you can do to your screens.");
			}
		}

		public override string Icon {
			get {
				return "desktop"; //fixme
			}
		}

		public override IEnumerable<Type> SupportedItemTypes {
			get {
				return new Type[] {
					typeof (IScreenItem) };
			}
		}

		public override IEnumerable<Item> Items {
			get {
                List<Item> items;
		
                items = new List<Item> ();
				items.Add (new ScreenItem (Catalog.GetString ("Current Desktop"), 
                   Catalog.GetString ("Everything on the Current Desktop"),
                   "desktop"));
				
				return items;
			}
		}

		
		public ScreenItemSource()
		{
		}

		public override IEnumerable<Item> ChildrenOfItem (Item item)
		{
			return null;
		}

		public override void UpdateItems ()
		{
		}
	}
}
