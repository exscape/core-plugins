// YouTubeOwnVideosSource.cs created with MonoDevelop
// User: luis at 08:50 p 10/09/2008
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//

using System;
using System.Collections.Generic;
using System.Threading;
using Mono.Unix;
using Do.Universe;

namespace YouTube
{
	
	
	public class YouTubeOwnVideosItemSource : ItemSource
	{
		public YouTubeOwnVideosItemSource()
		{
		}
		
		public override IEnumerable<Type> SupportedItemTypes {
			get {
				return new Type[] {
					typeof (YoutubeVideoItem),
				};
			}
		}
		
		public override string Name { get { return "YouTube Videos"; } }
		public override string Description { get { return "Your own YouTube videos"; } }
		public override string Icon {get { return "youtube_logo.png@" + GetType ().Assembly.FullName; } }
		
		public override IEnumerable<Item> Items {
			get { return Youtube.own; }
		}
		
		public override IEnumerable<Item> ChildrenOfItem (Item item)
		{
			return null;
		}
		
		public override void UpdateItems ()
		{
			try {
				Thread thread = new Thread ((ThreadStart) (Youtube.updateOwn));
				thread.IsBackground = true;
				thread.Start ();
			} catch (Exception e) {
				Console.Error.WriteLine (e.Message);
			}
		}
		
	}
}
