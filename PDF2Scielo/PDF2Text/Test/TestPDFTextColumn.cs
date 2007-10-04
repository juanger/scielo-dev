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
text += "                    COMUNICACIONES BREVES\n"; 
text += "\n"; 
text += "\n"; 
text += "                    Aspectos electrocardiogr�ficos de la hipertrofia ventricular\n"; 
text += "                    derecha en el cor pulmonale cr�nico\n"; 
text += "                    Alfredo de Micheli,* Alberto Aranda,* Gustavo A Medrano*\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "\n"; 
text += "                    Resumen\n"; 
text += "\n"; 
text += "                    Se exponen los criterios electrofisiol�gicos para              ELECTROCARDIOGRAPHIC\n"; 
text += "                                                                                                F E A ~ OF ~\n"; 
text += "                                                                                                        R RIGHT\n"; 
text += "                    el diagn�stico electrocardiogr�fico de los creci-         VfNiRIC�LAR HYERTROPHY IN CHRONlC COR PULMONALE\n"; 
text += "                    mientos ventriculares derechos, a la luz de la su-\n"; 
text += "                    cesi�n del proceso de activaci�n del miocardio           The electrophysiological criteria for diagnosing\n"; 
text += "                    ventricular. La hipertrofia ventricular derecha por      right ventricular hypertrophy, characteristic of\n"; 
text += "                    sobrecarga sist�lica sostenida puede ser global o        chronic cor pulmonale, are described. Right ven-\n"; 
text += "                    segmentaria. En el primer caso, p. ej. cardiopat�a       tricular hypertrophy due to a sustained systolic\n"; 
text += "                    hipertensiva pulmonar cr�nica de origen vascu-           overload can be global or regional. In the first\n"; 
text += "                    lar, aumentan la magnitud y la manifestaci�n de          situation, as for example, an idiopathic pulmo-\n"; 
text += "                    todos los principales vectores resultantes de la         nary hypertension, the magnitude and manifes-\n"; 
text += "                    activaci�n de dicho ventriculo: IIs, Ild y Illd. Si la   tation of al1 the main vectors resulting from the\n"; 
text += "                    hipertrofia ventricular derecha es de tipo segmen-       depolarization of this ventricle are increased: IIs\n"; 
text += "                    tario, p. ej. neumopatia cr�nica por obstrucci�n         (septal), Ilr (parietal), and lllr (basal). When the\n"; 
text += "                    bronquial, aumentan la magnitud y la manifesta-          right ventricular hypertrophy is of the segmental\n"; 
text += "                    ci�n s�lo de algunos de los vectores resultantes         (regional) type, as for example, that due to a\n"; 
text += "                    de la activaci�n del ventr�culo derecho. En el caso      chronic bronchial obstruction, the magnitude and\n"; 
text += "                    mencionado, aumenta la manifestaci�n del vec-            manifestation of only some right vectors are in-\n"; 
text += "                    tor basal derecho: Illd. Cuando coexiste isquemia        creased. In this condition, only the magnitude of\n"; 
text += "                    subepic�rdica o transmural del ventr�culo dere-          the right basal vector (Illr) is augmented. In the\n"; 
text += "                    cho, la onda T negativa en las derivaciones pre-         presence of subepicardial or transmural is-\n"; 
text += "                    cordiales derechas y transicionales tiene caracte-       chemia of the right ventricle, negative T waves\n"; 
text += "                    r�sticas de tipo primario y se asocia a prolongaci�n     of primary type are recorded in right precordial\n"; 
text += "                    del intervalo Q-T, en ausencia de acci�n digit�li-       and transitional leads. where the Q-T, interval\n"; 
text += "                    ca. Se presentan dos ejemplos demostrativos de           is prolonged in the absence of digitalis effect.\n"; 
text += "                    las correlaciones existentes entre datos electro-        Two demonstrative examples of the correlations\n"; 
text += "                    cardiogr�ficos y datos anat�micos en la hipertro-        existing between the electrocardiographic and\n"; 
text += "                    fia global y en la segmentaria del ventriculo dere-      anatomical findings in global and regional hy-\n"; 
text += "                    cho, respectivamente.                                    pertrophies, respectively, of the right ventricle\n"; 
text += "                                                                             are presented.\n"; 
text += "                                                                             (Arch Cardiol Mex 2006; 76: 69-74)\n"; 
text += "\n"; 
text += "\n"; 
text += "Abreviaturas:\n"; 
text += "AD = Auricula derecha\n"; 
text += "Al = Auricula izquierda\n"; 
text += "VD = Ventriculo derecho\n"; 
text += "VI = Ventr�culo izquierdo\n"; 
text += "I = lnfundibulo\n"; 
text += " Del Instituto Nacional de Cardiologia \"Ignacio Ch�vez\". M�xico.\n"; 
text += "Correspondencia: Alfredo de Micheli. Instituto Nacional de Cardiolog�a \"Ignacio Ch�vez\" (INCICH, Juan Badiano N�m. 1, Col. Secci�n\n"; 
text += "XVI Tlalpan 14080 M�xico, D.F.).\n"; 
text += "Recibido: 10 de enero de 2006\n"; 
text += "Ace~tado:16 de enero de 2006\n"; 
text += "\n"; 
text += "\n"; 
text += "                                                                                        Vol. 76 N�mero IfEnero-Marzo 2006:69-74\n"; 
                PDFTextColumn pdftc = new PDFTextColumn(text);
                pdftc.GetTextInColumns ();
                Console.WriteLine(pdftc.TextInColumn);  
        }


}
}
}