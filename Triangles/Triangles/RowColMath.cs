using System;

namespace Triangles.Triangles
{
    // Static utility class for computing Triangle Row and Column given coordinates for three vertices.
    public class RowColMath
    {
        // Will compute Row and Column of triangle. Vertices and be in any order, as long as they
        // form the shape of the one the two valid triangle configurations:
        //
        //  o          o----o
        //  |`.    OR   `.  |
        //  |  `.         `.|
        //  o----o          o
        public static RowCol GetRowColForCoordinates(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            // Check for V1 and V2 in vertical leg configuration
            if (v1x == v2x) {
                if (v1y + 10 == v2y) {
                    // V1 is above V2 in vertical leg
                    return GetRowColForVerticalLeg(v1x, v1y, v2x, v2y, v3x, v3y);
                } else if (v1y - 10 == v2y) {
                    // V1 is above V2 in vertical leg
                    return GetRowColForVerticalLeg(v2x, v2y, v1x, v1y, v3x, v3y);
                } else {
                    throw new ArgumentOutOfRangeException("Vertex 1 and 2 are in vertical alignment, but Y coordinates are not 10 appart. " +
                                                          $"V1: ({v1x}, {v1y}), V2: ({v2x}, {v2y})");
                }
            }
            // Check for V1 and V2 in horizontal leg configuration
            if (v1y == v2y) {
                if (v1x + 10 == v2x) {
                    // V1 is left of V2 in horizontal leg
                    return GetRowColForHorizontalLeg(v1x, v1y, v2x, v2y, v3x, v3y);
                } else if (v1x - 10 == v2x) {
                    // V1 is right of V2 in horizontal leg
                    return GetRowColForHorizontalLeg(v2x, v2y, v1x, v1y, v3x, v3y);
                } else {
                    throw new ArgumentOutOfRangeException("Vertex 1 and 2 are in horizontal alignment, but Y coordinates are not 10 appart. " +
                                                          $"V1: ({v1x}, {v1y}), V2: ({v2x}, {v2y})");
                }
            }
            // Check for V1 and V2 in diagonal (hypotenuse) configuration
            if ((v1x + 10 == v2x) && (v1y + 10 == v2y)) {
                // V1
                //   `.
                //     V2
                return GetRowColForHypotenuse(v1x, v1y, v2x, v2y, v3x, v3y);
            }
            if ((v1x - 10 == v2x) && (v1y - 10 == v2y)) {
                // V2
                //   `.
                //     V1
                return GetRowColForHypotenuse(v2x, v2y, v1x, v1y, v3x, v3y);
            }
            throw new ArgumentOutOfRangeException("Vertex 1 and 2 are not in horizontal or vertical alignment, and are not in a top-left " +
                                                  "to bottom-right diagonal where the difference between V1 and V2 must be 10 points, horizontally and vertically." +
                                                  $"V1: ({v1x}, {v1y}), V2: ({v2x}, {v2y})");
        }

        // Computes Row/Col for vertices where V1 and V2 form a vertical triangle leg.
        private static RowCol GetRowColForVerticalLeg(int vLegUpperX, int vLegUpperY,
                                                      int vLegLowerX, int vLegLowerY,
                                                      int otherX, int otherY)
        {
            // Upper
            //  | `.
            //  |   `.
            //  |     `.
            // Lower---Other
            if ((vLegLowerX + 10 == otherX) && (vLegLowerY == otherY)) {
                return GetRowColForLowerTriangle(vLegUpperX, vLegUpperY);
            }
            // Other---Upper
            //   `.      |
            //     `.    |
            //       `.  |
            //         Lower
            if ((vLegUpperX - 10 == otherX) && (vLegUpperY == otherY)) {
                return GetRowColForUpperTriangle(otherX, otherY);
            }
            throw new ArgumentOutOfRangeException("Illegal coordinate configuration where V1 and V2 are in vertical leg. " +
                                                  $"Vertical Leg (Upper): ({vLegUpperX}, {vLegUpperY}), " +
                                                  $"Vertical Leg (Lower): ({vLegLowerX}, {vLegLowerY}), " +
                                                  $"Other: ({otherX}, {otherY})");
        }

