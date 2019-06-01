public class Tile
{
    public int X { get; }
    public int Y { get; }
    public TileState TileState { get; set; }

    public Tree Tree { get; set; }

    public Branch Branch { get; set; }

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }
}