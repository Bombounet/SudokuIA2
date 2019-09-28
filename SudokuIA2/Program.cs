using System;

namespace SudokuIA2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Sudoku sudoku = new Sudoku();

            String solved = "483921657967345821251876493548132976729564138136798245372689514814253769965417382";



            sudoku.showInitialSudoku();

            sudoku.setSudoku(sudoku.stringToSudoku(solved));
            sudoku.showSudoku();

            Console.WriteLine("laaaaa");
        }
    }
}

