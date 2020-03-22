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
        int Digit, nDigit;
        long GoalNumber;
        int NeededNumberOfDigits1, NeededNumberOfDigits2;
        byte Task;
        long NumberOfDeletedTerms;
        bool BoolModulo = false;
        bool GoalNumber1Reached, GoalNumber2Reached;

        List<List<Term>> ListTerms = new List<List<Term>>();
        SortedDictionary<long, byte> DictionaryResult = new SortedDictionary<long, byte>();
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
                GoalNumber1Reached = false;
                GoalNumber2Reached = false;
                ListTerms = new List<List<Term>>();
                DictionaryResult = new SortedDictionary<long, byte>();

                BoolModulo = (bool)CheckBoxModulo.IsChecked;
                GoalNumber = long.Parse(TextBoxNumberToCalculate.Text);
                Digit = int.Parse(TextBoxDigit.Text);

                if (Digit < 1 || Digit > 9)
                {
                    MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");
                    return;
                }

                stopwatch.Restart();
                Task = 1;
                CalculateTerm();
                CalculationTime = stopwatch.ElapsedMilliseconds;
                CalculationTime /= 1000;
                stopwatch.Stop();

                LabelResult1Time.Content = CalculationTime.ToString();
                LabelResult1NeededTerms.Content = DictionaryResult.Count.ToString();
                LabelResult1DeletedTerms.Content = NumberOfDeletedTerms.ToString();
                LabelResult1nDigits.Content = NeededNumberOfDigits1.ToString();


                if (sender != null)
                {
                    stopwatch.Restart();
                    ListTerms = new List<List<Term>>();
                    DictionaryResult = new SortedDictionary<long, byte>();
                    Task = 2;
                    NumberOfDeletedTerms = 0;
                    CalculateTerm();
                    CalculationTime = stopwatch.ElapsedMilliseconds;
                    CalculationTime /= 1000;
                    stopwatch.Stop();

                    LabelResult2Time.Content = CalculationTime.ToString();
                    LabelResult2NeededTerms.Content = DictionaryResult.Count.ToString();
                    LabelResult2DeletedTerms.Content = NumberOfDeletedTerms.ToString();
                    LabelResult2nDigits.Content = NeededNumberOfDigits2.ToString();

                    DictionaryResult = null;
                    ListTerms = null;
                }

            }
            //catch (Exception)
            {
                //MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");

            }
        }

        private void CalculateTerm()
        {

            //Erstelle den ersten Digit
            Literal FirstLiteral = new Literal(Digit);

            //Lege eine Liste an Index 0 an und speicher in ihr First Literal
            ListTerms.Add(new List<Term>());
            ListTerms[0].Add(FirstLiteral);
            DictionaryResult.Add(FirstLiteral.GetResult(), 0);

            //Schaue, ob Literal GoalNumber ist
            if (FirstLiteral.GetResult() == GoalNumber)
            {
                LabelResult1Term.Content = FirstLiteral.GetResult().ToString();
                NeededNumberOfDigits1 = 1;
                return;
            }


            //Erstelle für jede Ziffernlänge (alle) Terme
            for (nDigit = 1; true; nDigit++)
            {
                //Lege Liste für nDigit an
                ListTerms.Add(new List<Term>());

                //Erstelle Literal für nDigit
                long LiteralValue = ListTerms[nDigit - 1][0].GetResult();
                LiteralValue = LiteralValue * 10 + Digit;
                Literal NewLiteral = new Literal(LiteralValue);
                ListTerms[nDigit].Add(NewLiteral);

                //Muss theoretisch noch überprüft werden, ob wert nicht schon erreicht
                if (LiteralValue == GoalNumber)
                {
                    GoalNumber1Reached = true;
                    LabelResult1Term.Content = LiteralValue.ToString();
                }

                if (Task == 2)
                {
                    //Wende Fakultät für nDigit-1 an
                    Term OldTerm;
                    int Lenght = ListTerms[nDigit - 1].Count;

                    for (int i = 0; i < Lenght; i++)
                    {
                        OldTerm = ListTerms[nDigit - 1][i];
                        while (FactorialOperator.IsCalculatable(OldTerm))
                        {
                            Term NewTerm = new FactorialOperator(OldTerm);

                            if (CheckTerm(NewTerm))
                            {
                                ListTerms[nDigit - 1].Add(NewTerm);
                                DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                            }
                            else if (NewTerm.GetResult() == OldTerm.GetResult())
                            {
                                break;
                            }
                            OldTerm = NewTerm;
                        }
                    }
                }
                if (Task == 2 && NeededNumberOfDigits1 - 1 <= nDigit)
                {
                    NeededNumberOfDigits2 = nDigit;
                    LabelResult2Term.Content = "Keine kürzere Lösung gefunden";
                    return;
                }



                //Gehe Ziffernlänge bis zur Hälfte der aktuellen hoch
                for (int DigitLenght = 0; DigitLenght < (nDigit + 1) / 2; DigitLenght++)
                {
                    //Für jede dieser Ziffernlänge gehe alle ihre Terme durch
                    int UpperBound = ListTerms[DigitLenght].Count;
                    for (int ElementsOfDigitLength = 0; ElementsOfDigitLength < UpperBound; ElementsOfDigitLength++)
                    {
                        //Für jede dieser Terme verknüpfe sie mit mit allen nDigit-DigitLenght Termen
                        int RemainingDigitDifference = nDigit - DigitLenght - 1;
                        for (int ElementsOfRemainingDigitDifference = 0; ElementsOfRemainingDigitDifference < ListTerms[RemainingDigitDifference].Count; ElementsOfRemainingDigitDifference++)
                        {
                            //Erstelle alle sinnvollen Terme aus den zwei aktuellen Termen
                            CreateTerms(DigitLenght, ElementsOfDigitLength, RemainingDigitDifference, ElementsOfRemainingDigitDifference);

                            //Breche ab, wenn GoalNum2 erreicht

                            if (Task == 2 && GoalNumber2Reached)
                            {
                                NeededNumberOfDigits2 = nDigit + 1;
                                return;
                            }
                            else if (Task == 1 && GoalNumber1Reached)
                            {
                                NeededNumberOfDigits1 = nDigit + 1;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void CreateTerms(int IndexA1, int IndexA2, int IndexB1, int IndexB2)
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
            if (Task == 1 || AddOperator.IsCalculatable(Term1, Term2))
            {
                NewTerm = new AddOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                }
            }

            //Subtraction
            NewTerm = new SubtractOperator(Term1, Term2);
            if (CheckTerm(NewTerm))
            {
                ListTerms[nDigit].Add(NewTerm);
                DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
            }

            //Multiplication
            if (Task == 1 || MultiplyOperator.IsCalculatable(Term1, Term2))
            {
                NewTerm = new MultiplyOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                }
            }


            //Division
            if (DivisionOperator.IsCalculatable(Term1, Term2))
            {
                NewTerm = new DivisionOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                }
            }

            //Modulo
            if (BoolModulo)
            {
                NewTerm = new ModuloOperator(Term1, Term2);
                if (CheckTerm(NewTerm))
                {
                    ListTerms[nDigit].Add(NewTerm);
                    DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                }
            }

            //Power - Wird nur ausgeführt, wenn GoalNumber1 schon erreicht wurde
            if (Task == 2)
            {
                if (PowerOperator.IsCalculatable(Term1, Term2))
                {
                    NewTerm = new PowerOperator(Term1, Term2);
                    if (CheckTerm(NewTerm))
                    {
                        ListTerms[nDigit].Add(NewTerm);
                        DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                    }
                }
                if (PowerOperator.IsCalculatable(Term2, Term1))
                {
                    NewTerm = new PowerOperator(Term2, Term1);
                    if (CheckTerm(NewTerm))
                    {
                        ListTerms[nDigit].Add(NewTerm);
                        DictionaryResult.Add(NewTerm.GetResult(), (byte)nDigit);
                    }
                }
            }



        }
        private bool CheckTerm(Term NewTerm)
        {
            long TermResult = NewTerm.GetResult();

            //Überprüfe, ob Ergebnis schon existiert
            if (DictionaryResult.ContainsKey(TermResult))
            {
                NumberOfDeletedTerms++;
                return false;
            }
            else if (TermResult <= 0)
            {
                NumberOfDeletedTerms++;
                return false;
            }
            else if (Task == 2 && TermResult == GoalNumber)
            {
                //GoalNumber2 wurde getroffen
                GoalNumber2Reached = true;
                LabelResult2Term.Content = NewTerm.PrintTerm();
                //TB2.Text = NewTerm.PrintTerm();

                return false;
            }
            else if (Task == 1 && TermResult == GoalNumber)
            {
                //GoalNumber1 wurde getroffen
                GoalNumber1Reached = true;
                LabelResult1Term.Content = NewTerm.PrintTerm();
                //TB1.Text = NewTerm.PrintTerm();

                return false;
            }

            return true;
        }



        private long GetNumberOfPossibleTerms()
        {
            return 0;
        }
        private void ButtonCalculateA_Click(object sender, RoutedEventArgs e)
        {
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
                TextBoxDigit.Text = 1.ToString();
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
                TextBoxDigit.Text = 1.ToString();
                return;
            }
        }
    }
}
