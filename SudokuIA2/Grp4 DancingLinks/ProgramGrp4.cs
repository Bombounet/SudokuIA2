using System;
using System.Text;

namespace SudokuIA2.Grp4_DancingLinks
{
    class ProgramGrp4:ISudokuSolver
    {
        public Sudoku Sudoku { get; set; }
        public string Name { get; }

        private DlxSudokuSolver s = new DlxSudokuSolver(); 

        public ProgramGrp4()
        {
            Sudoku = new Sudoku();
            Name = "Grp4 Dancing Links";
            s.sudoku = Sudoku;
        }

        public void Solve()
        {
            //Résoudre le sudoku ici
            s.sudoku = Sudoku;
            s.Solve();
            Sudoku = s.sudoku;
        }

    }
}
