using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _Item;

    public Sprite HiddenItemSprite;
    public Sprite ItemSprite;

    private bool isSelected = false;
    
    public void OnCardClick()
    {
        if (!isSelected)
        {
            Show();
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
