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
		}
		
		[Test()]
		public void ColumnsAverage()
		{       
			string text = "EVPO. Otros autores como Yamaki y colsZ8de-                       y rápida progresión d e la EVPO que los niños\n";
text += "mostraron que los pacientes con síndrome d e                      con cromosomas normales. Los hallazgos en re-\n";
text += "Down tienen además adelgazamiento d e la capa                     lación a una mejor respuesta a la aplicación d e\n";
text += "media d e las arterias, lo que favorece la prolife-               oxígeno del diámetro d e las arterias monopedia-\n";
text += "ración fibrosa d e la capa intima. Además s e ha                  les en el grupo Down, hace suponer que estos\n";
text += "observado q u e cuando estos pacientes tienen                     pacientes pudieran presentar un componente más\n";
text += "cardiopatía presentan asociación a PCA con *                      reactivo. Estos hallazgos pueden ser importan-\n";
text += "mayor frecuencia, lo que también predispone a                     tes en la clínica y en el manejo quirúrgico, to-\n";
text += "desarrollo precoz d e la H A P y a mayor severi-                  mando en cuenta que cuando son sometidos a\n";
text += "dad.lo Esta asociación d e cardiopatía y PCA tam-                 cirugía e n el momento oportuno su evolución es\n";
text += "bién se demuestra en el presente trabajo.                         satisfactoria, sin embargo e s importante tener\n";
text += "Nosotros observamos que a pesar d e que en el                     presente q u e las complicaciones extracardíacas\n";
text += "grupo con síndrome d e Down se presentan car-                     pueden presentarse con mayor frecuencia en los\n";
text += "diopatías con mayor predisposición a desarro-                     pacientes con esta cromosomopatía y ocasionar\n";
text += "llar hipertensión arteria1 pulmonar irreversible                  mala evolución,30como sucedió con dos pacien-\n";
text += "como el canal atrioventricular común, el com-                     tes que fallecieron en el perioperatorio por com-\n";
text += "portamiento hemodinámico d e la H A P fue simi-                   plicaciones infecciosas.\n";
text += "lar a los niños con cromosomas normales, a dife-                  E s importante mencionar que en ambos grupos\n";
text += "rencia d e lo reportado por Clapp y ~ o l s , 2 ~ q u i e n e s   la valoración cuantitativa y morfológica d e la\n";
text += "reportan que los niños con síndrome d e Down y                    APCM fue un parámetro determinante como pre-\n";
text += "canal AV tienen una elevación mayor d e las Rvp                   dictor d e enfermedad vascular pulmonar.\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                        Referencias\n";
text += "\n";
text += "1. BAIRD SADOVNICK Life fables for Down\n";
text += "            PA,             AD:                                   10. WILSON GROVER NELLY Hyperfensi-\n";
text += "                                                                              SK.                     CA:\n";
text += "                                                                                           MH,\n";
text += "    Syndrome. Hum Genet 1989; 82: 291-292.                            ve pulmonary vascular disease in Down's syn-\n";
text += "2. KORENBERC     J, KURNIT Molecular and sfochasfic\n";
text += "                          D:                                          dronie. 1 Pediat 1979; 95: 722-726.\n";
text += "                                                                  11. VÁz~uez    ANTONA C A L D E R ~GILM. G A R CY A\n";
text += "                                                                                         C,            J. N               ~\n";
text += "    basis of congenital hearr defects in Down Syndro-\n";
text += "    me. En: Marino B. Pueschel SM. \"Heart disease                      OTERO BUENDLAA.\n";
text += "                                                                              A,              MART~NEL  M, ATTIE E s ~ u -\n";
text += "                                                                                                                   F:\n";
text += "    in persons with Down syndrome\". Baltirnore: Bro-                   dio en pacienfes con hipertensión arteria1 secun-\n";
text += "    okes. 1996: 21-38.                                                 daria a cardiopa~ía congénita medianre angiogra-\n";
text += "3. GREENWOOD NADAS The Clinical Course\n";
text += "                  RD,        AS:                                      fía pulmonar en cuña magnificada. Arch lnst\n";
text += "    of Cardiac Disease. En: Down's Syndrome. Pe-                       Cardiol Mex 1991; 61(4) supl: 4.\n";
text += "    diatrics 1976; 58: 893-897.                                   12. LA FARGE MIET~INEM The esfimulation of\n";
text += "                                                                                  CG,              OS:\n";
text += "4. MARINO Pat~errisof congeniral hearr disease\n";
text += "              B:                                                       oqgen consumprion. Cardiovasc Res 1970; 4: 23-27\n";
text += "                                                                  13. NIHILL MCNAMARA Magnification pitl-\n";
text += "                                                                               MR.                DG:\n";
text += "    and associated cardiacanomalies in children with\n";
text += "    Down syzdrome. En: Marino B. Pueschel SM.                          monary wedge angiography in the evaluafion of\n";
text += "    \"Heart disease in persons with Down ' S syndro-                    children wifhcongeniral hean disease and pidmo-\n";
text += "    me\". Baltimore: Brookes, 1996: 133-40.                             nary hyperfension. Circulation 1978; 58-6: 1094-\n";
text += "5 . RODR~GUEZ-HERNANDEZ     L. REYES-NUNEZ  J: Cardio-                 1106.\n";
text += "    pafías congénitas en e1 síndrome de Down. Bol                 14. RABINOVITCH   M. KEANE FELLOWS CASTANE-\n";
text += "                                                                                                JF,         EK.\n";
text += "    Méd Hosp lnfant Mex 1984; 41 : 622-625.                            DA AR, REID L: Quanfifarive  analysis of thepitlrno-\n";
text += "6. DE RUBENS DELPOZZO PABLOS CALDE-\n";
text += "                 FJ,           MB,         JL,                         nary wedge angiogram in congenital heart defecfs.\n";
text += "    R6N C, C A S T R E J ~ N\n";
text += "                        R: Malformaciones cardíacas                    Circulation 1981: 63: 152- 164.\n";
text += "    en los niños con síndrome de Down. Rev Esp Car-               15. CIVIN  WB, EDWARDS Pathology of fhe pulmo-\n";
text += "                                                                                             JE:\n";
text += "    dio1 2003; 56: 894-899.                                            nary vascular free. A comparison of infrapulmo-\n";
text += "7. LAURSEN Congeniral heart disease in Down 'S\n";
text += "              HB:                                                      nary arteries in Eisenmenger's complex and in\n";
text += "    syndrome. Br Hearc J 1976; 39: 72 1-726.                           stenosis of osfium infudibuli associated with bi-\n";
text += "8. CHI   TL, KROVETZ Thcpulmonary vasciclarbed\n";
text += "                      JL:                                              venfricular origin of the aorta. Circulation 1950;\n";
text += "    in children with Down's syndronie. J Pediatr 1975;                 2: 545-553.\n";
text += "                                                                  16. SELZER LAQUEUR i'he Eisennlenger com-\n";
text += "                                                                               A,            GL:\n";
text += "    86: 533-538.\n";
text += "9. PLETT TANDON MOLLER EDWARDS\n";
text += "           JA,                       JH,           JE:\n";
text += "                         R,                                           plex and its relation to uncomplicafed defecr of the\n";
text += "    Hyprrterzsive Prrltnorzary Vascular Disease. Arch                  ventricular septutn. Arch Intem Med 195 1; 87:\n";
text += "    Pathol 1974; 97: 187- 188.                                         2 18-226.\n";



          				
			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoInPage (1);
			//float average = pdftc.GetArithmeticAverageInPage (aL, 1);
			float average = pdftc.GetRepeatPosition (aL, 1);
			Console.WriteLine("valor:::::::#########################"+average);
			pdftc.GetTextInColumns (1, aL, average);
			//Assert.AreEqual (average, 41, "CA");			
		}
		
	}
	
}
}