namespace Dlx
{
    class MatrixNodeHead : MatrixNode
    {
        internal string name;
        internal new MatrixNode item;
        internal int size;

        public MatrixNodeHead(string s) : base(null)
        {
            name = s;
        }

    }
}
