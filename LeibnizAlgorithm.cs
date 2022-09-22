using java.math;

namespace Formula_Leibniz
{
    /// <summary>
    ///     class can execulte Leibniz algorithm
    /// </summary>
    public class LeibnizAlgorithm
    {

        /// <summary>
        ///     Context for math calcs
        /// </summary>
        public MathContext Context { get; private set; }

        /// <summary>
        ///     RawPI 
        /// </summary>
        /// <example>
        ///     0,785398... * 4 = 3.141592...
        /// </example>
        public BigDecimal RawPI { get; private set; }

        /// <summary>
        ///     Initial value 
        /// </summary>
        public long Initial { get; private set; }

        /// <summary>
        ///     Actual n value iteration
        /// </summary>
        public long N { get; private set; }

        /// <summary>
        ///     Target for algorith calc
        /// </summary>
        public long Max { get; private set; }

        /// <summary>
        ///     Constructor for new algorith instance
        /// </summary>
        /// 
        /// <param name="initial">
        ///     Initial value
        /// </param>
        /// 
        /// <param name="max">
        ///     Target for algorith calc (this.Max = initial + max)
        /// </param>
        /// 
        /// <param name="context">
        ///     Context for math calcs
        /// </param>
        public LeibnizAlgorithm(long initial, long max, MathContext context)
        {
            this.N = this.Initial = initial;
            this.Max = initial + max;
            this.Context = context;
            this.RawPI = ConstNumber.ZERO;
        }

        /// <summary>
        ///     Start Synchronously 
        /// </summary>
        /// 
        /// <returns>
        ///     BigDecimal RawPi
        /// </returns>
        public BigDecimal Start()
        {
            N = Initial;
            this.RawPI = ConstNumber.ZERO;
            while (N < Max)
            {
                RawPI = RawPI.add(CalculateN(N++));
            }

            return RawPI;
        }

        /// <summary>
        ///     <seealso cref="LeibnizAlgorithm.Start"/> ASynchronously 
        /// </summary>
        /// 
        /// <returns>
        ///     Task<BigDecimal> RawPi
        /// </returns>
        public async Task<BigDecimal> StartAsync() => await Task.Run(Start);

        /// <summary>
        ///     Calculate next value
        /// </summary>
        /// 
        /// <param name="n">
        ///     value
        /// </param>
        /// 
        /// <returns>
        ///     value result
        /// </returns>
        private BigDecimal CalculateN(long n)
        {
            BigDecimal signal = (n % 2 == 0) ? ConstNumber.ONE : ConstNumber.NAGATIVE_ONE;
            BigDecimal divisor = BigDecimal.valueOf(n)
                                 .multiply(ConstNumber.TWO)
                                 .add(ConstNumber.ONE);

            return signal.divide(divisor, Context);
        }
    }

    /// <summary>
    ///     Static class with methods for work with algorithms 
    /// </summary>
    public static class LeibnizAlgorithmUtils
    {
        /// <summary>
        ///     Sum all RawPI and multiply four to get result PI
        /// </summary>
        /// 
        /// <param name="algorithms">
        ///     Array of algorithms
        /// </param>
        /// 
        /// <returns>
        ///     BigDecimal PI value
        /// </returns>
        public static BigDecimal CalcResultPI(this LeibnizAlgorithm[] algorithms)
        {
            return algorithms.Aggregate(BigDecimal.ZERO, (accumulator, value) =>
                { return accumulator.add(value.RawPI); })
                .multiply(ConstNumber.FOUR);
        }

        /// <summary>
        ///     Start all algorithms
        /// </summary>
        /// 
        /// <param name="algorithms">
        ///     Array of algorithms
        /// </param>
        /// 
        /// <returns>
        ///     Started task array with same length of algorithms
        /// </returns>
        public static Task<BigDecimal>[] StartAllTasks(this LeibnizAlgorithm[] algorithms)
        {
            return (from LeibnizAlgorithm a in algorithms select a.StartAsync()).ToArray();
        }

        /// <summary>
        ///     Splits tasks to multiple instances of the algorithm
        /// </summary>
        /// 
        /// <param name="numberOfThreads">
        ///     Number of threads 
        /// </param>
        /// 
        /// <param name="limitSeries">
        ///     Limit of series
        /// </param>
        ///
        /// <param name="mathContext">
        ///     Math context used in calcs
        /// </param>
        /// 
        /// <returns>
        ///     Array with length numberOfThrea 
        /// </returns>
        public static LeibnizAlgorithm[] DivideTasks(int numberOfThreads, long limitSeries, MathContext mathContext)
        {
            long perThread = limitSeries / numberOfThreads;
            long lastThreadMax = perThread + (limitSeries - perThread * numberOfThreads);
            long max = perThread;

            LeibnizAlgorithm[] algorithms = new LeibnizAlgorithm[numberOfThreads];

            for (int i = 0; i < numberOfThreads; i++)
            {
                long initial = perThread * i;

                if (i == numberOfThreads - 1) max = lastThreadMax;
                algorithms[i] = new LeibnizAlgorithm(initial, max, mathContext);
            }

            return algorithms;
        }
    }
}
