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
			string text = "text = \n";
text += "\n";
text += "\n";
text += "                      sión, después de un fracaso primario. Después        de la conducción por la vía lenta anterógrada\n";
text += "                      de un seguimiento de 38 + 15 meses, 5 de los 99      (salto de conducción y un solo latido en eco\n";
text += "                                                                           auricular) grupo 1 y 36 pacientes (36%) elimi-\n";
text += "                      pacientes (5%) tuvieron una recurrencia docu-\n";
text += "                                                                           nación de la TRNAV con eliminación completa\n";
text += "                      mentada de TRNAV, 1 en la forma no común de\n";
text += "                                                                           de la vía lenta anterógrada, grupo 11. En condi-\n";
text += "                      TRNAV.\n";
text += "                      Doce de 101 pacientes, con el método de RF r?a-      ciones de base el tiempo de conducción atrio-\n";
text += "                                                                           His basa1 fue 74 2 14 y 73 I 1 ms en el grupo 1\n";
text += "                      nifestaron taquicardia con síntomas similares o                                     2\n";
text += "\n";
text += "                                                        a,                 y 11 respectivamente, p = NS y tampoco hubo\n";
text += "                      de menor magnitud preabl ción pero no docu-\n";
text += "                      mentada al ECG-12 y con extr toles y10 taqui-        diferencia estadística entre los grupos con res-\n";
text += "                                                                           pecto al punto de Wenckebach anterógrado o en\n";
text += "                      cardia sinusal al monitoreo Holter. Siete acepta-\n";
text += "                      ron un nuevo estudio electrofisiológico sin          los períodos refractarios efectivos anterógrados\n";
text += "                      demostrar ningún mecanismo de taquicardia clí-       de la vía lenta y de la vía rápida.\n";
text += "                                                                           Grupo I. Pacientes con persistencia residual de\n";
text += "                      nica.\n";
text += "                                                                           la conducciónpor la vía lenta (Fig. 1 ) . El núme-\n";
text += "                      Se presentaron 4 casos de bloqueo A completo\n";
text += "                                                           V\n";
text += "                                                                           ro medio de aplicaciones de RF en este grupo\n";
text += "                      agudo y permanente que indicó marcapaso defi-\n";
text += "                      nitivo, 2 de cada grupo y todos en aplicación        fue 5 I con taquicardia recurrente documenta-\n";
text += "                                                                                   4,\n";
text += "                      mesoseptal. Un bloqueo A de segundo grado\n";
text += "                                                  V                        da en 3 de 63 (4.7%) en un seguimiento medio\n";
text += "                                                                           de 38 + 15 meses. El período refractario efectivo\n";
text += "                      tipo Wenckebach fue detectado dentro de las 24\n";
text += "                                                                           anterógrado de la vía rápida (punro del salto de\n";
text += "                      h en un paciente del grupo 11 y no requirió mar-\n";
text += "                                                                           conducción) mostró una tendencia a disminuir\n";
text += "                      capaso.\n";
text += "                      En el seguimiento temprano de 3 + 1 meses se         pero sin diferencia estadística, 340 I ms a39\n";
text += "                                                                           329 + 45 ms, p = NS.\n";
text += "                      documentó por Holter bloqueo AV de segundo\n";
text += "                                                                           El punto de Wenckebach anterógrado aumentó\n";
text += "                      grado, uno tipo Wenckebach intermitente y el\n";
text += "                                                                           de 360 I a 375 2 61 ms, p < 0.05 y el período\n";
text += "                                                                                      65\n";
text += "                      otro con bloqueo A tipo Wenckebach y Mobitz\n";
text += "                                         V\n";
text += "                                                                           refractario efectivo anterógrado de la vía lenta\n";
text += "                      2 altemante e intermitentes de predominio noc-\n";
text += "                                                                           no cambió, 290 + 46 ms a. 279 1; 43 ms, p = NS.\n";
text += "                      turno en pacientes asintomáticos que no requi-\n";
text += "                      rieron marcapaso permanente, uno de cada gru-        En 3 de 5 pacientes tratados con crioterapia hubo\n";
text += "                                                                           fisiología nodal A residual sin cambios signifi-\n";
text += "                                                                                              V\n";
text += "                      PO.\n";
text += "                      Se logró el éxito en los cinco pacientes someti-     cativos en el PRE o punto de Wenckebach pos-\n";
text += "                                                                           tablación.\n";
text += "                      dos a tratamiento con criotermia. El número de\n";
text += "                      aplicaciones fue 3.2 2 2. Se presentó un bloqueo     Grupo 11. Pacientes con ausencia total de la\n";
text += "                                                                           conducción por la vía lenta (Fig. 2). El número\n";
text += "                      AV completo agudo y transitorio de 3.6 s de du-\n";
text += "                                                                           medio de aplicaciones de RF fue 7.3 + 6, con\n";
text += "                      ración que apareció después de 5 S durante la\n";
text += "                      fase de crioablación quedando con un intervalo       taquicardia recurrente documentada en 2 de 36\n";
text += "                      PR 0.24\" permanente y ausencia de doble fisio-       (5%) pacientes en el mismo tiempo de seguimien-\n";
text += "                      logía nodal. No hubo recurrencias de TRNAV en        to. Estos dos parámetros no fueron significati-\n";
text += "                      un seguimiento a 12 + 4 meses.                       vos comparados con el grupo 1. El período re-\n";
text += "                                                                           fractario efectivo anterógrado de la' vía rápida\n";
text += "                      Características electrofisiológicas (Tabla 1)        (punto del salto de conducción) mostró un acor-\n";
text += "                                                                           tamiento significativo, 328 -c 83 ms a 282 + 75\n";
text += "                      En 63 de los pacientes (64%) se logró la elimi-\n";
text += "                      nación de la TRNAV con persistencia residual         ms, p c 0.001.\n";
text += "                                                                           El punto de Wenckebach anterógrado aumentó de\n";
text += "                                                                           manera significativa, 35 1 A 20 a 38 1 + 14 ms, p <\n";
text += "                                                                           0.001. En 2 de 5 pacientes tratados con criotemia\n";
text += "Tabla l. Comparación de parámetros electrofisiológicos.\n";
text += "                                                                           se presentó ausencia completa de la conducción\n";
text += "                  Grupo 1             P             Grupo II        P      por la vía lenta (Fig. y con acortamiento signifi-\n";
text += "                                                                                                 3)\n";
text += "                                                                           cativo del PRE de la vía rápida y alargamiento\n";
text += "Punto de         360 I 65                           351 i 28\n";
text += "                                                                           significativo del punto de Wenckebach.\n";
text += "                                   < 0.05\n";
text += "Wenckebach       375 I 61                           381 i 35       0.001\n";
text += "                                                               e\n";
text += "PRE-A            340 i 39                           328 I83\n";
text += "                                     NS\n";
text += "Vía rápida                                                                 Discusión\n";
text += "                 329 i 45                           280 I 75       0.001\n";
text += "                                                               e\n";
text += "PRE-A            290 I 46                                                  El nodo AV siendo una estructura que ha sido\n";
text += "                                     NS\n";
text += "Vía lenta        279 I 43\n";
text += "                                                                           conocida desde hace poco menos de 100 años\n";
text += "PRE-A = Periodo refractario efectivo anter6grado.                          ha sido objeto de varias controversias.\n";
text += "\n";



          				
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