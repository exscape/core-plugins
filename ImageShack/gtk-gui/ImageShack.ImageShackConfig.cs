// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace ImageShack {
    
    
    public partial class ImageShackConfig {
        
        private Gtk.VBox vbox1;
        
        private Gtk.Frame frame1;
        
        private Gtk.Alignment GtkAlignment1;
        
        private Gtk.VBox vbox2;
        
        private Gtk.HBox hbox1;
        
        private Gtk.Label label2;
        
        private Gtk.HBox hbox2;
        
        private Gtk.Label label1;
        
        private Gtk.Entry RegistrationCodeEntry;
        
        private Gtk.HBox hbox3;
        
        private Gtk.Button RegistrationButton;
        
        private Gtk.Label GtkLabel1;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize(this);
            // Widget ImageShack.ImageShackConfig
            Stetic.BinContainer.Attach(this);
            this.Name = "ImageShack.ImageShackConfig";
            // Container child ImageShack.ImageShackConfig.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            this.vbox1.Spacing = 6;
            // Container child vbox1.Gtk.Box+BoxChild
            this.frame1 = new Gtk.Frame();
            this.frame1.Name = "frame1";
            this.frame1.ShadowType = ((Gtk.ShadowType)(0));
            // Container child frame1.Gtk.Container+ContainerChild
            this.GtkAlignment1 = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment1.Name = "GtkAlignment1";
            this.GtkAlignment1.LeftPadding = ((uint)(12));
            // Container child GtkAlignment1.Gtk.Container+ContainerChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox1 = new Gtk.HBox();
            this.hbox1.Name = "hbox1";
            this.hbox1.Spacing = 6;
            // Container child hbox1.Gtk.Box+BoxChild
            this.label2 = new Gtk.Label();
            this.label2.Name = "label2";
            this.label2.Xalign = 0F;
            this.label2.LabelProp = Mono.Unix.Catalog.GetString("If you have an ImageShack account, a registration code allows you to save images to the My Images sections of your account.\n\nPlease log-in to your ImageShack account before getting your registration code.");
            this.label2.Wrap = true;
            this.hbox1.Add(this.label2);
            Gtk.Box.BoxChild w1 = ((Gtk.Box.BoxChild)(this.hbox1[this.label2]));
            w1.Position = 0;
            w1.Expand = false;
            w1.Fill = false;
            this.vbox2.Add(this.hbox1);
            Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
            w2.Position = 0;
            w2.Expand = false;
            w2.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox2 = new Gtk.HBox();
            this.hbox2.Name = "hbox2";
            this.hbox2.Spacing = 6;
            this.hbox2.BorderWidth = ((uint)(3));
            // Container child hbox2.Gtk.Box+BoxChild
            this.label1 = new Gtk.Label();
            this.label1.Name = "label1";
            this.label1.Xalign = 0F;
            this.label1.LabelProp = Mono.Unix.Catalog.GetString("_Registration Code");
            this.label1.UseUnderline = true;
            this.hbox2.Add(this.label1);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox2[this.label1]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            // Container child hbox2.Gtk.Box+BoxChild
            this.RegistrationCodeEntry = new Gtk.Entry();
            this.RegistrationCodeEntry.CanFocus = true;
            this.RegistrationCodeEntry.Name = "RegistrationCodeEntry";
            this.RegistrationCodeEntry.IsEditable = true;
            this.RegistrationCodeEntry.WidthChars = 35;
            this.RegistrationCodeEntry.InvisibleChar = '●';
            this.hbox2.Add(this.RegistrationCodeEntry);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.hbox2[this.RegistrationCodeEntry]));
            w4.Position = 1;
            w4.Expand = false;
            this.vbox2.Add(this.hbox2);
            Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox2]));
            w5.Position = 1;
            w5.Expand = false;
            w5.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.hbox3 = new Gtk.HBox();
            this.hbox3.Name = "hbox3";
            this.hbox3.Spacing = 6;
            this.hbox3.BorderWidth = ((uint)(3));
            // Container child hbox3.Gtk.Box+BoxChild
            this.RegistrationButton = new Gtk.Button();
            this.RegistrationButton.CanFocus = true;
            this.RegistrationButton.Name = "RegistrationButton";
            this.RegistrationButton.UseUnderline = true;
            this.RegistrationButton.Xalign = 0F;
            this.RegistrationButton.Label = Mono.Unix.Catalog.GetString("_Get Registration Code");
            this.hbox3.Add(this.RegistrationButton);
            Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.hbox3[this.RegistrationButton]));
            w6.Position = 0;
            w6.Expand = false;
            w6.Fill = false;
            this.vbox2.Add(this.hbox3);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
            w7.Position = 2;
            w7.Expand = false;
            w7.Fill = false;
            this.GtkAlignment1.Add(this.vbox2);
            this.frame1.Add(this.GtkAlignment1);
            this.GtkLabel1 = new Gtk.Label();
            this.GtkLabel1.Name = "GtkLabel1";
            this.GtkLabel1.Xalign = 0F;
            this.GtkLabel1.LabelProp = Mono.Unix.Catalog.GetString("<b>Registration Code</b>");
            this.GtkLabel1.UseMarkup = true;
            this.frame1.LabelWidget = this.GtkLabel1;
            this.vbox1.Add(this.frame1);
            Gtk.Box.BoxChild w10 = ((Gtk.Box.BoxChild)(this.vbox1[this.frame1]));
            w10.Position = 0;
            w10.Expand = false;
            w10.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.label1.MnemonicWidget = this.RegistrationCodeEntry;
            this.Show();
            this.RegistrationCodeEntry.Changed += new System.EventHandler(this.OnRegistrationCodeEntryChanged);
            this.RegistrationButton.Clicked += new System.EventHandler(this.OnRegistrationButtonClicked);
        }
    }
}