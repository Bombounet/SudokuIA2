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
            {
                for(int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        Console.WriteLine(String.Format("  {0} = {1}", variables[i][j].ShortString(), Value(variables[i][j])));
                    }
                }
            }
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
