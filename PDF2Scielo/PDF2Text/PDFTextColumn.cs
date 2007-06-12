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
	
	public void GetInfoInPage (int index)
	{
		ArrayList totalValues = new ArrayList ();
		int [] valuesRaw = new int[2];
		int space = 0;
		int position = 0;
		string [] rawCollection = (pages[index]).Split (new Char [] {'\n'} );
		for (int i=0; i< rawCollection.Length; i++){	
			if (pages[i] == " "){
				position = i;
				if (pages[i-1] == " ")
					space ++;	
			}
		}
		//al terminar el renglon
		if (space > 3){
			valuesRaw[0] = position;
			valuesRaw[1] = space;
		}
		totalValues.Add(valuesRaw);
	}
	
}
}
}