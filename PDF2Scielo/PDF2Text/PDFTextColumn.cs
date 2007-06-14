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
			
	public void DataPreprocessing()
	{
		/* This method eliminates the spaces*/
		;//text
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
					position = count;
					if (count>0 && line[count-1] == ' '){
						space ++;
						position_value = count;
					}
				}
				if (space >= 2 && count<line.Length -1 && line[count+1] != ' '){
					if (!ht.Contains(position_value))
						ht.Add (position_value, space);
				}
				count++;
			}
			Console.WriteLine("line:::"+line);
			Console.WriteLine("::::::::::::::::::::...end line");			
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
				sum = sum + (int)ht[count];	
				count ++;
			}
		}
		average = sum / count;
		return average;
	}
}
}
}