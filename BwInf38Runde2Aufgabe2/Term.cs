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
        public static double GetLogLong()
        {
            return Math.Log10(long.MaxValue);
        }
        public static double GetLogLogLong()
        {
            return Math.Log10(Math.Log10(long.MaxValue));
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
        long result;
        public FactorialOperator(Term PriorTerm) : base()
        {
            result = 1;
            long Number = PriorTerm.GetResult();
            while (Number != 1)
            {
                result *= Number;
                Number--;
            }
        }

        public override long GetResult()
        {
            return result;
        }

        public static bool IsCalculatable(Term Term1)
        {
            if (Term1.GetResult() <= 20)
            {
                return true;
            }
            else
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
        public static bool IsCalculatable(Term Term1, Term Term2)
        {
            long Check = long.MaxValue;
            Check -= Term1.GetResult() + Term2.GetResult();
            if (Check > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public static bool IsCalculatable(Term Term1, Term Term2)
        {
            if (GetLogLong() - 0.00000001 > Math.Log10(Term1.GetResult()) + Math.Log10(Term2.GetResult()))
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public ModuloOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = Term1.GetResult() % Term2.GetResult();
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + "%" + Term2.PrintTerm() + ")";
        }

    }

    public class PowerOperator : DoubleInputOperator
    {
        long result;
        public PowerOperator(Term Term1, Term Term2) : base(Term1, Term2)
        {
            result = (long)Math.Pow(Term1.GetResult(), Term2.GetResult());
        }

        public override long GetResult()
        {
            return result;
        }
        public override string PrintTerm()
        {
            return "(" + Term1.PrintTerm() + ")^(" + Term2.PrintTerm() + ")";
        }

        public static bool IsCalculatable(Term Term1, Term Term2)
        {
            if (GetLogLogLong() - 0.000001 > Math.Log10(Term2.GetResult()) + Math.Log10(Math.Log10(Term1.GetResult())))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
