using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private int _health;
    public int Health => _health;
    
    private List<PatternData> _movementPatterns;
    public List<PatternData> MovementPatterns => _movementPatterns;
    public List<Card> originalDeck;
    
    private Renderer _renderer;
    public GameObject canvas;
    
    public delegate void OnMakePlayerDelegate();
    public event OnMakePlayerDelegate OnMakePlayerEvent = delegate { };

    public void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void MakePlayer(PlayerData playerData)
    {
        _health = playerData.health;
        _movementPatterns = playerData.movementPatterns;

        foreach (CardData cardData in playerData.originalDeck)
        {
            originalDeck.Add(new Card(cardData));
        }

        _renderer.material.color = playerData.color;
        OnMakePlayerEvent();
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
    }
    
    /// <summary>
    /// Changes the reference to its on the board and moves itself
    /// </summary>
    /// <param name="cellPos"></param>
    /// 
    public void Move(Vector2Int cellPos)
    {
        //Changing refrence on board
        Board.boardInstance.ChangePlayerPos(gameObject, cellPos);
        //Physically lerping the player
        transform.DOMove(Board.boardInstance.CellLayer[cellPos.x, cellPos.y].transform.position + Vector3.up, 2f).SetEase(Ease.OutQuint);
    }
    
    public void HighlightMovementCells()
    {
        Vector2Int thisPosition = Board.boardInstance.GetPlayerPosition(gameObject);
        
        foreach (PatternData pattern in _movementPatterns)
        {
            foreach (Vector2Int pos in pattern.positions)
            {
                Vector2Int resultingPos = thisPosition + pos;
                
                if (resultingPos.x < 0 || resultingPos.x > Board.boardInstance.Width - 1 || resultingPos.y < 0 || resultingPos.y > Board.boardInstance.Height - 1) // outside of the board
                {
                    break;
                }

                if (Board.boardInstance.PlayerLayer[resultingPos.x, resultingPos.y] != null) // player is on cell
                {
                    break;
                }
                
                Board.boardInstance.CellLayer[resultingPos.x, resultingPos.y].Highlight();
            }
        }
    }

    //TODO:: deal card when turn starts
    /*public void StartTurn()
    {
        _deckManager.DealCard();
    }*/
}
