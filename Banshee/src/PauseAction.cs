/* PauseAction.cs 
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

using Mono.Addins;

using Do.Platform;
using Do.Universe;

namespace Banshee
{	
	public class PauseAction : AbstractPlayerAction
	{
		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("Pause"); }
		}

		public override string Description {
			get { return AddinManager.CurrentLocalizer.GetString ("Pause playing track"); }
		}

		public override string Icon {
			get { return "media-playback-pause"; }
		}

		protected override bool IsAvailable()
		{
			return Banshee.IsPlaying;
		}

		protected override void Perform ()
		{
			Banshee.Pause ();
		}
	}
}
