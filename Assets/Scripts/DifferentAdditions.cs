using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DifferentAdditions : MonoBehaviour
{
    public static Quaternion DirectionToRotation(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Quaternion.Euler(0, 0, 0);
            case Direction.Right:
                return Quaternion.Euler(0, 0, -90);
            case Direction.Down:
                return Quaternion.Euler(0, 0, 180);
            case Direction.Left:
                return Quaternion.Euler(0, 0, 90);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static Orientation AnotherOrientation(Orientation orientation)
    {
        if (orientation == Orientation.Horizontal) return Orientation.Vertical;
        else return Orientation.Horizontal;
    }

    public static Quaternion RotationPhaseToRotation(int phase)
    {
        switch (phase)
        {
            case 1:
                return Quaternion.Euler(0, 0, -90);
            case 2:
                return Quaternion.Euler(0, 0, -180);
            case 3:
                return Quaternion.Euler(0, 0, -270);
            case 4:
                return Quaternion.Euler(0, 0, 0);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static Direction NextDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Right;
            case Direction.Right:
                return Direction.Down;
            case Direction.Down:
                return Direction.Left;
            case Direction.Left:
                return Direction.Up;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static Direction PreviousDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Left;
            case Direction.Right:
                return Direction.Up;
            case Direction.Down:
                return Direction.Right;
            case Direction.Left:
                return Direction.Down;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public static Vector2Int Vector2ToVector2Int(Vector2 original)
    {
        return new Vector2Int(Convert.ToInt32(original.x), Convert.ToInt32(original.y));
    }

    public static Vector2Int DirectionToVector2Int(Direction direction)
    {
        switch (direction)
        {
            case Direction.Down:
                return new Vector2Int(0, -1);
            case Direction.Up:
                return new Vector2Int(0, 1);
            case Direction.Right:
                return new Vector2Int(1, 0);
            case Direction.Left:
                return new Vector2Int(-1, 0);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Right,
    Left
}

public enum Color
{
    Color1,
    Color2
}

public enum Orientation
{
    Horizontal,
    Vertical
}

public enum VerticalDirection
{
    Up,
    Down
}