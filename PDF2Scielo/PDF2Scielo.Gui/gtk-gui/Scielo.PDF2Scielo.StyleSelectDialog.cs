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
    
    
    public partial class StyleSelectDialog {
        
        private Gtk.Button buttonCancel;
        
        private Gtk.Button buttonOk;
        
        protected virtual void Build() {
            Stetic.Gui.Initialize();
            // Widget Scielo.PDF2Scielo.StyleSelectDialog
            this.Name = "Scielo.PDF2Scielo.StyleSelectDialog";
            this.WindowPosition = ((Gtk.WindowPosition)(4));
            this.HasSeparator = false;
            // Internal child Scielo.PDF2Scielo.StyleSelectDialog.VBox
            Gtk.VBox w1 = this.VBox;
            w1.Name = "dialog1_VBox";
            w1.BorderWidth = ((uint)(2));
            // Internal child Scielo.PDF2Scielo.StyleSelectDialog.ActionArea
            Gtk.HButtonBox w2 = this.ActionArea;
            w2.Name = "dialog1_ActionArea";
            w2.Spacing = 6;
            w2.BorderWidth = ((uint)(5));
            w2.LayoutStyle = ((Gtk.ButtonBoxStyle)(4));
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonCancel = new Gtk.Button();
            this.buttonCancel.CanDefault = true;
            this.buttonCancel.CanFocus = true;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseStock = true;
            this.buttonCancel.UseUnderline = true;
            this.buttonCancel.Label = "gtk-cancel";
            this.AddActionWidget(this.buttonCancel, -6);
            Gtk.ButtonBox.ButtonBoxChild w3 = ((Gtk.ButtonBox.ButtonBoxChild)(w2[this.buttonCancel]));
            w3.Expand = false;
            w3.Fill = false;
            // Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
            this.buttonOk = new Gtk.Button();
            this.buttonOk.CanDefault = true;
            this.buttonOk.CanFocus = true;
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseStock = true;
            this.buttonOk.UseUnderline = true;
            this.buttonOk.Label = "gtk-ok";
            this.AddActionWidget(this.buttonOk, -5);
            Gtk.ButtonBox.ButtonBoxChild w4 = ((Gtk.ButtonBox.ButtonBoxChild)(w2[this.buttonOk]));
            w4.Position = 1;
            w4.Expand = false;
            w4.Fill = false;
            if ((this.Child != null)) {
                this.Child.ShowAll();
            }
            this.DefaultWidth = 214;
            this.DefaultHeight = 131;
            this.Show();
        }
    }
}
