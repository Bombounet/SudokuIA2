using Google.OrTools.Sat;
using System;

namespace SudokuIA2.Grp2_CSP
{
    public class VarArraySolutionPrinter : CpSolverSolutionCallback
    {


        public VarArraySolutionPrinter(IntVar[][] variables)
        {
            this.variables = variables;
        }

        public override void OnSolutionCallback()
        {
            //fix valide par le grp 2 (FD)
        }

        public int[][] getValues()
        {
            int[][] values = new int[9][];

            for (int k = 0; k < 9; k++)
                values[k] = new int[9];

            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    values[i][j] = (int)Value(variables[i][j]);
                }
            }
            return values;
        }

        private IntVar[][] variables;
    }
}
