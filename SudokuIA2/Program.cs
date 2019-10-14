using System;
using System.Collections.Generic;
using System.Diagnostics;

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

            solvers = new List<ISudokuSolver>();
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var objType in assembly.GetTypes())
                {
                    if (typeof(ISudokuSolver).IsAssignableFrom(objType) && !(typeof(ISudokuSolver) == objType))
                    {
                        solvers.Add((ISudokuSolver)Activator.CreateInstance(objType));
                    }
                }
            }

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

                int choix = int.Parse(Console.ReadLine());

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
                    benchmark(sudoku.getFile("Sudoku_Easy50.txt"));
                }
                else if (choix == solvers.Count + 1)
                {
                    benchmark(sudoku.getFile("Sudoku_hardest.txt"));
                }
                else if (choix == solvers.Count + 2)
                {
                    benchmark(sudoku.getFile("Sudoku_Top95.txt"));
                }
                else if (choix == solvers.Count + 3)
                {
                    int nb = 10;
                    String[] lines = new String[nb];
                    for (int i = 0; i < nb; i++)
                    {
                        lines[i] = sudoku.create(25);
                    }
                    benchmark(lines);
                }
                else if (choix == solvers.Count + 4)
                {
                    Quitter = true;
                }

            } while (!Quitter);

            Console.WriteLine("\n\n\n        FIIIIIN");
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void benchmark(String[] lines)
        {
            float[] scores = new float[solvers.Count];

            for (int i = 0; i < solvers.Count; i++)
            {
                Console.WriteLine("        Test " + solvers[i].Name);
                for (int j = 0; j < lines.Length; j++)
                {
                    float score = testSolution(solvers[i], lines[j]);
                    if (score == -1)
                    {
                        scores[i] = -1;
                        Console.WriteLine("        Sudoku " + j + " failed validation");
                        break;
                    }
                    scores[i] += score;
                    Console.WriteLine("        Sudoku " + j + " success in : " + score + "ms");
                }
            }

            Console.WriteLine("\n        /*--------------------Résultat du Benchmark--------------------*/\n");
            Console.WriteLine("        " + lines.Length + " sodokus\n");
            for (int i = 0; i < solvers.Count; i++)
            {
                if (scores[i] == -1)
                    Console.WriteLine("        " + solvers[i].Name + " : Non validé\n");
                else
                    Console.WriteLine("        " + solvers[i].Name + " : Validé en " + scores[i] + "ms  (" + (scores[i] / lines.Length) + " ms/sodoku)\n");
            }
            Console.WriteLine("\n        /*--------------------FIN Résultat du Benchmark--------------------*/\n");
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
                return -1;
            solver.Sudoku.showTwoSudoku();

            return elapsedMs;
        }
    }
}
