using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    public UnityEvent OnBossLost;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    private void OnEnable()
    {
        BossController.OnBossSpawned += InitView;
        BossController.OnHealthChanged += UpdateView;
    }

    private void OnDisable()
    {
        BossController.OnBossSpawned -= InitView;
        BossController.OnHealthChanged -= UpdateView;
    }

    public void InitView(int value)
    {
        healthSlider.maxValue = value;
        healthSlider.value = value;
        healthText.text = value.ToString();
    }

    public void UpdateView(int value)
    {
        healthSlider.value = value;
        healthText.text = value.ToString();

        if (value == 0)
        {
            OnBossLost?.Invoke();
        }
    }
}
