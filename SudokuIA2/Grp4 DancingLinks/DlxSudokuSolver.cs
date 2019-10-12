using System.Collections.Generic;
using System.Linq;
using System;

namespace SudokuIA2.Grp4_DancingLinks
{
    class DlxSudokuSolver
    {
        public Sudoku sudoku; //= new Sudoku();
        private DlxLib.Dlx sol = new DlxLib.Dlx();
        private int[,] matrix;
        private const int NBCONSTRAIN = 9 * 9 * 4;

        private void matrixBuilder()
        {
            int nbCaseRemplie = sudoku.getSudoku(null).Aggregate(0, (acc, x) => acc + x.Aggregate(0, (a, b) => a + ((b == 0) ? 0 : 1)));
            matrix = new int[(81-nbCaseRemplie)*9+nbCaseRemplie,NBCONSTRAIN];
            int imatrix = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    imatrix  = buildLine(i, j, sudoku.getCaseSudoku(i, j),imatrix);
                }
            }
        }

        private int buildLine(int i, int j, int value, int imatrix)
        {
            if (value == 0)
            {
                int RCC = calcRCConstrain(i, j);
                int RNC = calcRNConstrain(i, 1);
                int CNC = calcCNConstrain(j, 1);
                int BNC = calcBNConstrain(i, j, 1);
                int end = imatrix + 9;
                for (; imatrix < end; imatrix++)
                {
                    matrix[imatrix,RCC] = 1;
                    matrix[imatrix,RNC++] = 1;
                    matrix[imatrix,CNC++] = 1;
                    matrix[imatrix,BNC++] = 1;
                }
                return end;
            }
            else
            {
                matrix[imatrix,calcRCConstrain(i, j)] = 1;
                matrix[imatrix,calcRNConstrain(i, value)] = 1;
                matrix[imatrix,calcCNConstrain(j, value)] = 1;
                matrix[imatrix,calcBNConstrain(i, j, value)] = 1;
                return imatrix + 1;
            }
        }

        private int calcRCConstrain(int i, int j)
        {
            return 9 * i + j;
        }

        private int calcRNConstrain(int i, int value)
        {
            return 81 + 9 * i + value - 1;
        }

        private int calcCNConstrain(int j, int value)
        {
            return 162 + 9 * j + value - 1;
        }

        private int calcBNConstrain(int i, int j, int value)
        {
            return 243 + ((i / 3) * 3 + j / 3) * 9 + value - 1;
        }

        private void convertSolutionToSudoku(IEnumerable<int> r, int[,] m)//DlxLib.Solution s, int[,] m)
        {
            foreach (int row in r)
            {
                int x = 0, y = 0, nb = 0;
                for (int j = 0; j < 81; j++)
                {
                    if (m[row, j] == 1)
                    {
                        x = j % 9; y = j / 9;
                        break;
                    }
                }
                for (int j = 81; j < 162; j++)
                {
                    if (m[row, j] == 1)
                    {
                        nb = (j - 81) % 9 + 1;
                        break;
                    }
                }
                sudoku.setCaseSudoku(y, x, nb);
                //sudoku.setCaseSudoku((nb / 9), (nb % 9), (row % 10) + 1);
            }
        }


        public void Solve()
        {
            matrixBuilder();
            //DlxLib.Solution tmp = sol.Solve(matrix).FirstOrDefault();
            Dlx.MatrixList test = new Dlx.MatrixList(matrix);
            test.search(0);
            convertSolutionToSudoku(test.rowIndexes, matrix);
            /*if (tmp != null)
            {
                convertSolutionToSudoku(tmp, matrix);
            }*/
        }
    }
}
