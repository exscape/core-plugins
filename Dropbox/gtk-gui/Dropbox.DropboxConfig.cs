// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Dropbox {
    
    
    public partial class DropboxConfig {
        
        private Gtk.VBox vbox2;
        
        private Gtk.Frame frame2;
        
        private Gtk.Alignment GtkAlignment;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Entry base_path_entry;
        
        private Gtk.Button base_path_button;
        
        private Gtk.Label GtkLabel1;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget Dropbox.DropboxConfig
            Stetic.BinContainer.Attach(this);
            this.Name = "Dropbox.DropboxConfig";
            // Container child Dropbox.DropboxConfig.Gtk.Container+ContainerChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.frame2 = new Gtk.Frame();
            this.frame2.Name = "frame2";
            this.frame2.ShadowType = ((Gtk.ShadowType)(0));
            // Container child frame2.Gtk.Container+ContainerChild
            this.GtkAlignment = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment.Name = "GtkAlignment";
            this.GtkAlignment.LeftPadding = ((uint)(2));
            this.GtkAlignment.TopPadding = ((uint)(8));
            this.GtkAlignment.RightPadding = ((uint)(8));
            this.GtkAlignment.BottomPadding = ((uint)(8));
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            // Container child hbox2.Gtk.Box+BoxChild
            this.base_path_entry = new Gtk.Entry();
            this.base_path_entry.Sensitive = false;
            this.base_path_entry.CanFocus = true;
            this.base_path_entry.Name = "base_path_entry";
            this.base_path_entry.IsEditable = false;
            this.base_path_entry.InvisibleChar = '●';
            this.hbox2.Add(this.base_path_entry);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.hbox2[this.base_path_entry]));
            w1.Position = 0;
            // Container child hbox2.Gtk.Box+BoxChild
            this.base_path_button = new Gtk.Button();
            this.base_path_button.CanFocus = true;
            this.base_path_button.Name = "base_path_button";
            this.base_path_button.UseStock = true;
            this.base_path_button.UseUnderline = true;
            this.base_path_button.Label = "gtk-open";
            this.hbox2.Add(this.base_path_button);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.hbox2[this.base_path_button]));
            w2.Position = 1;
            w2.Expand = false;
            w2.Fill = false;
            this.GtkAlignment.Add(this.hbox2);
            this.frame2.Add(this.GtkAlignment);
            this.GtkLabel1 = new Gtk.Label();
            this.GtkLabel1.Name = "GtkLabel1";
            this.GtkLabel1.LabelProp = Mono.Addins.AddinManager.CurrentLocalizer.GetString("Dropbox Location");
            this.GtkLabel1.UseMarkup = true;
            this.frame2.LabelWidget = this.GtkLabel1;
            this.vbox2.Add(this.frame2);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox2[this.frame2]));
            w5.Position = 0;
            w5.Expand = false;
            w5.Fill = false;
            w5.Padding = ((uint)(5));
            this.Add(this.vbox2);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.Hide();
            this.base_path_button.Clicked += new System.EventHandler(this.OnBasePathBtnClicked);
        }
    }
}
