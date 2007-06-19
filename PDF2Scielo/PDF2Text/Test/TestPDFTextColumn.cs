using System;
using System.Collections;
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
			Assert.AreEqual(pagesTmp.Length, 5, "CP");
		}
			
		[Test()]
		public void CreateColumns()
		{
			PDFTextColumn pdftc = new PDFTextColumn("columna 1   columna2 \n columna1   Columna 2 \n COLUMNA 1        COLUMNA2");
			string page = pdftc.pages[1];		
		}
		
		[Test()]
		public void ColumnsAverage()
		{       
			string text = "Presentación del caso                                      normal y sin fenómenos agregados, no se aus-\n";
				text += "        ombre de 69 años de edad, originario\n";
				text += "				\n";
                    		text += "\n";
                                text += "                                               culta frote pericárdico. Pulmones con estertores\n";
                             	text += "        del estado de Guerrero. Ocupación cam-            crepitantes basales derechos. Abdomen plano,\n";
                                text += "                                               blando y depresible, no se encuentran viscero-\n";
                             	text += "        pesino, con alcoholismo suspendido.\n";
                    		text += "Sin otros antecedentes de importancia para el              megalias ni puntos álgidos, peristaltisrno nor-\n";
                    		text += "padecimiento actual. Negó enfermedades cardio-             mal. Miembros pélvicos con edema hasta tercio\n";
                    		text += "vasculares previas. Ingresa a nuestro hospital con         proximal de ambas piernas, blando y no doloro-\n";
                    		text += "cuadro clínico de tres meses y medio de evolu-             so. Pulsos periféricos normales.";
				
			PDFTextColumn pdftc = new PDFTextColumn(text);
			string page = pdftc.pages[1];
			ArrayList aL = pdftc.GetInfoInPage (1);
			float average = pdftc.GetArithmeticAverageInPage (aL);
			pdftc.GetTextInColumns (1, aL, average);
			//Assert.AreEqual (average, 41, "CA");			
		}
		
	}
	
}
}