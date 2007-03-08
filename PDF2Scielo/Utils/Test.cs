
using System;
using System.IO;

namespace Scielo {
namespace Utils {

public class Test {
			
	public static string PathOfTest ()
	{
		// FIXME: Este es un hack para correr los casos que depende de la
		// locacion cuando se corre el test.
		string path;
		path = Environment.CurrentDirectory;
		path = path.Replace ("bin" + Path.DirectorySeparatorChar + "Debug", String.Empty);
		path = Path.Combine (path, "Test");
		
		return path;
	}
}
}
}