using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Triangles.Triangles
{
    public class CoordinateMathTest
    {
        [Fact]
        public void FailsWhenRowLetterIsAtSymbol() =>
            Assert.Throws<IndexOutOfRangeException>(() => CoordinateMath.GetCoordinatesForRowCol('@', 1));

        [Fact]
        public void FailsWhenRowLetterIsG() =>
            Assert.Throws<IndexOutOfRangeException>(() => CoordinateMath.GetCoordinatesForRowCol('G', 1));

        [Fact]
        public void FailsWhenColIsZero() =>
            Assert.Throws<IndexOutOfRangeException>(() => CoordinateMath.GetCoordinatesForRowCol('A', 0));

        [Fact]
        public void FailsWhenColIs13() =>
            Assert.Throws<IndexOutOfRangeException>(() => CoordinateMath.GetCoordinatesForRowCol('A', 13));

        [Theory]
        [ClassData(typeof(TrianglesByRowCol))]
        public void ReturnsCorrectCoordinatesForRowAndCol(char row, int col,
            int expectedV1x, int expectedV1y, int expectedV2x, int expectedV2y, int expectedV3x, int expectedV3y)
        {
            CoordinateMath.Coordinates coordinates = CoordinateMath.GetCoordinatesForRowCol(row, col);
            Assert.Equal(expectedV1x, coordinates.Vertex1.X);
            Assert.Equal(expectedV1y, coordinates.Vertex1.Y);
            Assert.Equal(expectedV2x, coordinates.Vertex2.X);
            Assert.Equal(expectedV2y, coordinates.Vertex2.Y);
            Assert.Equal(expectedV3x, coordinates.Vertex3.X);
            Assert.Equal(expectedV3y, coordinates.Vertex3.Y);
        }

        private class TrianglesByRowCol : IEnumerable<object[]>
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
                            yield return new Object[] {row, col, v1x, v1y, v2x, v2y, v3x, v3y};
                        }
                    }                    
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }
    }
}