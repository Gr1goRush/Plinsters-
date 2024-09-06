using UnityEngine;
using UnityEngine.Events;

public class BossTutorial : MonoBehaviour
{
    public UnityEvent OnTutorialComplete;
    private int counter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        counter++;

        if (counter == 3)
        {
            PlayerPrefs.SetString("Tutorial", "Complete");
            OnTutorialComplete?.Invoke();
        }
    }
}
