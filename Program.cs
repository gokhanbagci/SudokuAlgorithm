Console.WriteLine("### Sudoku Algorithm ###");
Console.WriteLine(Environment.NewLine);

Console.WriteLine("What is the number of row of the board?");
if (!int.TryParse(Console.ReadLine(), out var rowCount))
{
    Console.WriteLine("Incorrect number!");
    Console.WriteLine(Environment.NewLine);
    return;
}

Console.WriteLine("What is the number of column of the board?");
if (!int.TryParse(Console.ReadLine(), out var columnCount))
{
    Console.WriteLine("Incorrect number!");
    Console.WriteLine(Environment.NewLine);
    return;
}

if (rowCount != columnCount)
{
    Console.WriteLine("Columns and Rows should be equal!");
    Console.WriteLine(Environment.NewLine);
    return;
}

int[,] board = new int[rowCount, columnCount];

for (int row = 0; row < rowCount; row++)
{
    Console.WriteLine($"Enter the {row+1} row elements? (with a comma)");
    var columns = Console.ReadLine()?.Split(",").Select(int.Parse).ToList();
    if (columns?.Count != columnCount)
    {
        Console.WriteLine($"Incorrect column elements! (Only {columnCount} elements must be entered)");
        return;
    }
    for (int column = 0; column < columnCount; column++)
    {
        board[row, column] = columns[column];
    }
    Console.WriteLine(Environment.NewLine);
}

for (int row = 0; row < rowCount; row++)
{
    Console.Write($"Row {row + 1}: ");
    for (int column = 0; column < columnCount; column++)
    {
        Console.Write(board[row, column] + (column != columnCount ? ", " : ""));
    }
    Console.WriteLine(Environment.NewLine);
}
Console.WriteLine(Environment.NewLine);

var solver = new SudokuSolver(board, rowCount, columnCount);
var solved = solver.IsSolved();

Console.WriteLine($"Solved: {solved}");
Console.Read();

public class SudokuSolver
{
    private int _columnCount;
    private int _rowCount;
    private int[,] _board;
    public SudokuSolver(int[,] board, int columnCount, int rowCount)
    {
        _board = board;
        _columnCount = columnCount;
        _rowCount = rowCount;
    }
    public bool IsSolved()
    {
        for (int row = 0; row < _rowCount; row++)
        {
            for (int column = 0; column < _columnCount; column++)
            {
                int value = _board[row, column];
                for (int otherColumn = 0; otherColumn < _columnCount; otherColumn++)
                {
                    if (column == otherColumn)
                    {
                        continue;
                    }
                    int otherValue = _board[row, otherColumn];
                    if (value == otherValue)
                    {
                        return false;
                    }
                }
                for (int otherRow = 0; otherRow < _rowCount; otherRow++)
                {
                    if (row == otherRow)
                    {
                        continue;
                    }
                    int otherValue = _board[otherRow, column];
                    if (value == otherValue)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }
}