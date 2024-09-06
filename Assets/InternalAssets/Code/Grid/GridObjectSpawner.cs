using UnityEngine;

[RequireComponent(typeof(GridBuilder))]
public class GridObjectSpawner : MonoBehaviour
{
    [SerializeField, HideInInspector] private GridBuilder _gridBuilder;
    [SerializeField] private GridObject _gridObject;



    private void OnValidate()
    {
        _gridBuilder ??= GetComponent<GridBuilder>();
    }

    [ContextMenu("Spawn")]
    public void FillGrid()
    {


        foreach (var data in _gridBuilder.Grid)
        {
            Instantiate(_gridObject, data.Position, Quaternion.identity).Init(data.Row, data.Column);
        }
    }

}
