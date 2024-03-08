
using System;

[Serializable]
public struct TilePosition
{
    public int x;
    public int y;
    public TilePosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public static class TileExtensions
{
    public static bool Equals(this TilePosition a, TilePosition b)
    {
        if(a.x == b.x && a.y == b.y) return true;
        else return false;
    }
}

