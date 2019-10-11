using System.Collections.Generic;
using UnityEngine;
using Static;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private int handLimit;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int amountOfCardsToDeal;
    
    [SerializeField] private List<Card> originalDeck = new List<Card>();
    [SerializeField] private Stack<Card> deck = new Stack<Card>();

    [SerializeField] private  HandArea handDeck;
    [SerializeField] private List<ScheduleArea> schedulePanels = new List<ScheduleArea>();
    
    public delegate void OnCardDropDelegate();
    public event OnCardDropDelegate CardDropEvent = delegate { };

    public delegate void OnCardReceivedCellDelegate();
    public event OnCardReceivedCellDelegate CardReceivedCellEvent = delegate { };

    
    private void OnEnable()
    {
        handDeck = GetComponentInChildren<HandArea>();
        
        foreach (var scheduleArea in GetComponentsInChildren<ScheduleArea>())
        {
            schedulePanels.Add(scheduleArea);
        }

        CardDropEvent += handDeck.DisableDraggable;
        CardReceivedCellEvent += handDeck.EnableDraggable;
    }

    public void InitDeck(List<Card> list)
    {
        originalDeck = list;
        ResetDeck();
    }

    void ResetDeck()
    {
        originalDeck.Shuffle();

        foreach (Card card in originalDeck)
        {
            deck.Push(card);
        }
    }

    public void DealCard()
    {
        for (int i = 0; i < amountOfCardsToDeal; i++)
        {
            if (deck.Count <= 0)
            {
                ResetDeck();
            }
            
            CardExecutor go = Instantiate(cardPrefab, handDeck.transform).GetComponent<CardExecutor>();
            go.Init(deck.Pop());
        }
    }

    public void EndTurn()
    {
        Debug.Log(handDeck.transform.childCount);
        if (handDeck.transform.childCount > handLimit) return;
        
        
        foreach (Transform card in schedulePanels[0].transform)
        {
            //TODO:: execute card
            Debug.Log(card.GetComponent<CardDisplayer>().Name);
            Destroy(card.gameObject);
        }
        
        //Move cards starting from 2nd schedule deck
        for (int i = 1; i < schedulePanels.Count; i++)
        {
            for (int j = schedulePanels[i].transform.childCount; j > 0; j--)
            {
                schedulePanels[i].transform.GetChild(j-1).SetParent(schedulePanels[i - 1].transform);
            }
        }

        Board.BoardInstance.gameManager.OnEndPlayerTurnEvent();
    }

    public void OnCardDropEvent()
    {
        //Call disable draggable on hand area
        CardDropEvent?.Invoke();
    }

    public void OnCardReceivedCellEvent()
    {
        //Call enable draggable on hand area
        CardReceivedCellEvent?.Invoke();
    }
}

