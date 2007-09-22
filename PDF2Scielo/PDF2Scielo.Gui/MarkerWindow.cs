//
// MarkerWindow.cs: Main Window of application.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using Gtk;
using System;
using System.IO;
using Scielo.PDF2Text;
using Scielo.Markup;
using Scielo.Utils;
using Gecko;

namespace Scielo.PDF2Scielo {
public partial class MarkerWindow: Gtk.Window {
	private RawDocument rdocument;
	private NormDocument ndocument;
	private HTMLDocument html_document;
	private PreviewDialog preview = null;
	private Gtk.ListStore store;
	private Gtk.TreeModelFilter filter;
	
	public MarkerWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		rdocument = null;
		ndocument = null;
		html_document = null;
		AddColumns ();
	}
	
	private void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	private void OnQuitActivated (object sender, System.EventArgs e)
	{
		Application.Quit ();
	}
	
	private void OnOpenActivated (object sender, System.EventArgs e)
	{
		OpenPDFDialog dialog = new OpenPDFDialog ();
		if (dialog.Run () == (int) ResponseType.Ok) {
			Uri uri = new Uri (dialog.Document);
			PDFPoppler reader = new PDFPoppler (uri);
			rdocument = reader.CreateRawDocument ();
			textview.Buffer.Text = rdocument.GetText ();
			Markup.Sensitive = true;
			Normalize.Sensitive = true;
			store.Clear ();
//			Logger.ClearList ();
		}
		
		dialog.Destroy ();
	}
	
	private void OnMarkupActivated (object sender, System.EventArgs e)
	{
		if (ndocument == null) {
			StyleSelectDialog dialog = new StyleSelectDialog ();
			
			if (dialog.Run () == (int) ResponseType.Ok) {
				try {
					string format = dialog.Box.ActiveText;
					if (format != null)
						ndocument = rdocument.Normalize (format);
					
					MarkupHTML marker = new MarkupHTML (ndocument);
					html_document = marker.CreateHTMLDocument ();
					textview.Buffer.Text = html_document.GetText ();
					Markup.Sensitive = false;
					Normalize.Sensitive = false;
					Preview.Sensitive = true;
				} catch (StyleException exception){
					MessageDialog md = new MessageDialog (this,
						DialogFlags.DestroyWithParent, 
						MessageType.Error, 
						ButtonsType.Ok, 
						exception.Message);
					md.Run ();
					md.Destroy();
				} catch (NormalizerException exception){
					MessageDialog md = new MessageDialog (this,
						DialogFlags.DestroyWithParent, 
						MessageType.Error, 
						ButtonsType.Ok, 
						exception.Message);
					md.Run ();
					md.Destroy();
				}
			}
			
			dialog.Destroy ();
		} else {
			MarkupHTML marker = new MarkupHTML (ndocument);
			html_document = marker.CreateHTMLDocument ();
			textview.Buffer.Text = html_document.GetText ();
			Markup.Sensitive = false;
			Normalize.Sensitive = false;
			Preview.Sensitive = true;
		}
	}

	private void OnNormalizeActivated (object sender, System.EventArgs e)
	{
		StyleSelectDialog dialog = new StyleSelectDialog ();
		if (dialog.Run () == (int) ResponseType.Ok) {
			try {
				string format = dialog.Box.ActiveText;
				
				if (format != null) {
					ndocument = rdocument.Normalize (format);
					textview.Buffer.Text = ndocument.GetText ();
					Normalize.Sensitive = false;
				}
			} catch (StyleException exception){
				MessageDialog md = new MessageDialog (this,
					DialogFlags.DestroyWithParent, 
					MessageType.Error, 
					ButtonsType.Ok, 
					exception.Message);
				md.Run ();
				md.Destroy();
			}catch (NormalizerException exception){
				MessageDialog md = new MessageDialog (this,
					DialogFlags.DestroyWithParent, 
					MessageType.Error, 
					ButtonsType.Ok, 
					exception.Message);
				md.Run ();
				md.Destroy();
			} finally {
				store.Clear ();
				DisplayMessages ();
				Logger.ClearList ();
			}
		}
		
		dialog.Destroy ();
	}

