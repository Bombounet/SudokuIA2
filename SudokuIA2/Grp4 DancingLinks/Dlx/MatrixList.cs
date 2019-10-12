using System;
using System.Collections.Generic;

namespace Dlx
{
    public class MatrixList
    {
        private MatrixNodeHead root;
        private LinkedList<int> rows = new LinkedList<int>();
        public LinkedList<int> rowIndexes;

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

        public void search(int k)
        {
            if (root.right == root)
            {
                rowIndexes = new LinkedList<int>(rows);
                //Console.WriteLine("yes : " + rows.Count);
                return;
            }
            
            MatrixNodeHead selected = (MatrixNodeHead)root.right;
            int c = selected.size;
            for(MatrixNodeHead currentNode = (MatrixNodeHead)root.right; currentNode != root; currentNode = (MatrixNodeHead)currentNode.right)
            {
                if (c > currentNode.size)
                {
                    c = currentNode.size;
                    selected = currentNode;
                }
            }

            //Console.WriteLine("k : " + k);

            cover(selected);

            for(MatrixNode iNode = selected.down; iNode!=selected; iNode = iNode.down)
            {

                rows.AddLast(iNode.rowIndex);

                for(MatrixNode jNode = iNode.right;jNode != iNode;jNode = jNode.right)
                {
                    cover(jNode.item);
                }
                
                search(k + 1);

                rows.RemoveLast();

                for (MatrixNode jNode = iNode.left; jNode != iNode; jNode = jNode.left)
                {
                    uncover(jNode.item);
                }
            }
            uncover(selected);
            return;
        }

        private void cover(MatrixNodeHead node)
        {
            node.left.right = node.right;
            node.right.left = node.left;
            ///Console.WriteLine("depart : " + node.name);
            for (MatrixNode iNode = node.down;iNode != node;iNode = iNode.down) { 
                for(MatrixNode jNode = iNode.right;jNode != iNode;jNode= jNode.right)
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
