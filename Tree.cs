using System;
using System.Collections.Generic;

public class Tree
{
    private readonly int maxBranchLength;
    private readonly int branchingFactor;

    private Random rng = new Random();
    private int rootPosition;
    private List<Branch> trunkBranches;
    private List<Branch> leafBranches;

    public Tree(int position, World world)
    {
        maxBranchLength = (rng.Next() % 5) + 1;
        branchingFactor = rng.Next() % 2 == 0 ? 2 : 2;// 3;

        rootPosition = position;
        var rootTile = world.Tiles[position, 0];
        rootTile.TileState = TileState.Leaf;
        rootTile.Tree = this;
        trunkBranches = new List<Branch>();
        leafBranches = new List<Branch>();
        var rootBranch = new Branch(TileState.StraightBranch, rootTile);
        rootTile.Branch = rootBranch;
        leafBranches.Add(rootBranch);
    }

    public void GiveSunlight(Branch branch)
    {
        branch.IsSunlit = true;
    }

    public void Grow(World world)
    {
        var splitBranches = new List<Branch>();
        var newBranches = new List<Branch>();
        foreach (var branch in leafBranches)
        {
            if (!branch.IsSunlit)
            {
                continue;
            }

            if (branch.Tiles.Count == maxBranchLength)
            {
                newBranches.AddRange(SplitBranch(branch, world));
                splitBranches.Add(branch);
            }
            else
            {
                ExtendBranch(branch, world);
            }
        }

        trunkBranches.AddRange(splitBranches);
        leafBranches.AddRange(newBranches);
        foreach (var splitBranch in splitBranches)
        {
            leafBranches.Remove(splitBranch);
        }
    }

    public void ExtendBranch(Branch branch, World world)
    {
        Tile nextTile = null;
        var leafTile = branch.Leaf;
        switch (branch.Direction)
        {
            case TileState.StraightBranch:
                nextTile = world.Tiles[leafTile.X, leafTile.Y + 1];
                break;
            case TileState.LeftBranch:
                if (leafTile.X > 0 && world.Tiles[leafTile.X - 1, leafTile.Y].Tree == null)
                {
                    nextTile = world.Tiles[leafTile.X - 1, leafTile.Y + 1];
                }

                break;
            case TileState.RightBranch:
                if (leafTile.X < world.WorldWidth - 1 && world.Tiles[leafTile.X + 1, leafTile.Y].Tree == null)
                {
                    nextTile = world.Tiles[leafTile.X + 1, leafTile.Y + 1];
                }

                break;
        }

        if (nextTile != null && nextTile.Tree == null)
        {
            nextTile.Tree = this;
            nextTile.Branch = branch;
            nextTile.TileState = TileState.Leaf;
            leafTile.TileState = branch.Direction;
            branch.Tiles.Add(nextTile);
            branch.Leaf = nextTile;
        }
    }

    public List<Branch> SplitBranch(Branch branch, World world)
    {
        var branchDirections = new List<TileState>();
        if (branchingFactor == 2)
        {
            var roll = rng.Next() % 3;
            switch (roll)
            {
                case 0:
                    branchDirections.Add(TileState.LeftBranch);
                    branchDirections.Add(TileState.StraightBranch);
                    break;
                case 1:
                    branchDirections.Add(TileState.StraightBranch);
                    branchDirections.Add(TileState.RightBranch);
                    break;
                case 2:
                    branchDirections.Add(TileState.LeftBranch);
                    branchDirections.Add(TileState.RightBranch);
                    break;
            }
        }
        else if (branchingFactor == 3)
        {
            branchDirections.Add(TileState.LeftBranch);
            branchDirections.Add(TileState.StraightBranch);
            branchDirections.Add(TileState.RightBranch);
        }

        var newBranches = new List<Branch>();
        foreach (var direction in branchDirections)
        {
            var newBranch = new Branch(direction, branch.Leaf);
            ExtendBranch(newBranch, world);
            newBranches.Add(newBranch);
        }

        branch.IsBranching = true;
        branch.Leaf.TileState = branch.Direction;
        branch.Leaf = null;
        return newBranches;
    }
}