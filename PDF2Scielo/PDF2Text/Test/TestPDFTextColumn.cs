using System;
using NUnit.Framework;

namespace Scielo {
namespace PDF2Text{
	
	
	[TestFixture()]
	public class TestPDFTextColumn
	{
		
		[Test()]
		public void CreatePages()
		{
			PDFTextColumn pdftc = new PDFTextColumn("page1  page 2    page 3  page 4     ");
			string [] pagesTmp = pdftc.pages;
			Console.WriteLine("the number pages:" + pagesTmp.Length );
		}
	}
}
}