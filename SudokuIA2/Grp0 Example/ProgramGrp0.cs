using System;

namespace SudokuIA2.Grp0_Example
{
    class ProgramGrp0
    {
        public Sudoku sudoku;
        int[][] sudo;


        public ProgramGrp0()
        {
            sudoku = new Sudoku();
            sudo = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudo[i] = new int[9];
            }

        }

        /*--------------------Résolution du Sudoku--------------------*/

        public void solve()
        {
            String solved = "483921657967345821251876493548132976729564138136798245372689514814253769695417382";
            sudo = sudoku.stringToSudoku(solved);
            sudoku.setSudoku(sudo);
            int nb = 15000;
            int clc = 0;
            for (int i = 0; i < nb; i++)
            {
                for (int j = 0; j < nb; j++)
                {
                        clc += i * j;
                }
            }
        }

        /*-----------------------------------------------------------*/
    }
}
