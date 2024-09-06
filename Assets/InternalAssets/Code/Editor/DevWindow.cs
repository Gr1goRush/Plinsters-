#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevWindow : EditorWindow
{
    private MenuSystem menuSystem;

    [MenuItem("Window/Developer Window")]
    public static void ShowWindow()
    {
        GetWindow<DevWindow>("Developer Window");
    }

    private void OnGUI()
    {
        DrawCheatWindow();
        DrawMenuButtons();
    }

    public void DrawMenuButtons()
    {
        if (FindObjectOfType<MenuSystem>() == null) return;

        menuSystem = FindObjectOfType<MenuSystem>();
        Print("Menu Buttons");

        for (int i = 0; i < menuSystem.MenuTabs.Length; i++)
        {
            if (GUILayout.Button(menuSystem.MenuTabs[i].gameObject.name))
            {
                menuSystem.ForceOpenTab(i);
            }
        }

    }

    public void DrawCheatWindow()
    {
        Print("Menu Cheats");

        if (GUILayout.Button("Add 100 money"))
        {
            MoneyHelper.AddMoney(100);
        }
        if (GUILayout.Button("Remove 100 money"))
        {
            MoneyHelper.RemoveMoney(100);
        }
        if (GUILayout.Button("Clear save"))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void Print(string text)
    {
        GUILayout.Label(text);
    }
}
#endif