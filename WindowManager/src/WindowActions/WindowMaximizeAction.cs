//  
//  Copyright (C) 2009 GNOME Do
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;
using System.Collections.Generic;
using System.Linq;

using Do.Universe;
using WindowManager.Wink;

using Wnck;
using Mono.Addins;

namespace WindowManager
{
	public class WindowMaximizeAction : WindowActionAction
	{
		public override string Name {
			get { return AddinManager.CurrentLocalizer.GetString ("(Un)Maximize"); }
		}
		
		public override string Description {
			get { return AddinManager.CurrentLocalizer.GetString ("Make a window consume the whole screen or revert to its normal size"); }
		}

		public override string Icon {
			get { return "up"; }
		}

		public override void Action (IEnumerable<Window> windows)
		{
			if (!windows.Any ())
				return;
			WindowControl.MaximizeWindow (windows.First ());
		}
	}
}
