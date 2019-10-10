using System;
using System.Collections.Generic;
using Google.OrTools.Sat;

namespace SudokuIA2.Grp2_CSP
{
    class ProgramGrp2
    {
        public Sudoku sudoku;

        public ProgramGrp2()
        {
            sudoku = new Sudoku();
        }

        public void solve()
        {
            CpModel model = new CpModel();
            int num_vals = 9;

            IntVar[][] grid = new IntVar[9][];

            for (int k = 0; k < 9; k++)
                grid[k] = new IntVar[9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku.getCaseInitialSudoku(i, j) == 0)
                        grid[i][j] = model.NewIntVar(1, num_vals, "C" + i.ToString() + j.ToString());
                    else
                        grid[i][j] = model.NewIntVar(sudoku.getCaseInitialSudoku(i, j), sudoku.getCaseInitialSudoku(i, j), "C" + i.ToString() + j.ToString());
                }
            }

            for (int k = 0; k < 9; k++)
            {
                model.AddAllDifferent(grid[k]);
                model.AddAllDifferent(Transpose(grid)[k]);
            }

            for (int k = 0; k < 3; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    List<IntVar> boxList = new List<IntVar>();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            boxList.Add(grid[k * 3 + i][l * 3 + j]);
                        }
                    }
                    model.AddAllDifferent(boxList);
                }
            }
            CpSolver solver = new CpSolver();
            VarArraySolutionPrinter cb = new VarArraySolutionPrinter(grid);
            solver.SearchAllSolutions(model, cb);
            int[][] values = cb.getValues();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku.getCaseSudoku(i, j) == 0)
                        sudoku.setCaseSudoku(i, j, values[i][j]);
                }
            }
        }

        protected IntVar[][] Transpose(IntVar[][] input)
        {
            IntVar[][] grid = new IntVar[9][];
            for (int k = 0; k < 9; k++)
            {
                grid[k] = new IntVar[9];
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    IntVar tmp = input[j][i];
                    grid[i][j] = tmp;
                }
            }
            return grid;
        }

        
    }
}
