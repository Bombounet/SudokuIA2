using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SudokuIA2
{
    interface ISudokuSolver
    {
        Sudoku Sudoku { get; set; }
        string Name { get; }
        void Solve();
    }

    class Program
    {
        static Sudoku sudoku;
        static List<ISudokuSolver> solvers;

        static void Main(string[] args)
        {
            sudoku = new Sudoku();
            refreshSolver();

            bool Quitter = false;

            do
            {
                Console.WriteLine("\n\n\n                Résolution de Sudoku\n");

                for (int i = 0; i < solvers.Count; i++)
                {
                    Console.WriteLine("                " + i + ". " + solvers[i].Name);
                }

                Console.WriteLine("                " + (solvers.Count) + ". Benchmark Easy");
                Console.WriteLine("                " + (solvers.Count + 1) + ". Benchmark Hardest");
                Console.WriteLine("                " + (solvers.Count + 2) + ". Benchmark Top 95");
                Console.WriteLine("                " + (solvers.Count + 3) + ". Benchmark Custom");
                Console.WriteLine("                " + (solvers.Count + 4) + ". Quitter");

                Console.WriteLine("\n                Que voulez vous faire ?");

                int choix;
                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    choix = -1;
                    Console.WriteLine("\n\n                Saisie invalide\n\n");
                }

                var watch = Stopwatch.StartNew();
                watch.Stop();
                float elapsedMs;

                if (choix >= 0 && choix < solvers.Count)
                {
                    Console.WriteLine("\n        /*--------------------" + solvers[choix].Name + "--------------------*/\n");
                    watch = Stopwatch.StartNew();
                    solvers[choix].Solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (solvers[choix].Sudoku.validationSudoku())
                        Console.WriteLine("        !!! FELICITATION !!! : Ce sudoku est validé");
                    solvers[choix].Sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN " + solvers[choix].Name + "--------------------*/\n");
                }
                else if (choix == solvers.Count)
                {
                    showScore(benchmark(sudoku.getFile("Sudoku_Easy50.txt")), 50, "Easy");
                }
                else if (choix == solvers.Count + 1)
                {
                    showScore(benchmark(sudoku.getFile("Sudoku_hardest.txt")), 11, "Hardest");
                }
                else if (choix == solvers.Count + 2)
                {
                    showScore(benchmark(sudoku.getFile("Sudoku_Top95.txt")), 95, "Top 95");
                }
                else if (choix == solvers.Count + 3)
                {
                    List<Dictionary<String, float>> scores = new List<Dictionary<string, float>>();

                    benchmark(customSudoku(30));
                    for (int i = 30; i >= 17; i--)
                    {
                        scores.Add(benchmark(customSudoku(i)));
                        foreach (KeyValuePair<String, float> score in scores[-i + 30])
                        {
                            if (score.Value > 5000 || score.Value < 0)
                            {
                                for (int j = 0; j < solvers.Count; j++)
                                {
                                    if (solvers[j].Name == score.Key)
                                    {
                                        solvers.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    refreshSolver();
                    Console.WriteLine("\n        /*--------------------Résultat du Benchmark // Custom--------------------*/\n");
                    for (int i = 30; i >= 17; i--)
                    {
                        showCustomScore(scores[-i + 30], 10, ""+i);
                    }
                    Console.WriteLine("\n        /*--------------------FIN Résultat du Benchmark--------------------*/\n");
                }
                else if (choix == solvers.Count + 4)
                {
                    Quitter = true;
                }

            } while (!Quitter);

            Console.WriteLine("\n\n\n        FIIIIIN");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void refreshSolver()
        {
            List<ISudokuSolver> solversInvers = new List<ISudokuSolver>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var objType in assembly.GetTypes())
                {
                    if (typeof(ISudokuSolver).IsAssignableFrom(objType) && !(typeof(ISudokuSolver) == objType))
                    {
                        solversInvers.Add((ISudokuSolver)Activator.CreateInstance(objType));
                    }
                }
            }
            solvers = new List<ISudokuSolver>();
            for(int i = solversInvers.Count-1; i >= 0; i--)
            {
                solvers.Add(solversInvers[i]);
            }
        }

        public static String[] customSudoku(int dificulty)
        {
            int nb = 10;
            String[] lines = new String[nb];
            for (int i = 0; i < nb; i++)
            {
                lines[i] = sudoku.create(dificulty);
            }

            return lines;
        }

        public static Dictionary<String, float> benchmark(String[] lines)
        {
            Dictionary<String, float> scores = new Dictionary<String, float>();

            for (int i = 0; i < solvers.Count; i++)
            {
                scores.Add(solvers[i].Name, 0);
                Console.WriteLine("\n\n        Test " + solvers[i].Name);
                for (int j = 0; j < lines.Length; j++)
                {
                    float score;
                    try
                    {
                        score = testSolution(solvers[i], lines[j]);
                    }
                    catch (Exception e)
                    {
                        score = -1;
                        Console.WriteLine("\n\n{0} Exception caught.\n\n", e);
                    }
                    if (score == -1)
                    {
                        scores[solvers[i].Name] = -1;
                        Console.WriteLine("        Sudoku " + j + " failed validation");
                        break;
                    }
                    scores[solvers[i].Name] += score;
                    Console.WriteLine("        Sudoku " + j + " success in : " + score + "ms");
                }
            }

            return scores;
        }

        public static float testSolution(ISudokuSolver solver, String sudoku)
        {
            var watch = Stopwatch.StartNew();
            float elapsedMs;

            solver.Sudoku.newSudoku(sudoku);
            watch = Stopwatch.StartNew();
            solver.Solve();
            watch.Stop();
            elapsedMs = watch.ElapsedMilliseconds;
            if (!solver.Sudoku.validationSudoku())
            {
                solver.Sudoku.showTwoSudoku();
                return -1;
            }
            //solver.Sudoku.showTwoSudoku();
            return elapsedMs;
        }

        public static void showScore(Dictionary<String, float> scores, int nbSudoku, String dificulty)
        {
            Console.WriteLine("\n        /*--------------------Résultat du Benchmark // Dificulté : " + dificulty + "--------------------*/\n");
            Console.WriteLine("        " + nbSudoku + " sodokus\n");
            foreach (KeyValuePair<String, float> score in scores)
            {
                if (score.Value == -1)
                    Console.WriteLine("        " + score.Key + " : Non validé\n");
                else
                    Console.WriteLine("        " + score.Key + " : Validé en " + score.Value + "ms  (" + (score.Value / nbSudoku) + " ms/sodoku)\n");
            }
            Console.WriteLine("\n        /*--------------------FIN Résultat du Benchmark--------------------*/\n");
        }

        public static void showCustomScore(Dictionary<String, float> scores, int nbSudoku, String dificulty)
        {
            Console.WriteLine("\n\n        /*---Dificulté : " + dificulty + " // " + nbSudoku + " sodokus---*/\n");
            foreach (KeyValuePair<String, float> score in scores)
            {
                if (score.Value == -1)
                    Console.WriteLine("        " + score.Key + " : Non validé\n");
                else
                    Console.WriteLine("        " + score.Key + " : Validé en " + score.Value + "ms  (" + (score.Value / nbSudoku) + " ms/sodoku)\n");
            }
            
        }
    }
}
