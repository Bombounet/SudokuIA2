using System;
using System.Text;

namespace SudokuIA2.Grp4_DancingLinks
{
    class ProgramGrp4
    {
        public Sudoku sudoku;
        private DlxSudokuSolver s = new DlxSudokuSolver(); 

        public ProgramGrp4()
        {
            sudoku = new Sudoku();
            s.sudoku = sudoku;
        }

        public void solve()
        {
            //Résoudre le sudoku ici
            s.sudoku = sudoku;
            s.Solve();
            sudoku = s.sudoku;
        }
    }
}
