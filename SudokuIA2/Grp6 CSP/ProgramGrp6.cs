using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.OrTools.Sat;
using SudokuIA2.Grp2_CSP;

namespace SudokuIA2.Grp6_CSP
{
    class ProgramGrp6 : ISudokuSolver
    {
        public Sudoku Sudoku { get; set; }

        public string Name { get; }

        public ProgramGrp6()
        {
            Sudoku = new Sudoku();
            Name = "Grp6 CSP";
        }

        public void Solve()
        {
            // Creates the model.
            CpModel model = new CpModel();
            // Creates the variables.
            int num_vals = 9;

            //Creat the Sudoku grid
            IntVar[][] grid = new IntVar[9][];
            for (int k = 0; k < 9; k++) grid[k] = new IntVar[9];

            //get initial sudoku grid and put the 9 possibilities in Empty cells
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Sudoku.getCaseInitialSudoku(i, j) == 0)
                        grid[i][j] = model.NewIntVar(1, num_vals, "C" + i.ToString() + j.ToString());
                    else
                        grid[i][j] = model.NewIntVar(Sudoku.getCaseInitialSudoku(i, j), Sudoku.getCaseInitialSudoku(i, j), "C" + i.ToString() + j.ToString());
                }
            }


            // Adds a different constraint.
            for (int k = 0; k < 9; k++)
            {
                //All the ligne might have a different number in each cells
                model.AddAllDifferent(GetRow(grid, k));
                //All the columns might have a different number in each cells
                model.AddAllDifferent(GetColumn(grid, k));
            }
            //All 9 Regions might have a different number in each cells
            for (int region = 0; region < 9; region++)
            {
                model.AddAllDifferent(GetRegion(grid, region)); 
            }

            // Creates a solver and solves the model.
            CpSolver solver = new CpSolver();
            VarArraySolutionPrinter cb = new VarArraySolutionPrinter(grid);
            solver.SearchAllSolutions(model, cb);
            int[][] values = cb.getValues();

            Sudoku.setSudoku(values); 

        }

        public IntVar[] GetColumn(IntVar[][] grid, int columnNumber)
        {
            return Enumerable.Range(0, grid.GetLength(0))
                    .Select(x => grid[x][columnNumber])
                    .ToArray();
        }

        public IntVar[] GetRow(IntVar[][] grid, int rowNumber)
        {
            return Enumerable.Range(0, grid.GetLength(0))
                    .Select(x => grid[rowNumber][x])
                    .ToArray();
        }

        private List<IntVar> GetRegion(IntVar[][] grid, int k)
        {
            List<IntVar> region = new List<IntVar>();

            if (k < 3)
            {
                for(int i = k*3; i < k * 3 + 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        region.Add(grid[j][i]); 
                    }
                }

            }else if( k < 6)
            {
                for (int i = (k-3) * 3; i < (k - 3) * 3 + 3; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        region.Add(grid[j][i]);
                    }
                }

            }
            else if( k < 9)
            {
                for (int i = (k-6) * 3; i < (k - 6) * 3 + 3; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        region.Add(grid[j][i]);
                    }
                }
            }

            return region; 
        }
    }
}
