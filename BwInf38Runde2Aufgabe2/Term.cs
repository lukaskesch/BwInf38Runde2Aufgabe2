using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BwInf38Runde2Aufgabe2
{
    public abstract class Term
    {

        public virtual long GetResult()
        {
            return 0;
        }
        public virtual string PrintTerm()
        {
            return string.Empty;
        }
    }
    public class Literal : Term
    {
        long Value;
        public Literal(long _Value) : base()
        {
            Value = _Value;
        }

        public override long GetResult()
        {
            return Value;
        }
        public override string PrintTerm()
        {
            return Value.ToString();
        }
    }
    public class FactorialOperator : Term
    {
        private long Number;
        public FactorialOperator(long Input) : base() 
        {
            Number = Input;
        }

        public override long GetResult()
        {
            long result = 1;
            while (Number != 1)
            {
                result *= Number;
                Number--;
            }
            return result;
        }

        public static bool IsCalculatable(Term Term1)
        {
            return true;
        }
    }

    public abstract class DoubleInputOperator : Term
    {
        protected Term Term1;
        protected Term Term2;
        //Konstruktor
        public DoubleInputOperator(Term _Term1, Term _Term2)
        {
            Term1 = _Term1;
            Term2 = _Term2;
        }
    }

    public class AddOperator : DoubleInputOperator
    {
        public AddOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return  Term1.GetResult() + Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "+" + Term2.PrintTerm() + ")";
        }
    }

    public class SubtractOperator : DoubleInputOperator
    {
        public SubtractOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return Term1.GetResult() - Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "-" + Term2.PrintTerm() + ")";
        }
    }

    public class MultiplyOperator : DoubleInputOperator
    {
        public MultiplyOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return Term1.GetResult() * Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "*" + Term2.PrintTerm() + ")";
        }
    }

    public class DivisionOperator : DoubleInputOperator
    {
        public DivisionOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return Term1.GetResult() / Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "/" + Term2.PrintTerm() + ")";
        }
    }

    public class ModuloOperator : DoubleInputOperator
    {
        public ModuloOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return Term1.GetResult() % Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "%" + Term2.PrintTerm() + ")";
        }
    }

    public class PowerOperator : DoubleInputOperator
    {
        public PowerOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return (int)Math.Pow(Term1.GetResult(), Term2.GetResult());
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + ")^(" + Term2.PrintTerm() + ")";
        }

        public static bool IsCalculatable(Term Term1, Term Term2)
        {
            return true;
        }
    }
}
