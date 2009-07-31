// Sync.cs created with MonoDevelop
// User: anders at 15:12 18-01-2009
//
////  This program is free software: you can redistribute it and/or modify
////  it under the terms of the GNU General Public License as published by
////  the Free Software Foundation, either version 3 of the License, or
////  (at your option) any later version.
////
////  This program is distributed in the hope that it will be useful,
////  but WITHOUT ANY WARRANTY; without even the implied warranty of
////  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
////  GNU General Public License for more details.
////
////  You should have received a copy of the GNU General Public License
////  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using Do.Universe;

namespace SqueezeCenter.PlayerCommands
{
		
	public class Sync : PlayerCommand 
	{		
		public Sync () : base(
		                      "Sync with", "Synchronize this player with another", 
		                      "sync.png@" + typeof (Sync).Assembly.FullName,		
		                      new PlayerStatus[] {PlayerStatus.Playing, PlayerStatus.Paused, PlayerStatus.Stopped, PlayerStatus.TurnedOff}
		) {}
		
		public override IEnumerable<Type> SupportedModifierItemTypes {
			get { 				
				yield return typeof (Player);				
			}
		}

		public override bool SupportsModifierItemForItems (IEnumerable<Item> items, Item modItem)
		{
			// all players except the current one
			return !items.Contains (modItem);		
		}
		
		public override bool ModifierItemsOptional
		{
			get {
				return false;
			}
		}
		
		public override string GetCommand (Player player, Item modifierItem)
		{
			return string.Format ("{1} sync {0}", player.Id, (modifierItem as Player).Id);
		}
	}
}
