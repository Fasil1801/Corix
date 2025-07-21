using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _Item;

    public Sprite HiddenItemSprite;
    public Sprite ItemSprite;

    private bool isSelected = false;
    CardManager manager;
    private void Start()
    {
        manager = CardManager.instance;
    }
    public void OnCardClick()
    {
        if (!isSelected)
        {
            manager.CardSelected(this);
        }
    }
    public void Show()
    {
        _Item.sprite = ItemSprite;
        isSelected = true;
    }

    public void Hide()
    {
        _Item.sprite = HiddenItemSprite;
        isSelected = false;
    }
}
