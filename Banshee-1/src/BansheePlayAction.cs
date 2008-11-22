/* BansheePlayAction.cs
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
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Do.Universe;
using Do.Addins;

using Mono.Unix;

namespace Banshee1
{	
	public class BansheePlayAction : AbstractAction
	{		
		public override string Name {
			get { return Catalog.GetString ("Play"); }
		}
		
		public override string Description {
			get { return Catalog.GetString ("Play from your Banshee Collection"); }
		}
		
		public override string Icon {
			get { return "media-playback-start"; }
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get { return 
				new Type[] { 
					typeof (MediaItem),
				};
			}
		}
		
		public override bool SupportsModifierItemForItems (IEnumerable<IItem> items, IItem modItem)
		{
			return false;
		}

		public override IEnumerable<IItem> Perform (IEnumerable<IItem> items, IEnumerable<IItem> modItems)
		{
			new Thread ((ThreadStart) delegate {
				List<string> files = new List<string> ();
				BansheeDBus bd = new BansheeDBus ();
			
				foreach (IItem item in items) {
					if (item == null) continue;
					foreach (SongMusicItem song in Banshee.LoadSongsFor ((item as MusicItem))) {
						files.Add (song.File);
					}
				}
			
				bd.Enqueue (files.ToArray (), true);
				Thread.Sleep (4000);
				bd.Next ();
			}).Start ();
			return null;
		}
	}
}