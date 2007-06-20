// PDFTextColumn: This class works with text in two columns and separate in two 
//		  columns.
// Archivos de Cardiología de México
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría.
//
// Copyright (C) 2007 UNAM DGB
//
using System;
using System.Collections;
namespace Scielo {
namespace PDF2Text
{	
	
public class PDFTextColumn
{
	public string [] pages;
	public string text;
		
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
	
	public ArrayList GetInfoInPage (int index)
	{
		ArrayList totalValues = new ArrayList ();
		string [] rawCollection = (pages[index]).Split (new Char [] {'\n'} );
		foreach (string line in rawCollection) {
			int [] valuesRaw = new int[2];
			int count = 0;
			int space = 0;
			int position = 0;
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
			/*foreach (DictionaryEntry de in ht){
				Console.Write("element in hash Key = {0}, Value = {1}", de.Key, de.Value);
			}
			Console.WriteLine("\n");*/
			totalValues.Add(ht);
		}
		return totalValues;
	}
	
	public float GetArithmeticAverageInPage (ArrayList values)
	{
		int sum = 0;
		int count = 0;
		float average = 0;
		foreach (Hashtable ht in values){
			if (ht.Count == 1){
				foreach (DictionaryEntry de in ht){
					sum = sum + (int)de.Key;
					count ++;
				}
			}
		}
		average = sum / count;
		Console.WriteLine("Average::"+average);
		return average;
	}
	
	public void GetTextInColumns (int indexPage, ArrayList values, float average){
		
		string column1 = ""; 
 		string column2 = ""; 
 		string [] rawCollection = (pages[indexPage]).Split (new Char [] {'\n'} ); 
 		int number_raw = 0; 
 		foreach (Hashtable ht in values){ 
 			float distance_now = 0; 
 			float distance = 0; 
 			int position = 0;
 			string line = rawCollection [number_raw];
 			int count = 1;
 			
			foreach (DictionaryEntry de in ht){
					if(count == 1){
 						distance = distance_now = Math.Abs ((int)de.Key - average);
 						position = (int)de.Key;
 						count = 2;
 					}else{
	 					distance_now = Math.Abs ((int)de.Key - average); 
 						if (distance > distance_now){ 
 							distance = distance_now; 
 							position = (int)de.Key; 
 			       	   		} 
 		       	   		}	
 	    			} 
 			if( position == 0 || position < average){ 
 				column1 += line + "\n";
 				column2 +="\n";
 				Console.WriteLine("Position:: "+position+" Average::"+average+" line::"+line);
 			}
 			else{ 
 				column1 +=  line.Substring (0,position) + "\n"; 
 				column2 += line.Substring (position) + "\n";
 			}
 			number_raw ++; 
 		} 
 		Console.WriteLine("--------------------LAS COLUMAS----------------"); 
 		Console.WriteLine("--------------Columna1 ------------------------"); 
 		Console.WriteLine(column1); 
 		Console.WriteLine("-----------------------------------------------"); 
 		Console.WriteLine("--------------Columna2 ------------------------"); 
 		Console.WriteLine(column2); 
 		Console.WriteLine("-----------------------------------------------"); 
	}
}
}
}