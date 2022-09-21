using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula_Leibniz
{
    public static class ConstNumber
    {
        public static readonly BigDecimal ZERO = BigDecimal.ZERO;

        public static readonly BigDecimal ONE = BigDecimal.ONE;
        public static readonly BigDecimal NAGATIVE_ONE = BigDecimal.ONE.negate();

        public static readonly BigDecimal TWO = BigDecimal.valueOf(2);

        public static readonly BigDecimal FOUR = BigDecimal.valueOf(4);

        public static readonly BigDecimal ONE_HUNDRED = BigDecimal.valueOf(100);
    }
}
