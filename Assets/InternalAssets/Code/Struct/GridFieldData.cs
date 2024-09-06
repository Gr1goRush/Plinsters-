using System;
using UnityEngine;

[Serializable]
public struct GridFieldData
{
    public Vector3 Position;

    public int Row;
    public int Column;

    public GridFieldData(Vector3 position, int row, int column)
    {
        Position = position;
        Row = row;
        Column = column;
    }
}
