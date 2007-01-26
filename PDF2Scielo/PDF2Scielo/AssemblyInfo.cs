//
// AssemblyInfo.cs: Assembly Information.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez (anaidv@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.Text;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("PDF2Scielo")]
[assembly: AssemblyDescription("Convierte un archivo PDF a un archivo HTML con formato SciELO")]
[assembly: AssemblyCompany("UNAM - DGB")]
[assembly: AssemblyProduct("pdf2scielo")]
[assembly: AssemblyCopyright("Copyright 2007 DGB UNAM")]
[assembly: AssemblyVersion("0.0.1")]

//[assembly: AssemblyConfiguration("")]
//[assembly: AssemblyTrademark("")]
//[assembly: AssemblyCulture("")]
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

namespace Scielo {
namespace PDF2Scielo {

public class AssemblyInfo {

	private string _name;
	private string _title;
	private string _copyright;
	private string _description;
	private string _version;
	
	public AssemblyInfo () : this (Assembly.GetExecutingAssembly ())
	{
	}
	
	public AssemblyInfo (Assembly a)
	{
		if (a == null)
			throw new ArgumentNullException ("a");
		
		AssemblyName an = a.GetName ();
		_name = an.ToString ();
		
		object [] att = a.GetCustomAttributes (typeof (AssemblyTitleAttribute), false);
		_title = ((att.Length > 0) ? ((AssemblyTitleAttribute) att [0]).Title : String.Empty);
		
		att = a.GetCustomAttributes (typeof (AssemblyCopyrightAttribute), false);
		_copyright = ((att.Length > 0) ? ((AssemblyCopyrightAttribute) att [0]).Copyright : String.Empty);
		
		att = a.GetCustomAttributes (typeof (AssemblyDescriptionAttribute), false);
		_description = ((att.Length > 0) ? ((AssemblyDescriptionAttribute) att [0]).Description : String.Empty);
		
		_version = an.Version.ToString ();
	}
	
	public string Copyright {
		get { return _copyright; }
	}
	
	public string Description {
		get { return _description; }
	}
	
	public string Name {
		get { return _name; }
	}
	
	public string Title {
		get { return _title; }
	}
	
	public string Version {
		get { return _version; }
	}
	
	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder ();
		sb.AppendFormat ("{1} - version {2}{0}{3}{0}{4}{0}",
			Environment.NewLine,
			_title, _version,
			_description,
			_copyright);
			
		return sb.ToString ();
	}
}
}
}