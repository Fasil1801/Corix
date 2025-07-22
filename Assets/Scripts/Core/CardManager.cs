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
    AudioManager audioManager;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        uiManager = UiManager.instance;
        audioManager = AudioManager.instance;
        uiManager.SetMaxTurn(MaxTurn);
    }
    public void CardSelected(Card card)
    {
        if (isChecking) return;
        card.Show();
        audioManager.PlayFlip();
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
            audioManager.PlayMatch();
            Destroy(fist.gameObject);
            Destroy(second.gameObject);
            uiManager.SetMatch();
        }
        else
        {
            audioManager.PlayMisMatch();
            fist.Hide();
            second.Hide();
            uiManager.SetTurn();
        }

        firstCard = secondCard = null;
        isChecking = false;
    }
}
