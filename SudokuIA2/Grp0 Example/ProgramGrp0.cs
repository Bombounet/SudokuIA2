using System;
using System.Text;

namespace SudokuIA2.Grp0_Example
{
    class ProgramGrp0
    {
        Sudoku sudoku;
        int[][] sudo;


        public ProgramGrp0()
        {
            sudoku = new Sudoku();
            sudo = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudo[i] = new int[9];
            }

            /*--------------------Résolution du Sudoku--------------------*/

            solve(sudo);

            /*--------------------Résolution du Sudoku--------------------*/

            sudoku.showInitialSudoku();
            sudoku.showSudoku();

            sudoku.validationSudoku();

            sudoku.newTop95Sudoku(10);
            sudoku.showSudoku();

        }

        public void solve(int[][] tab)
        {
            String solved = "483921657967345821251876493548132976729564138136798245372689514814253769695417382";
            tab = sudoku.stringToSudoku(solved);
            sudoku.setSudoku(tab);
        }
    }
}
