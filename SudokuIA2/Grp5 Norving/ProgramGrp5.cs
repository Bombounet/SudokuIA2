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


        }

        public void Solve()
        {
            source = engine.CreateScriptSourceFromFile(script);
            scope = engine.CreateScope();

            string gridValues = Sudoku.sudokuToString(Sudoku.getSudoku(new int[9][]));
            engine.GetSysModule().SetVariable("argv", gridValues);
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