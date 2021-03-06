// ExtractArchiveAction.cs
//
//  Copyright (C) 2008 Guillaume Béland
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
//


using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Mono.Addins;
using Do.Universe;

namespace Archive {
        
        
        public class ExtractAction : Act {
                
                public ExtractAction()
                {
                }
                
                public override string Name {
                        get { return AddinManager.CurrentLocalizer.GetString ("Extract archive"); }
                }
                
                public override string Description {
                        get { return AddinManager.CurrentLocalizer.GetString ("Extract an archive to a given folder"); }
                }
        
                public override string Icon {
                        get { return "file-roller"; }
                }
                
                public override IEnumerable<Type> SupportedItemTypes {
                        get {
                                return new Type[] {                             
                                        typeof (IFileItem),
                                };
                        }
                }
                
                public override bool SupportsItem (Item item) 
                {
                        return IsArchive (item as IFileItem);
                }
                        
                public override bool SupportsModifierItemForItems (IEnumerable<Item> items, Item modItem)
                {
                        return Directory.Exists ((modItem as IFileItem).Path);
                }

                public override IEnumerable<Type> SupportedModifierItemTypes {
                        get { yield return typeof (IFileItem); }
                }
                
                public override bool ModifierItemsOptional {
                        get { return false; }
                }
                
                public override IEnumerable<Item> Perform (IEnumerable<Item> items, IEnumerable<Item> modItems)
                {
                        ExtractArchive ( (items.First () as IFileItem), (modItems.First () as IFileItem));
                        yield break;
                }

                private bool IsArchive (IFileItem item)
                {
                        return item.Path.EndsWith(".tar.gz") ||
                               item.Path.EndsWith (".tar.bz2") ||
                               item.Path.EndsWith (".tar") ;
                
                }

                private void ExtractArchive ( IFileItem archive, IFileItem where)
                {
                        if ( archive.Name.EndsWith ("tar.gz"))
                                Process.Start ("tar", string.Format ("-xzf {0} -C {1}", EscapeString(archive.Path), EscapeString(where.Path)));  
                        else if ( archive.Name.EndsWith ("tar.bz2"))
                                Process.Start ("tar", string.Format ("-xjf {0} -C {1}", EscapeString(archive.Path), EscapeString(where.Path)));
                        else if (archive.Name.EndsWith ("tar"))
                                Process.Start ("tar", string.Format ("-xf {0} -C {1}", EscapeString (archive.Path), EscapeString(where.Path)));
                                               
                }

                private string EscapeString (string str)
                {
                        return str.Replace (" ", "\\ ")
                                  .Replace ("'", "\\'");
                }
        }
}
