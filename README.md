Installer Visual Studio Comunity
Clonez le git : git clone https://github.com/Bombounet/SudokuIA2
Ouvrez le avec Visual studio et lancer Program.cs
Choisissez la methode de resolution dans la console
C'est a vous de coder chaque methode dans le dossier associé (ex : Grp0 Example)

La class Sudoku est composé de 2 tableaux de int[][] de taille 9x9
Le premier initialSudoku correspond au sudoku a resoudre, il ne peut etre modifié
Le deuxieme wokrkingSudoku correspond au sudoku que vous aller utiliser pour resoudre le sudoku

getInitialSudoku permet de recuperer le sudoku initial, getCaseInitialSudoku recupére une case
getSudoku permet de recuperer le sudoku de "travail", getCaseSudoku recupére une case
setSudoku permet d'attribuer le sudoku de "travail", setCaseSudoku attribu une case

showInitialSudoku permet d'afficher le sudoku initial
showSudoku permet d'afficher le sudoku de "travail"
show permet d'afficher le sudoku envoyé en parametre

validationSudoku permet de verrifier que le sudoku de "travail" est conforme au regle du jeu
validation permet de verrifier que le sudoku envoyé en parametre
