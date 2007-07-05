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
			string text = "Assessment of myocardial viability\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                      1. Basal anterior          insulinemic clamp\" has been reported to improve\n";
text += "                                                      2. Basal anteroseptal\n";
text += "                                                                                 PET accuracy in diabetic patient~.~~However,       in\n";
text += "                                                      3. Basal inferoseptal\n";
text += "                                                                                 our study, no differences between SPECT and\n";
text += "                                                      4. Basal inferior\n";
text += "                                                      5. Basal inferolateral     PET scores were found in the analysis by vascu-\n";
text += "                                                      6. Basal anterolateral\n";
text += "                                                                                 lar territories in diabetic vs non-diabetic patients,\n";
text += "                                                      7. Middle anterior\n";
text += "                                                                                 even in the absence of the euglycemic clamp\n";
text += "                                                      8. Middle anteroseptal\n";
text += "                                                      9. Middle inferoseptal     method.\n";
text += "                                                     10. Middle inferior\n";
text += "                                                                                 Liniitations of the study: This study did not in-\n";
text += "                                                     11. Middle inferolateral\n";
text += "                                                                                 clude the follow up of patients that were revas-\n";
text += "                                                     12. Middle anterolateral\n";
text += "                                                     13. Apical anterior         cularized in agreement with the PET or SPECT\n";
text += "                                                     14. Apical septal\n";
text += "                                                                                 results. However, several studies have shown\n";
text += "                                                     15. Apical inferior\n";
text += "                                                                                 that 18-FDG PET as well as T1-201 SPECT are\n";
text += "                                                     16. Apical lateral\n";
text += "                                                     17. Apex                    accurate methods for the prediction of postre-\n";
text += "                                                                                 vascularization improvement of ventricular\n";
text += "                                                                                 function. Bonow et alZ0      reported a good corre-\n";
text += "                                                                                 lation between qualitative and quantitative\n";
text += "Fig. 1. Correlation between SPECT and PET techniques based to\n";
text += "                                                                                 image analysis, in the present study only semi-\n";
text += "myocardial segrnentation rnodel. Segrnents in white represent areas of\n";
text += "                                                                                 quantitative analysis was performed. We should\n";
text += "myocardial viability identified by PET and non by SPECT (p ~ 0 . 0 5 segrnents\n";
text += "                                                                     )\n";
text += "in black represent areas of rnyocardial viability identified by PET and          also take an account the issue of limited clini-\n";
text += "SPECT.\n";
text += "                                                                                 cal sample, we only included 23 patients. Tri-\n";
text += "                                                                                 als that include a larger number of patients\n";
text += "                     Table II. Nurnber of segrnents detected as viable\n";
text += "                                                                                 should be done in order to confirm the results\n";
text += "                     and non-viable by SPECT and PET.\n";
text += "                                                                                 founded in this study.\n";
text += "                                                              PET\n";
text += "                                                     Viable         Non-viable   Conclusions\n";
text += "                                                                                 The recognition OSmyocardial viability has im-\n";
text += "                                     Viable           205                5\n";
text += "                                                                                 portant therapeutic and prognostic implications\n";
text += "                     SPECT                          (52.4%)           (1.2%)\n";
text += "                                   Non-viable         130               51       in patients with coronary artery disease and left\n";
text += "                                                    (33.3%)          (13.1 %)\n";
text += "                                                                                 ventricular dysf~nction.'.~\n";
text += "                                                                                 PET constitutes an advanced technique that has\n";
text += "                                                                                 high sensitivity in detection of myocardial via-\n";
text += "                     Comparing this study to the one reported by                 bility. The segmental model analysis could be\n";
text += "                     Bonow et        in the present study a higher per-          useful to recognize in precise form the specific\n";
text += "                     centage of viable segments were founded, how-               number of viable segments that can improve af-\n";
text += "                     ever patients at Bonow et al study had a lower              ter a coronary revascularization procedure.\n";
text += "                     mean EF than ours (27% vs 32%) which may                    Based on the results of this study, I8FDG PET\n";
text += "                     explain this difference,                                    detects a higher number of viable segments in\n";
text += "                     The results of this study are in agreement with             comparison with 201T1   SPECT justifying its use\n";
text += "                     those reported by Burt et aL2' who compared                 in patients with coronary artery disease and left\n";
text += "                     both methods in 20 patients with coronary ar-               ventricular dysfunction.\n";
text += "                     tery disease and left ventricular dysfunction, and\n";
text += "                     reported that in 23.3% of the segments that had             Acknowledgements\n";
text += "                     been classified as non-viable by SPECT, PET                 We wish to thank the physicist Adolfo Zárate-\n";
text += "                     detected viability.                                         Morales, and the chemist Efrain Zamora for their\n";
text += "                     A poor myocardial radiotracer uptake has been               help in the radiotracer production, as well as the\n";
text += "                     described in diabetic patients, with a subsequent           nuclear medicine technicians Luis Osorio and\n";
text += "                     poor image quality and lower accuracy of this               Isabel Porras who helped us with the acquiring\n";
text += "                     technique.\" The use of the \"euglycemic hyper-               and processing of data.\n";
text += "\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                                                                                                  -\n";
text += "                                                                                             Vol. 76 Número 11Enero-Marzo 2006:9-15\n";



          				
			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoInPage (1);
			//float average = pdftc.GetArithmeticAverageInPage (aL, 1);
			float average = pdftc.GetRepeatPosition (aL, 1);
			Console.WriteLine("valor::::::::::::::::::::#########################################"+average);
			pdftc.GetTextInColumns (1, aL, average);
			//Assert.AreEqual (average, 41, "CA");			
		}
		
	}
	
}
}