# How to Run Service

```sh
# Starting from base directory...
cd Triangles
dotnet run
```

# How to Run Tests

```sh
# Starting from base directory...
cd Triangles.Tests
dotnet test
```

# How to Query Service

**Get Coordinates for Row and Column**:

1. Run the Service (using above dotnet command)
1. Visit the REST URL for querying by row and colum: `https://localhost:5001/Triangles/CoordinatesByRowAndColumn?row=A&column=1`
1. Replace `row` and `column` arguments with your own values.

**Get Row and Column for Coordinates**:

1. Run the Service (using above dotnet command)
1. Visit the REST URL for querying by row and colum: `https://localhost:5001/Triangles/RowAndColByCoordinates?v1x=40&v1y=30&v2x=40&v2y=40&v3x=30&v3y=30`
1. Replace coordinate arguments with your own values.
