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
        int AbortCondition;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            Prepare();
            AddOperator addOperator = new AddOperator(new AddOperator(2, 2), 4);
            //MessageBox.Show(addOperator.GetResult().ToString());
            PowerOperator powerOperator = new PowerOperator(5, 2);
            MessageBox.Show(powerOperator.GetResult().ToString());

            Calculation.CalculateTerm(0, 0);
        }
        private void Prepare()
        {
            try
            {
                Number = int.Parse(TextBoxNumberToCalculate.Text);
                Digit = int.Parse(TextBoxDigit.Text);
                AbortCondition = DataProcessing.GetAbortCondition(Number, Digit);
            }
            catch (Exception)
            {
                MessageBox.Show("Die eingegebenen Parameter konnten nicht entgegen genommen werden");
                throw;
            }
        }
       
    }
}
