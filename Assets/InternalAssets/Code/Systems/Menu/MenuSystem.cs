using DG.Tweening;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    private static int _preloadTab = 0;

    [SerializeField] private GameObject[] _menuTabs;

    private GameObject _currentTab = null;

    public GameObject[] MenuTabs => _menuTabs;

    private void Start()
    {
        _preloadTab = Mathf.Clamp(_preloadTab, 0, _menuTabs.Length);
        ForceOpenTab(_preloadTab);
        _preloadTab = 0;
    }

    public void ForceOpenTab(int TabID)
    {
        _currentTab = _menuTabs[TabID];
        foreach (var tab in _menuTabs)
        {
            tab.gameObject.SetActive(false);
        }

        _menuTabs[TabID].gameObject.SetActive(true);
    }

    public void OpenTab(int TabID)
    {
        float transitionDuration = 0.1f;

        Transform firstTab = _currentTab.transform;
        Transform secondTab = _menuTabs[TabID].transform;

        firstTab.transform.DOShakeScale(transitionDuration, 0.3f).
            onComplete = () =>
            {
                firstTab.gameObject.SetActive(false);
                firstTab.transform.DOScale(Vector3.one, 0);
                secondTab.gameObject.SetActive(true);
                secondTab.DOShakeScale(transitionDuration, 0.3f).
                onComplete = () =>
                {
                    firstTab.transform.DOScale(Vector3.one, 0.3f);
                    _currentTab = _menuTabs[TabID];
                };

            };

    }

    public static void SetPreloadTab(int TabID)
    {
        _preloadTab = TabID;
    }
}
