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
        int Number;
        int Digit;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            Prepare();


        }
        private void Prepare()
        {
            //try
            {
                Number = int.Parse(TextBoxNumberToCalculate.Text);
                Digit = int.Parse(TextBoxDigit.Text);
                Calculation.CalculateTerm(Number, Digit);
                LabelResult1.Content = Calculation.GetFirstTerm();
                LabelResult2.Content = Calculation.GetSecondTerm();
                
            }
            //catch (Exception)
            {
                //MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");
                //throw;
            }
        }
       
    }
}
