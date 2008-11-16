/*
 * TwitterTweet.cs
 * 
 * GNOME Do is the legal property of its developers, whose names are too numerous
 * to list here.  Please refer to the COPYRIGHT file distributed with this
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
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using Mono.Unix;

using Do.Universe;
using Do.Addins;
using Twitterizer.Framework;

namespace DoTwitter
{		
	public sealed class TweetAction : IAction, IConfigurable
	{
		public string Name {
			get { return Catalog.GetString ("Tweet"); }
		}
		
		public string Description {
			get { return Catalog.GetString ("Update Twitter status"); }
		}
		
		public string Icon {
			get { return "twitter-icon.png@" + GetType ().Assembly.FullName; }
		}
		
		public IEnumerable<Type> SupportedItemTypes {
            get {
                return new Type [] {
                    typeof (ITextItem),
                };
            }
        }

        public bool SupportsItem (IItem item) 
        {
            return (item as ITextItem).Text.Length <= 140;
        }
		
		public IEnumerable<Type> SupportedModifierItemTypes {
            get { 
                return new Type [] {
                    typeof (ContactItem),
                };
            }
        }

        public bool ModifierItemsOptional {
            get { return true; }
        }
                        
        public bool SupportsModifierItemForItems (IEnumerable<IItem> items, IItem modItem)
        {
        	//make sure we dont go over 140 chars with the contact screen name
            return (modItem as ContactItem) ["twitter.screenname"] != null &&
            	((items.First () as ITextItem).Text.Length + ((modItem as ContactItem) 
            	["twitter.screenname"]).Length < 140);
        }
        
        public IEnumerable<IItem> DynamicModifierItemsForItem (IItem item)
        {
            return null;
        }

        public IEnumerable<IItem> Perform (IEnumerable<IItem> items, IEnumerable<IItem> modItems)
        {			
			TwitterAction.Status = (items.First () as ITextItem).Text;
			if (modItems.Any ())
				TwitterAction.Status = BuildTweet (items.First (), modItems.ToArray ());
			
			Thread updateRunner = new Thread (new ThreadStart (TwitterAction.Tweet));
			updateRunner.Start ();
			
			return null;
		}
		
		public Gtk.Bin GetConfiguration ()
		{
			return new GenConfig ();
		}
		
		private string BuildTweet(IItem t, IItem [] c)
		{
			string tweet = "";
			ITextItem text = t as ITextItem;
			//ContactItem contact = c as ContactItem;
			
			
			//Handle situations without a contact
			if (c.Length == 0) return text.Text;
			
			// Direct messaging
			if (text.Text.Substring (0,2).Equals ("d "))
				tweet = "d " + (c [0] as ContactItem) ["twitter.screenname"] +
					" " +	text.Text.Substring (2);
			// Tweet replying
			else {
				foreach (ContactItem contact in c) {
					tweet += "@" + contact ["twitter.screenname"] + " " ;
				}
				tweet+= text.Text;
			}
			return tweet;
		}
	}
}