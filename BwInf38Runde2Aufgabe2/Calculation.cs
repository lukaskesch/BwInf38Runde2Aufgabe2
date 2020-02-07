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
        public TResult(long _Result, int _Row, int _Column)
        {
            Result = _Result;
            Row = _Row;
            Column = _Column;
        }
        public long Result;
        public int Row;
        public int Column;
    }
    class Calculation
    {
        static int Digit;
        static int GoalNumber;
        static string StringTerm1 = string.Empty;
        static string StringTerm2 = string.Empty;
        static bool BoolModulo = false;
        static bool GoalNumber1Reached = false;
        static bool GoalNumber2Reached = false;
        static List<List<Term>> ListTerms = new List<List<Term>>();
        static List<TResult> ListResults = new List<TResult>();
        public static string GetFirstTerm()
        {
            return StringTerm1;
        }
        public static string GetSecondTerm()
        {
            return StringTerm2;
        }
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
            for (int nDigit = 1; nDigit < 100; nDigit++)
            {
                //Schaue ob GoalNumber1 schon erstellt wurde
                if (!GoalNumber1Reached)
                {
                    //Lege Liste für nDigit an
                    ListTerms.Add(new List<Term>());

                    //Erstelle Literal für nDigit
                    Literal OldLiteral = (Literal)ListTerms[nDigit - 1][0];
                    long OldLiteralValue = OldLiteral.GetResult();
                    long NewLiteralValue = OldLiteralValue * 10 + Digit;
                    Literal NewLiteral = new Literal(NewLiteralValue);
                    ListTerms[nDigit].Add(NewLiteral); //Muss theoretisch noch überprüft werden, ob wert nicht schon erreicht
                }
                else
                {
                    return;
                }


                //Gehe jede Ziffernlänge bis eins vor das Aktuelle durch
                for (int DigitLenght = 0; DigitLenght < nDigit; DigitLenght++)
                {
                    //Für jede Ziffernlänge gehe alle ihre Terme durch
                    for (int ElementsOfDigitLength = 0; ElementsOfDigitLength < ListTerms[DigitLenght].Count; ElementsOfDigitLength++)
                    {
                        //Für jede dieser Terme verknüpfe sie mit mit nDigit-i
                        int RemainingDigitDifference = nDigit - 1;
                        RemainingDigitDifference = ListTerms[RemainingDigitDifference].Count;
                        for (int ElementsOfRemainingDigitDifference = 0; ElementsOfRemainingDigitDifference < RemainingDigitDifference; ElementsOfRemainingDigitDifference++)
                        {
                            //Erstelle sinnvolle Terme
                            //Überprüfe Term
                            CreateTerms(DigitLenght,ElementsOfDigitLength, RemainingDigitDifference, ElementsOfRemainingDigitDifference);
                        }
                    }
                }
            }
        }
        public static void CreateTerms(int IndexA1, int IndexA2, int IndexB1, int IndexB2)
        {
            Term Term1 = ListTerms[IndexA1][IndexA2];
            Term Term2 = ListTerms[IndexB1][IndexB2];

            //Schaue ob Term2 größer ist als Term1, wenn ja tausche diese
            if(Term1.GetResult() < Term2.GetResult())
            {
                Term1 = ListTerms[IndexB1][IndexB2];
                Term2 = ListTerms[IndexA1][IndexA2];
            }

            Term NewTerm;
            int nDigit = IndexA1 + IndexB1;

            //Addition
            NewTerm = new AddOperator(Term1, Term2);
            if (CheckTerm(NewTerm))
                AddTermToLists(NewTerm,nDigit);
            

            //Subtraction
            NewTerm = new SubtractOperator(Term1, Term2);
            if (CheckTerm(NewTerm))
                AddTermToLists(NewTerm, nDigit);


            //Multiplication
            NewTerm = new MultiplyOperator(Term1, Term2);
            if (CheckTerm(NewTerm))
                AddTermToLists(NewTerm, nDigit);

            //Division
            int Remainder = (int)(Term1.GetResult() % Term2.GetResult());
            if(Remainder == 0)
            {
                NewTerm = new DivisionOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                    AddTermToLists(NewTerm, nDigit);
            }

            //Modulo
            if (BoolModulo)
            {
                NewTerm = new ModuloOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                    AddTermToLists(NewTerm, nDigit);
            }

            //Wird nur ausgeführt, wenn GoalNumber1 schon erreicht wurde
            if (GoalNumber1Reached)
            {
                //Power
                if(PowerOperator.IsCalculatable(Term1, Term2))
                {
                    NewTerm = new PowerOperator(Term1, Term2);
                    if (CheckTerm(NewTerm))
                        AddTermToLists(NewTerm, nDigit);
                }

                
            }


        }
        public static bool CheckTerm(Term NewTerm)
        {
            long TermResult = NewTerm.GetResult();
            foreach(TResult OldTermResults in ListResults)
            {
                if(OldTermResults.Result == TermResult)
                {
                    /*
                     Sehr wichtig: es muss noch überprüft werden, ob ein neuer Term kleiner ist
                     als der Alte, wenn er ein Term von Teil B ist
                    */
                    return false;
                }
            }
            if(TermResult == GoalNumber)
            {
                //Wenn bereits GoalNumber1 getroffen wurde, dann wurde jetzt GoalNumber2 getroffen
                if (GoalNumber1Reached)
                {
                    GoalNumber2Reached = true;
                    StringTerm2 = NewTerm.PrintTerm();
                }
                else
                {
                    //GoalNumber1 wurde getroffen
                    GoalNumber1Reached = true;
                    StringTerm1 = NewTerm.PrintTerm();
                }
                

                return false;
            }
            return true;
        }
        private static void AddTermToLists(Term NewTerm, int nDigit)
        {
            //Füge neuen Term beider Listen hinzu
            ListTerms[nDigit].Add(NewTerm);
            int Index = ListTerms[nDigit].Count - 1;
            ListResults.Add(new TResult(NewTerm.GetResult(), nDigit, Index));
        }
    }
}
