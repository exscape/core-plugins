/* RTMSetUrl.cs
 *
 * GNOME Do is the legal property of its developers. Please refer to the
 * COPYRIGHT file distributed with this source distribution.
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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mono.Unix;

using Do.Universe;
using Do.Platform;

namespace RememberTheMilk
{
	public class RTMSetUrl : Act
	{
		// URL regex taken from http://www.osix.net/modules/article/?id=586
		const string UrlPattern = "^(https?://)"
			+ "?(([0-9a-zA-Z_!~*'().&=+$%-]+: )?[0-9a-zA-Z_!~*'().&=+$%-]+@)?" //user@
			+ @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
			+ "|" // allows either IP or domain
			+ @"([0-9a-zA-Z_!~*'()-]+\.)*" // tertiary domain(s)- www.
			+ @"([0-9a-zA-Z][0-9a-zA-Z-]{0,61})?[0-9a-zA-Z]\." // second level domain
			+ "[a-zA-Z]{2,6})" // first level domain- .com or .museum
			+ "(:[0-9]{1,4})?" // port number- :80
			+ "((/?)|" // a slash isn't required if there is no file name
			+ "(/[0-9a-zA-Z_!~*'().;?:@&=+$,%#-]+)+/?) *$";
		
		public override string Name {
			get { return Catalog.GetString ("Set URL"); }
		}
		
		public override string Description {
			get { return Catalog.GetString ("Set the URL of a task."); }
        	}
			
		public override string Icon {
			get { return "task-seturl.png@" + GetType ().Assembly.FullName; }
		}

		public bool CheckValidURL(string url) {
			Regex url_regex;
			url_regex = new Regex (UrlPattern, RegexOptions.Compiled);
			return url_regex.IsMatch (url);
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get {
				return new Type[] {
					typeof (RTMTaskItem),
				};
			}
		}
		
		public override IEnumerable<Type> SupportedModifierItemTypes {
			get { 
				return new Type[] {
					typeof (ITextItem),
				};
			}
		}
        
		public override bool ModifierItemsOptional {
			get { return true; }
		}
		
		public override bool SupportsItem (Item item) {
			return true;
		}
		
		public override bool SupportsModifierItemForItems (IEnumerable<Item> item, Item modItem) 
		{
			return true;
		}
		
		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modifierItems) 
		{
			string url = String.Empty;
			
			if (modifierItems.FirstOrDefault() != null) {
				url = ((modifierItems.FirstOrDefault() as ITextItem).Text);
			}
			
                	// User may have entered explicit mode and entered a blank line.
                	// To be safe; strip out all new line characters from input
			// for URL resetting.
			url = url.Replace("\n", "");

			// The URL set to the task may be reset if the entered text is empty.
			// Check if it's not empty.
			if (!string.IsNullOrEmpty(url)) {
				// Check if the entered text is a valid URL.
				if (!CheckValidURL(url)) {
					// Error in entered URL.
					Services.Notifications.Notify("Remember The Milk",
					                              "Invalid URL provided.");
					yield break;
				}
			}
			
            		Services.Application.RunOnThread (() => {
				RTM.SetURL ((items.First () as RTMTaskItem).ListId,
				            (items.First () as RTMTaskItem).TaskSeriesId,
				            (items.First () as RTMTaskItem).Id, url);
			});
			yield break;
		}
	}
}
