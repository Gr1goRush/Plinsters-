using UnityEngine;
using UnityEngine.Rendering;

public class GridObject : MonoBehaviour
{
    public GridObject[] _connectedGrids;

    private int _row;
    private int _col;

    public int Row => _row;
    public int Col => _col;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (_connectedGrids == null) return;
        foreach (var grid in _connectedGrids)
        {
            if (grid == null) continue;
            Gizmos.DrawLine(transform.position, grid.transform.position);
            Gizmos.DrawWireSphere(grid.transform.position, 0.05f);
        }
    }

    public GridObject Init(int row, int col)
    {
        _row = row;
        _col = col;

        return this;
    }
}
