/* AbstractPlayerAction.cs 
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
using System.Collections.Generic;

using Do.Universe;

namespace Banshee
{	
	public abstract class AbstractPlayerAction : Act
	{
		protected abstract void Perform ();

		protected virtual bool IsAvailable ()
		{
			return true;
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get { yield return typeof (IApplicationItem); }
		}

		public override bool SupportsItem (Item item)
		{
			return IsBanshee (item as IApplicationItem) && IsAvailable ();
		}

		bool IsBanshee (IApplicationItem app)
		{
			return app.Exec.Contains ("banshee");
		}

		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			Perform ();
			yield break;
		}
	}
}
