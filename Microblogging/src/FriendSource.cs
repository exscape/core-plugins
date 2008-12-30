// FriendSource.cs
// 
// GNOME Do is the legal property of its developers, whose names are too
// numerous to list here.  Please refer to the COPYRIGHT file distributed with
// this source distribution.
// 
// This program is free software: you can redistribute it and/or modify it under
// the terms of the GNU General Public License as published by the Free Software
// Foundation, either version 3 of the License, or (at your option) any later
// version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

using Mono.Unix;

using Do.Universe;
using Do.Platform;
using Do.Platform.Linux;

namespace Microblogging
{
	/// <summary>
	/// This is a dummy class, all it does is set up the Miroblog class on Do load
	/// and return a configuration page.
	/// </summary>
	public sealed class FriendSource : ItemSource, IConfigurable
	{
	
		public FriendSource()
		{
			Microblog.Connect (Microblog.Preferences.Username, Microblog.Preferences.Password);
		}
		
		public override string Name {
			get { return Catalog.GetString ("Microblog friends"); }
		}
		
		public override string Description {
			get { return Catalog.GetString ("Indexes your microblog friends"); }
		}
		
		public override string Icon {
			get { return "twitter_items.png@" + GetType ().Assembly.FullName; }
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get { yield return typeof (ContactItem); }
		}

		public Gtk.Bin GetConfiguration () 
		{
			return new Configuration ();
		}
	}
}