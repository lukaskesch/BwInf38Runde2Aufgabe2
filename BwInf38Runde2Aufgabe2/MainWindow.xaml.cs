using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        double CalculationTime;
        int GoalNumber;
        int Digit;
        int NeededNumberOfDigits1;
        int NeededNumberOfDigits2;
        long NumberOfDeletedTerms;
        double PercentOfList;
        bool CalculateJustA = false;
        bool BoolModulo = false;
        bool GoalNumber1Reached;
        bool GoalNumber2Reached;
        List<List<Term>> ListTerms = new List<List<Term>>();

        //List<long> ListResults = new List<long>();
        SortedList<long, byte> ListResults = new SortedList<long, byte>();
        Stopwatch stopwatch = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
            TextBoxDigit.Text = 1.ToString();
        }


        private void ButtonCalculateAB_Click(object sender, RoutedEventArgs e)
        {
            //try
            {
                BoolModulo = false;
                NumberOfDeletedTerms = 0;
                PercentOfList = 0;
                GoalNumber1Reached = false;
                GoalNumber2Reached = false;
                ListTerms = new List<List<Term>>();
                ListResults = new SortedList<long, byte>();
                //ListResults = new List<long>();

                BoolModulo = (bool)CheckBoxModulo.IsChecked;
                GoalNumber = int.Parse(TextBoxNumberToCalculate.Text);
                Digit = int.Parse(TextBoxDigit.Text);

                if (Digit < 1 || Digit > 9)
                {
                    MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");
                    return;
                }

                stopwatch.Restart();
                CalculateTerm(1);
                CalculationTime = stopwatch.ElapsedMilliseconds;
                CalculationTime /= 1000;

                LabelResult1Time.Content = CalculationTime.ToString();
                LabelResult1NeededTerms.Content = ListResults.Count.ToString();
                LabelResult1DeletedTerms.Content = NumberOfDeletedTerms.ToString();
                LabelResult1nDigits.Content = NeededNumberOfDigits1.ToString();
                LabelResult1nDigits.Content = (PercentOfList / NumberOfDeletedTerms).ToString();

                if (sender != null)
                {
                    stopwatch.Restart();
                    CalculateTerm(2);
                    CalculationTime = stopwatch.ElapsedMilliseconds;
                    stopwatch.Stop();

                }



            }
            //catch (Exception)
            {
                //MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");

            }
        }

        private void CalculateTerm(int Task)
        {
            if (Task == 1)
            {
                //Erstelle den ersten Digit
                Literal FirstLiteral = new Literal(Digit);

                //Lege eine Liste an Index 0 an und speicher in ihr First Literal
                ListTerms.Add(new List<Term>());
                ListTerms[0].Add(FirstLiteral);
            }



            //Erstelle für jede Ziffernlänge (alle) Terme
            for (int nDigit = 1; true; nDigit++)
            {
                //Schaue ob GoalNumber1 schon erstellt wurde in Teil 1
                if (Task == 1 && GoalNumber1Reached)
                {
                    NeededNumberOfDigits1 = nDigit;
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
                else if (Task == 2 && NeededNumberOfDigits1 < nDigit)
                {
                    LabelResult2Term.Content = "Keine bessere Lösung gefunden";
                    return;
                }


                //Gehe Ziffernlänge bis zur Hälfte der aktuellen hoch
                for (int DigitLenght = 0; DigitLenght < (nDigit + 1) / 2; DigitLenght++)
                {
                    //Für jede dieser Ziffernlänge gehe alle ihre Terme durch
                    int UpperBound = ListTerms[DigitLenght].Count;
                    for (int ElementsOfDigitLength = 0; ElementsOfDigitLength < UpperBound; ElementsOfDigitLength++)
                    {
                        //Wende Fakultät für jeden an


                        //Für jede dieser Terme verknüpfe sie mit mit allen nDigit-DigitLenght Termen
                        int RemainingDigitDifference = nDigit - DigitLenght - 1;
                        for (int ElementsOfRemainingDigitDifference = 0; ElementsOfRemainingDigitDifference < ListTerms[RemainingDigitDifference].Count; ElementsOfRemainingDigitDifference++)
                        {
                            //Erstelle alle sinnvollen Terme aus den zwei aktuellen Termen
                            CreateTerms(DigitLenght, ElementsOfDigitLength, RemainingDigitDifference, ElementsOfRemainingDigitDifference, nDigit, Task);

                            //Breche ab, wenn GoalNum2 erreicht
                            if (Task == 2 && GoalNumber2Reached)
                            {
                                NeededNumberOfDigits2 = nDigit + 1;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void CreateTerms(int IndexA1, int IndexA2, int IndexB1, int IndexB2, int nDigit, int Task)
        {
            Term NewTerm;
            Term Term1 = ListTerms[IndexA1][IndexA2];
            Term Term2 = ListTerms[IndexB1][IndexB2];

            //Schaue ob Term2 größer ist als Term1, wenn ja tausche diese
            if (Term1.GetResult() < Term2.GetResult())
            {
                Term1 = ListTerms[IndexB1][IndexB2];
                Term2 = ListTerms[IndexA1][IndexA2];
            }


            //Addition
            NewTerm = new AddOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
            {
                ListTerms[nDigit].Add(NewTerm);
                ListResults.Add(NewTerm.GetResult(), 0);
            }

            //Subtraction
            NewTerm = new SubtractOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
            {
                ListTerms[nDigit].Add(NewTerm);
                ListResults.Add(NewTerm.GetResult(), 0);
            }

            //Multiplication
            NewTerm = new MultiplyOperator(Term1, Term2);
            if (CheckTerm(NewTerm, Task))
            {
                ListTerms[nDigit].Add(NewTerm);
                ListResults.Add(NewTerm.GetResult(), 0);
            }

            //Division
            if (DivisionOperator.IsCalculatable(Term1, Term2))
            {
                NewTerm = new DivisionOperator(Term1, Term2);
                if (CheckTerm(NewTerm, Task))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    ListResults.Add(NewTerm.GetResult(), 0);
                }
            }

            //Modulo
            if (BoolModulo)
            {
                NewTerm = new ModuloOperator(Term1, Term2);
                if (CheckTerm(NewTerm, Task) && ModuloOperator.IsCalculatable(Term2))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    ListResults.Add(NewTerm.GetResult(), 0);
                }
            }

            //Wird nur ausgeführt, wenn GoalNumber1 schon erreicht wurde
            if (Task == 2)
            {
                //Power
                if (PowerOperator.IsCalculatable(Term1, Term2))
                {
                    NewTerm = new PowerOperator(Term1, Term2);
                    if (CheckTerm(NewTerm, Task))
                    {
                        ListTerms[nDigit].Add(NewTerm);
                        ListResults.Add(NewTerm.GetResult(), 0);

                    }
                }
            }
        }
        private bool CheckTerm(Term NewTerm, int Task)
        {
            long TermResult = NewTerm.GetResult();
            if (ListResults.ContainsKey(TermResult))
            {
                NumberOfDeletedTerms++;
                return false;
            }
            else if (TermResult == 0)
            {
                NumberOfDeletedTerms++;
                return false;
            }
            else if (TermResult == GoalNumber)
            {
                if (Task == 1 && !GoalNumber1Reached)
                {
                    //GoalNumber1 wurde getroffen
                    GoalNumber1Reached = true;
                    LabelResult1Term.Content = NewTerm.PrintTerm();

                }
                else if (Task == 2)
                {
                    //GoalNumber2 wurde getroffen
                    GoalNumber2Reached = true;
                    LabelResult2Term.Content = NewTerm.PrintTerm();

                }
            }
            return true;
        }
        private long GetNumberOfPossibleTerms(int Task, int nDigit)
        {
            return 0;
        }



        private void ButtonCalculateA_Click(object sender, RoutedEventArgs e)
        {
            //CalculateJustA = true;
            ButtonCalculateAB_Click(null, e);
        }
        private void DigitPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Digit = int.Parse(TextBoxDigit.Text);
                if (Digit == 9)
                {
                    return;
                }
                TextBoxDigit.Text = (++Digit).ToString();
            }
            catch
            {
                return;
            }
        }
        private void DigitMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Digit = int.Parse(TextBoxDigit.Text);
                if (Digit == 1)
                {
                    return;
                }
                TextBoxDigit.Text = (--Digit).ToString();
            }
            catch
            {
                return;
            }
        }
    }
}
