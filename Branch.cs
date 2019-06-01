using System.Collections.Generic;

public class Branch
{
    public bool IsBranching { get; set; }
    public bool IsSunlit { get; set; }
    public List<Tile> Tiles { get; }
    public Tile Leaf { get; set; }
    public TileState Direction { get; }
    public List<Branch> Branches { get; }


    public Branch(TileState direction, Tile leaf)
    {
        Direction = direction;
        Tiles = new List<Tile>();
        Leaf = leaf;
        Branches = new List<Branch>();
        IsBranching = false;
        IsSunlit = false;
    }
}