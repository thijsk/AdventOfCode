using Common;

namespace ConsoleApp2021;

public class Day04 : IDay
{
    public long Part1()
    {
        var (draws, cards) = Parse(PuzzleContext.Input);

        foreach (var draw in draws)
        {
            foreach (var card in cards)
            {
                card.Draw(draw);
                if (card.IsComplete())
                {
                    return card.GetTotal() * draw;
                }
            }
        }

    return 0;
}

    public long Part2()
    {
        var (draws, cards) = Parse(PuzzleContext.Input);

        BingoCard lastWonCard = cards.First();
        long lastWonDraw = 0;

        foreach (var draw in draws)
        {
            foreach (var card in cards.Where(c => !c.IsComplete()))
            {
                card.Draw(draw);
                if (card.IsComplete())
                {
                    lastWonCard = card;
                    lastWonDraw = draw;
                }
            }
        }

        return lastWonCard.GetTotal() * lastWonDraw;
    }

    public (IEnumerable<int>, List<BingoCard>) Parse(string[] input)
    {
        var draw = input.First().Split(',').Select(int.Parse);

        var cards = new List<BingoCard>();

        cards.AddRange(input.Skip(1).Chunk(6).Select(x => new BingoCard(x.Skip(1))));


        return (draw, cards);
    }

}

public class BingoCard
{
    private readonly BingoCell[,] _cells;

    public BingoCard(IEnumerable<string> lines)
    {
        _cells = new BingoCell[5, 5];

        var row = 0;
        foreach (var line in lines)
        {
            var col = 0;
            foreach (var value in line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            {
                _cells[row, col] = new BingoCell(value);
                col++;
            }

            row++;
        }
    }

    public void Draw(int draw)
    {
        foreach (var cell in _cells)
        {
            if (cell.Number == draw)
            {
                cell.Found = true;
            }
        }
    }

    public bool IsComplete()
    {
        return _cells.GetRows().Any(IsComplete) || _cells.GetColumns().Any(IsComplete);
    }

    private bool IsComplete(BingoCell[] arg)
    {
        return arg.All(cell => cell.Found);
    }

    public long GetTotal()
    {
        return _cells.Cast<BingoCell>().Where(c => c.Found == false).Sum(c => c.Number);
    }
}

public class BingoCell
{
    public int Number;
    public bool Found;

    public BingoCell(string value)
    {
        Number = int.Parse(value);
        Found = false;
    }

    public override string ToString()
    {
        return $"{Number}{Found}";

    }
}