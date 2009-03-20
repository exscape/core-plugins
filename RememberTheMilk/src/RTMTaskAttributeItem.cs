/* RTMTaskAttributeItem.cs
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

using Do.Universe;
using Do.Platform;

namespace RememberTheMilk
{
	
	public class RTMTaskAttributeItem : Item, IUrlItem
	{
		string name;
		string description;
		string icon;
		string url;
		
		public RTMTaskAttributeItem (string name, string description, string url) :
			this (name, description, url, "rtm.png@" + typeof(RTMTaskAttributeItem).Assembly.FullName)
		{			
		}
		
		public RTMTaskAttributeItem (string name, string description, string url, string icon)
		{
			this.name = name;
			this.description = description;
			this.url = url;
			this.icon = icon;
		}
		
		public override string Name {
			get { return name; }
		}
		
		public override string Description {
			get { return description;}
		}
		
		public override string Icon {
			get { return icon; }
		}
		
		public string Url {
			get { return url; }
		}
	}
}
