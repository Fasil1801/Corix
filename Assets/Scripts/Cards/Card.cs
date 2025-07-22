using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _Item;
    [SerializeField] float flipDuration = 0.3f;

    public Sprite HiddenItemSprite;
    public Sprite ItemSprite;

    private bool isSelected = false;
    private bool isAnimating = false;
    CardManager manager;
    private void Start()
    {
        manager = CardManager.instance;
        StartCoroutine(InitialShow());
    }
    public void OnCardClick()
    {
        if (!isSelected)
        {
            manager.CardSelected(this);
        }
    }
    public void SetItem(Sprite sprite)
    {
        ItemSprite = sprite;
    }
    public void Show()
    {
        if (isAnimating) return;
        
        StartCoroutine(FlipAnimation(ItemSprite));
        isSelected = true;
    }

    public void Hide()
    {
        StartCoroutine(FlipAnimation(HiddenItemSprite));
        isSelected = false;
    }
    IEnumerator InitialShow()
    {
        Show();
        yield return new WaitForSeconds(manager.InitialShowTime);
        Hide();
    }
    private IEnumerator FlipAnimation(Sprite item)
    {
        isAnimating = true;

        float time = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, isSelected ? 0 : 180, 0);
        while (time < flipDuration)
        {
            time += Time.deltaTime;
            float t = time / flipDuration;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
            yield return null;
        }
        _Item.sprite = item;
        isAnimating = false;
    }
}
