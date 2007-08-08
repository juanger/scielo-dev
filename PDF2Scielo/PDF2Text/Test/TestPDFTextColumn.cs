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
			Assert.AreEqual(line1.Count, 2);
			Assert.AreEqual(line2.Count, 1);
			Assert.AreEqual(line3.Count, 4);
		}
			
		[Test()]
		public void CreateColumns()
		{
			string text = "columna 1   columna2 \n";
				text +="columna1    Columna 2 \n";
				text +="COLUMNA 1        COLUMNA2";
			string columna1 = "columna 1\n columna1\nCOLUMNA 1";
			string columna2 = " columna2\nColumna 2\nCOLUMNA2";
			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoSpacesPage (1);
			float average = pdftc.GetRepeatPosition (aL, 1);
			pdftc.GetTextInColumns (1, aL, average);
			Console.WriteLine(columna1+columna2);
			Console.WriteLine("Las Columnas");
			Console.WriteLine(pdftc.Column1 );
			Console.WriteLine("Columna 2");
			Console.WriteLine(pdftc.Column2);
		}
		
		[Test()]
		public void ColumnsAverage()
		{       
			string text = "\n";
				text += "\n";
				text += "\n";
				text += "Tabla II. Datos generales.                                                                  Tabla III. Tipos de cardiopatias congénitas\n";
				text += "                                                                       --\n";
				text += "    --          -                                  -                                          -         -    -\n";
				text += "                                                            -\n";
				text += "\n";
				text += "\n";
				text += "                                                                       Valor de p =\n";
				text += "Variables       Down                        Control                                         Diagnóstico                    Down          Control\n";
				text += "Número          16                                                         NS\n";
				text += "                                            14\n";
				text += "                                                                                                                             3              8\n";
				text += "                                                                                            CIV\n";
				text += "                x = 4.7 I años\n";
				text += "Edad (años)                             = 5.3 I años                                        PCA\n";
				text += "                         5.8                       4.5                                                                       3              1\n";
				text += "                                            j?\n";
				text += "                (6 meses - 18 años) (9 meses - 13 años)                                     Canal AV + PCA\n";
				text += "                                                                            NS                                               6              1\n";
				text += "                                                                                            CIV + PCA\n";
text += "Género          Femenino = 11         Femenino = 5                                                                           3              2\n";
text += "                                   }                                        Ns\n";
text += "                                                        )0.5:1                        *\n";
text += "                                                                                            PCA + CIA\n";
text += "                Masculino = 5   2.2'1 Masculino = 9                                                                          1              1\n";
text += "                x = 15.7 r 14.7\n";
text += "Peso (kg)                             x = 17.71 13.2                                        TC Tipo 1                                       1\n";
text += "                                                                                                                             O\n";
text += "                      -               (6.3 - 45.5 kg)                       NS\n";
text += "                (3.8 54 kg)                                                                 Total                            16             14\n";
text += "                x = 0.92 I            x = 0 . 9 8 r 0.28\n";
text += "Talla (m)                  0.33\n";
text += "                (0.54 - 1.57 m)                                                             CIV = comunicac~ón  intewentricular. PCA = persistencia de con-\n";
text += "                                      (0.75 - 1.56 m)                       NS\n";
text += "                                                                                            ducto arterioso, Canal AV = canal atrioventricular comun, CIA =\n";
text += "                                                                                            comunicación interauricular,TC = tronco arterioso comun\n";
text += "\n";
text += "\n";
text += "Tabla IV. Valoración hemodinamica\n";
text += "\n";
text += "                                          Sin oxigeno                                                                 Con oxigeno\n";
text += "                                         Control N = 14\n";
text += "                  Oown N = 16                                     P=                      Oown                          Control                  P=\n";
text += "\n";
text += "               x = 84.87 + 13.6                                               NO. = 11 x = 78.2 + 26.8          No = 13 x = 75.6 + 23.9\n";
text += "PSAP                                                                                                                                             0.75\n";
text += "                                                                              (40 - 124)\n";
text += "               (60 - 104)                                                                                       (44 - 124)\n";
text += "                                                                              No. = 11 x = 56.4 + 22.8          No. = 12 x = 52.5 + 19.5\n";
text += "               x = 64.33 + 11.5\n";
text += "PMAP                                                                                                                                             0.71\n";
text += "                                                                                  -\n";
text += "               (42 - 84)                                                      (25 95)                           (25 - 82)\n";
text += "               x = 13.11 + 11.5                                               No. = 11 x = 10.4 + 13.2          NO. = 12 x = 5.7 + 5.8           0.51\n";
text += "RVP\n";
text += "                      -                                                                                         (04 - 19)\n";
text += "               (10.9 46)                                                      (1.0 - 42.9)\n";
text += "                           *                                                                                    NO = 8 = 0.58 + 0.43\n";
text += "                                                                              NO. = 6 x = 0.52 + 0.46\n";
text += "               í( = 0.91\n";
text += "RpIRs                      0.31                                                                                                                  0.84\n";
text += "                                                                                                                       -\n";
text += "               (0.40 - 1.3)                                                   (0.10 - 1.3)                      (0.12 1.3)\n";
text += "               x = 0.96 i 0.09                                                                                  No. = 9 x = 0.82 +O.l6\n";
text += "PAPlPao                                                                                                                                          0.87\n";
text += "                                                                              N o = 1 0 x = 0 . 8 1 +0.18\n";
text += "               (0.83 - 1.20)                                                  (0.45 - 1.05)                     (0.06 - 1 .O)\n";
text += "               x = 0.95 + 0.27\n";
text += "QpIQs                                                                                                           N 0 = 1 1 x=3.49+1.94            0.12\n";
text += "                                                                              No.=9x=2.14+1.4\n";
text += "               (0.6 - 1.8)                                                                                      (1.10 - 6.20)\n";
text += "                                                                              (0.40 - 5.0)\n";
text += "\n";
text += "PSAP = presión sistólica de la arteria pulmonar, PMAP = presidn media de la arteria pulmonar, Rvp = resistencias vasculares pulmonares,RplRs =relaciónentre\n";
text += "resistencias vasculares pulmonares y sistémicas PAPlPao = relación entre presión pulmonar y sistémica, QplQs = relación entre gasto pulmonar y sictémico\n";
text += "\n";
text += "\n";
text += "Tabla V. Valoración cuantitativa de la angiografia pulmonar en cuña magnificada (APCM).\n";
text += "\n";
text += "                                                   Sin oxígeno                                                               Con oxígeno\n";
text += "                            Down                     Control                P                       Down                       Control              P\n";
text += "                                                              -                           -    --\n";
text += "\n";
text += "\n";
text += "                    No. = 10                     No = 10                                                                   No. = 10\n";
text += "Arterias                                                                  0.72                                                                     0.47\n";
text += "                                                                                          No. = 10\n";
text += "                    X = 321 8.0 + 569.0          x = 3254.0 + 664.0                                                          = 3666.1 + 966.5\n";
text += "                                                                                          x = 3419.8 + 1035.1\n";
text += "proximales\n";
text += "                                                                                                    -                      (2709 - 4950)\n";
text += "                    (2300 - 4620)\n";
text += "(rnicras)                                        (1 650 - 4620)                           (2284 5370)\n";
text += "                    No. = 10\n";
text += "Venas                                            No. = 10                                 No. 10                           NO.= 9\n";
text += "                                                                          0.89                                                                     0.42\n";
text += "                                                                                                                           x = 3936.2 + 1246.4\n";
text += "                                                 x = 3685.0 + 934.0\n";
text += "                    x = 3599.0 + 1286.0                                                   x = 3837 + 1322.8\n";
text += "paralobulares\n";
text += "                    (1363 - 6416)                                                                                          (1697 - 5220)\n";
text += "(micras)                                         (2475 - 5130)                            (2730 - 7071 )\n";
text += "Arterias            No. = 10                     No. = 10                 0.75            No. = 10                         No. = 10                0.1\n";
text += "                    x = 692.0 + 199.0                                                     x = 568 3 + 21 1.O               x = 782.3 + 305.6\n";
text += "                                                 x = 684.3 + 376.0\n";
text += "monopediales\n";
text += "                                                                                          (282 - 1026)\n";
text += "                    (412 - 1283)                 (110 - 1283)                                                              (416 - 1320)\n";
text += "Tiempo de                                        No. = 10                                                                  No. = 10\n";
text += "                    No. = 10                                              0.33                                                                     0.06\n";
text += "                                                                                          No. = 10\n";
text += "                    x = 0.96 + 0.34                                                       x = 0.72 + 0.25                  x = 1.O3 + 0.50\n";
text += "llenado capilar                                  R = 1.18+0.52\n";
text += "                                                                                                                           (0.40 - 2.20)\n";
text += "                    (0.54 - 1.60)                (0.42 - 2.50)                            (0.33-1.17)\n";
text += "(seg)\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                                                  tensión severa, son producidas por las condi-\n";
text += "                          nitos y presión arteria1 pulmonar elevada. A prin-\n";
text += "                          cipios de la década de los 60 De Micheli y c o l s l ~ c i o n e circulatorias particulares del lecho vas-\n";
text += "                                                                                           s\n";
text += "                                                                                  cular pulmonar. Durante los pasados 30 años,\n";
text += "                          establecen que las alteraciones histológicas de\n";
text += "                                                                                  cardiólogos y cirujanos realizaron pruebas para\n";
text += "                          la capa media, en presencia de hipertensión ar-\n";
text += "                          terial moderada y de la capa media e íntima de          determinar la edad óptima de intervención qui-\n";
text += "                                                                                  rúrgica y evitar el desarrollo de hipertensión\n";
text += "                          las arterias pulmonares en presencia de hiper-\n";

      			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoSpacesPage (1);
			float average = pdftc.GetRepeatPosition (aL, 1);
			Console.WriteLine("valor:::::::#########################"+average);
			pdftc.GetTextInColumns (1, aL, average);
			Console.WriteLine("--------------------LAS COLUMNAS----------------"); 
 			Console.WriteLine("--------------Columna1 ------------------------"); 
 			Console.WriteLine(pdftc.Column1); 
 			Console.WriteLine("-----------------------------------------------"); 
 			Console.WriteLine("--------------Columna2 ------------------------"); 
 			Console.WriteLine(pdftc.Column2); 
 			Console.WriteLine("-----------------------------------------------"); 
		}
	}
}
}