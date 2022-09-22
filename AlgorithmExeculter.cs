using java.math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula_Leibniz
{
    public class AlgorithmExeculter
    {

        /// <summary>
        ///     Delay between updates
        /// </summary>
        private int StatusUpdateDelay { get; set; }

        /// <summary>
        ///     Limit of interations
        /// </summary>
        private long MaxInterations { get; set; }

        /// <summary>
        ///     Context used in math calcs
        /// </summary>
        private MathContext MathContext { get; set; }

        /// <summary>
        ///     Time seted when execulte start
        /// </summary>
        private DateTime StartTime { get; set; }

        /// <summary>
        ///     Array with all algorithms instances
        /// </summary>
        private LeibnizAlgorithm[] Algorithms { get; set; }

        /// <summary>
        ///     Array with all workers threads
        /// </summary>
        private Task<BigDecimal>[]? Threads { get; set; }

        public AlgorithmExeculter(int numberOfThreads, long maxInterations, int statusUpdateDelay, MathContext mathContext)
        {
            this.Algorithms = LeibnizAlgorithmUtils.DivideTasks(numberOfThreads, maxInterations, mathContext);
            this.MaxInterations = maxInterations;
            this.StatusUpdateDelay = statusUpdateDelay;
            this.MathContext = mathContext;
        }

        /// <summary>
        ///     Execulte algorithm, run status loop and all tasks
        /// </summary>
        /// 
        /// <returns>
        ///     Result PI when all tasks is end
        /// </returns>
        public BigDecimal Execulte()
        {
            StartTime = DateTime.Now;
            Task OnEnd = InitTasks();

            Task statusLoop = Task.Run(() =>
            {
                int oldLine1Length = 0, oldLine2Length = 0, oldLine3Length = 0;
                int lastIteration = 0;

                while (lastIteration <= 1)
                {
                    Console.SetCursorPosition(0, 0);

                    #region Line 1

                    string line1 = $"Enlapsed Time: {EnlapsedTime()} - " +
                        $"Expected Time To Complete: {ExpectedTime()} - " +
                        $"Progress: {ProgressPercentegeString()}";
                    Console.WriteLine(line1.PadRight(oldLine1Length, ' '));
                    oldLine1Length = line1.Length;

                    #endregion Line 1

                    #region Line 2

                    string line2 = $"Active Threads: {CountActiveThreads():N0}/{Threads?.Length:N0} - ";
                    Console.Write(line2);

                    int percentege = (int)ProgressPercentege().doubleValue() / 2;
                    Console.Write("Progress Bar: (");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("".PadRight(percentege, ' '));
                    Console.ResetColor();
                    int line2dif = oldLine2Length - line2.Length;
                    Console.WriteLine(")".PadLeft(50 - percentege, ' ') + "".PadRight(line2dif > 0 ? line2dif : 0, ' '));
                    oldLine2Length = line2.Length;

                    #endregion Line 2

                    #region Line 3

                    string line3 = $"Actual PI Value: {Algorithms.CalcResultPI()}";
                    Console.WriteLine(line3.PadRight(oldLine3Length, ' '));
                    oldLine3Length = line3.Length;

                    #endregion Line 3
                    
                    Thread.Sleep(StatusUpdateDelay);
                    if (OnEnd.IsCompleted) lastIteration++;
                }
            });
            
            statusLoop.Wait();

            return Algorithms.CalcResultPI();
        }

        #region Progress Calcs
  
        /// <summary>
        ///     Sum all algorithms progress
        /// </summary>
        /// 
        /// <returns>
        ///     Long result of sum
        /// </returns>
        private long Progress()
        {
            return (from LeibnizAlgorithm a in Algorithms select a.N - a.Initial).Sum();
        }

        /// <summary>
        ///     Calculate progress percentage  
        ///     (<see cref="Progress"/> divide by <see cref="MaxInterations"/>) and multiply by 100
        /// </summary>
        /// 
        /// <returns>
        ///     BigDecimal with calc result
        /// </returns>
        private BigDecimal ProgressPercentege()
        {
            return BigDecimal.valueOf(Progress())
                                .divide(BigDecimal.valueOf(MaxInterations), MathContext)
                                .multiply(ConstNumber.ONE_HUNDRED, MathContext);
        }

        /// <summary>
        ///     Convert <see cref="ProgressPercentege"/> result to formated string
        /// </summary>
        /// 
        /// <returns>
        ///     Percentege string
        /// </returns>
        private string ProgressPercentegeString()
        {
            return String.Format("{0:0.00################}%", ProgressPercentege().doubleValue());
        }

        #endregion Progress Calcs

        #region Time Calc

        /// <summary>
        ///     Old progress geted in last execulte
        /// </summary>
        private long oldProgress = 0;

        /// <summary>
        ///     Old time geted in last execulte
        /// </summary>
        private DateTime oldTime = DateTime.Now;

        /// <summary>
        ///     Calculate expected time to complete execution
        /// </summary>
        /// 
        /// <returns>
        ///     Formated string of time
        /// </returns>
        private string ExpectedTime()
        {
            long enlapsedMS = (long)Math.Max((DateTime.Now - oldTime).TotalMilliseconds, 1);
            oldTime = DateTime.Now;
            
            long progressNow = Progress();
            double progressDif = Math.Max((progressNow - oldProgress) * (1000.0 / enlapsedMS), 1);
            oldProgress = progressNow;
            
            try
            {
                return TimeSpan.FromSeconds((MaxInterations - progressNow) / progressDif).ToString("hh':'mm':'ss");
            }
            catch
            {
                return "NA";
            }
        }

        /// <summary>
        ///     Difference of now and start time
        /// </summary>
        /// 
        /// <returns>
        ///     Formated string of time
        /// </returns>
        private string EnlapsedTime()
        {
            return (DateTime.Now - StartTime).ToString("hh':'mm':'ss");
        }

        #endregion Time Calc

        #region Task Init and Count

        /// <summary>
        ///     Count of active threads
        /// </summary>
        /// 
        /// <returns>
        ///     Count 
        /// </returns>
        private int CountActiveThreads()
        {
            return Threads != null ? Threads.Where(t => !t.IsCompleted).Count() : 0;
        }

        /// <summary>
        ///     Init all threads 
        /// </summary>
        /// 
        /// <returns>
        ///     Task that will complete when all tasks is terminated
        /// </returns>
        private Task InitTasks()
        {
            Task startTask = Task.Run(() => Threads = Algorithms.StartAllTasks());
            string loadingSTR = "\rIniciando threads";
            while (!startTask.IsCompleted)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(loadingSTR += ".");
                Thread.Sleep(250);
            }
            
            Console.Clear();
            return Threads != null ? Task.WhenAll(Threads) : Task.CompletedTask;
        }

        #endregion Task Init and Count
    }
}
