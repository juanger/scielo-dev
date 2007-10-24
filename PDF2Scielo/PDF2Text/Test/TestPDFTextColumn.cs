// PDFTextColumn: Test for PDFTextColumn class.
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//
using System;
using System.Collections;
using NUnit.Framework;

namespace Scielo {
namespace PDF2Text{
	
	
[TestFixture()]
public class TestPDFTextColumn
{
	[Test()]
	public void Constructor ()
	{
		PDFTextColumn pdftc = new PDFTextColumn ("");
		Type etype = Type.GetType ("Scielo.PDF2Text.PDFTextColumn");
		Assert.IsInstanceOfType (etype, pdftc, "CI01");
		Assert.IsNotNull (pdftc, "CI02");
	}
	
	[Test()]
	public void CreatePages()
	{
		string text = "page 1page2page3page4";
		PDFTextColumn pdftc = new PDFTextColumn (text);
		string [] pagesTmp = pdftc.Pages;
		Assert.AreEqual(pagesTmp.Length, 5, "CP");
	}

	[Test()]
	public void InfoSpacesPage()
	{
		string text = "column    column    column\n";
			text +="                                        column\n";
			text +="    cosas      column      column     column";
		PDFTextColumn pdftc = new PDFTextColumn(text);
		ArrayList spacesInPage = pdftc.GetInfoSpacesPage(1);
		Hashtable line1 = (Hashtable)spacesInPage[0];
		Hashtable line2 = (Hashtable)spacesInPage[1];
		Hashtable line3 = (Hashtable)spacesInPage[2];
		Assert.AreEqual(line1.Count, 2, "ISP1");
		Assert.AreEqual(line2.Count, 1, "ISP2");
		Assert.AreEqual(line3.Count, 4, "ISP3");
	}
			
	[Test()]
	public void CreateColumns()
	{
		string text = "columna 1   columna2 \n";//3 spaces between columns
			text +="columna1    Columna 2 \n";//4 spaces 
			text +="COLUMNA 1        COLUMNA2";//8 spaces
		//columna1 = columna1(2 spaces)\ncolumna1(3 spaces)\nCOLUMNA 1(7spaces)\n"	
		string columna1 = "columna 1  \ncolumna1   \nCOLUMNA 1       \n";
		string columna2 = " columna2 \n Columna 2 \n COLUMNA2\n";
		PDFTextColumn pdftc = new PDFTextColumn(text);
		ArrayList aL = pdftc.GetInfoSpacesPage (1);
		float average = pdftc.GetRepeatPosition (aL, 1);
		pdftc.GetTextInCol(1, aL, average);
		Assert.AreEqual (pdftc.Column1, columna1,"CC1");
		Assert.AreEqual (pdftc.Column2, columna2,"CC2");
	}
		
	[Test()]
	public void GetTextFile()
	{       
		string page1 = "columna1     columna2 \n";
			page1 +="columna1        Columna 2 \n";
			page1 +="COLUMNA 1     COLUMNA2\n";
		string page2 = "columna 1          columna2 \n";
			page2 +="columna1   col1         Columna 2 \n";
			page2 +="COLUMNA 1          COLUMNA2   col2\n";
			page2 +="COLUMNA 1          COLUMNA2\n";
		string text = page1 + page2;
   		PDFTextColumn pdftc = new PDFTextColumn(text);
		pdftc.GetTextInColumns ();
		Console.WriteLine(pdftc.TextInColumn);		
	}
			
