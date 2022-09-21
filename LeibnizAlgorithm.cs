using java.math;

namespace Formula_Leibniz
{
    public class LeibnizAlgorithm
    {
        public MathContext Context { get; private set; }
        public BigDecimal RawPI { get; private set; }
        public long Initial_i { get; private set; }
        public long I { get; private set; }
        public long Max { get; private set; }

        public LeibnizAlgorithm(long i, long max, MathContext context)
        {
            this.I = i;
            this.Initial_i = i;
            this.Max = i + max;
            this.Context = context;
            this.RawPI = ConstNumber.ZERO;
        }

        public BigDecimal Start()
        {
            I = Initial_i;
            this.RawPI = ConstNumber.ZERO;
            while (I < Max)
            {
                RawPI = RawPI.add(CalculateN(I++), Context);
            }

            return RawPI;
        }

        public async Task<BigDecimal> StartAsync() => await Task.Run(Start);

        private BigDecimal CalculateN(long n)
        {
            BigDecimal signal = (n % 2 == 0) ? ConstNumber.ONE : ConstNumber.NAGATIVE_ONE;
            BigDecimal subResult2 = BigDecimal.valueOf(n)
                                 .multiply(ConstNumber.TWO, Context)
                                 .add(ConstNumber.ONE, Context);

            return signal.divide(subResult2, Context);
        }
    }

    public static class LeibnizAlgorithmUtils
    {
        public static BigDecimal CalcResultPI(this LeibnizAlgorithm[] algorithms)
        {
            return algorithms.Aggregate(BigDecimal.ZERO, (accumulator, value) =>
                { return accumulator.add(value.RawPI, value.Context); })
                .multiply(ConstNumber.FOUR);
        }

        public static Task<BigDecimal>[] StartAllTasks(this LeibnizAlgorithm[] algorithms)
        {
            return (from LeibnizAlgorithm a in algorithms select a.StartAsync()).ToArray();
        }

        public static LeibnizAlgorithm[] SubDiviteTasks(int threadCount, long maxInterations, MathContext mathContext)
        {
            long perThread = maxInterations / threadCount;
            long lastThreadMax = perThread + (maxInterations - perThread * threadCount);

            LeibnizAlgorithm[] algorithms = new LeibnizAlgorithm[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                long inital = perThread * i;
                long max = perThread;
                
                if (i == threadCount - 1) max = lastThreadMax;
                algorithms[i] = new LeibnizAlgorithm(inital, max, mathContext);
            }

            return algorithms;
        }
    }
}
