using UnityEngine;

public class StoreLoader : MonoBehaviour
{
    public StoreTab[] tabs;


    private void Start()
    {
        foreach (var tab in tabs)
        {
            tab.SetData();
        }
    }
}
