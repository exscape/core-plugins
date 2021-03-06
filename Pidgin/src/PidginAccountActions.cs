// PidginAccountActions.cs
// 
// Copyright (C) 2008 Alex Launi <alex.launi@gmail.com>
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
using System.Linq;
using System.Collections.Generic;

using Mono.Addins;

using Do.Universe;
using Do.Platform;

namespace PidginPlugin
{

	public class PidginEnableAccount : Act
	{
		const string gtkGaim = "gtk-gaim";

		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("Sign on"); }
		}

		public override string Description {
			get { return AddinManager.CurrentLocalizer.GetString ("Enable pidgin account"); }
		}

		public override string Icon {
			get { return "pidgin"; }
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get { yield return	typeof (PidginAccountItem); }
		}
		
		public override bool SupportsItem (Item item)
		{
			Pidgin.IPurpleObject prpl;
			 try {
				prpl = Pidgin.GetPurpleObject ();
				if (!prpl.PurpleAccountIsConnected ((item as PidginAccountItem).Id))
					return true;
			} catch { }
			
			return false;
		}
		
		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			Pidgin.IPurpleObject prpl;
			PidginAccountItem account = items.First () as PidginAccountItem;
			
			try {
				prpl = Pidgin.GetPurpleObject ();
				try {
					prpl.PurpleAccountSetEnabled (account.Id, gtkGaim, (int) 1);
				}
				catch {
					prpl.PurpleAccountSetEnabled (account.Id, gtkGaim, (uint) 1);
				}
			} catch (Exception e) {
				Log<PidginEnableAccount>.Error ("Could not disable Pidgin account: {0}", e.Message);
				Log<PidginEnableAccount>.Debug (e.StackTrace);
			}
			
			yield break;
		}		
	}
	
	public class PidginDisableAccount : Act
	{
		const string gtkGaim = "gtk-gaim";
		
		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("Sign off"); }
		}

		public override string Description {
			get { return AddinManager.CurrentLocalizer.GetString ("Disable Pidgin account"); }
		}

		public override string Icon {
			get { return "pidgin"; }
		}

		public override IEnumerable<Type> SupportedItemTypes {
			get { yield return typeof (PidginAccountItem); }
		}

		public override bool SupportsItem (Item item)
		{
			Pidgin.IPurpleObject prpl = Pidgin.GetPurpleObject ();
			PidginAccountItem account = item as PidginAccountItem;
			return prpl.PurpleAccountIsConnected (account.Id);
		}

		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			Pidgin.IPurpleObject prpl;
			PidginAccountItem account = items.First () as PidginAccountItem;

			try {
				prpl = Pidgin.GetPurpleObject ();
				try {
					prpl.PurpleAccountSetEnabled (account.Id, gtkGaim, (int) 0);
				}
				catch {
					prpl.PurpleAccountSetEnabled (account.Id, gtkGaim, (uint) 0);
				}
			} catch (Exception e) {
				Log<PidginDisableAccount>.Error ("Could not disable Pidgin account: {0}", e.Message);
				Log<PidginDisableAccount>.Debug (e.StackTrace);
			}
			yield break;
		}
	}
}
