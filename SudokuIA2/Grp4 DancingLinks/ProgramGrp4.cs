using System;
using System.Text;

namespace SudokuIA2.Grp4_DancingLinks
{
   

    class ProgramGrp4:ISudokuSolver
    {
        //public Sudoku sudoku;
        private DlxSudokuSolver s = new DlxSudokuSolver(); 

        public ProgramGrp4()
        {
            Sudoku = new Sudoku();
            s.sudoku = Sudoku;
        }

        public Sudoku Sudoku { get ; set ; }

        public void Solve()
        {
            //Résoudre le sudoku ici
            s.sudoku = Sudoku;
            s.Solve();
            Sudoku = s.sudoku;
        }

      
    }
}
