using System;
using System.Collections.Generic;
using System.IO;

namespace SudokuIA2
{
    class Sudoku
    {

        private int[][] initialSudoku;  //Sudoku original et vide (ne peut �tre modifi�)
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

        public int[][] getInitialSudoku(int[][] sudoku)  //r�cup�rele sudoku initiale
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

        public int getCaseInitialSudoku(int line, int column)  //r�cup�reune case du sudoku initiale
        {
            return initialSudoku[line][column];
        }

        public int[][] getSudoku(int[][] sudoku)  //r�cup�rele sudoku de "travail"
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
        public int getCaseSudoku(int line, int column)  //r�cup�re une case du sudoku de "travail"
        {
            return workingSudoku[line][column];
        }

        public bool setSudoku(int[][] sudoku)  //Attribue un nouveau sudoku de "travail" 
        {
            if (!checkSudoku(sudoku, "setSudoku"))  //Renvoie false si ce n'est pas autoris�
                return false;
            if (!checkAllCase(sudoku, "setSudoku"))  //Renvoie false si ce n'est pas autoris�
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
            if (!checkCase(line, column, value, "setCaseSudoku"))  //Renvoie false si ce n'est pas autoris�
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

        public String getLine(String fileName, int index)  //R�cup�re un String Sudoku d'un fichier 
        {
            String[] lines = getFile(fileName);

            if (index < 0 || index >= lines.Length)
            {
                Random rnd = new Random();
                index = rnd.Next(lines.Length);
            }
            return lines[index];
        }

        public String[] getFile(String fileName)  //R�cup�re un tout les Sudokus d'un fichier 
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
            if (!checkSudoku(workingSudoku, "show"))  //Renvoie false si il y a un probleme 
                return false;
            if (!checkAllCase(workingSudoku, "show"))  //Renvoie false si il y a un probleme 
                return false;
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
            if (!checkSudoku(workingSudoku, "showTwo"))  //Renvoie false si il y a un probleme 
                return false;
            if (!checkAllCase(workingSudoku, "showTwo"))  //Renvoie false si il y a un probleme 
                return false;
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
            if (!checkSudoku(sudoku, "validation"))  //Renvoie false s'il y a un probl�me 
                return false;
            if (!checkAllCase(sudoku, "validation"))  //Renvoie false s'il y a un probl�me 
                return false;

            for (int i = 0; i < 9; i++)  //Validation des lignes
            {
                int[] list9 = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    list9[j] = sudoku[i][j];
                }
                if (!validationList9(list9, ("ligne " + i)))
                    return false;
            }

            for (int j = 0; j < 9; j++)  //Validation des colonnes
            {
                int[] list9 = new int[9];
                for (int i = 0; i < 9; i++)
                {
                    list9[i] = sudoku[i][j];
                }
                if (!validationList9(list9, ("colonne " + j)))
                    return false;
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
                        return false;
                }
            }

            return true;
        }

        /*--------------------G�n�ration--------------------*/

        public String create(int dificulty)
        {
            if (dificulty < 17 || dificulty > 30)
                return null;

            int[][] grid = new int[9][];
            int shuffleLevel = 100;

            for (int i = 0; i < 9; i++)
            {
                grid[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    grid[i][j] = (i * 3 + i / 3 + j) % 9 + 1;

                }
            }

            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChangeTwoCell(ref grid, rand.Next(1, 9), rand2.Next(1, 9));
            }

            var cases = new List<int>();
            Random random = new Random();
            while (cases.Count != dificulty)
            {
                random = new Random();
                int rnd = random.Next(81);
                if (!cases.Contains(rnd))
                    cases.Add(rnd);
            }

            bool flag;  //Tri de la liste
            do
            {
                flag = false;
                for (int k = 0; k < (cases.Count - 1); k++)
                {
                    if (cases[k] > cases[k + 1])
                    {
                        int buffer = cases[k];
                        cases[k] = cases[k + 1];
                        cases[k + 1] = buffer;
                        flag = true;
                    }
                }
            } while (flag);

            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (index < dificulty)
                    {
                        if ((i * 9 + j) == cases[index])
                            index++;
                        else
                            grid[i][j] = 0;
                    }
                    else
                        grid[i][j] = 0;
                }
            }

            return sudokuToString(grid);
        }

        /*--------------------Outils--------------------*/

        public int[][] stringToSudoku(String stringSudoku)  //Transforme un String en sudoku (tableau de int[9][9])
        {
            if (stringSudoku.Length != 81)
            {
                Console.WriteLine("        !!! ERROR !!! : Ce String a une taille non conforme pour �tre un sudoku");
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
                            Console.WriteLine("        !!! ERROR !!! : Caract�re non valide pour un sudoku");
                            return stringToSudoku("003020600900305001001806400008102900700000008006708200002609500800203009005010300");
                        }
                        else
                            sudoku[i][j] = number;
                    }
                }
            }
            return sudoku;
        }

        public String sudokuToString(int[][] sudoku)
        {
            if (!checkSudoku(sudoku, "sudokuToString"))
                return null;

            String stringSudoku = "";

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    stringSudoku += sudoku[i][j];
                }
            }

            return stringSudoku;
        }

        public bool checkSudoku(int[][] sudoku, String log)  //V�rifie la validit� d'un sudoku (taille 9x9)
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
                    Console.WriteLine("        !!! WARNING !!! : Nombre de colonnes incorrect � la ligne " + i + " (" + sudoku.Length + ", au lieu de 9) lors de la commande " + log);
                    return false;
                }
            }
            return true;
        }

        public bool checkAllCase(int[][] sudoku, String log)  //V�rifie la validit� de toutesles cases du sudoku (valeur comprise entre 0 et 9 et conforme au sudoku initial)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (!checkCase(i, j, sudoku[i][j], log))
                        return false;
                }
            }
            return true;
        }

        public bool checkCase(int line, int column, int value, String log)  //V�rifie la validit� d'une case de sudoku (valeur comprise entre 0 et 9 et conforme au sudoku initial)
        {
            if (initialSudoku[line][column] != 0 && value != initialSudoku[line][column])
            {
                Console.WriteLine("        !!! WARNING !!! : Cette case ne peut �tre modifi�e, c'est une case fix�e par le sudoku. (case [" + line + "][" + column + "]) lors de la commande " + log);
                return false;
            }

            if (value < 0 || value > 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Valeur non valable � la case [" + line + "][" + column + "] (" + value + ", au lieu de [0,1,2,3,4,5,6,7,8,9]) lors de la commande " + log);
                return false;
            }

            return true;
        }

        public bool validationList9(int[] list9, String log)  //Valide qu'une liste contient bien les 9 chiffres attendus
        {
            if (list9.Length != 9)
            {
                Console.WriteLine("        !!! WARNING !!! : Nombre d'�l�ments incorrects (" + log + ")");
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

        private void ChangeTwoCell(ref int[][] grid, int findValue1, int findValue2)
        {
            int xParm1, yParm1, xParm2, yParm2;
            xParm1 = yParm1 = xParm2 = yParm2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int k = 0; k < 9; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid[i + j][k + z] == findValue1)
                            {
                                xParm1 = i + j;
                                yParm1 = k + z;

                            }
                            if (grid[i + j][k + z] == findValue2)
                            {
                                xParm2 = i + j;
                                yParm2 = k + z;

                            }
                        }
                    }
                    grid[xParm1][yParm1] = findValue2;
                    grid[xParm2][yParm2] = findValue1;
                }
            }
        }
    }
}


