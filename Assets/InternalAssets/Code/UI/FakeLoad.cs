using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FakeLoad : MonoBehaviour
{
    public Slider slider;


    private void Start()
    {
        slider.DOValue(99, 2f)
            .SetEase(Ease.Linear)
            .onComplete = () =>
            {
                SceneHelper.LoadScene(ProjectScene.Menu);
            };
    }
}
