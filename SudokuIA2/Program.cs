using System;

namespace SudokuIA2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Sudoku sudoku = new Sudoku();

            String solved = "483921657967345821251876493548132976729564138136798245372689514814253769695417382";

            int[][] sudo = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudo[i] = new int[9];
            }

            sudo = sudoku.stringToSudoku(solved);

            sudoku.showInitialSudoku();
            sudoku.show(sudo);

            sudoku.validation(sudo);






            //sudoku.setSudoku(sudoku.stringToSudoku(solved));


            Console.WriteLine("FIIIIIN");
        }
    }
}

