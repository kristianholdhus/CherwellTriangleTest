using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using CherwellTriangleTest.Triangles;

namespace CherwellTriangleTest.Controllers
{
    public class Triangles : Controller
    {
        // 
        // GET: /Triangles/

        public string Index()
        {
            return "This is my default action...";
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
    }

}