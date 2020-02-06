using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf38Runde2Aufgabe2
{
    public abstract class SingleInputOperator
    {
        protected double Number;

        public SingleInputOperator(double Input)
        {
            Number = Input;
        }
        public virtual double GetResult()
        {
            return -1;
        }
    }
    public class FactorialOperator : SingleInputOperator
    {
        public FactorialOperator(double Input) : base(Input) { }

        public override double GetResult()
        {
            double result = 1;
            while (Number != 1)
            {
                result *= Number;
                Number--;
            }
            return base.GetResult();
        }
    }

    public abstract class DoubleInputOperator
    {
        protected double Number1;
        protected double Number2;
        //Konstruktor
        public DoubleInputOperator(double Input1, double Input2)
        {
            Number1 = Input1;
            Number2 = Input2;
        }
        public virtual double GetResult()
        {
            return -1;
        }
    }

    public class AddOperator : DoubleInputOperator
    {
        public AddOperator(double Input1, double Input2) : base(Input1, Input2) { }

        public override double GetResult()
        {
            return Number1 + Number2;
        }
    }

    public class SubtractOperator : DoubleInputOperator
    {
        public SubtractOperator(double Input1, double Input2) : base(Input1, Input2) { }

        public override double GetResult()
        {
            return Number1 - Number2;
        }
    }

    public class MultiplyOperator : DoubleInputOperator
    {
        public MultiplyOperator(double Input1, double Input2) : base(Input1, Input2) { }

        public override double GetResult()
        {
            return Number1 * Number2;
        }
    }

    public class DivisionOperator : DoubleInputOperator
    {
        public DivisionOperator(double Input1, double Input2) : base(Input1, Input2) { }

        public override double GetResult()
        {
            return Number1 / Number2;
        }
    }

    public class PowerOperator : DoubleInputOperator
    {
        public PowerOperator(double Input1, double Input2) : base(Input1, Input2) { }

        public override double GetResult()
        {
            return Math.Pow(Number1, Number2);
        }
    }
}
