using System;
using System.IO;
using System.Reflection;
using System.Text;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace SudokuIA2.Grp5_Norving
{
    class ProgramGrp5
    {
        public Sudoku sudoku;
        private ScriptEngine engine = Python.CreateEngine();
        private string script = @"../../../Grp5 Norving/norving.py";
        private ScriptSource source;
        private ScriptScope scope;

        public ProgramGrp5()
        {
            sudoku = new Sudoku();
            source = engine.CreateScriptSourceFromFile(script);
            scope = engine.CreateScope();

            int[][] grid = sudoku.getSudoku(new int[9][]);

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

        public void solve()
        {
            source.Execute(scope);
            String gridSolved = scope.GetVariable<string>("gridSolved");

            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sudoku.getCaseSudoku(i, j) == 0)
                        sudoku.setCaseSudoku(i, j, int.Parse(gridSolved[k].ToString()));
                    k++;
                }
            }
        }
    }
}