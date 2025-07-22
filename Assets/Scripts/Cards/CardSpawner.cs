using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    [Header("CardSettings")]
    [SerializeField] Card CardPrefab;
    [SerializeField] Transform Container;
    [SerializeField] Sprite[] ItemSprites;

    [Header("GridSettings")]
    [SerializeField] int column;
    [SerializeField] int row;
    [SerializeField] GridLayoutGroup gridLayout;

    private List<Sprite> CardPairs;
    void Start()
    {
        GeneratePairs();
        SpawnCards();
        StartCoroutine(SetGrid());
    }
    void GeneratePairs()
    {
        CardPairs = new List<Sprite>();
        for (int i = 0; i < (column * row)/2; i++)
        {
            CardPairs.Add(ItemSprites[i]);
            CardPairs.Add(ItemSprites[i]);
        }
        Shuffle();
    }
    
    void SpawnCards()
    {
        for (int i = 0;i < CardPairs.Count;i++)
        {
            Card card = Instantiate(CardPrefab,Container);
            card.SetItem(CardPairs[i]);
        }
    }

    void Shuffle()
    {
        for (int i = CardPairs.Count -1 ; i > 0; i--)
        {
            int random = Random.Range(0,i + 1);
            (CardPairs[i], CardPairs[random]) = (CardPairs[random], CardPairs[i]);
        }
    }
    IEnumerator SetGrid()
    {
        gridLayout.constraintCount = column;
        yield return new WaitForSeconds(0.3f);
        gridLayout.enabled = false;
    }
}
