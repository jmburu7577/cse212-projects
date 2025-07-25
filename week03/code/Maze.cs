using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap ?? new Dictionary<ValueTuple<int, int>, bool[]>();
    }

    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        if (CanMove(-1, 0)) _currX--;
        else throw new InvalidOperationException("Can't go that way!");
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        if (CanMove(1, 0)) _currX++;
        else throw new InvalidOperationException("Can't go that way!");
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveUp()
    {
        if (CanMove(0, -1)) _currY--;
        else throw new InvalidOperationException("Can't go that way!");
    }

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveDown()
    {
        if (CanMove(0, 1)) _currY++;
        else throw new InvalidOperationException("Can't go that way!");
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }

    private bool CanMove(int dx, int dy)
    {
        if (!_mazeMap.ContainsKey((_currX, _currY))) return false;
        int newX = _currX + dx;
        int newY = _currY + dy;
        if (newX < 1 || newX > 6 || newY < 1 || newY > 6) return false;
        var moves = _mazeMap[(_currX, _currY)];
        int moveIndex = dx == -1 ? 0 : dx == 1 ? 1 : dy == -1 ? 2 : 3;
        return moves[moveIndex] && _mazeMap.ContainsKey((newX, newY));
    }
}