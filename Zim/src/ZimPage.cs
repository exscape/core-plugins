// ZimPage.cs 
// User: Karol Będkowski at 17:33 2008-10-19
//
//Copyright Karol Będkowski 2008
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Mono.Unix;

using Do.Universe;

namespace Zim
{
	
	/// <summary>
	/// Object represent single Zim's page.
	/// </summary>
	public class ZimPage: Item {
		string notebook, name;
		
		public override string Name {
			get { return name; }
		}

		public override string Description {
			get { return Catalog.GetString("Zim page in notebook: ") + notebook; }
		}
		
		public string Notebook {
			get { return notebook; }
		}

		public override string Icon {
			get { return "document"; }
		}
		
		public ZimPage (string name, string notebook)
		{
			this.name = name;
			this.notebook = notebook;
		}
	}
}
