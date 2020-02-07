using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BwInf38Runde2Aufgabe2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int GoalNumber;
        int Digit;
        bool BoolModulo = false;
        bool GoalNumber1Reached = false;
        bool GoalNumber2Reached = false;
        List<List<Term>> ListTerms = new List<List<Term>>();
        List<TResult> ListResults = new List<TResult>();
        public MainWindow()
        {
            InitializeComponent();
        }
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

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                GoalNumber = int.Parse(TextBoxNumberToCalculate.Text);
                Digit = int.Parse(TextBoxDigit.Text);
                CalculateTerm(1);
                CalculateTerm(2);
                

            }
            //catch (Exception)
            {
                //MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");
                //throw;
            }
        }

        private void CalculateTerm(int Task)
        {
            //Erstelle den ersten Digit
            Literal FirstLiteral = new Literal(Digit);

            //Lege eine Liste an Index 0 an und speicher in ihr First Literal
            ListTerms.Add(new List<Term>());
            ListTerms[0].Add(FirstLiteral);

            //Erstelle für jede Ziffernlänge (alle) Terme
            for (int nDigit = 1; nDigit < 100; nDigit++)
            {
                //Schaue ob GoalNumber1 schon erstellt wurde in Teil 1
                if(Task == 1 && GoalNumber1Reached)
                {
                    return;
                }
                else if (Task == 1)
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
               


                //Gehe jede Ziffernlänge bis eins vor das Aktuelle durch
                for (int DigitLenght = 0; DigitLenght < nDigit; DigitLenght++)
                {
                    //Für jede dieser Ziffernlänge gehe alle ihre Terme durch
                    for (int ElementsOfDigitLength = 0; ElementsOfDigitLength < ListTerms[DigitLenght].Count; ElementsOfDigitLength++)
                    {
                        //Für jede dieser Terme verknüpfe sie mit mit allen nDigit-DigitLenght Termen
                        int RemainingDigitDifference = nDigit - DigitLenght;
                        for (int ElementsOfRemainingDigitDifference = 0; ElementsOfRemainingDigitDifference < ListTerms[RemainingDigitDifference].Count; ElementsOfRemainingDigitDifference++)
                        {
                            //Erstelle alle sinnvollen Terme aus den zwei aktuellen Termen
                            CreateTerms(DigitLenght, ElementsOfDigitLength, RemainingDigitDifference, ElementsOfRemainingDigitDifference, Task);
                            
                            //Breche ab, wenn GoalNum2 erreicht
                            if(Task == 2 && GoalNumber2Reached)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void CreateTerms(int IndexA1, int IndexA2, int IndexB1, int IndexB2, int Task)
        {
            Term Term1 = ListTerms[IndexA1][IndexA2];
            Term Term2 = ListTerms[IndexB1][IndexB2];

            //Schaue ob Term2 größer ist als Term1, wenn ja tausche diese
            if (Term1.GetResult() < Term2.GetResult())
            {
                Term1 = ListTerms[IndexB1][IndexB2];
                Term2 = ListTerms[IndexA1][IndexA2];
            }

            Term NewTerm;
            int nDigit = IndexA1 + IndexB1;

            //Addition
            NewTerm = new AddOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
                AddTermToLists(NewTerm, nDigit);


            //Subtraction
            NewTerm = new SubtractOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
                AddTermToLists(NewTerm, nDigit);


            //Multiplication
            NewTerm = new MultiplyOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
                AddTermToLists(NewTerm, nDigit);

            //Division
            if (DivisionOperator.IsCalculatable(Term1,Term2))
            {
                NewTerm = new DivisionOperator(Term1, Term2);
                if (CheckTerm(NewTerm, Task))
                    AddTermToLists(NewTerm, nDigit);
            }

            //Modulo
            if (BoolModulo)
            {
                NewTerm = new ModuloOperator(Term1, Term2);
                if (CheckTerm(NewTerm, Task))
                    AddTermToLists(NewTerm, nDigit);
            }

            //Wird nur ausgeführt, wenn GoalNumber1 schon erreicht wurde
            if (Task == 2)
            {
                //Power
                if (PowerOperator.IsCalculatable(Term1, Term2))
                {
                    NewTerm = new PowerOperator(Term1, Term2);
                    if (CheckTerm(NewTerm,2))
                        AddTermToLists(NewTerm, nDigit);
                }


            }


        }
        private bool CheckTerm(Term NewTerm, int Task)
        {
            long TermResult = NewTerm.GetResult();
            foreach (TResult OldTermResults in ListResults)
            {
                if (OldTermResults.Result == TermResult)
                {
                    /*
                     Sehr wichtig: es muss noch überprüft werden, ob ein neuer Term kleiner ist
                     als der Alte, wenn er ein Term von Teil B ist
                    */
                    return false;
                }
            }
            if (TermResult == 0)
            {
                return false;
            }
            else if (TermResult == GoalNumber)
            {
                
                if (Task == 1 && !GoalNumber1Reached)
                {
                    GoalNumber1Reached = true;
                    LabelResult1.Content = NewTerm.PrintTerm();
                    
                }
                else if(Task == 2)
                {
                    //GoalNumber2 wurde getroffen
                    GoalNumber2Reached = true;
                    LabelResult1.Content = NewTerm.PrintTerm();
                    
                }
            }
            return true;
        }
        private void AddTermToLists(Term NewTerm, int nDigit)
        {
            //Füge neuen Term beider Listen hinzu
            ListTerms[nDigit].Add(NewTerm);
            int Index = ListTerms[nDigit].Count - 1;
            ListResults.Add(new TResult(NewTerm.GetResult(), nDigit, Index));
        }
    }
}
