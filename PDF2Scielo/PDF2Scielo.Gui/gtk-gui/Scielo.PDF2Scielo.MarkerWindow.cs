// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.42
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Scielo.PDF2Scielo {
    
    
    public partial class MarkerWindow {
        
        private Gtk.Action File;
        
        private Gtk.Action Open;
        
        private Gtk.Action Quit;
        
        private Gtk.Action Tools;
        
        private Gtk.Action Help;
        
        private Gtk.Action About;
        
        private Gtk.Action Markup;
        
        private Gtk.Action Save;
        
        private Gtk.Action SaveAs;
        
        private Gtk.Action Normalize;
        
        private Gtk.Action Preview;
        
        private Gtk.ToggleAction dialogError;
        
        private Gtk.ToggleAction dialogWarning;
        
        private Gtk.ToggleAction dialogInfo;
        
        private Gtk.VBox vbox1;
        
        private Gtk.MenuBar menubar1;
        
        private Gtk.Toolbar toolbar1;
        
        private Gtk.VPaned vpaned1;
        
        private Gtk.ScrolledWindow scrolledwindow1;
        
        private Gtk.TextView textview;
        
        private Gtk.Frame frame1;
        
        private Gtk.Alignment GtkAlignment;
        
        private Gtk.VBox vbox2;
        
        private Gtk.Toolbar toolbar2;
        
        private Gtk.ScrolledWindow scrolledwindow2;
        
        private Gtk.TreeView treeview1;
        
        private Gtk.Label GtkLabel3;
        
        private Gtk.Statusbar statusbar1;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget Scielo.PDF2Scielo.MarkerWindow
            Gtk.UIManager w1 = new Gtk.UIManager();
            Gtk.ActionGroup w2 = new Gtk.ActionGroup("Default");
            this.File = new Gtk.Action("File", Mono.Unix.Catalog.GetString("_File"), null, null);
            this.File.ShortLabel = Mono.Unix.Catalog.GetString("_File");
            w2.Add(this.File, null);
            this.Open = new Gtk.Action("Open", Mono.Unix.Catalog.GetString("_Open"), null, "gtk-open");
            this.Open.ShortLabel = Mono.Unix.Catalog.GetString("_Open");
            w2.Add(this.Open, null);
            this.Quit = new Gtk.Action("Quit", Mono.Unix.Catalog.GetString("_Quit"), null, "gtk-quit");
            this.Quit.ShortLabel = Mono.Unix.Catalog.GetString("_Quit");
            w2.Add(this.Quit, null);
            this.Tools = new Gtk.Action("Tools", Mono.Unix.Catalog.GetString("_Tools"), null, null);
            this.Tools.ShortLabel = Mono.Unix.Catalog.GetString("_Tools");
            w2.Add(this.Tools, null);
            this.Help = new Gtk.Action("Help", Mono.Unix.Catalog.GetString("_Help"), null, null);
            this.Help.ShortLabel = Mono.Unix.Catalog.GetString("_Help");
            w2.Add(this.Help, null);
            this.About = new Gtk.Action("About", Mono.Unix.Catalog.GetString("About"), null, "gnome-stock-about");
            this.About.ShortLabel = Mono.Unix.Catalog.GetString("About");
            w2.Add(this.About, null);
            this.Markup = new Gtk.Action("Markup", Mono.Unix.Catalog.GetString("_Markup"), null, "gtk-convert");
            this.Markup.Sensitive = false;
            this.Markup.ShortLabel = Mono.Unix.Catalog.GetString("_Markup");
            w2.Add(this.Markup, "<Control><Mod2>m");
            this.Save = new Gtk.Action("Save", Mono.Unix.Catalog.GetString("_Save"), null, "gtk-save");
            this.Save.ShortLabel = Mono.Unix.Catalog.GetString("_Save");
            w2.Add(this.Save, null);
            this.SaveAs = new Gtk.Action("SaveAs", Mono.Unix.Catalog.GetString("Save _As"), null, "gtk-save-as");
            this.SaveAs.ShortLabel = Mono.Unix.Catalog.GetString("Save _As");
            w2.Add(this.SaveAs, null);
            this.Normalize = new Gtk.Action("Normalize", Mono.Unix.Catalog.GetString("_Normalize"), null, "gtk-execute");
            this.Normalize.Sensitive = false;
            this.Normalize.ShortLabel = Mono.Unix.Catalog.GetString("_Normalize");
            w2.Add(this.Normalize, "<Control><Mod2>n");
            this.Preview = new Gtk.Action("Preview", Mono.Unix.Catalog.GetString("_Preview"), null, "gtk-print-preview");
            this.Preview.Sensitive = false;
            this.Preview.ShortLabel = Mono.Unix.Catalog.GetString("_Preview");
            w2.Add(this.Preview, null);
            this.dialogError = new Gtk.ToggleAction("dialogError", null, null, "gtk-dialog-error");
            w2.Add(this.dialogError, null);
            this.dialogWarning = new Gtk.ToggleAction("dialogWarning", null, null, "gtk-dialog-warning");
            w2.Add(this.dialogWarning, null);
            this.dialogInfo = new Gtk.ToggleAction("dialogInfo", null, null, "gtk-dialog-info");
            w2.Add(this.dialogInfo, null);
            w1.InsertActionGroup(w2, 0);
            this.AddAccelGroup(w1.AccelGroup);
            this.Name = "Scielo.PDF2Scielo.MarkerWindow";
            this.Title = Mono.Unix.Catalog.GetString("Marcador Automatico PDF2Scielo");
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            // Container child Scielo.PDF2Scielo.MarkerWindow.Gtk.Container+ContainerChild
            this.vbox1 = new Gtk.VBox();
            this.vbox1.Name = "vbox1";
            // Container child vbox1.Gtk.Box+BoxChild
            w1.AddUiFromString("<ui><menubar name='menubar1'><menu action='File'><menuitem action='Open'/><separator/><menuitem action='Save'/><menuitem action='SaveAs'/><separator/><menuitem action='Quit'/></menu><menu action='Tools'><menuitem action='Normalize'/><menuitem action='Markup'/><menuitem action='Preview'/></menu><menu action='Help'><menuitem action='About'/></menu></menubar></ui>");
            this.menubar1 = ((Gtk.MenuBar)(w1.GetWidget("/menubar1")));
            this.menubar1.Name = "menubar1";
            this.vbox1.Add(this.menubar1);
            Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
            w3.Position = 0;
            w3.Expand = false;
            w3.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            w1.AddUiFromString("<ui><toolbar name='toolbar1'><toolitem action='Open'/><separator/><toolitem action='Normalize'/><toolitem action='Markup'/><separator/><toolitem action='Preview'/></toolbar></ui>");
            this.toolbar1 = ((Gtk.Toolbar)(w1.GetWidget("/toolbar1")));
            this.toolbar1.Name = "toolbar1";
            this.toolbar1.ShowArrow = false;
            this.toolbar1.ToolbarStyle = ((Gtk.ToolbarStyle)(0));
            this.vbox1.Add(this.toolbar1);
            Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox1[this.toolbar1]));
            w4.Position = 1;
            w4.Expand = false;
            w4.Fill = false;
            // Container child vbox1.Gtk.Box+BoxChild
            this.vpaned1 = new Gtk.VPaned();
            this.vpaned1.CanFocus = true;
            this.vpaned1.Name = "vpaned1";
            this.vpaned1.Position = 160;
            // Container child vpaned1.Gtk.Paned+PanedChild
            this.scrolledwindow1 = new Gtk.ScrolledWindow();
            this.scrolledwindow1.CanFocus = true;
            this.scrolledwindow1.Name = "scrolledwindow1";
            this.scrolledwindow1.VscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow1.HscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow1.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow1.Gtk.Container+ContainerChild
            this.textview = new Gtk.TextView();
            this.textview.CanFocus = true;
            this.textview.Name = "textview";
            this.scrolledwindow1.Add(this.textview);
            this.vpaned1.Add(this.scrolledwindow1);
            Gtk.Paned.PanedChild w6 = ((Gtk.Paned.PanedChild)(this.vpaned1[this.scrolledwindow1]));
            w6.Resize = false;
            w6.Shrink = false;
            // Container child vpaned1.Gtk.Paned+PanedChild
            this.frame1 = new Gtk.Frame();
            this.frame1.Name = "frame1";
            this.frame1.ShadowType = ((Gtk.ShadowType)(0));
            this.frame1.LabelXalign = 0F;
            // Container child frame1.Gtk.Container+ContainerChild
            this.GtkAlignment = new Gtk.Alignment(0F, 0F, 1F, 1F);
            this.GtkAlignment.Name = "GtkAlignment";
            this.GtkAlignment.LeftPadding = ((uint)(12));
            // Container child GtkAlignment.Gtk.Container+ContainerChild
            this.vbox2 = new Gtk.VBox();
            this.vbox2.Name = "vbox2";
            this.vbox2.Spacing = 6;
            // Container child vbox2.Gtk.Box+BoxChild
            w1.AddUiFromString("<ui><toolbar name='toolbar2'><toolitem action='dialogError'/><separator/><toolitem action='dialogWarning'/><separator/><toolitem action='dialogInfo'/></toolbar></ui>");
            this.toolbar2 = ((Gtk.Toolbar)(w1.GetWidget("/toolbar2")));
            this.toolbar2.Name = "toolbar2";
            this.toolbar2.ShowArrow = false;
            this.toolbar2.ToolbarStyle = ((Gtk.ToolbarStyle)(3));
            this.toolbar2.IconSize = ((Gtk.IconSize)(1));
            this.vbox2.Add(this.toolbar2);
            Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.vbox2[this.toolbar2]));
            w7.Position = 0;
            w7.Expand = false;
            w7.Fill = false;
            // Container child vbox2.Gtk.Box+BoxChild
            this.scrolledwindow2 = new Gtk.ScrolledWindow();
            this.scrolledwindow2.CanFocus = true;
            this.scrolledwindow2.Name = "scrolledwindow2";
            this.scrolledwindow2.VscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow2.HscrollbarPolicy = ((Gtk.PolicyType)(1));
            this.scrolledwindow2.ShadowType = ((Gtk.ShadowType)(1));
            // Container child scrolledwindow2.Gtk.Container+ContainerChild
            this.treeview1 = new Gtk.TreeView();
            this.treeview1.CanFocus = true;
            this.treeview1.Name = "treeview1";
            this.treeview1.HeadersClickable = true;
            this.scrolledwindow2.Add(this.treeview1);
            this.vbox2.Add(this.scrolledwindow2);
            Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.vbox2[this.scrolledwindow2]));
            w9.Position = 1;
            this.GtkAlignment.Add(this.vbox2);
            this.frame1.Add(this.GtkAlignment);
            this.GtkLabel3 = new Gtk.Label();
            this.GtkLabel3.Name = "GtkLabel3";
            this.GtkLabel3.LabelProp = Mono.Unix.Catalog.GetString("<b>Messages</b>");
            this.GtkLabel3.UseMarkup = true;
            this.frame1.LabelWidget = this.GtkLabel3;
            this.vpaned1.Add(this.frame1);
            Gtk.Paned.PanedChild w12 = ((Gtk.Paned.PanedChild)(this.vpaned1[this.frame1]));
            w12.Resize = false;
            this.vbox1.Add(this.vpaned1);
            Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.vbox1[this.vpaned1]));
            w13.Position = 2;
            // Container child vbox1.Gtk.Box+BoxChild
            this.statusbar1 = new Gtk.Statusbar();
            this.statusbar1.Name = "statusbar1";
            this.statusbar1.Spacing = 6;
            this.vbox1.Add(this.statusbar1);
            Gtk.Box.BoxChild w14 = ((Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
            w14.Position = 3;
            w14.Expand = false;
            w14.Fill = false;
            this.Add(this.vbox1);
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 614;
            this.DefaultHeight = 414;
            this.Show();
            this.DeleteEvent += new Gtk.DeleteEventHandler(this.OnDeleteEvent);
            this.Open.Activated += new System.EventHandler(this.OnOpenActivated);
            this.Quit.Activated += new System.EventHandler(this.OnQuitActivated);
            this.About.Activated += new System.EventHandler(this.OnAboutActivated);
            this.Markup.Activated += new System.EventHandler(this.OnMarkupActivated);
            this.Normalize.Activated += new System.EventHandler(this.OnNormalizeActivated);
            this.Preview.Activated += new System.EventHandler(this.OnPreviewActivated);
            this.dialogError.Toggled += new System.EventHandler(this.OnMessageFilterToggled);
            this.dialogWarning.Toggled += new System.EventHandler(this.OnMessageFilterToggled);
            this.dialogInfo.Toggled += new System.EventHandler(this.OnMessageFilterToggled);
        }
    }
}