        // Computes Row/Col for vertices where V1 and V2 form a horizontal triangle leg.
        private static RowCol GetRowColForHorizontalLeg(int hLegLeftX, int hLegLeftY,
                                                      int hLegRightX, int hLegRightY,
                                                      int otherX, int otherY)
        {
            // Other
            //  | `.
            //  |   `.
            //  |     `.
            // Left----Right
            if ((hLegLeftX == otherX) && (hLegLeftY - 10 == otherY)) {
                return GetRowColForLowerTriangle(otherX, otherY);
            }
            // Left----Right
            //   `.      |
            //     `.    |
            //       `.  |
            //         Other
            if ((hLegRightX == otherX) && (hLegRightY + 10 == otherY)) {
                return GetRowColForUpperTriangle(hLegLeftX, hLegLeftY);
            }
            throw new ArgumentOutOfRangeException("Illegal coordinate configuration where V1 and V2 are in horizontal leg. " +
                                                  $"Horizontal Leg (Left): ({hLegLeftX}, {hLegLeftY}), " +
                                                  $"Horizontal Leg (Right): ({hLegRightX}, {hLegRightY}), " +
                                                  $"Other: ({otherX}, {otherY})");
        }

        // Computes Row/Col for vertices where V1 and V2 form the hypotenuse.
        private static RowCol GetRowColForHypotenuse(int hypoTopLeftX, int hypoTopLeftY,
                                                      int hypoBotRightX, int hypoBotRightY,
                                                      int otherX, int otherY)
        {
            // TopLeft
            //   | `.
            //   |   `.
            //   |     `.
            // Other---BotRight
            if ((hypoTopLeftX == otherX) && (hypoBotRightY == otherY)) {
                return GetRowColForLowerTriangle(hypoTopLeftX, hypoTopLeftY);
            }
            // TopLeft---Other
            //    `.      |
            //      `.    |
            //        `.  |
            //        BotRight
            if ((hypoBotRightX == otherX) && (hypoTopLeftY == otherY)) {
                return GetRowColForUpperTriangle(hypoTopLeftX, hypoTopLeftY);
            }
            throw new ArgumentOutOfRangeException("Illegal coordinate configuration where V1 and V2 are in diagonal. " +
                                                  $"Hypotenuse (TopLeft): ({hypoTopLeftX}, {hypoTopLeftY}), " +
                                                  $"Hypotenuse (BottomRight): ({hypoBotRightX}, {hypoBotRightY}), " +
                                                  $"Other: ({otherX}, {otherY})");
        }

        // Computes Row/Col for vertices that for the shape of an upper triangle in a grid square.
        private static RowCol GetRowColForUpperTriangle(int topLeftX, int topLeftY)
        {
            // TopLeft---Other
            //      `.     |
            //        `.   |
            //          `. |
            //           Other
            ValidateTopLeftCoordinate(topLeftX, topLeftY);

            int xColIndex = topLeftX / 10;
            int yColIndex = topLeftY / 10;

            // 0 -> 2, 1 -> 4, 2 -> 6, 3 -> 8, 4 -> 10, 5 -> 12
            int col = (xColIndex + 1) * 2;
            // 0 -> A, 1 -> B, 2 -> C, 3 -> D, 4 -> E, 5 -> F
            char row = (char) (yColIndex + 'A');

            return new RowCol(row, col);
        }

        // Computes Row/Col for vertices that for the shape of a lower triangle in a grid square.
        private static RowCol GetRowColForLowerTriangle(int topLeftX, int topLeftY)
        {
            // TopLeft
            //   | `.
            //   |   `.
            //   |     `.
            // Other---Other
            ValidateTopLeftCoordinate(topLeftX, topLeftY);

            int xColIndex = topLeftX / 10;
            int yColIndex = topLeftY / 10;

            // 0 -> 1, 1 -> 3, 2 -> 5, 3 -> 7, 4 -> 9, 5 -> 11
            int col = (xColIndex * 2) + 1;
            // 0 -> A, 1 -> B, 2 -> C, 3 -> D, 4 -> E, 5 -> F
            char row = (char) (yColIndex + 'A');

            return new RowCol(row, col);
        }

        // Validates that a top-left coordinate is sane (within bounds, and multiple of 10).
        private static void ValidateTopLeftCoordinate(int topLeftX, int topLeftY)
        {
            if (!(topLeftX >= 0 && topLeftX <= 50)) {
                throw new ArgumentOutOfRangeException("TopLeft X coordinate must be >= 0, and <= 50, but was: " + topLeftX);
            }
            if (!(topLeftY >= 0 && topLeftY <= 50)) {
                throw new ArgumentOutOfRangeException("TopLeft Y coordinate must be >= 0, and <= 50, but was: " + topLeftY);
            }
            if (!(topLeftX % 10 == 0)) {
                throw new ArgumentOutOfRangeException("TopLeft X coordinate must be multiple of 10, but was: " + topLeftX);
            }
            if (!(topLeftY % 10 == 0)) {
                throw new ArgumentOutOfRangeException("TopLeft Y coordinate must be multiple of 10, but was: " + topLeftY);
            }
        }

        public sealed class RowCol
        {
            public char Row {get;}
            public int Col {get;}

            public RowCol(char row, int col)
            {
                this.Row = row;
                this.Col = col;
            }
        }

    }
}
