using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Triangles.Triangles;

namespace Triangles.Controllers
{
    // Restful Controller for interacting with Triangles
    public class Triangles : Controller
    {
        //
        // GET: /Triangles/
        public string Index()
        {
            return "Refer to README.md for instructions.";
        }

        //
        // GET: /Triangles/CoordinatesByRowAndColumn/?row={row}&column={column}
        public string CoordinatesByRowAndColumn(char row, int column)
        {
            CoordinateMath.Coordinates coordinates = CoordinateMath.GetCoordinatesForRowCol(row, column);
            return HtmlEncoder.Default.Encode($"Vertex1: ({coordinates.Vertex1.X}, {coordinates.Vertex1.Y}), " +
                                              $"Vertex2: ({coordinates.Vertex2.X}, {coordinates.Vertex2.Y}), " +
                                              $"Vertex3: ({coordinates.Vertex3.X}, {coordinates.Vertex3.Y})");
        }

        //
        // GET: /Triangles/RowAndColByCoordinates/?v1x={v1x}&v1y={v1y}&v2x=...
        public string RowAndColByCoordinates(int v1x, int v1y, int v2x, int v2y, int v3x, int v3y)
        {
            RowColMath.RowCol rowCol = RowColMath.GetRowColForCoordinates(v1x, v1y, v2x, v2y, v3x, v3y);
            return HtmlEncoder.Default.Encode($"Row: {rowCol.Row}, Col: {rowCol.Col}");
        }
    }

}