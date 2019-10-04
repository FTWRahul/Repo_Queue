using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Static;

public class DeckManager : MonoBehaviour
{
    public int amountOfCardsToDeal;
    
    public List<Card> originalDeck = new List<Card>();
    public Stack<Card> deck = new Stack<Card>();

    public Transform handPanel;
    public List<Transform> schedulePanels = new List<Transform>();
    
    public GameObject cardPrefab;
    
    
    private void Start()
    {
        handPanel = GetComponentInChildren<HandArea>().transform;
        
        foreach (var scheduleArea in GetComponentsInChildren<ScheduleArea>())
        {
            schedulePanels.Add(scheduleArea.transform);
        }
        
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

    [ContextMenu("Deal card")]
    void DealCard()
    {
        for (int i = 0; i < amountOfCardsToDeal; i++)
        {
            if (deck.Count <= 0)
            {
                ResetDeck();
            }

            CardDisplayer go = Instantiate(cardPrefab, handPanel).GetComponent<CardDisplayer>();
            go.Init(deck.Pop());
        }
    }

    public void SetDeck(List<Card> cards)
    {
        originalDeck = cards;
    }

    [ContextMenu("End turn")]
    void EndTurn()
    {
        foreach (Transform card in schedulePanels[0])
        {
            //TODO:: call card execution  
            Debug.Log(card.GetComponent<CardDisplayer>().Name + " was played");
            Destroy(card.gameObject);
        }

        //Move cards starting from 2nd schedule deck
        for (int i = 1; i < schedulePanels.Count; i++)
        {
            for (int j = schedulePanels[i].childCount; j > 0; j--)
            {
                schedulePanels[i].GetChild(j-1).SetParent(schedulePanels[i - 1]);
            }
        }
    }
}

