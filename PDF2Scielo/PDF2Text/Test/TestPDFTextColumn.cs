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
			string text = "Pericarditis purulenta\n";
text += "\n";
text += "\n";
text += "                     8. MAHER SHEPHERD ToDDTR:\n";
text += "                                  EA.           FA,                                2 1. CANVER PATEL KOSOLCHAROEN\n";
text += "                                                                                                   CC,          KA,               P, VOYTOV-\n";
text += "                                                               Pericardial Scle-\n";
text += "                                                                                        ICH CM: Fungal Purulent Constrictive Pericardi-\n";
text += "                          rosis as the Primaty Management ofMalignantPeri-\n";
text += "                          cardial Effusion and Cardiac Tamponade. J Thorac              tis in a Heart Transplant Patient. Ann Thorac Surg\n";
text += "                          Cardiovasc Surg 1996; 1 12(3): 637-643.                       1998; 65: 1792-1794.\n";
text += "                     9. CHERIANDiagnosis of Tuberculosis Etiology in               22. JUNEIA SHARMAKOTHARI Submitral Pseu-\n";
text += "                                                                                                 R.            R.         SS:\n";
text += "                                     G:\n";
text += "                          Pericardial Effusions. Postgrad Med J 2004; 80:               do aneurysm in Purulent Pericarditis. Heart 2000;\n";
text += "                          262-66.                                                       83(6): 713-714.\n";
text += "                     10. KOHK, KIM CHO Adenosine Deaminase And\n";
text += "                                         E,    C:                                  23. RAJNISH KHOTARI SAXENA RAJESH\n";
text += "                                                                                                    J,             SS,         A.         S,\n";
text += "                                                                                        ANURADHA        J: lntrapericardial Streptokinase in\n";
text += "                          Carcinoembtyonic Antigen in Pericardial Efision\n";
text += "                                                                                        Purulent Pericarditis. Arch Dis Child 1999; 80(3):\n";
text += "                          Diagnosis, Especially in Suspected Tuberculosis\n";
text += "                                                                                        275-277.\n";
text += "                          Pericarditis. Circulation 1994; 89: 2728-2735.\n";
text += "                                                                                   24. ROSENTHAL       A: Massive Purulent Pericarditis and\n";
text += "                     1 l. LEVY COREY BERCER HABIB BONNET\n";
text += "                                 PY,         R,           P.        G,\n";
text += "                                                                                        Cardiac Tamponade Caused by Staphylococcus\n";
text += "                          JL, MESSANADJIANE Etiulogic Diagnosis of\n";
text += "                                          T,         P:\n";
text += "                                                                                        aureus Urosepsis. J Cardiovasc Surg 2002; 43:\n";
text += "                          Pericardial Efision. Medicine 2003; 82: 385-39 1.\n";
text += "                     12. BROOK FRAZIER Microbiology ofAcute Pu-\n";
text += "                                   1,        HE:                                        837-839.\n";
text += "                          rulent Pericarditis. Arch Intern Med 1996; 156:          25. LING OHJL, BREEN Calcific Constrictive\n";
text += "                                                                                               LH,                   JF:\n";
text += "                          18.57-1860.                                                   Pericarditis is it still with us? Ann Intem Med\n";
text += "                     13. MAISCH RISTIC The ClassiJication of Peri-\n";
text += "                                    B,       AD:                                        2000; 132: 444-450.\n";
text += "                                                                                   26. TALREJA EDWARDS GK DANIELSON,\n";
text += "                                                                                                   DR,             DW,                SCHAFF\n";
text += "                          cardial Disease in the Age of Modern Medicine.\n";
text += "                          Curr Cardiol Rep 2002; 4: 13-21.                              VH, TAJIK TAZELAAR Constrictive Peri-\n";
text += "                                                                                                       JA,             DH:\n";
text += "                                                                                        carditis in 26 Patients with Histologically Normal\n";
text += "                     14. SAULEDAALMENAR\n";
text += "                                     SJ,          L, ANGELBARDAJ~\n";
text += "                                                            J,        A, BOSCH\n";
text += "                          X, GUIDO Guías de Práctica Clínica de la So-\n";
text += "                                      J:                                                Pericardial Thickness. Circulation 2003; 108:\n";
text += "                          ciedad Española de Cardiología para Patología                  1852-1857.\n";
text += "                          Pericárdica. Rev Esp Cardiol2000; 53: 389-394.           27. SAULEDAPericardial Constriction: Uncommon\n";
text += "                                                                                                    SJ:\n";
text += "                     15. SNYDER TODD Purulent Pericarditis with\n";
text += "                                    RW,        B:                                       Putterns. Heart 2004; 90: 257-258.\n";
text += "                          Tanzponade in a Postpartum Putient Due to Group          28. BERTOG THAMBIDORAI\n";
text += "                                                                                                   CS,                  KS, PARAKHSCHOE-\n";
text += "                                                                                                                                   K,\n";
text += "                          F Streptococcus. Chest 1999; 1 15(6): 1746-1747.              NHAGEN P, OZDURA HOUGHTALING\n";
text += "                                                                                                                V,             LP: Constric-\n";
text += "                     16. A L T E M E I RWA, T O N E L L I MR, AITKEN       ML:          tive Pericarditis: Etiology and Cause-Specijk Sur-\n";
text += "                                                                                        viva1 after Pericardiectomy. JACC 2004; 43(8):\n";
text += "                          Pseudomonal Pericarditis Cornplicating Cystic\n";
text += "                          Fibrosis. Pediatr Pulmonol 1999; 27: 62-64.                    1445- 1452.\n";
text += "                     17. NARDELL DALI SHEPARD MARK Case\n";
text += "                                      EA,      F.          JA,         EJ:         29. SAULEDA SANCHEZ SOLER PERMANYER\n";
text += "                                                                                                    SJ,             JA,       SJ.\n";
text += "                          22-2004: A 30-Year-Old Woman with a Pericardial               MG: Effusive-Constrictive Pericarditis. N Engl J\n";
text += "                          Efision. N Engl J Med 2004; 35 l(3): 279-287.                 Med 2004; 350(5): 469-475.\n";
text += "                     18. RAYO    GM, LACAUADA LAYNEZ BOSA\n";
text += "                                                    AJ,          CI,        OF,                                                         VH,\n";
text += "                                                                                   30. HALEY TAJIK DANIELSON SCHAFF\n";
text += "                                                                                                 HJ,          JA.            GK,\n";
text += "                          DOM~NCUEZARMAS Pericarditis Menin-\n";
text += "                                         RA,          TD:                                MULVAGH OHJK: Transient Constrictive Peri-\n";
text += "                                                                                                     LS.\n";
text += "                          gocócica Primaria por Serogrupo C. Rev Esp                    carditis: Causes and Natural Histoty. JACC 2004;\n";
text += "                          Cardiol 2000; 53: 1541-1544.                                  43(2): 271-275.\n";
text += "                     19. SAULEDA BARRABES MIRALDA SOLER\n";
text += "                                      SJ.             JA,           PG,            3 1. ULLOA AVILA\n";
text += "                                                                                                 GR,            AM: Varicella associate with\n";
text += "                          SJ: Purulent Pericarditis: Review of a 20-Year                Staphylococcus aureus purulent pericarditis. Pe-\n";
text += "                                                                                        diatr lnfect Dis J 2003; 22(10): 935-936.\n";
text += "                          Experience in a General Hospital. J Am Col1 Car-\n";
text += "                                                                                                L,\n";
text += "                          dio1 1993; 22: 1661- 1665.                               32. GOLD BARBOUR           S, GUERREROTL,  Koom R. LEWIS\n";
text += "                                                                                         K, RUDINSKY Staphylococcus aureus Endocardi-\n";
text += "                                                                                                         MF:\n";
text += "                     20. MAISCH RISTIC\n";
text += "                                     B,        DA: Practica1 Aspects of the\n";
text += "                                                                                        tis Associatet with Varicella Jnfection in Children.\n";
text += "                          Maiiugemeiit of Pericurdial Diseuse. Heart 2003;\n";
text += "                                                                                         Pcdiatric Infcct Dis J 1996; 15(4):377-379.\n";
text += "                          89: 1096-1 103.\n";
text += "\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                                                             Vol. 76 Número 1IEnero-Marzo 2006:83-89 ";
                                                                                           

          				
			PDFTextColumn pdftc = new PDFTextColumn(text);
			ArrayList aL = pdftc.GetInfoInPage (1);
			//float average = pdftc.GetArithmeticAverageInPage (aL, 1);
			float average = pdftc.GetRepeatPosition (aL, 1);

			pdftc.GetTextInColumns (1, aL, average);
			//Assert.AreEqual (average, 41, "CA");			
		}
		
	}
	
}
}