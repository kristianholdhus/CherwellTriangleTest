using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Triangles.Triangles
{
    public class RowColMathTest
    {
        [Theory]
        [ClassData(typeof(TrianglesByCoordinates))]
        public void ReturnsCorrectCoordinatesForRowAndCol(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y, char expectedRow, int expectedCol)
        {
            RowColMath.RowCol rowCol = RowColMath.GetRowColForCoordinates(v1x, v1y, v2x, v2y, v3x, v3y);
            Assert.Equal(expectedRow, rowCol.Row);
            Assert.Equal(expectedCol, rowCol.Col);
        }

        private class TrianglesByCoordinates : IEnumerable<object[]>
        {

            //   v2
            //    |`.
            //    |  `.
            //    |    `.
            //   v1------v3
            private static readonly int[] LOWER_TRIANGLE_OFFSETS = {1, 0, 10, 0, 0, 10, 10}; // col, v1x, v1y, v2x, v2y,...

            //   v2------v1
            //     `.     |
            //       `.   |
            //         `. |
            //           v3
            private static readonly int[] UPPER_TRIANGLE_OFFSET = {2, 10, 0, 0, 0, 10, 10}; // col, v1x, v1y, v2x, v2y,...
            public IEnumerator<object[]> GetEnumerator()
            {
                for (int baseRowOffset = 0; baseRowOffset < 6; baseRowOffset++)
                {
                    for (int baseColOffset = 0; baseColOffset < 6; baseColOffset++)
                    {
                        char row = (char) ('A' + baseRowOffset); // 'A' -> 'F'
                        int baseYCoordinateOffset = baseRowOffset * 10;
                        int baseXCoordinateOffset = baseColOffset * 10;

                        foreach (int[] offsetMatrix in new int[][]{LOWER_TRIANGLE_OFFSETS, UPPER_TRIANGLE_OFFSET})
                        {
                            int col = (baseColOffset * 2) + offsetMatrix[0];
                            int v1x = baseXCoordinateOffset + offsetMatrix[1];
                            int v1y = baseYCoordinateOffset + offsetMatrix[2];
                            int v2x = baseXCoordinateOffset + offsetMatrix[3];
                            int v2y = baseYCoordinateOffset + offsetMatrix[4];
                            int v3x = baseXCoordinateOffset + offsetMatrix[5];
                            int v3y = baseYCoordinateOffset + offsetMatrix[6];
                            // Pass vertices in all order permutations; they should all work.
                            yield return new Object[] {v1x, v1y, v2x, v2y, v3x, v3y, row, col};
                            yield return new Object[] {v1x, v1y, v3x, v3y, v2x, v2y, row, col};
                            yield return new Object[] {v2x, v2y, v1x, v1y, v3x, v3y, row, col};
                            yield return new Object[] {v2x, v2y, v3x, v3y, v1x, v1y, row, col};
                            yield return new Object[] {v3x, v3y, v1x, v1y, v2x, v2y, row, col};
                            yield return new Object[] {v3x, v3y, v2x, v2y, v1x, v1y, row, col};
                        }
                    }                    
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }
    }
}