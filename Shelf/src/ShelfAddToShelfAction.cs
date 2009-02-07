using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Unix;
using Do.Platform;
using Do.Universe;

namespace Shelf
{	
	public class ShelfAddToShelfAction : Act
	{
		public override string Name {
			get { return Catalog.GetString ("Add To Shelf"); }
		}
		
		public override string Description {
			get { return Catalog.GetString ("Add Selected Item to one Shelf"); }
		}

		public override string Icon {
			get { return "bookmark_add"; }
		}
		
		public override bool ModifierItemsOptional {
			get { return true; }
		}

		public override IEnumerable<Type> SupportedItemTypes {
			get { yield return typeof (Item); }			
		}

		public override bool SupportsItem (Item item)
		{
			return (!(item is ShelfItem));
		}
		
		public override bool SupportsModifierItemForItems (IEnumerable<Item> items, Item moditem)
		{
			return true;
		}
		
		public override IEnumerable<Type> SupportedModifierItemTypes {
			get { yield return typeof(ShelfItem); yield return typeof (ITextItem);}
		}
		
		public override IEnumerable<Item> DynamicModifierItemsForItem (Item item)
		{
			foreach (Item i in ShelfItemSource.Shelves.Values)
					yield return i;
		}

		public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
		{
			if (modItems.Any ())
			{
				if(modItems.First () is ITextItem)
			    {
					string name = (modItems.First () as ITextItem).Text;
					ShelfItemSource.CreateShelf(name);
					ShelfItemSource.Shelves[name].AddItem(items.First ());
			    }
				else
				{
					(modItems.First () as ShelfItem).AddItem (items.First ());
				}
			}
			else
			{
				ShelfItemSource.AddToDefault (items.First ());
			}
			yield break;
		}

	}
}
