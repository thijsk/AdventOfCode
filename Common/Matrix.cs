using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Common
{
    public class Matrix<T>
    {
        T[,] _matrix;

        Rotation _rotation;
        private Flip _flip;

        private int _xUpperbound;
        private int _yUpperbound;

        private enum Rotation
        {
            None,
            Clockwise90,
            Clockwise180,
            Clockwise270
        }

        private enum Flip
        {
            None,
            Flipped
        }


        public Matrix(T[,] matrix)
        {
            _matrix = matrix;
            _rotation = Rotation.None;
            _xUpperbound = _matrix.GetUpperBound(0);
            _yUpperbound = _matrix.GetUpperBound(1);
        }

        //  the transformation routines
        public void RotateClockwise90()
        {
            _rotation = (Rotation)(((int)_rotation + 1) & 3);
        }

        public void Rotate180()
        {
            _rotation = (Rotation)(((int)_rotation + 2) & 3);
        }

        public void RotateAntiClockwise90()
        {
            _rotation = (Rotation)(((int)_rotation + 3) & 3);
        }

        public void FlipMe()
        {
            _flip = (_flip == Flip.None ? Flip.Flipped : Flip.None);
        }

        //  accessor property to make class look like a two dimensional array
        public T this[int row, int column]
        {
            get
            {
                var (x, y) = TranslatePosition(row, column);
                return _matrix[x, y];
            }

            set
            {
                var (x, y) = TranslatePosition(row, column);
                _matrix[x, y] = value;
            }
        }

        private (int row, int column) TranslatePosition(int row, int column)
        {
            int outrow = 0;
            int outcolumn = 0;

            switch (_rotation)
            {
                case Rotation.None:
                    outrow = row;
                    outcolumn = column;
                    break;

                case Rotation.Clockwise90:
                    outrow = _xUpperbound - column;
                    outcolumn = row;
                    break;

                case Rotation.Clockwise180:
                    outrow = _xUpperbound - row;
                    outcolumn = _yUpperbound - column;
                    break;

                case Rotation.Clockwise270:
                    outrow = column;
                    outcolumn = _yUpperbound - row;
                    break;
            }

            if (_flip == Flip.Flipped)
            {
                int tmp = outrow;
                outrow = outcolumn;
                outcolumn = tmp;
            }

            return (outrow, outcolumn);
        }

        //  creates a string with the matrix values
        public override string ToString()
        {
            int
                nu_rows = 0,
                nu_columns = 0;

            switch (_rotation)
            {
                case Rotation.None:
                case Rotation.Clockwise180:
                    nu_rows = _matrix.GetUpperBound(0);
                    nu_columns = _matrix.GetUpperBound(1);
                    break;

                case Rotation.Clockwise90:
                case Rotation.Clockwise270:
                    nu_rows = _matrix.GetUpperBound(1);
                    nu_columns = _matrix.GetUpperBound(0);
                    break;
            }

            StringBuilder
                output = new StringBuilder();

            output.Append("{");

            for (int row = 0; row <= nu_rows; ++row)
            {
                if (row != 0)
                {
                    output.Append(", ");
                }

                output.Append("{");

                for (int column = 0; column <= nu_columns; ++column)
                {
                    if (column != 0)
                    {
                        output.Append(", ");
                    }

                    output.Append(this[row, column]);
                }

                output.Append("}");
            }

            output.Append("}");

            return output.ToString();
        }


    }
}