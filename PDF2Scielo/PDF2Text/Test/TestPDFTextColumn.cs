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
			Assert.AreEqual(pagesTmp.Length, 5, "CP");
		}
			
		//[Test()]
		/*public void CreateColumns()
		{
			PDFTextColumn pdftc = new PDFTextColumn("columna 1   columna2 \n columna1       Columna 2 \n COLUMNA 1        COLUMNA2");
			string page = pdftc.pages[1];
			pdftc.GetInfoInPage (1);			
		}*/
		
		[Test()]
		public void TextColumns()
		{
			PDFTextColumn pdftc = new PDFTextColumn(" Resumen "+
                    +"\n"+
                    +"Se exponen los criterios electrofisiolÓgicos para              ELECTROCARDIOGRAPHIC\n"+
                    +"                                                                            F E A ~ OF ~\n"+
                    +"                                                                                        R RIGHT\n"+
                    +"el diagnóstico electrocardiográfico de los creci-         VfNiRICüLAR HYERTROPHY IN CHRONlC COR PULMONALE\n"+
                    +"mientos ventriculares derechos, a la luz de la su-\n"+
                    +"cesión del proceso de activación del miocardio           The electrophysiological criteria for diagnosing\n"+
                    +"ventricular. La hipertrofia ventricular derecha por      right ventricular hypertrophy, characteristic of\n"+
                    +"sobrecarga sistólica sostenida puede ser global o        chronic cor pulmonale, are described. Right ven-\n"+
                    +"segmentaria. En el primer caso, p. ej. cardiopatía       tricular hypertrophy due to a sustained systolic\n"+
                    +"hipertensiva pulmonar crónica de origen vascu-           overload can be global or regional.      \n"+
                    +""+
                    +"columna1                 columna2\n");
			string page = pdftc.pages[1];
			pdftc.GetInfoInPage (1);
		}		
	}
	
}
}







