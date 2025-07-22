using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance { get; private set; }
    public float InitialShowTime = 2f;

    [SerializeField]private int MaxTurn = 5;
    Card firstCard, secondCard;
    private bool isChecking = false;
    UiManager uiManager;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        uiManager = UiManager.instance;
        uiManager.SetMaxTurn(MaxTurn);
    }
    public void CardSelected(Card card)
    {
        if (isChecking) return;
        card.Show();
        if (firstCard == null)
        {
            firstCard = card;
        }
        else if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CheckMatches(firstCard,secondCard));
        }
    }

    private IEnumerator CheckMatches(Card fist , Card second)
    {
        isChecking = true;
        yield return new WaitForSeconds(0.6f);

        if (firstCard.ItemSprite == secondCard.ItemSprite)
        {
            Destroy(fist.gameObject);
            Destroy(second.gameObject);
            uiManager.SetMatch();
        }
        else
        {
            fist.Hide();
            second.Hide();
            uiManager.SetTurn();
        }

        firstCard = secondCard = null;
        isChecking = false;
    }
}
