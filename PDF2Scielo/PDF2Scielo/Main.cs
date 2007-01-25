// project created on 1/22/2007 at 4:46 PM
using Gtk;
using System;
using Scielo.PDF2Text;

namespace Scielo {
namespace Prueba {

public class MainClass {
	public static void Main(string[] args)
	{
			Application.Init ();
			DocReader art = new DocReader (args[0]);
			art.CreateFile ();
	}
}
}
}