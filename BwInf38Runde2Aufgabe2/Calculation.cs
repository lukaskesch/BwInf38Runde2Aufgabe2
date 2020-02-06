using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BwInf38Runde2Aufgabe2
{
    struct TResult
    {
        public TResult(int _Result, int _Row, int _Column)
        {
            Result = _Result;
            Row = _Row;
            Column = _Column;
        }
        public int Result;
        public int Row;
        public int Column;
    }
    class Calculation
    {
        static int Digit;
        static int GoalNumber;
        static List<List<Term>> ListTerms = new List<List<Term>>();
        static List<TResult> ListResults = new List<TResult>();
        public static void CalculateTerm(int _GoalNumber, int _Digit)
        {
            GoalNumber = _GoalNumber;
            Digit = _Digit;

            //Erstelle den ersten Digit
            Literal FirstLiteral = new Literal(Digit);

            //Lege eine Liste an Index 0 an und speicher in ihr First Literal
            ListTerms.Add(new List<Term>());
            ListTerms[0].Add(FirstLiteral);

            //Erstelle für jede Ziffernlänge (alle) Terme
            for (int nDigit = 0; nDigit < 100; nDigit++)
            {
                //Lege Liste für nDigit an
                ListTerms.Add(new List<Term>());

                //Erstelle Literal für nDigit
                Literal OldLiteral = (Literal)ListTerms[nDigit - 1][0];
                int OldLiteralValue = OldLiteral.GetResult();
                int NewLiteralValue = OldLiteralValue * 10 + Digit;
                Literal NewLiteral = new Literal(NewLiteralValue);
                //ListTerms[nDigit].Add(NewLiteral); muss noch überprüft werden, ob wert nicht schon erreicht

                //Gehe jede Ziffernlänge bis eins vor das Aktuelle durch
                for (int i = 0; i < nDigit; i++)
                {
                    //Für jede Ziffernlänge gehe alle ihre Terme durch
                    for (int j = 0; j < ListTerms[i].Count; j++)
                    {
                        //Für jede dieser Terme verknüpfe sie mit mit nDigit-i
                        int max = nDigit - 1;
                        max = ListTerms[max].Count;
                        for (int k = 0; k < 0; k++)
                        {
                            //Erstelle Term
                            //Überprüfe Term
                        }
                    }
                }

                //Überprüfe, ob das Ergebnis schon einmal erstellt wurde und ob es die erwünschte Zahl ist
                if (false)
                {

                }
                if (false)
                {

                }
                else
                {
                    //Füge neuen Term beider Listen hinzu
                    ListTerms[nDigit].Add(null);
                }
            }
        }

        public static bool CheckIfResultAlreadyExist(int NewTermResult)
        {
            foreach(TResult OldTermResults in ListResults)
            {
                if(OldTermResults.Result == NewTermResult)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
