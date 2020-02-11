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
        private Term PriorTerm;
        private static SortedList<long, long> FactorialResults = new SortedList<long, long>();
        public FactorialOperator(Term _PriorTerm) : base()
        {
            PriorTerm = _PriorTerm;
        }

        public override long GetResult()
        {
            int index = FactorialResults.IndexOfValue(PriorTerm.GetResult());
            return FactorialResults[index];
        }

        public static bool IsCalculatable(Term Term1)
        {
            try
            {
                long result = 1;
                long Number = Term1.GetResult();
                while (Number != 1)
                {
                    result *= Number;
                    Number--;
                }
                FactorialResults.Add(Number, Term1.GetResult());
                return true;
            }
            catch
            {
                return false;
            }
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
        long result;
        public AddOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = Term1.GetResult() + Term2.GetResult();
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "+" + Term2.PrintTerm() + ")";
        }
    }

    public class SubtractOperator : DoubleInputOperator
    {
        long result;
        public SubtractOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = Term1.GetResult() - Term2.GetResult();
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "-" + Term2.PrintTerm() + ")";
        }
    }

    public class MultiplyOperator : DoubleInputOperator
    {
        long result;
        public MultiplyOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = Term1.GetResult() * Term2.GetResult();
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "*" + Term2.PrintTerm() + ")";
        }
    }

    public class DivisionOperator : DoubleInputOperator
    {
        long result;
        public DivisionOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = Term1.GetResult() / Term2.GetResult();
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "/" + Term2.PrintTerm() + ")";
        }
        public static bool IsCalculatable(Term Term1, Term Term2)
        {
            int Remainder = (int)(Term1.GetResult() % Term2.GetResult());
            if (Remainder == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class ModuloOperator : DoubleInputOperator
    {
        long result;
        public ModuloOperator(Term Term1, Term Term2) : base(Term1, Term2) { }

        public override long GetResult()
        {
            return Term1.GetResult() % Term2.GetResult();
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "%" + Term2.PrintTerm() + ")";
        }
        public static bool IsCalculatable(Term Term2)
        {
            if (Term2.GetResult() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class PowerOperator : DoubleInputOperator
    {
        long result;
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
            long Dummy;
            return long.TryParse(Math.Pow(Term1.GetResult(), Term2.GetResult()).ToString(), out Dummy);
        }
    }
}
