using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Extensions.Sudoku;
using System;
using System.Text;

namespace SudokuIA2.Grp1_Genetic
{
    class ProgramGrp1 : ISudokuSolver
    {
        public Sudoku Sudoku { get; set; }
        public string Name { get; }

        public ProgramGrp1()
        {
            Sudoku = new Sudoku();
            Name = "Grp1 Genetic Sharp";
        }

        public void Solve()
        {
            SudokuBoard sudokuBoard = Transform(Sudoku);
            IChromosome chromosome = new SudokuPermutationsChromosome(sudokuBoard);

            var fitness = new SudokuFitness(sudokuBoard);
            var selection = new EliteSelection();
            var crossover = new UniformCrossover();
            var mutation = new UniformMutation();

            var population = new Population(5000, 5000, chromosome);
            var ga = new GeneticAlgorithm(population, fitness, selection, crossover, mutation)
            {
                Termination = new OrTermination(new ITermination[]
                {
                    new FitnessThresholdTermination(0),
                    new GenerationNumberTermination(100)
                })
            };

            ga.Start();

            var bestIndividual = ((ISudokuChromosome)ga.Population.BestChromosome);
            var solutions = bestIndividual.GetSudokus();

            Sudoku = Transform(solutions[0]);

        }

        private SudokuBoard Transform(Sudoku sudoku)
        {
            SudokuBoard sudoBrd = new SudokuBoard();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoBrd.SetCell(i, j, sudoku.getCaseSudoku(i, j));
                }
            }
            return sudoBrd;
            throw new NotImplementedException();
        }

        private Sudoku Transform(SudokuBoard sudoku)
        {
            Sudoku sudoGr = new Sudoku();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudoGr.setCaseSudoku(i, j, sudoku.GetCell(i, j));
                }

            }
            return sudoGr;
            throw new NotImplementedException();
        }

    }
    
}
