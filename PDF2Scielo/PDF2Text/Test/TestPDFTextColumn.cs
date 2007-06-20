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
			string text = " Aspectos electrocardiogr~ficos la hipertrofia ventricular derecha en el cor pulmonale crónico\n";
text += "                              de\n";
text += "\n";
text += "\n";
text += "\n";
text += "                                                                              Hipertrofia segmentaria\n";
text += "                                                                              Aumentan la magnitud y la manifestación sólo\n";
text += "                                                                              de algunos de los principales vectores resultan-\n";
text += "                                                                              tes del proceso de activación del ventrículo de-\n";
text += "                                                                              recho, cuyas estructuras están alteradas parcial-\n";
text += "                                                                              mente. Puede estar reducida la manifestación de\n";
text += "                                                                              los vectores izquierdos por cambios de la posi-\n";
text += "                                                                              ción cardíaca y por los efectos contrarrestantes\n";
text += "                                                                              de los poderosos vectores derechos implicados.\n";
text += "                                                                              Un ejemplo de dicha hipertrofia se observa en\n";
text += "                                                                              casos de cardiopatía pulmonar crónica de origen\n";
text += "                                                                              obstructivo bronquial. En estos casos, se produce\n";
text += "                                                                              una hipertrofia basal, a saber de las regiones cir-\n";
text += "                                                                              cunvecinas al surco auriculoventricular así como\n";
text += "                                                                              del infundíbulo de la arteria pulmonar. Por consi-\n";
text += "                                                                              guiente. predomina la manifestación del vector\n";
text += "                                                                              IIId (basal derecho), orientado hacia la derecha,\n";
text += "                                                                              arriba y atrás, se reduce la magnitud de los vecto-\n";
text += "                                                                              res 11s (septal derecho) y IIi (parietal izquierdo) y\n";
text += "                                                                              queda oculta la manifestación del vector IIIi (ba-\n";
text += "                   Fig. 1. Principales vectores resultantes de la activa-\n";
text += "                                                                              sal izquierdo). Pero aumenta la profundidad de la\n";
text += "                   ción ventricular en presencia de hipertrofia global del\n";
text += "                   ventriculo derecho (plano frontal).                        onda S en las derivaciones V,-V,.\n";
text += "                                                                              Los complejos ventriculares son de neto predo-\n";
text += "                                                                              minio negativo (rS o S > R con onda S profunda)\n";
text += "                               Normal                   HVD                   en las derivaciones precordiales derechas y parti-\n";
text += "                                                                              cularmente en V,, reflejando el incremento consi-\n";
text += "                                                                              derable de las fuerzas electromotrices basales del\n";
text += "                                                                                                         A\n";
text += "                                                                              ventrículo h~molateral.~estas ondas S profun-\n";
text += "                                                                              das corresponden ondas R tardías de alto voltaje\n";
text += "                                                                              en aVR: expresión de la magnitud incrementada\n";
text += "                                                                              del vector IIIdGS También es posible ver una caída\n";
text += "                                                                              brusca del voltaje de la onda R de V, a V2,6debida\n";
text += "                                                                              probablemente a que el electrodo de V, capta las\n";
text += "                                                                              fuerzas electromotrices importantes de las regio-\n";
text += "                                                                              nes basales del ventrículo derecho, mientras que\n";
text += "                                                 Ild\n";
text += "                     QRS,                                                     los electrodos de V,. V,, y a veces V,, exploran la\n";
text += "                                                                              zona trabecular de dicho ventrículo.\n";
text += "                   Fig. 2. Proyección de los principales vectores resul-\n";
text += "                                                                              Los datos electrocardiográficos relacionados con\n";
text += "                   tantes de la activaci6n ventricular en la curva\n";
text += "                   vectocardiogrtífica correspondiente. QRSF:Plano fron-      cambios de la posición cardíaca por enfisema se\n";
text += "                   tal. QRS,: Plano Horizontal. HVD: Hipertrofiaventricular\n";
text += "                                                                              han discutido en una publicación anterior.'\n";
text += "                   derecha global.\n";
text += "                                                                              Ejemplo\n";
text += "                                                                              El electrocardiograma registrado el día 28M1985\n";
text += "                                                                              (Fig. 5) proporcionó estos datos: RS 125lmin. P-R\n";
text += "                   ción media de la pared libre del ventrículo derecho\n";
text += "                                                                              0.14\". P, 0.09\", P anclada en V,. AQRS -1 70\". QRS\n";
text += "                   es de 13 mm (normal = 3 mm). El espesor medio de\n";
text += "                                                                              0.12\" en VI y aVR. rS en D,, D,,, ~ V L . en R~ aVR\n";
text += "                   la pared libre del ventrículo izquierdo es de 12 rnrn\n";
text += "                                                                                             S\n";
text += "                                                                              (TIDI O.IOw), >ren aVF, R (TIDI 0.06\") enV,, rS\n";
text += "                   (normal hasta 15 mm). La cavidad ventricular de-\n";
text += "                                                                              enV, (TIDI = 0.03\"),S > R enV, (TIDI 0.03\"),V4,V5\n";
text += "                   recha tiene aspecto ovoide. En resumen. hay un\n";
text += "                                                                               y V, (TIDI 0.04\"). T negativa de tipo secundario y\n";
text += "                   acentuado engrosamiento de la cresta supraventri-\n";
text += "                                                                              Q-T, =VM + 0.02\" enV,, positiva deV, aV,.\n";
text += "                   cular (espolón de Wolf) y de la pared libre ventri-\n";
text += "                                                                              TS: Posición cardíaca intermedia. Taquicardia\n";
text += "                   cular derecha. que supera el espesor de la pared\n";
text += "                                                                              sinusal. Hipertrofia basal del ventrículo derecho.\n";
text += "                   libre del ventriculo izquierdo. También las trabé-\n";
text += "                                                                               Bloqueo troncular derecho de grado intermedio.\n";
text += "                   culas del ventrículo derecho están netamente en-\n";
text += "                                                                              Los hallazgos anatómicos obtenidos al día si-\n";
text += "                   grosadas respecto a las del ventrículo izquierdo.\n";
text += "\n";
text += "\n";
text += "                                                                                        Vol. 76 Número IIEnero-Marzo 2006:69-74\n ";
                                                                                           

          				
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