using System;
using System.IO;
using System.Reflection;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace SudokuIA2.Grp5_Norving
{
    class ProgramGrp5 : ISudokuSolver
    {
        public Sudoku Sudoku { get; set; }
        public string Name { get; }

        private ScriptEngine engine = Python.CreateEngine();
        private string script = @"../../../Grp5 Norving/norving.py";
        private ScriptSource source;
        private ScriptScope scope;

        public ProgramGrp5()
        {
            Sudoku = new Sudoku();
            Name = "Grp5 Norvig";
            source = engine.CreateScriptSourceFromFile(script);
            scope = engine.CreateScope();

            int[][] grid = Sudoku.getSudoku(new int[9][]);

            string gridValues = "";
            foreach (var line in grid)
            {
                foreach (var col in line)
                {
                    gridValues += col;
                }
            }
            engine.GetSysModule().SetVariable("argv", gridValues);
        }

        public void Solve()
        {
            source.Execute(scope);
            String gridSolved = scope.GetVariable<string>("gridSolved");

            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Sudoku.getCaseSudoku(i, j) == 0)
                        Sudoku.setCaseSudoku(i, j, int.Parse(gridSolved[k].ToString()));
                    k++;
                }
            }
        }
    }
}