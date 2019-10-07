using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Static;
using UnityEngine.Video;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private int amountOfCardsToDeal;
    
    [SerializeField] private List<Card> originalDeck = new List<Card>();
    [SerializeField] private Stack<Card> deck = new Stack<Card>();

    [SerializeField] private Transform handPanel;
    [SerializeField] private List<Transform> schedulePanels = new List<Transform>();

    //TODO:: do we need to store player ref for deck manager^^
    private Player _player;
    
    
    //TODO:: start has null  error
    private void OnEnable()
    {
        _player = GetComponent<Player>();
        _player.OnMakePlayerEvent += InitDeck;
        
        handPanel = GetComponentInChildren<HandArea>().transform;
        
        foreach (var scheduleArea in GetComponentsInChildren<ScheduleArea>())
        {
            schedulePanels.Add(scheduleArea.transform);
        }
    }

    void InitDeck()
    {
        originalDeck = _player.originalDeck;
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
    public void DealCard()
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