	[Test()]
        public void GetTextFirstPage()
			{
				 string text = "";
text += "ARCHIVOS\n"; 
text += "DE CARDIOLOGIA\n"; 
text += "DE MEXICO\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "                   Tejido valvular mitra1 accesorio como causa de obstrucción\n"; 
text += "                   subaórtica. A propósito de un caso\n"; 
text += "                   Nilda Espínola Zavaleta,* Luis Muñoz Castellanos*\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "                   Resumen                                                  Summaiy\n"; 
text += "\n"; 
text += "                   Se presenta el caso de una mujer de 28 años                 ACCESSORY VALVE ~ S S U AS A CAUSE OF\n"; 
text += "                                                                                          M~TRAL             E\n"; 
text += "                   de edad con estenosis subaórtica severa por                   S U B A O R OBSTRUC~ON. A CASE REFQRT\n"; 
text += "                                                                                             ~\n"; 
text += "                   tejido valvular mitral accesorio, en forma de una\n"; 
text += "                   segunda valva media ectópica por debajo de la            It is a case of a 28-year-female with severe sub-\n"; 
text += "                   valva anterior. El diagnóstico se efectuó median-        aortic stenosis due to accesory mitra1 valve tis-\n"; 
text += "                   te ecocardiografía.                                      sue of the type of an accesory media1 valve be-\n"; 
text += "                                                                            love the normal anterior one. The diagnosis was\n"; 
text += "                                                                            performed by echocardiography.\n"; 
text += "                                                                            (Arch Cardiol Mex 2006; 76: 109-1 12)\n"; 
text += "\n"; 
text += "\n"; 
text += "                   Palabras clave: Tejido mitral accesorio. Obstrucción subaórtica. Ecocardiografía.\n"; 
text += "                   Key words: Accesory mitra1 valve tissue. Subaortic obstruction. Echocardiography.\n"; 
text += "\n"; 
text += "\n"; 
text += "                   Cuadro clínico                                           se encontró levantamiento sistólico en ápex y so-\n"; 
text += "\n"; 
text += "\n"; 
text += "                   P\n"; 
text += "                           aciente femenina de 28 años de edad con          plo expulsivo en foco aórtico IImV áspero con\n"; 
text += "                           historia familiar de diabetes mellitus y         irradiación a vasos del cuello y disociación acús-\n"; 
text += "                           miocardiopatía dilatada. Es conocida en          tica de Gallavardin. El electrocardiograma de su-\n"; 
text += "                   nuestra institución desde 1998 por disnea de gran-       perficie en ritmo sinusal que mostró hipertrofia\n"; 
text += "                   des a medianos esfuerzos y palpitaciones ocasio-         ventricular izquierda y sobrecarga sistólica del\n"; 
text += "                   nales. El ecocardiograma realizado en ese mismo          ventrículo izquierdo y el ecocardiograma trans-\n"; 
text += "                   año demostró estenosis subaórtica con gradiente          torácico realizado en diciembre de este año de-\n"; 
text += "                   máximo de 120 mm Hg, sin especificar la causa e          mostró hipertrofia concéntrica del ventrículo iz-\n"; 
text += "                   insuficiencia mitral ligera. El monitoreo electro-       quierdo, obstrucción subaórtica severa con\n"; 
text += "                   cardiográfico de 24 horas evidenció extrasistolia        gradiente máximo de 123 mm Hg por tejido mi-\n"; 
text += "                   ventricular polimórfica y el cateterismo cardíaco        tral valvular accesorio situado debajo de la valva\n"; 
text += "                   efectuado en 1999 hipertrofia septal asimétrica y        anterior (Figs.1, 2 y 3).El gradiente a nivel de la\n"; 
text += "                   obstrucción subaórtica con gradiente máximo in-          válvula aórtica es normal (8 mm Hg). La función\n"; 
text += "                   traventricular de 100mm Hg. Abandonó el segui-           sistólica del ventrículo izquierdo es normal con\n"; 
text += "                   miento desde 1999 hasta el 2005 en que regresó           fracción de expulsión del ventrículo izquierdo\n"; 
text += "                   con la sintomatología antes descrita, además de          del 65%. La paciente se encuentra en espera de\n"; 
text += "                   ortopnea de 2 almohadas. A la exploración física         tratamiento quirúrgico.\n"; 
text += "\n"; 
text += " Ecocardiografía en Consulta Externa. Instituto Nacional de Cardiología \"Ignacio Chávez\".\n"; 
text += "\n"; 
text += "Correspondencia: Dra. Nilda Espínola Zavaleta. Ecocardiografia en Consulta Externa. Instituto Nacional de Cardiología \"Ignacio\n"; 
text += "Chávez\" (INCICH, Juan Badiano Núm. 1, Col. Sección XVI, Tlalpan 14800 México D.F.). E-mail: niesza2001@hotmail.com\n"; 
text += "\n"; 
text += "Recibido: 16 de diciembre de 2005\n"; 
text += "Aceptado: 09 de enero de 2006\n"; 
text += "\n"; 
text += "\n"; 
text += "                                                                                    Vol. 76 Número 11Enero-Marzo 2006:lOS-112\n"; 

                PDFTextColumn pdftc = new PDFTextColumn(text);
                pdftc.GetTextInColumns ();
                Console.WriteLine(pdftc.TextInColumn);  
        }


}
}
}