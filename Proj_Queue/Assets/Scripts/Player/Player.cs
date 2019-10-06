using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private int _health;
    public int Health => _health;
   // public List<Pattern> movementPatterns;
    public List<PatternData> movementPatterns;

    public List<Card> originalDeck;

    public Renderer rend;
    public void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void MakePlayer(PlayerData playerData)
    {
        _health = playerData.health;
        movementPatterns = playerData.movementPatterns;

        foreach (CardData cardData in playerData.originalDeck)
        {
            originalDeck.Add(new Card(cardData));
        }

        rend.material.color = playerData.color;
        
        
        //Some shit about adding player deck to the deck manager :(
        GetComponent<DeckManager>().SetDeck(originalDeck);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
    }
    
    /// <summary>
    /// Changes the reference to its on the board and moves itself
    /// </summary>
    /// <param name="cellPos"></param>
    public void Move(Vector2Int cellPos)
    {
        //Changing refrence on board
        Board.boardInstance.ChangePlayerPos(gameObject, cellPos);
        //Physically lerping the player
        transform.DOMove(Board.boardInstance.CellLayer[cellPos.x, cellPos.y].transform.position + Vector3.up, 2f).SetEase(Ease.OutQuint);
    }
}
