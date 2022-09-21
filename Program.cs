using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Numerics;
using System.Reflection;
using java.math;

namespace Formula_Leibniz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int threadCount = 4;
            int precision = 100;
            int updateDelay = 250;
            long maxInterations = 3_000_000;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Calculadora formula Leibniz para PI");

                Console.WriteLine("\r\nConfiguraçoes de execução\r\n");

                Console.WriteLine($"1 - Iniciar execulção");
                Console.WriteLine($"2 - Quantidade de threads     = {threadCount:N0}");
                Console.WriteLine($"3 - Limite da serie           = {maxInterations:N0}");
                Console.WriteLine($"4 - Precissão de calculo      = {precision:N0}");
                Console.WriteLine($"5 - Delay entre atualizações = {updateDelay:N0}");
                Console.WriteLine($"6 - Sair");

                Console.Write("\r\nDigite uma das opções: ");

                if (int.TryParse(Console.ReadLine(), out int option))
                {
                    switch (option)
                    {
                        case 1:
                            {
                                Case_1_ExeculteAlgorith(threadCount, maxInterations, updateDelay, precision);
                                break;
                            }
                        case 2:
                            {
                                Case_2_RequestThreadCount(out threadCount);
                                break;
                            }
                        case 3:
                            {
                                Case_3_RequestLimit(out maxInterations);
                                break;
                            }
                        case 4:
                            {
                                Case_4_RequestPrecision(out precision);
                                break;
                            }
                        case 5:
                            {
                                Case_5_RequestDelay(out updateDelay);
                                break;
                            }
                        case 6:
                            Case_6_Exit();
                            break;
                    }
                }
            }
        }

        #region case 2 - 5

        private static void Case_1_ExeculteAlgorith(int threadCount, long maxInterations, int updateDelay, int precision)
        {
            Console.Clear();
            AlgorithmExeculter execulter = new(threadCount, maxInterations, updateDelay, new(precision, RoundingMode.HALF_EVEN));
            BigDecimal result = execulter.Execulte();
            Console.WriteLine($"\r\nFinal Result: {result}\r\n" + 
                              "Clique enter para continuar");
            Console.ReadLine();
        }

        private static void Case_2_RequestThreadCount(out int threadCount)
        {
            Console.WriteLine("\r\nA quantidade de threads é a quantidade de processos\r\n" +
                              "simultâneos que irão acontecer durante a execução, um número\r\n" +
                              "maior pode agilizar o processo, porem se for realmente\r\n" +
                              "grande >100 pode travar o seu computador, recomendado entre 4 a 12\r\n" +
                              $"valor minimo: 1 | valor maximo: {int.MaxValue:N0}\r\n");
            while (true)
            {
                Console.Write("Digite a nova quantidade de threads: ");
                string? line = Console.ReadLine();
                if (line != null) line = line.Replace(".", "");
                if (int.TryParse(line, out int newThreadCount))
                {
                    if (newThreadCount > 0)
                    {
                        threadCount = newThreadCount;
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Case_3_RequestLimit(out long maxInterations)
        {
            Console.WriteLine("\r\nLimita ate qual numero sera calculado\r\n" +
                              "numeros maiores demoram mais porem aumenta a precissão\r\n" +
                              $"valor minimo: 1 | valor maximo: {long.MaxValue:N0}\r\n");
            while (true)
            {
                Console.Write("Digite o novo limite valor: ");
                string? line = Console.ReadLine();
                if (line != null) line = line.Replace(".", "");
                if (long.TryParse(line, out long newMaxInterations))
                {
                    if (newMaxInterations > 0)
                    {
                        maxInterations = newMaxInterations;
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Case_4_RequestPrecision(out int precision)
        {
            Console.WriteLine("\r\nPrecissão de calculo um valor maior melhora o resultado\r\n" +
                              "porem levara mais tempo para calcular recomendado entre 50 e 200\r\n" +
                              $"valor minimo: 1 | valor maximo: {int.MaxValue:N0}\r\n");
            while (true)
            {
                Console.Write("Digite a nova precissão: ");
                string? line = Console.ReadLine();
                if (line != null) line = line.Replace(".", "");
                if (int.TryParse(line, out int newPrecision))
                {
                    if (newPrecision > 0)
                    {
                        precision = newPrecision;
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Case_5_RequestDelay(out int updateDelay)
        {
            Console.WriteLine("\r\nPrecissão de calculo um valor maior melhora o resultado\r\n" +
                              "porem levara mais tempo para calcular recomendado entre 50 e 200\r\n" +
                              $"valor minimo: 1 | valor maximo: 5.000\r\n");
            while (true)
            {
                Console.Write("Digite o novo delay: ");
                string? line = Console.ReadLine();
                if (line != null) line = line.Replace(".", "");
                if (int.TryParse(line, out int newDelay))
                {
                    if (newDelay > 0 && newDelay <= 5000)
                    {
                        updateDelay = newDelay;
                        break;
                    }
                }
                Console.WriteLine();
            }
        }

        private static void Case_6_Exit()
        {
            Environment.Exit(0);
        }

        #endregion case 2 - 5
    }
}
