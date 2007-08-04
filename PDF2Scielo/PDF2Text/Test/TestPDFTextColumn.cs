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
			string [] pagesTmp = pdftc.Pages;
			Assert.AreEqual(pagesTmp.Length, 5, "CP");
		}
			
		[Test()]
		public void CreateColumns()
		{
			PDFTextColumn pdftc = new PDFTextColumn("columna 1   columna2 \n columna1   Columna 2 \n COLUMNA 1        COLUMNA2");
			ArrayList aL = pdftc.GetInfoInPage (1);
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
			//Assert.AreEqual (average, 41, "CA");			
		}
		
		[Test()]
		public void ColumnsAverage()
		{       
			string text = "\n";
text += "\n";
text += "\n";
text += "study population are described in Table 1. Dis-        Arnong the 391 analyzed segments, we found\n";
text += "lipidernia (n = 16, 61.5%) and smoking (n = 14,        more segrnents that showed perfusionírnetabolisrn\n";
text += "53.8%) were the rnost prevalent coronary risk          misrnatch than perfusionírnetabolism match (127\n";
text += "                                                       vs 67). A bad correlation (r < 0.7) between SPECT\n";
text += "factors; 10 patients (43.4%) had diagnosis of non-\n";
text += "insulin dependent diabetcs rnellitus; rnost (n =       and PET was observed in the left anterior descen-\n";
text += "16, 69.5%) of thern were in NYHA class 1. The          dent artery and in the right coronary artery terri-\n";
text += "most prevalent myocardial infarction localiia-         tories when vascular territories were analyzed.\n";
text += "tion was anteroseptal, in 12 patients (52.1%).         Using the segrnental model, we showed a low\n";
text += "                                                       correlation (p < 0.50) between both techniques\n";
text += "Fourteen patients (60.8%) presented triple-ves-\n";
text += "se1 disease, as docurnented by coronary angiog-        in 5 of the 7 segrnents assigned to the LAD terri-\n";
text += "raphy. A total of 391 segrnents were analyzed,         tory (al1 except basal anterior and basal an-\n";
text += "                                                       teroseptal) in 3 of the 5 segrnents assigned to the\n";
text += "127 were classified as normal, 67 matched, 182\n";
text += "mismatched and 15 reverse rnisrnatched seg-            RCA territory (al1 except basal inferoseptal and\n";
text += "ments. Correlation between rnethods for each           rnid inferoseptal) and in 2 of the 5 segments as-\n";
text += "segrnent is shown in Figure 1. Differences in the      signed to the LCx (basal inferolateral and rnid\n";
text += "analysis were found arnong both rnethods. No           inferolateral), which rneans that PET detected\n";
text += "differences were found regarding the score as-         more viable segrnents than SPECT did (1 1 of 17\n";
text += "signed by SPECT and PET in the analysis by             segrnents analyzed) and that these differences\n";
text += "vascular territory between diabetic vs non-dia-        are statistically significant.\n";
text += "betic patients. Differences were encountered be-       In this study, I8FDG PET detected more viable\n";
text += "tween the number of segrnents detected as non-         segments (30%) than 201TI    SPECT. It means that\n";
text += "viable by SPECT and PET, most of the segrnents         3 of each 10 patients may be diagnosed as with-\n";
text += "considered non-viable by SPECT were found              out rnyocardial viability when in fact there is\n";
text += "viable with PET (Fig. 1).                              viability present.\n";
text += "Arnong the 39 1 segments analyzed, 205 segrnents\n";
text += "(52.4%)'weredetected as viable by PET as well as\n";
text += "                                                       Table l. Characteristics of the study population\n";
text += "SPECT, 130 segments (33.3%) were defined as\n";
text += "viable by PET and as non-viable by 201T1 SPECT,\n";
text += "                                                                                             N = 23 (%)\n";
text += "                                                       Characteristic\n";
text += "5 segments (1.2%) were defined as viable by\n";
text += "SPECT and as non-viable by PET and the rest of         Age (years)                           51.7 I14\n";
text += "                                                       Gender\n";
text += "the segrnents ( 13.1%)did not show viability nei-\n";
text += "                                                        Male                                 20 (86.9)\n";
text += "ther by PET not by SPECT (Table 11).\n";
text += "                                                        Female                               3 (13.0)\n";
text += "                                                       Family history                        4 (1 1.5)\n";
text += "Discussion                                             Smoking                               14 (60.8)\n";
text += "                                                       Diabetes mellitus                     10 (43.4)\n";
text += "Nuclear medicine, using \"'TI SPECT (stress-re-\n";
text += "                                                       Hypertension                          12 (52.1)\n";
text += "distribution-reinjection) has been the most used\n";
text += "                                                       Dyslipidemia                          16 (69.5)\n";
text += "method to evaluate rnyocardial ~iability.~'    FDG     NYHA Class\n";
text += "PET study has been reported as the better tech-         I                                    13 (56.5)\n";
text += "                                                        II                                    7 (30.4)\n";
text += "nique for the evaluation of rnyocardial viability\n";
text += "                                                        111                                   2 (8.6)\n";
text += "previously to the revascularization therapy. How-\n";
text += "                                                        IV                                     1 (4.3)\n";
text += "ever, because of the poor availability of this tech-   EF (%)                                32 I10%\n";
text += "nique, a comparison between both techniques has        Number of affected vessels\n";
text += "                                                        Three                                 13 (56.5)\n";
text += "not been done in our country before. The impor-\n";
text += "                                                        Two                                   3 (13.0)\n";
text += "tance of the detection of myocardial viability is\n";
text += "                                                        One                                   7 (30.4)\n";
text += "funded in the fact that patients with rnyocardial      Previous angioplasty                   8 (34.7)\n";
text += "infarctions and left ventricular dysfunction with      Previous surgical revascularization     2 (8.6)\n";
text += "                                                       Myocardial infarction localization\n";
text += "viability demonstrated by PET may be benefited\n";
text += "                                                        Anteroseptal                         12 (52.1)\n";
text += "from a revascularization procedure and patients\n";
text += "                                                        Anterior                              2 (8.6)\n";
text += "without viability dernonstrated rnay n ~ t .ln the\n";
text += "                                               ~'        Extense anterior                    4 (17.3)\n";
text += "present study, we cornpared 20'TISPECT and              Inferior                             7 (30.4)\n";
text += "                                                         Posteroinferior                     3 (13.0)\n";
text += "\"FDG for the detection of rnyocardial viability in\n";
text += "                                                        Lateral                              5 (21.7)\n";
text += "patients with coronary artery diseaseand left ven-     -\n";
text += "\n";
text += "tricular dysfunction.                                  EF Ejection lraction\n";

      			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoInPage (1);
			//float average = pdftc.GetArithmeticAverageInPage (aL, 1);
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
			//Assert.AreEqual (average, 41, "CA");			
		}
	}
}
}