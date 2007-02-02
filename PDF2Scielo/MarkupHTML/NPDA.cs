//
// NPDA.cs:
//
// Author:
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Alejandro Rosendo Robles (rosendo69@hotmail.com)
//
// Copyright (C) 2007 UNAM DGB
// 

using System;
using System.Xml;
using System.Collections;


using System.IO;

namespace Scielo {
namespace MarkupHTML {

public class NPDA{

   /*
   description;
   stateSet;
   finalStates; 
   */
   
   /*
    bloks BF, BB and BBk
   */
  
   protected Hashtable deltaTransitions;

   protected string currentState;
   protected ArrayList inputString;
   protected Stack stack;
   protected int head = 0;
   protected GStarSXQ nextConfiguration; 
   protected string currentToken;
   protected string [] sigmaAlphabet;
   protected string [] gammaAlphabet;
   protected string symbolStack;
   protected string initialState;

   
   public NPDA(string filename, string [] input){
      
      XReaderNPDA xr = new XReaderNPDA("npdaAtm.xml");
      deltaTransitions = xr.GetDelta();
      sigmaAlphabet = xr.GetSigma();
      gammaAlphabet = xr.GetGamma();
      symbolStack = xr.GetSymbolStack();     
      initialState = xr.GetInitialState();

      inputString  = new ArrayList(input); 
      currentState = initialState;
      stack = new Stack();
     
   }

   public void PrepareInput(){
      string end = "EOF";
      string end2 = "epsilon"; 
      inputString.Add(end); 
      inputString.Add(end2); 
      inputString.Add(end2); 
   }

   public void RunMachine(){
          
      stack.Push(symbolStack);
     
      string currentToken = (string) inputString[head];
      MatchSymbol();
        
      while( DoTransition(currentToken) ){
         head++;
         UpdateStack();
         currentToken = (string) inputString [head];   
         MatchSymbol();
      }

      /*if(head==inputString.lenght+1 && stack.peek()==symbolStack && currentState=="10")
        Console.WriteLine("uffffffffffffffff ............ por fin!!! ");  */
             
   }

   public void UpdateStack(){
      
      ArrayList symbols = new ArrayList( nextConfiguration.GammaStar );
      stack.Pop();      
      if(symbols.Count == 2){
         stack.Push(symbols [1]);        
         stack.Push(symbols [0]);        
      }else{
         if((string) symbols [0] !="epsilon")
           stack.Push(symbols [0]);        
      }
   }

   public bool DoTransition (string cInput ){
   
       string glance = (string) stack.Peek();    
       
       Configuration config = new Configuration (currentState, cInput.ToString(), glance);
       nextConfiguration = (GStarSXQ) deltaTransitions[config];
       currentState = nextConfiguration.State;
 	   
       if(nextConfiguration == null)            
          return false;

       return true;
   }

   /* This method checks the currentToken belongs to Sigma Alphabet.
   */
   public bool MatchSymbol(){

      ArrayList alphabetS = new ArrayList(sigmaAlphabet);
      if(!alphabetS.Contains( currentToken)){
         currentToken = "asterisc";
         return false;         
      }
      return true;     
   }

   /*public static void Main(){

       
      DocReader doc = DocReader.createInstance ( new Uri("/compartido/atm/v18n4/pdf/v18n4a02.pdf")); 
      Tokens t = new Token(doc.getText(), 
                           new char[]{' '});

      NPDA np = new NPDA("npdaAtm.xml");
      Hashtable hash = rXN.GetDelta();
      rXN.GetSymbolStack();

      rXN.PrintKeysAndValues( hash );      
   }*/
   

}
}
}