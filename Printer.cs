using System;
using System.Collections.Generic;

public class Printer
{
    public readonly int ScreenWidth = Console.WindowWidth;
    public readonly int ScreenHeight = Console.WindowHeight;

    private readonly Dictionary<TileState, char> charSet = new Dictionary<TileState, char>
    {
        { TileState.Leaf, 'o' },
        { TileState.LeftBranch, '\\' },
        { TileState.RightBranch, '/' },
        { TileState.Shadow, ' ' },
        { TileState.StraightBranch, '|' },
        { TileState.Sunlit, ' ' },
    };
    
    public Printer()
    {
        for (var i = 0; i < ScreenWidth; i++)
        {
            for (var j = 0; j < ScreenHeight; j++)
            {
                PrintChar(i, j, ' ');
            }
        }
    }

    public void PrintChar(int x, int y, char value)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(value);
    }

    public void PrintWorld(World world)
    {
        for (var i = 0; i < world.WorldWidth; i++)
        {
            for (var j = 0; j < world.WorldHeight; j++)
            {
                var y = world.WorldHeight - j - 1;
                PrintChar(i, y, charSet[world.Tiles[i, j].TileState]);
            }
        }
    }

    public void PrintTreeIds(World world)
    {
        var treeIds = new Dictionary<Tree, int>();
        for (var i = 0; i < world.Trees.Count; i++)
        {
            treeIds.Add(world.Trees[i], i % 10);
        }

        for (var i = 0; i < world.WorldWidth; i++)
        {
            for (var j = 0; j < world.WorldHeight; j++)
            {
                var y = world.WorldHeight - j - 1;
                if (world.Tiles[i, j].Tree == null)
                {
                    PrintChar(i, y, ' ');
                }
                else
                {
                    PrintChar(i, y, treeIds[world.Tiles[i, j].Tree].ToString()[0]);
                }
            }
        }
    }
}