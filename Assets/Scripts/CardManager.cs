using System.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance { get; private set;}
    public float InitialShowTime = 2f;

    Card firstCard, secondCard;

    private void Awake()
    {
        instance = this;
    }
    public void CardSelected(Card card)
    {
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
        yield return new WaitForSeconds(0.3f);

        if (firstCard.ItemSprite == secondCard.ItemSprite)
        {
            Destroy(fist.gameObject);
            Destroy(second.gameObject);
        }
        else
        {
            fist.Hide();
            second.Hide();
        }

        firstCard = secondCard = null;
    }
}
