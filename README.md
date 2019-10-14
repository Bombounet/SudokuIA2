1. Installer Visual Studio Comunity

2. Clonez le git : git clone https://github.com/Bombounet/SudokuIA2

3. Ouvrez le avec Visual studio et lancer Program.cs

4. La console se lance, choisissez la méthode de résolution dans la console

    C'est a vous de coder chaque methode dans le dossier associé (ex : Grp0 Example)




La class Sudoku : 
  - Elle est composée de 2 tableaux de int[][] de taille 9x9

  - Le premier initialSudoku correspond au sudoku a resoudre, il ne peut etre modifié

  - Le deuxieme wokrkingSudoku correspond au sudoku que vous allez utiliser pour résoudre le sudoku

  - getInitialSudoku() permet de recuperer le sudoku initial, getCaseInitialSudoku() recupére une case

  - getSudoku() permet de recuperer le sudoku de "travail", getCaseSudoku() recupére une case

  - setSudoku() permet d'attribuer le sudoku de "travail", setCaseSudoku() attribu une case

  - showInitialSudoku() permet d'afficher le sudoku initial

  - showSudoku() permet d'afficher le sudoku de "travail"

  - show() permet d'afficher le sudoku envoyé en parametre

  - validationSudoku() permet de verrifier que le sudoku de "travail" est conforme aux règles du jeu

  - validation() permet de verrifier que le sudoku envoyé en parametre


Génération de sudoku inspiré de : https://gist.github.com/fabiosoft/b41067106bebf1498399f4eb9826e4de