	private void OnPreviewActivated (object sender, System.EventArgs e)
	{
		if (preview == null)
			preview = new PreviewDialog (html_document.GetText ());
		else
			preview.Render (html_document.GetText ());
		
		preview.Run ();
		preview.Hide ();
	}

	private void OnAboutActivated (object sender, System.EventArgs e)
	{
		AboutDialog dialog = new AboutDialog ();
		dialog.Run ();
	}
	
	private void AddColumns ()
	{
		Gtk.TreeViewColumn iconColumn = new Gtk.TreeViewColumn ();
		iconColumn.Title = "Type";
		Gtk.CellRendererPixbuf iconRender = new Gtk.CellRendererPixbuf ();
		iconColumn.PackStart (iconRender, true);
		
		Gtk.TreeViewColumn msgColumn = new Gtk.TreeViewColumn ();
		msgColumn.Title = "Message";
		Gtk.CellRendererText msgRender = new Gtk.CellRendererText ();
		msgColumn.PackStart (msgRender, true);
		
		Gtk.TreeViewColumn typeColumn = new Gtk.TreeViewColumn ();
		typeColumn.Visible = false;
		Gtk.CellRendererText typeRender = new Gtk.CellRendererText ();
		typeColumn.PackStart (typeRender, true);
		
		treeview1.AppendColumn (typeColumn);
		treeview1.AppendColumn (iconColumn);
		treeview1.AppendColumn (msgColumn);
		
		typeColumn.AddAttribute (typeRender, "text", 0);
		iconColumn.AddAttribute (iconRender, "pixbuf", 1);
		msgColumn.AddAttribute (msgRender, "text", 2);
		
		store = new Gtk.ListStore (typeof (string), typeof (Gdk.Pixbuf), typeof (string));
		filter = new Gtk.TreeModelFilter (store, null);
		treeview1.Model = filter;
		filter.VisibleFunc = new Gtk.TreeModelFilterVisibleFunc (FilterTree);
		dialogInfo.Active = true;
		dialogWarning.Active = true;
		dialogError.Active = true;
		
	}
	
	private void DisplayMessages ()
	{
		foreach (LogEntry entry in Logger.List){
			Gdk.Pixbuf icon;
			string message = entry.Message;
			
			switch (entry.Level) {
			case Level.ERROR:
				icon = scrolledwindow2.RenderIcon (Gtk.Stock.DialogError,
					Gtk.IconSize.Menu,
					"");
				break;
			case Level.WARNING:
				icon = scrolledwindow2.RenderIcon (Gtk.Stock.DialogWarning,
					Gtk.IconSize.Menu,
					"");
				break;
			case Level.INFO:
				icon = scrolledwindow2.RenderIcon (Gtk.Stock.DialogInfo,
					Gtk.IconSize.Menu,
					"");
				break;
			default:
				icon = scrolledwindow2.RenderIcon (Gtk.Stock.Cancel,
					Gtk.IconSize.Menu,
					"");
				break;
			}
			
			store.AppendValues (entry.Level.ToString (), icon, message);
		}
		
		dialogInfo.Active = false;
		dialogWarning.Active = true;
		dialogError.Active = true;

	}
	
	private bool FilterTree (Gtk.TreeModel model, Gtk.TreeIter iter)
	{
		try {
			string level = model.GetValue (iter, 0).ToString ();
			if (dialogInfo.Active && level.Equals ("INFO"))
				return true;
			if (dialogWarning.Active && level.Equals ("WARNING"))
				return true;
			if (dialogError.Active && level.Equals ("ERROR"))
				return true;
			else
				return false;
		} catch {
			return false;
		}
	}
	
	protected virtual void OnMessageFilterToggled (object sender, System.EventArgs e)
	{
		if (filter != null)
			filter.Refilter ();
	}
}
}