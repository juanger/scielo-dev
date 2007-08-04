// PDFTextColumn: This class works with text in two columns and separate in two 
// columns.
// Archivos de Cardiología de México
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
using System.Text.RegularExpressions;
namespace Scielo {
namespace PDF2Text
{	
	
public class PDFTextColumn
{
	private int threshold = -1;
	private string [] pages;
	private string text;
	private bool referencesFlag = false;
	private string column1 = "";
	private string column2 = "";
	Hashtable vr = new Hashtable ();
		
	public PDFTextColumn (RawDocument document)
	{
		text = document.GetText ();
		pages = GetTextInPages ();
	}
	
	public PDFTextColumn (string data)
	{
		text = data;
		pages = GetTextInPages ();
	}
	
	public string [] GetTextInPages ( )
	{
		return text.Split (new Char [] {'\u000c'});
	}
	
	public string Column1{
		get {
			return column1;
		}
	}
	
	public string Column2{
		get {
			return column2;
		}
	}
	
	public string[] Pages{
		get {
			return pages;
		}
	}
	
	public ArrayList GetInfoInPage (int index)
	{
		ArrayList totalValues = new ArrayList ();
		string [] rawCollection = (pages[index]).Split (new Char [] {'\n'} );
		foreach (string line in rawCollection) {
			int count = 0;
			int space = 0;
			int position_value = 0;
			Hashtable ht = new Hashtable ();
			foreach (char character in line) {
				if (character == ' '){
					if (count>0 && line[count-1] == ' '){
						space ++;
						position_value = count;
					}
				}
				if (space >= 2 && count < line.Length-1 && line[count+1] != ' '){
					if (!ht.Contains(position_value)){
						ht.Add (position_value, space);
					}
				}	
				count++;	
			}
			Console.WriteLine("------------------->la linea actual es:::"+line);
			foreach (DictionaryEntry de in ht){
				int ele= (int)de.Key;
				Console.WriteLine("Posi::"+de.Key+"::sub::"+line.Substring(ele));
			}	
			totalValues.Add(ht);
		}
		return totalValues;
	}
	
	public float GetArithmeticAverageInPage (ArrayList values, int index)
	{
		int sum = 0;
		int count = 0;
		float average = 0;
		int i = 0;
		foreach (Hashtable ht in values){
			if (ht.Count == 1){
				foreach (DictionaryEntry de in ht){
					sum = sum + (int)de.Key;
					count ++;
				}			
			}
			i++;
		}
		average = sum / count;
		return average;
	}
	/*
	Find the repetition position in the lines with only ONE spaces.
	*/
	public float GetRepeatPosition (ArrayList values, int index)
	{
		//Hashtable vr = new Hashtable ();
		int i = 0;
		foreach (Hashtable ht in values){
			if (ht.Count == 1){
				foreach (DictionaryEntry de in ht){
					if(!vr.ContainsKey((int)de.Key)){
						vr.Add(de.Key,1);
					}else{
						int val = (int) vr[de.Key];
						vr[de.Key] = val+1;
					}
				}
				i++;
			}
		}
		Console.WriteLine("En hashtable::"+i);
		foreach (DictionaryEntry de in vr){
			Console.WriteLine("K::"+de.Key+"::V::"+de.Value);
		}
		SetThreshold (index);
		Console.WriteLine("here threshold:::"+threshold);
 		return UpperValueOnThreshold ();
	}
	
	private void SetThreshold (int index)
	{
		int maxL = UpperLength ((pages[index]).Split (new Char [] {'\n'}));
		threshold = (maxL/2)-3;
	}
	
	private float UpperValueOnThreshold ()
	{
		float upper_value = (float)UpperValue ();
		Console.WriteLine("in upperValue"+upper_value);
 		for (int k=0; k<vr.Count; k++){
 			if ( upper_value > threshold){
 				break;
 			}
 			upper_value = (float)UpperValue ();
 		}
 		return (upper_value);
	}
	
	/* Finds the position with great number of frecuency repetition  
	*/
	public float UpperValue ()
	{
		int valV = 0;
		int valK = 0;
		foreach (DictionaryEntry de in vr){
			if ((int)de.Value > valV){
				valV = (int)de.Value;
				valK = (int)de.Key;
			}
			Console.Write("key::pos::" + valK +" ");
			Console.WriteLine("val::" + valV);	
		}
		vr.Remove(valK);
		return (float)valK;
	}
	
	public int UpperLength (string[] rawCollection){
		int leng = 0;
		foreach (string element in rawCollection){
			if( element.Length > leng )
				leng = element.Length;
		}
		return leng;
	}

	private void DivideLine (string line, int position, float average){
		if (referencesFlag){
   			if (position==0 || position<threshold){ 
 				column1 += line;
 				column2 +="\n";
			}else{ 
				column1 +=  line.Substring (0,position)+"\n"; 
				column2 += line.Substring (position);
			}
 	    	}else{
 			if (position==0 || position<average){ 
 				column1 += line;
 				column2 +="\n";
			}else{ 
 				column1 +=  line.Substring (0,position)+"\n"; 
 				column2 += line.Substring (position);
 			}
 		}
	}
	
	private int PositionToDivideLine (Hashtable ht, float average){
		float distance_now = 0; 
 		float distance = 0; 
 		int position = 0;
		int count = 1;
		
		foreach (DictionaryEntry de in ht){
			if(count == 1){
 				distance = distance_now = Math.Abs ((int)de.Key - average);
 				position = (int)de.Key;
 				count = 2;
 			}else{
				distance_now = Math.Abs ((int)de.Key - average);
 				if (distance >= distance_now){ 
 					distance = distance_now; 
 					position = (int)de.Key;
 				} 
 	       		}	
 	 	}
 	 	return position;
	}
	
	public void GetTextInColumns (int indexPage, ArrayList values, float average)
	{			
 		string [] rawCollection = (pages[indexPage]).Split (new Char [] {'\n'} ); 
 		int number_raw = 0;
 		Regex regex = new Regex (@"[ ]+(Referencias|References)\n");
 		
 		foreach (Hashtable ht in values){ 
 			
 			string line = rawCollection [number_raw]+"\n"; 			
 			if (regex.IsMatch(line) == true)
 				referencesFlag = true;
 			
			int pos = PositionToDivideLine (ht, average);
 	    		DivideLine (line, pos, average);
 	    		Console.Write("Aver:"+average+":Thres:"+threshold+"::Pos::"+pos+"::linea::"+line+"::subS::"+line.Substring(pos));
 			number_raw ++; 
 		}
	}
}
}
}