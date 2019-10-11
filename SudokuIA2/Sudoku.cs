using System;
using System.Diagnostics;
using System.IO;

namespace SudokuIA2
{
    class Sudoku
    {

        private int[][] initialSudoku;  //Sudoku original et vide (ne peut être modifié)
        private int[][] workingSudoku;  //Sudoku sur lequel vous allez travailler 


        /*--------------------Constructeur--------------------*/
        public Sudoku()  //Constructeur
        {
            initialSudoku = new int[9][];
            workingSudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                initialSudoku[i] = new int[9];
                workingSudoku[i] = new int[9];
            }
            
            String init = "003020600900305001001806400008102900700000008006708200002609500800203009005010300";

            initialSudoku = stringToSudoku(init);
            workingSudoku = stringToSudoku(init);
        }

        /*--------------------Getter & Setter--------------------*/

        public int[][] getInitialSudoku(int[][] sudoku)  //récupèrele sudoku initiale
        {
            sudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = initialSudoku[i][j];
                }
            }
            return sudoku;
        }

        public int getCaseInitialSudoku(int line, int column)  //récupèreune case du sudoku initiale
        {
            return initialSudoku[line][column];
        }

        public int[][] getSudoku(int[][] sudoku)  //récupèrele sudoku de "travail"
        {
            sudoku = new int[9][];
            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    sudoku[i][j] = workingSudoku[i][j];
                }
            }
            return sudoku;
        }
        public int getCaseSudoku(int line, int column)  //récupère une case du sudoku de "travail"
        {
            return workingSudoku[line][column];
        }

        public bool setSudoku(int[][] sudoku)  //Attribue un nouveau sudoku de "travail" 
        {
            if (!checkSudoku(sudoku, "setSudoku"))  //Renvoie false si ce n'est pas autorisé
                return false;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    workingSudoku[i][j] = sudoku[i][j];
                }
            }
            return true;
        }

        public bool setCaseSudoku(int line, int column, int value)  //Attribue une nouvelle case au sudoku de "travail"
        {
            if (!checkCase(line, column, value, "setCaseSudoku"))  //Renvoie false si ce n'est pas autorisé
                return false;

            workingSudoku[line][column] = value;

            return true;
        }

        /*--------------------Ficher--------------------*/

        public void newEasySudoku(int index)  //Attribue un nouveau Sudoku facile
        {
            newSudoku(getLine("Sudoku_Easy50.txt", index));
        }

        public void newHardSudoku(int index)  //Attribue un nouveau Sudoku hard
        {
            newSudoku(getLine("Sudoku_hardest.txt", index));
        }

        public void newTop95Sudoku(int index)  //Attribue un nouveau Sudoku du top 95
        {
            newSudoku(getLine("Sudoku_Top95.txt", index));
        }

        public void newSudoku(String sudoku)  //Attribue un nouveau Sudoku
        {
            initialSudoku = stringToSudoku(sudoku);
            workingSudoku = stringToSudoku(sudoku);
        }

        public String getLine(String fileName, int index)  //Récupère un String Sudoku d'un fichier 
        {
            String[] lines = getFile(fileName);

            if (index < 0 || index >= lines.Length)
            {
                Random rnd = new Random();
                index = rnd.Next(lines.Length);
            }
            return lines[index];
        }

        public String[] getFile(String fileName)  //Récupère un tout les Sudokus d'un fichier 
        {
            DirectoryInfo myDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            String path = Path.Combine(myDirectory.Parent.Parent.Parent.FullName, fileName);
            String[] lines = File.ReadAllLines(path);
            return lines;
        }

        /*--------------------Affichage--------------------*/

        public void showInitialSudoku()  //Affiche le sudoku initial
        {
            show(initialSudoku);
        }

        public bool showSudoku()  //Affiche le sudoku de "travail"
        {
            return show(workingSudoku);
        }

        public bool show(int[][] sudoku)  //Affiche un sudoku
        {
            if (!checkSudoku(sudoku, "show"))  //Renvoie false si il y a un probleme 
                return false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");
            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 6)
                    Console.WriteLine("        ---+---+---");
                Console.Write("        ");
                for (int j = 0; j < 9; j++)
                {
                    if (j == 3 || j == 6)
                        Console.Write("|");
                    if (sudoku[i][j] == 0)
                        Console.Write(".");
                    else
                    {
                        if (sudoku[i][j] != 0 && sudoku[i][j] == initialSudoku[i][j])
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(sudoku[i][j]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.Write(sudoku[i][j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
            return true;
        }

        public bool showTwoSudoku()  //Affiche le sudoku initial & le sudoku de "travail"
        {
            return showTwo(initialSudoku, workingSudoku);
        }

        public bool showTwo(int[][] sudokuOne, int[][] sudokuTwo)  //Affiche deux sudokus
        {
            if (!checkSudoku(sudokuOne, "showTwo"))  //Renvoie false si il y a un probleme 
                return false;
            if (!checkSudoku(sudokuTwo, "showTwo"))  //Renvoie false si il y a un probleme 
                return false;

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");
            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 6)
                    Console.WriteLine("                ---+---+---        ---+---+---");
                Console.Write("                ");
                for (int j = 0; j < 9; j++)
                {
                    if (j == 3 || j == 6)
                        Console.Write("|");
                    if (sudokuOne[i][j] == 0)
                        Console.Write(".");
                    else
                    {
                        if (sudokuOne[i][j] != 0 && sudokuOne[i][j] == initialSudoku[i][j])
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(sudokuOne[i][j]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.Write(sudokuOne[i][j]);
                    }
                }
                Console.Write("        ");
                for (int j = 0; j < 9; j++)
                {
                    if (j == 3 || j == 6)
                        Console.Write("|");
                    if (sudokuTwo[i][j] == 0)
                        Console.Write(".");
                    else
                    {
                        if (sudokuTwo[i][j] != 0 && sudokuTwo[i][j] == initialSudoku[i][j])
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write(sudokuTwo[i][j]);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.Write(sudokuTwo[i][j]);
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
            return true;
        }

        /*--------------------Validation du Sudoku--------------------*/

        public bool validationSudoku()  //Valide le sudoku de "travail"
        {
            return validation(workingSudoku);  //Renvoie false s'il y a un probleme 
        }

        public bool validation(int[][] sudoku)  //Valide un sudoku  /*--------------------A Optimiser--------------------*/
        {
            if (!checkSudoku(sudoku, "validation"))  //Renvoie false s'il y a un problème 
                return false;

            bool error = false;

            for (int i = 0; i < 9; i++)  //Validation des lignes
            {
                int[] list9 = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    list9[j] = sudoku[i][j];
                }
                if (!validationList9(list9, ("ligne " + i)))
                    error = true;
            }

            for (int j = 0; j < 9; j++)  //Validation des colonnes
            {
                int[] list9 = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    list9[i] = sudoku[i][j];
                }
                if (!validationList9(list9, ("colonne " + j)))
                    error = true;
            }

            for (int ii = 0; ii < 3; ii++)  //Validation des blocs
            {
                for (int jj = 0; jj < 3; jj++)
                {
                    int[] list9 = new int[9];
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            list9[i * 3 + j] = sudoku[ii * 3 + i][jj * 3 + j];
                        }
                    }
                    if (!validationList9(list9, ("bloc [" + ii + "][" + jj + "]")))
                        error = true;
                }
            }

            if (error)
            {
                Console.WriteLine("        !!! ECHEC !!! : Ce sudoku n'est pas validé");
                return false;
            }
            else
                Console.WriteLine("        !!! FELICITATION !!! : Ce sudoku est validé");
            return true;
        }

        /*--------------------Outils--------------------*/

        public int[][] stringToSudoku(String stringSudoku)  //Transforme un String en sudoku (tableau de int[9][9])
        {
            if (stringSudoku.Length != 81)
            {
                Console.WriteLine("        !!! ERROR !!! : Ce String a une taille non conforme pour étre un sudoku");
                return stringToSudoku("003020600900305001001806400008102900700000008006708200002609500800203009005010300");
            }

            int[][] sudoku;
            sudoku = new int[9][];

            for (int i = 0; i < 9; i++)
            {
                sudoku[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    char car = stringSudoku[i * 9 + j];
                    if (car == '.')
                        sudoku[i][j] = 0;
                    else
                    {
                        int number = car - 48; // - 48 pour la conversion de la table ascii
                        if (number < 0 || number > 9)
                        {
                            Console.WriteLine("        !!! ERROR !!! : Caractère non valide pour un sudoku");
                            return stringToSudoku("003020600900305001001806400008102900700000008006708200002609500800203009005010300");
                        }
                        else
                            sudoku[i][j] = number;
                    }
                }
            }
            return sudoku;
        }

        public bool checkSudoku(int[][] sudoku, String log)  //Vérifie la validité d'un sudoku (taille 9x9) puis chaque case
        {
            if (sudoku.Length != 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Nombre de lignes incorrect (" + sudoku.Length + ", au lieu de 9) lors de la commande " + log);
                return false;
            }
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i].Length != 9)
                {
                    Console.WriteLine("        !!! WARNING !!! : Nombre de colonnes incorrect à la ligne " + i + " (" + sudoku.Length + ", au lieu de 9) lors de la commande " + log);
                    return false;
                }
                for (int j = 0; j < 9; j++)
                {
                    if (!checkCase(i, j, sudoku[i][j], log))
                        return false;
                }
            }
            return true;
        }

        public bool checkCase(int line, int column, int value, String log)  //Vérifie la validité d'une case de sudoku (valeur comprise entre 0 et 9 et conforme au sudoku initial)
        {
            if (initialSudoku[line][column] != 0 && value != initialSudoku[line][column])
            {
                Console.WriteLine("        !!! WARNING !!! : Cette case ne peut être modifiée, c'est une case fixée par le sudoku. (case [" + line + "][" + column + "]) lors de la commande " + log);
                return false;
            }

            if (value < 0 || value > 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Valeur non valable à la case [" + line + "][" + column + "] (" + value + ", au lieu de [0,1,2,3,4,5,6,7,8,9]) lors de la commande " + log);
                return false;
            }

            return true;
        }

        public bool validationList9(int[] list9, String log)  //Valide qu'une liste contient bien les 9 chiffres attendus
        {
            if (list9.Length != 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Nombre d'éléments incorrects (" + log + ")");
                return false;
            }

            bool flag;  //Tri de la liste
            do
            {
                flag = false;
                for (int k = 0; k < 8; k++)
                {
                    if (list9[k] > list9[k + 1])
                    {
                        int buffer = list9[k];
                        list9[k] = list9[k + 1];
                        list9[k + 1] = buffer;
                        flag = true;
                    }
                }
            } while (flag);

            for (int k = 0; k < 9; k++)
            {
                if (list9[k] == 0)  //Pas de 0 dans le sudoku
                {
                    Console.WriteLine("        !!! ERROR !!! : Solution non valide : il y a encore un 0 (" + log + ")");
                    return false;
                }
                if (k != 8)
                    if (list9[k] == list9[k + 1])  //Pas de doublon dans le sudoku
                    {
                        Console.WriteLine("        !!! ERROR !!! : Solution non valide : il y a un doublon (" + log + ")");
                        return false;
                    }
            }
            return true;
        }
    }
}


