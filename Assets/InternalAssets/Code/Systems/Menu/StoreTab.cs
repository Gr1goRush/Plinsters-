using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreTab : MonoBehaviour
{
    public event Action<int> OnNewID;

    public string StoreKey;
    public TextMeshProUGUI objectNameText;
    private int currentSelectedID
    {
        get
        { return PlayerPrefs.GetInt(StoreKey); }
        set
        {
            PlayerPrefs.SetInt(StoreKey, value);
        }
    }

    [SerializeField] private StoreItemData[] Items;
    [SerializeField] private Image _itemImage;
    [SerializeField] private TextMeshProUGUI _itemCostText;


    [Space(10f)]
    [SerializeField] private Button _interactButton;
    [SerializeField] private Sprite _buttonLockedSprite;
    [SerializeField] private Sprite _buttonSelectSprite;
    [SerializeField] private Sprite _buttonSelectedSprite;
    private int _currentItemArrayID;

    public void ChooseNext()
    {
        _currentItemArrayID++;
        if (_currentItemArrayID >= Items.Length) _currentItemArrayID = 0;
        UpdateView();
    }

    public void ChoosePrevious()
    {
        _currentItemArrayID--;
        if (_currentItemArrayID < 0) _currentItemArrayID = Items.Length - 1;
        UpdateView();
    }

    public void UpdateView()
    {
        StoreItemData data = Items[_currentItemArrayID];
        _itemImage.sprite = data.ItemSprite;

        if (objectNameText != null)
        {
            objectNameText.text = data.StoreKey;
        }

        OnNewID?.Invoke(data.ItemGameID);

        _itemImage.transform.DOShakeScale(0.3f, 0.2f).onComplete = () =>
        {
            _itemImage.transform.DOScale(1, 0);
        };

        if (data.IsUnlocked)
        {
            if (currentSelectedID == _currentItemArrayID)
            {
                ClearButton();
                _interactButton.interactable = false;
                _interactButton.image.sprite = _buttonSelectedSprite;
            }
            else
            {
                ClearButton();
                _interactButton.image.sprite = _buttonSelectSprite;

                _interactButton.onClick.AddListener(Select);
            }
        }
        else
        {
            if (MoneyHelper.Money <= data.ItemCost)
            {
                ClearButton();
                _interactButton.image.sprite = _buttonLockedSprite;
                _interactButton.interactable = false;
                _itemCostText.gameObject.SetActive(true);
                _itemCostText.text = data.ItemCost.ToString();
            }
            else
            {
                ClearButton();
                _interactButton.image.sprite = _buttonLockedSprite;
                _itemCostText.gameObject.SetActive(true);
                _itemCostText.text = data.ItemCost.ToString();

                _interactButton.onClick.AddListener(Purchase);
            }
        }

    }


    public void ClearButton()
    {
        _interactButton.interactable = true;
        _itemCostText.gameObject.SetActive(false);
        _interactButton.onClick.RemoveAllListeners();
    }

    public void Select()
    {
        currentSelectedID = _currentItemArrayID;

        StoreItemData data = Items[_currentItemArrayID];

        if (data.Type == ItemType.Background)
        {
            GameplayBoot.BGToLoad = data.ItemGameID;
        }
        else
        {
            GameplayBoot.MusicToLoad = data.ItemGameID;
        }

        UpdateView();
    }

    public void Purchase()
    {
        StoreItemData data = Items[_currentItemArrayID];
        MoneyHelper.TryPurchase(data.ItemCost);
        data.Unlock();
        UpdateView();
    }

    public void SetData()
    {
       _currentItemArrayID = currentSelectedID;
        Select();
    }
}
