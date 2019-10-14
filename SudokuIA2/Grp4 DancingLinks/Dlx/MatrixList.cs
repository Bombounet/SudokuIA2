using System;
using System.Collections.Generic;

namespace Dlx
{
    public class MatrixList
    {
        private MatrixNodeHead root;
        public LinkedList<int> rows = new LinkedList<int>();
        private bool stop = false;
        private LinkedList<MatrixNode> O = new LinkedList<MatrixNode>();
        private int[][] sudoku;

        public MatrixList()
        {
            root = new MatrixNodeHead("");
            root.right = root;
            root.left = root;
        }

        public MatrixList(int[,] grid)
        {
            root = new MatrixNodeHead("root (h)");
            MatrixNode currentNode = root;
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                currentNode.right = new MatrixNodeHead("" + j);
                currentNode.right.left = currentNode;
                currentNode = currentNode.right;
                currentNode.up = currentNode;
                currentNode.down = currentNode;
            }

            currentNode.right = root;
            root.left = currentNode;

            MatrixNode[] prevNode = new MatrixNode[grid.GetLength(0)];
            currentNode = root;

            int cpt;
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                cpt = 0;
                //Console.WriteLine(((MatrixNodeHead)currentNode).name);
                currentNode = currentNode.right;
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if (grid[i, j] == 1)
                    {
                        MatrixNode tmp = new MatrixNode((MatrixNodeHead)currentNode.down);
                        tmp.rowIndex = i;
                        cpt++;
                        tmp.down = currentNode.down;
                        currentNode.down = tmp;
                        tmp.up = currentNode;
                        if (prevNode[i] == null)
                        {
                            ((MatrixNodeHead)tmp.down).item = tmp;
                            tmp.right = tmp;
                            tmp.left = tmp;
                            prevNode[i] = tmp;
                        }
                        else
                        {
                            tmp.left = prevNode[i];
                            tmp.right = prevNode[i].right;
                            tmp.right.left = tmp;
                            prevNode[i].right = tmp;
                            prevNode[i] = tmp;
                        }
                        currentNode = currentNode.down;
                    }
                }
                currentNode.down.up = currentNode;
                currentNode = currentNode.down;
                ((MatrixNodeHead)currentNode).size = cpt;
            }
        }

        public MatrixList(int[][] sudoku)
        {
            this.sudoku = sudoku;
            MatrixNode[] tmp = new MatrixNode[9 * 9 * 4];

            root = new MatrixNodeHead("root (h)");
            MatrixNode currentNode = root;
            for (int j = 0; j < 324; j++)
            {
                currentNode.right = new MatrixNodeHead("" + j);
                currentNode.right.left = currentNode;
                currentNode = currentNode.right;
                currentNode.rowIndex = j;
                currentNode.up = currentNode;
                currentNode.down = currentNode;
                tmp[j] = currentNode;
            }

            currentNode.right = root;
            root.left = currentNode;

            int imatrix = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int value = sudoku[i][j];

                    MatrixNode tmpRCCNode;
                    MatrixNode tmpRNCNode;
                    MatrixNode tmpCNCNode;
                    MatrixNode tmpBNCNode;
                    if (value == 0)
                    {
                        value = 1;
                        int RCC = calcRCConstrain(i, j);
                        int RNC = calcRNConstrain(i, value);
                        int CNC = calcCNConstrain(j, value);
                        int BNC = calcBNConstrain(i, j, value);

                        int end = imatrix + 9;

                        for (; imatrix < end; imatrix++)
                        {
                            tmpRCCNode = new MatrixNode((MatrixNodeHead)tmp[RCC].down);
                            tmpRNCNode = new MatrixNode((MatrixNodeHead)tmp[RNC].down);
                            tmpCNCNode = new MatrixNode((MatrixNodeHead)tmp[CNC].down);
                            tmpBNCNode = new MatrixNode((MatrixNodeHead)tmp[BNC].down);

                            tmpRCCNode.rowIndex = i;
                            tmpRCCNode.column = j;
                            tmpRCCNode.value = value;
                            tmpRNCNode.rowIndex = i;
                            tmpRNCNode.column = j;
                            tmpRNCNode.value = value;
                            tmpCNCNode.rowIndex = i;
                            tmpCNCNode.column = j;
                            tmpCNCNode.value = value;
                            tmpBNCNode.rowIndex = i;
                            tmpBNCNode.column = j;
                            tmpBNCNode.value = value++;


                            ((MatrixNodeHead)tmp[RCC].down).size++;
                            ((MatrixNodeHead)tmp[RNC].down).size++;
                            ((MatrixNodeHead)tmp[CNC].down).size++;
                            ((MatrixNodeHead)tmp[BNC].down).size++;

                            tmpRCCNode.right = tmpRNCNode;
                            tmpRNCNode.right = tmpCNCNode;
                            tmpCNCNode.right = tmpBNCNode;
                            tmpBNCNode.right = tmpRCCNode;

                            tmpBNCNode.left = tmpCNCNode;
                            tmpCNCNode.left = tmpRNCNode;
                            tmpRNCNode.left = tmpRCCNode;
                            tmpRCCNode.left = tmpBNCNode;

                            tmpRCCNode.up = tmp[RCC];
                            tmpRCCNode.down = tmp[RCC].down;
                            tmp[RCC].down = tmpRCCNode;
                            tmp[RCC] = tmpRCCNode;

                            tmpRNCNode.up = tmp[RNC];
                            tmpRNCNode.down = tmp[RNC].down;
                            tmp[RNC].down = tmpRNCNode;
                            tmp[RNC++] = tmpRNCNode;

                            tmpCNCNode.up = tmp[CNC];
                            tmpCNCNode.down = tmp[CNC].down;
                            tmp[CNC].down = tmpCNCNode;
                            tmp[CNC++] = tmpCNCNode;

                            tmpBNCNode.up = tmp[BNC];
                            tmpBNCNode.down = tmp[BNC].down;
                            tmp[BNC].down = tmpBNCNode;
                            tmp[BNC++] = tmpBNCNode;
                        }
                    }
                    else
                    {
                        int RCC = calcRCConstrain(i, j);
                        int RNC = calcRNConstrain(i, value);
                        int CNC = calcCNConstrain(j, value);
                        int BNC = calcBNConstrain(i, j, value);

                        tmpRCCNode = new MatrixNode((MatrixNodeHead)tmp[RCC].down);
                        tmpRNCNode = new MatrixNode((MatrixNodeHead)tmp[RNC].down);
                        tmpCNCNode = new MatrixNode((MatrixNodeHead)tmp[CNC].down);
                        tmpBNCNode = new MatrixNode((MatrixNodeHead)tmp[BNC].down);

                        tmpRCCNode.rowIndex = i;
                        tmpRCCNode.column = j;
                        tmpRCCNode.value = value;
                        tmpRNCNode.rowIndex = i;
                        tmpRNCNode.column = j;
                        tmpRNCNode.value = value;
                        tmpCNCNode.rowIndex = i;
                        tmpCNCNode.column = j;
                        tmpCNCNode.value = value;
                        tmpBNCNode.rowIndex = i;
                        tmpBNCNode.column = j;
                        tmpBNCNode.value = value;

                        tmpRCCNode.right = tmpRNCNode;
                        tmpRNCNode.right = tmpCNCNode;
                        tmpCNCNode.right = tmpBNCNode;
                        tmpBNCNode.right = tmpRCCNode;

                        tmpBNCNode.left = tmpCNCNode;
                        tmpCNCNode.left = tmpRNCNode;
                        tmpRNCNode.left = tmpRCCNode;
                        tmpRCCNode.left = tmpBNCNode;

                        tmpRCCNode.up = tmp[RCC];
                        tmpRCCNode.down = tmp[RCC].down;
                        tmp[RCC].down = tmpRCCNode;
                        tmp[RCC] = tmpRCCNode;

                        tmpRNCNode.up = tmp[RNC];
                        tmpRNCNode.down = tmp[RNC].down;
                        tmp[RNC].down = tmpRNCNode;
                        tmp[RNC++] = tmpRNCNode;

                        tmpCNCNode.up = tmp[CNC];
                        tmpCNCNode.down = tmp[CNC].down;
                        tmp[CNC].down = tmpCNCNode;
                        tmp[CNC++] = tmpCNCNode;

                        tmpBNCNode.up = tmp[BNC];
                        tmpBNCNode.down = tmp[BNC].down;
                        tmp[BNC].down = tmpBNCNode;
                        tmp[BNC++] = tmpBNCNode;

                        imatrix++;
                    }
                }
            }
        }

        public int[][] convertMatrixSudoku()
        {
            foreach(MatrixNode node in O)
            {
                sudoku[node.rowIndex][node.column] = node.value;
            }
            return sudoku;
        }
        private int calcRCConstrain(int i, int j)
        {
            return 9 * i + j;
        }

        private int calcRNConstrain(int i, int value)
        {
            return 81 + 9 * i + value - 1;
        }

        private int calcCNConstrain(int j, int value)
        {
            return 162 + 9 * j + value - 1;
        }

        private int calcBNConstrain(int i, int j, int value)
        {
            return 243 + ((i / 3) * 3 + j / 3) * 9 + value - 1;
        }

        private void search(int k)
        {
            if (root.right == root)
            {
                //rowIndexes = new LinkedList<int>(rows);
                stop = true;
                //Console.WriteLine("yes : " + rows.Count);
                return;
            }

            MatrixNodeHead selected = (MatrixNodeHead)root.right;
            int c = selected.size;
            for (MatrixNodeHead currentNode = (MatrixNodeHead)root.right; currentNode != root; currentNode = (MatrixNodeHead)currentNode.right)
            {
                if (c > currentNode.size)
                {
                    c = currentNode.size;
                    selected = currentNode;
                }
            }

            cover(selected);

            for (MatrixNode iNode = selected.down; iNode != selected; iNode = iNode.down)
            {

                //rows.AddLast(iNode.rowIndex);
                O.AddLast(iNode);

                for (MatrixNode jNode = iNode.right; jNode != iNode; jNode = jNode.right)
                {
                    cover(jNode.item);
                }

                search(k + 1);
                if (stop)
                    return;
                //rows.RemoveLast();
                O.RemoveLast();

                for (MatrixNode jNode = iNode.left; jNode != iNode; jNode = jNode.left)
                {
                    uncover(jNode.item);
                }
            }
            uncover(selected);
            return;
        }

        public void search()
        {
            stop = false;
            search(0);
        }

        private void cover(MatrixNodeHead node)
        {
            node.left.right = node.right;
            node.right.left = node.left;
            ///Console.WriteLine("depart : " + node.name);
            for (MatrixNode iNode = node.down; iNode != node; iNode = iNode.down)
            {
                for (MatrixNode jNode = iNode.right; jNode != iNode; jNode = jNode.right)
                {
                    jNode.up.down = jNode.down;
                    jNode.down.up = jNode.up;
                    jNode.item.size--;
                    //Console.WriteLine("ici : "+ currentNode.item.name);
                }
                //Console.WriteLine("Balade : " + ((MatrixNodeHead)currentNode).name);
            }
        }

        private void uncover(MatrixNodeHead node)
        {
            ///Console.WriteLine("depart : " + node.name);
            for (MatrixNode iNode = node.down; iNode != node; iNode = iNode.down)
            {
                for (MatrixNode jNode = iNode.right; jNode != iNode; jNode = jNode.right)
                {
                    jNode.up.down = jNode;
                    jNode.down.up = jNode;
                    jNode.item.size++;
                    //Console.WriteLine("ici : "+ currentNode.item.name);
                }
                //Console.WriteLine("Balade : " + ((MatrixNodeHead)currentNode).name);
            }
            node.left.right = node;
            node.right.left = node;
        }

    }

}
