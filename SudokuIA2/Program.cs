using System;
using System.Diagnostics;

namespace SudokuIA2
{
    class Program
    {


        static void Main(string[] args)
        {
            bool Quitter = false;

            do
            {
                Console.WriteLine("\n\n\n                Résolution de Sudoku");

                Console.WriteLine("\n                0. Groupe 0 Example");
                Console.WriteLine("                1. Groupe 1 Genetic");
                Console.WriteLine("                2. Groupe 2 CSP");
                Console.WriteLine("                3. Groupe 3 SMT");
                Console.WriteLine("                4. Groupe 4 Dancing Links");
                Console.WriteLine("                5. Groupe 5 Norving");
                Console.WriteLine("                6. ");
                Console.WriteLine("                7. ");
                Console.WriteLine("                8. Tester tout les groupes");
                Console.WriteLine("                9. Quitter");

                Console.WriteLine("\n                Que voulez vous faire ?");

                int choix = int.Parse(Console.ReadLine());

                if (choix == 9)
                    Quitter = true;
                else if (choix == 8)
                {
                    int begin = 0;
                    int nbTest = 6;
                    int[] score = new int[nbTest-begin];
                    for (int i = begin; i < nbTest; i++)
                    {
                        score[i] = testSolution(i);
                    }
                    Console.WriteLine("\n        /*--------------------Résultat des Tests--------------------*/\n");
                    for (int i = begin; i < nbTest; i++)
                    {
                        String msg = "";
                        if (score[i] == -1)
                            msg = "Non validé";
                        else
                            msg = "Validé";
                        Console.WriteLine("        Groupe " + i + " : " + msg + " en " + score[i] + "ms\n");
                    }
                    Console.WriteLine("\n        /*--------------------FIN Résultat des Tests--------------------*/\n");
                }
                else if (0 <= choix && choix <= 7)
                {
                    testSolution(choix);
                }


            } while (!Quitter);

            Console.WriteLine("\n\n\n        FIIIIIN");
        }


        public static int testSolution(int choix)
        {
            var watch = Stopwatch.StartNew();
            var elapsedMs = watch.ElapsedMilliseconds;
            switch (choix)
            {
                case 0:
                    Console.WriteLine("\n        /*--------------------Groupe 0 Example--------------------*/\n");
                    Grp0_Example.ProgramGrp0 grp0 = new Grp0_Example.ProgramGrp0();
                    watch = Stopwatch.StartNew();
                    grp0.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp0.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp0.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 0 Example--------------------*/\n");
                    break;
                case 1:
                    Console.WriteLine("\n        /*--------------------Groupe 1 Genetic--------------------*/\n");
                    Grp1_Genetic.ProgramGrp1 grp1 = new Grp1_Genetic.ProgramGrp1();
                    watch = Stopwatch.StartNew();
                    grp1.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp1.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp1.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 1 Genetic--------------------*/\n");
                    break;
                case 2:
                    Console.WriteLine("\n        /*--------------------Groupe 2 CSP--------------------*/\n");
                    Grp2_CSP.ProgramGrp2 grp2 = new Grp2_CSP.ProgramGrp2();
                    watch = Stopwatch.StartNew();
                    grp2.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp2.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp2.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 2 CSP--------------------*/\n");
                    break;
                case 3:
                    Console.WriteLine("\n        /*--------------------Groupe 3 SMT--------------------*/\n");
                    Grp3_SMT.ProgramGrp3 grp3 = new Grp3_SMT.ProgramGrp3();
                    watch = Stopwatch.StartNew();
                    grp3.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp3.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp3.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 3 SMT--------------------*/\n");
                    break;
                case 4:
                    Console.WriteLine("\n        /*--------------------Groupe 4 DancingLinks--------------------*/\n");
                    Grp4_DancingLinks.ProgramGrp4 grp4 = new Grp4_DancingLinks.ProgramGrp4();
                    watch = Stopwatch.StartNew();
                    grp4.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp4.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp4.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 4 DancingLinks--------------------*/\n");
                    break;
                case 5:
                    Console.WriteLine("\n        /*--------------------Groupe 5 Norving--------------------*/\n");
                    Grp5_Norving.ProgramGrp5 grp5 = new Grp5_Norving.ProgramGrp5();
                    watch = Stopwatch.StartNew();
                    grp5.solve();
                    watch.Stop();
                    elapsedMs = watch.ElapsedMilliseconds;
                    Console.WriteLine("\n\n                SOLVED IN : " + elapsedMs + " ms\n\n");
                    if (!grp5.sudoku.validationSudoku())
                        elapsedMs = -1;
                    grp5.sudoku.showTwoSudoku();
                    Console.WriteLine("\n        /*--------------------FIN Groupe 5 Norving--------------------*/\n");
                    break;
                case 6:
                    Console.WriteLine("        Pas de groupe");
                    return -1;
                case 7:
                    Console.WriteLine("        Pas de groupe");
                    return -1;
                default:
                    return -1;
            }
            return (int)elapsedMs;
        }




    }
}

