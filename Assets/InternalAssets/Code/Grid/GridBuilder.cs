using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private Vector3 _startPoint;

    [SerializeField, Min(1)] private int _startRowsCount;
    [SerializeField, Min(1)] private int _columns;

    [SerializeField, Min(0.1f)] private float _spacingHeight;
    [SerializeField, Min(0.1f)] private float _spacingWidth;

    [SerializeField] private float _gridOffset;

    public float GridGizmoSize;

    [SerializeField, HideInInspector] private List<GridFieldData> _grid;

    public List<GridFieldData> Grid => _grid;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (_grid != null)
        {
            foreach (var grid in _grid)
            {
                Gizmos.DrawSphere(grid.Position, GridGizmoSize);
            }
        }
    }

    private void OnValidate()
    {
        if (_grid != null) BuildGrid();
    }

    [ContextMenu("Build Grid")]
    public void BuildGrid()
    {
        _grid.Clear();
        Vector3 currentPoint = _startPoint;
        int rowsCount = _startRowsCount;
        for (int column = 0; column < _columns; column++)
        {
            for (int row = 0; row < rowsCount; row++)
            {
                _grid.Add(new GridFieldData(currentPoint, row, column));
                currentPoint.x += _spacingWidth;
            }
            currentPoint = _startPoint;
            currentPoint.x -= _spacingWidth / _gridOffset * column;
            currentPoint.y -= _spacingHeight * column;

            rowsCount++;
        }
    }

}
