using System;
using System.Collections.Generic;
using System.Text;

namespace Dlx
{
    class MatrixNode
    {
        internal MatrixNode right;
        internal MatrixNode left;
        internal MatrixNode up;
        internal MatrixNode down;
        internal MatrixNodeHead item;
        internal int rowIndex;

        public MatrixNode(MatrixNodeHead t){
            item = t;
        }
    }
}
