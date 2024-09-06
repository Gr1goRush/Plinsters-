using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;

    private void OnEnable()
    {
        MoneyHelper.OnMoneyValueChanged += UpdateView;
    }

    private void OnDisable()
    {
        MoneyHelper.OnMoneyValueChanged -= UpdateView;
    }

    private void Start()
    {
        _moneyText.text = MoneyHelper.Money.ToString();
    }

    private void UpdateView(int value)
    {
        _moneyText.text = value.ToString();
    }
}
