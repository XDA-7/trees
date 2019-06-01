using System.Collections.Generic;

public class World
{
    public readonly int WorldWidth;
    public readonly int WorldHeight;
    private readonly double treeSpawnChance = 0.1;

    private System.Random rng = new System.Random();

    public Tile[,] Tiles { get; }
    public List<Tree> Trees { get; }

    public World(int width, int height)
    {
        WorldWidth = width;
        WorldHeight = height;
        Tiles = new Tile[WorldWidth, WorldHeight];
        Trees = new List<Tree>();
        for (var i = 0; i < WorldWidth; i++)
        {
            for (var j = 0; j < WorldHeight; j++)
            {
                Tiles[i, j] = new Tile(i, j) { TileState = TileState.Shadow };
            }
        }
    }

    public void Run()
    {
        RunTrees();
        RunSunlight();
    }

    private void RunSunlight()
    {
        for (var i = 0; i < WorldWidth; i++)
        {
            var sunBlocked = false;
            for (var j = WorldHeight - 1; j > -1; j--)
            {
                var tile = Tiles[i, j];
                if (!sunBlocked)
                {
                    switch (tile.TileState)
                    {
                        case TileState.Leaf:
                            tile.Tree.GiveSunlight(tile.Branch);
                            sunBlocked = true;
                            break;
                        case TileState.LeftBranch:
                        case TileState.RightBranch:
                        case TileState.StraightBranch:
                            sunBlocked = true;
                            break;
                        case TileState.Shadow:
                            tile.TileState = TileState.Sunlit;
                            break;

                    }
                }
                else
                {
                    switch (tile.TileState)
                    {
                        case TileState.Sunlit:
                            tile.TileState = TileState.Shadow;
                            break;
                    }
                }
            }
        }
    }

    private void RunTrees()
    {
        for (var i = 0; i < WorldWidth; i++)
        {
            var tile = Tiles[i, 0];
            if (tile.Tree == null && rng.NextDouble() < treeSpawnChance)
            {
                var tree = new Tree(i, this);
                Trees.Add(tree);
            }
        }

        foreach (var tree in Trees)
        {
            tree.Grow(this);
        }
    }
}