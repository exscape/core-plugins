/* SearchAction.cs
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
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Collections.Generic;
using GConf;
using Mono;
using Mono.Unix;
using Do.Universe;

namespace Delicious
{
	public class DeliciousCertify : ICertificatePolicy
	{
		public bool CheckValidationResult (ServicePoint sp, X509Certificate cert, WebRequest chain, int errs)
		{
			return true;
		}
	}

	public class SearchAction : IAction
	{
		public string Name {
			get {
				return "Search del.icio.us";
			}
		}
		
		public string Description {
			get {
				return "del.icio.us tag search";
			}
		}
		
		public string Icon {
			get {
				return "bookmark-new";
			}
		}
		
		public Type[] SupportedItemTypes {
			get {
				return new Type[] {
					typeof (ITextItem),
				};
			}
		}

		public bool SupportsItem (IItem item) {
			return true;
		}

		public IItem[] Perform (IItem[] items, IItem[] modItems)
		{
			String tags = (items [0] as ITextItem).Text.Replace(" ","+");

			String url = "https://api.del.icio.us/v1/posts/recent?tag=" + tags;
			//Console.WriteLine (url);
			HttpWebRequest request = WebRequest.Create (url) as HttpWebRequest;

			ServicePointManager.CertificatePolicy = new DeliciousCertify();

			//Console.WriteLine ("made it");

			GConf.Client gconf = new GConf.Client ();

			String username;
			String password;
			
			try {
				username = gconf.Get ("/apps/gnome-do/plugins/del.icio.us/username") as String;
				password = gconf.Get ("/apps/gnome-do/plugins/del.icio.us/password") as String;
			}
			catch (GConf.NoSuchKeyException) {
				gconf.Set ("/apps/gnome-do/plugins/del.icio.us/username", "");
				gconf.Set ("/apps/gnome-do/plugins/del.icio.us/password", "");
				return null;
			}

			//Console.WriteLine ("got data");
			//Console.WriteLine (username);
			//Console.WriteLine (password);

			request.Credentials = new NetworkCredential (username, password);
			request.Method = "POST";

			XmlTextReader reader;

			List<IItem> hits = new List<IItem> ();
			try {
				HttpWebResponse response = request.GetResponse () as HttpWebResponse;
				reader = new XmlTextReader (response.GetResponseStream ());
			} catch (Exception e) {
				Console.WriteLine (e.ToString ());
				hits.Add (new BookmarkItem ("See everybody's...", "http://del.icio.us/tag/" + tags));
				return hits.ToArray ();
			}

			while (reader.Read ()) {
				if (reader.Name == "post")
					hits.Add (new BookmarkItem (reader.GetAttribute ("description"), reader.GetAttribute ("href")));
			}

			hits.Add (new BookmarkItem ("See all mine...", "http://del.icio.us/search/?type=user&p=" + tags));
			hits.Add (new BookmarkItem ("See everybody's...", "http://del.icio.us/tag/" + tags));
			
			return hits.ToArray ();
		}
		
		public Type [] SupportedModifierItemTypes {
			get { return new Type [] {}; }
		}

		public bool ModifierItemsOptional {
			get { return true; }
		}
				
		public bool SupportsModifierItemForItems (IItem [] items, IItem modItem)
		{
			return true;
		}
		
		public IItem [] DynamicModifierItemsForItem (IItem item)
		{
			return null;
		}
		
	}
}