using System;

namespace Triangles.Triangles
{
    public class CoordinateMath
    {
        public static Coordinates GetCoordinatesForRowCol(char row, int col)
        {
            if (row >= 'a' && row <= 'f') {
                row = (char) (row - 32);
            }
            if (row < 'A' || row > 'F') {
                throw new IndexOutOfRangeException("Row must be a letter >= A, <= F.");
            }
            if (col < 1 || col > 12) {
                throw new IndexOutOfRangeException("Column must be >= 1, and <= 12");
            }
            int rowIndex = row - 'A'; // Convert to zero-indexed (number)
            if (col % 2 == 1) {
                int colIndex = (col - 1) / 2;
                return GetLowerTriangleCoordinates(rowIndex, colIndex);
            } else {
                int colIndex = (col - 2) / 2;
                return GetUpperTriangleCoordinates(rowIndex, colIndex);
            }
        }

        // rowIndex and colIndex are indexed-from-zero.
        private static Coordinates GetLowerTriangleCoordinates(int rowIndex, int colIndex)
        {

            //   v2
            //    |`.
            //    |  `.
            //    |    `.
            //   v1------v3

            // X coordinates
            int v1x = colIndex * 10;
            int v2x = colIndex * 10;
            int v3x = colIndex * 10 + 10;

            // Y coordinates
            int v1y = rowIndex * 10 + 10;
            int v2y = rowIndex * 10;
            int v3y = rowIndex * 10 + 10;

            return new Coordinates(v1x, v1y, v2x, v2y, v3x, v3y);
        }

        // rowIndex and colIndex are indexed-from-zero.
        private static Coordinates GetUpperTriangleCoordinates(int rowIndex, int colIndex)
        {
            //   v2------v1
            //     `.    |
            //       `.  |
            //         `.|
            //           v3

            // X coordinates
            int v1x = colIndex * 10 + 10;
            int v2x = colIndex * 10;
            int v3x = colIndex * 10 + 10;

            // Y coordinates
            int v1y = rowIndex * 10;
            int v2y = rowIndex * 10;
            int v3y = rowIndex * 10 + 10;

            return new Coordinates(v1x, v1y, v2x, v2y, v3x, v3y);
        }

        public sealed class Coordinates
        {
            public Coordinate Vertex1 {get;}
            public Coordinate Vertex2 {get;}
            public Coordinate Vertex3 {get;}

            public Coordinates(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
            {
                this.Vertex1 = new Coordinate(v1x, v1y);
                this.Vertex2 = new Coordinate(v2x, v2y);
                this.Vertex3 = new Coordinate(v3x, v3y);
            }
        }

        public sealed class Coordinate
        {
            public int X {get;}
            public int Y {get;}

            public Coordinate(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }
    }
}