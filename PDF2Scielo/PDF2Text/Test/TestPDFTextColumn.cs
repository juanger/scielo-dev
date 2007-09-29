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
text += "                  Aneurisma gigante del ap��ndice auricular izquierdo\n";
text += "                    Francisco L Moreno-Mart��nez,\" Osvaldo Gonz��lez Alfonso,**��lvaro L Lagomasino Hidalgo,***\n";
text += "                    Alejandro Gonz��lez D��az,**** Carlos Oliva C��spedes,**** Omaida J L��pez Bernal*****\n";
text += "\n";
text += "\n";
text += "\n";
text += "                    Resumen\n";
text += "\n";
text += "                    Los aneurismas de la aur��cula izquierda son\n";
text += "                    raros y pueden ser cong��nitos o adquiridos. Los\n";
text += "                    que interesan la pared libre o el ap��ndice auri-          Atrial aneurysms are rare entities that can be\n";
text += "                    cular son entidades m��s raras a��n, hasta 2002             congenital or acquired. Those involving the free\n";
text += "                    s��lo exist��an 49 casos reportados en la aur��cula          wall or atrial appendage are even rarer. There\n";
text += "                    izquierda y 8 en la derecha. La manifestaci��n             are only 49 cases reported in the literature in-\n";
text += "                    cl��nica m��s frecuente es la aparici��n de arrit-           volving the lefyatrium and 8 in the right atrium\n";
text += "                    m i a auriculares incesantes o recurrentes y pue-\n";
text += "                          ~                                                   until 2002. The most common clinical presenta-\n";
text += "                    den presentarse embolias sist��micas que pue-              tion is the appearance of recurring or incessant\n";
text += "                    den dar al traste con la vida del paciente.               atrial arrhythmias. In addition, systemic embo-\n";
text += "                    Presentamos el caso de una paciente adoles-               lization may occur as an imminent life-threaten-\n";
text += "                    cente que present�� una embolia cerebral en el             ing event. We present the case of a female teen-\n";
text += "                    curso de una fibrilaci��n auricular y ten��a un aneu-       ager who suffered from an embolic stroke during\n";
text += "                    risma gigante del ap��ndice auricular izquierdo            an atrial fibrillation. She had a giant aneurysm\n";
text += "                    que fue exitosamente extirpado. Se muestran               of the left atrial appendage that was successful-\n";
text += "                    las im��genes de la tomograf��a computada y el              ly removed. Images from computed tomography,\n";
text += "                    aneurisma durante la intervenci��n quir��rgica.             and of the aneurysm during the surgical inter-\n";
text += "                                                                              vention are shown.\n";
text += "                                                                              (Arch Cardiol Mex 2006; 76: 90-94)\n";
text += "\n";
text += "\n";
text += "                    Palabras clave: Aneurisma. Apendice auricular. Arritmias auriculares. Embolias. Cirug��a card��aca\n";
text += "                    Key words: Aneurysm. Atrial appendage. Atrial arrhythmias. Embolisms. Cardiac surgery.\n";
text += "\n";
text += "\n";
text += "                    Introducci��n                                              Los que interesan la pared libre de la A1 o el ap��ndi-\n";
text += "                          os aneurismas de la aur��cula izquierda (AI)         ce auricular son entidades muy raras en la pr��ctica\n";
text += "\n";
text += "                    L     son raros,' algunos los consideran como             ~ardiol��gica.~~' 2002, s��lo exist��an reportes\n";
text += "                                                                                               Hasta\n";
text += "                          extremadamente infrecuente? y pueden                en la literaturade 49 casos en la Al y 8 en la derecha?\n";
text += "                    ser cong��nitos o adquiridos.' Los cong��nitos se           Estos aneurismas pueden estar localizados al\n";
text += "                    presentan como enfermedades aisladas; sin em-             espacio intraperic��rdi~o~,~ secundarios a\n";
text += "                                                                                                              o ser\n";
text += "                    bargo, los adquiridos, se asocian a procesos in-          una ausencia parcial de esta membrana?v5produ-\n";
text += "                    flamatorio~ degenerativos del endocardio.'\n";
text += "                                o                                             ci��ndose una herniaci��n de la AL3\n";
text += "\n";
text += "' Especialista de I Grado en Cardiolog��a. Diplomado en Terapia Intensiva de Adultos. Profesor Colaborador de la Universidad Virtual\n";
text += "de Salud de Cuba.\n";
text += "\" Especialista de I Grado en Anestesiologia y Reanimaci��n. Profesor Asistente.\n";
text += "\"' Especialista de II Grado en Cirug��a Cardiovascular. Profesor Auxiliar.\n";
text += "\"** Especialista de I Grado en Medicina General Integral y Pediatr��a. Profesor Instructor.\n";
text += "\"\"'Especialista de I Grado en Anatom��a Patol��gica. Profesora Instructora.\n";
text += "Hospital Pedi��trico \"Jos�� Luis Miranda\", Santa Clara, Cuba.\n";
text += "Cardiocentro \"Ernesto Che Guevara\" Santa Clara, Cuba\n";
text += "Correspondencia: Dr. Francisco L Moreno-Mart��nez. Servicio Hemodin��mica y Cardiolog��a Intervencionista. Gaveta Postal 313.\n";
text += "Mor��n N��m. 2, 67220. Ciego de Avila, Cuba. E-mail: flmorenomQyahoo.com\n";
text += "Recibido: 5 de abril de 2005\n";
text += "Aceptado: 24 de noviembre de 2005\n";
text += "\n";
text += "\n";
text += "Vol. 76 Numero 1IEnero-Marzo2006:90-94\n";
text += "\n";
                PDFTextColumn pdftc = new PDFTextColumn(text);
                pdftc.GetTextInColumns ();
                Console.WriteLine(pdftc.TextInColumn);  
        }


}
}
}