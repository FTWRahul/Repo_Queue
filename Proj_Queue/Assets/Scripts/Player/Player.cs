using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _health;
    public int Health => _health;
    public List<Pattern> movementPatterns;

    public List<Card> originalDeck;
    
    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    public void MakePlayer(PlayerData playerData)
    {
        _health = playerData.health;
        
        foreach (PatternData patternData in playerData.movementPatterns)
        {
            movementPatterns.Add(new Pattern(patternData));
        }

        foreach (CardData cardData in playerData.originalDeck)
        {
            originalDeck.Add(new Card(cardData));
        }
        
        var block = new MaterialPropertyBlock();
        block.SetColor(BaseColor, playerData.color);
        GetComponent<Renderer>().SetPropertyBlock(block);
        
        
        //Some shit about adding player deck to the deck manager :(
        GetComponent<DeckManager>().SetDeck(originalDeck);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
    }
    
    public void Move(Vector2Int cellPos)
    {
        Board.boardInstance.ChangePlayerPos(gameObject, cellPos);
        StartCoroutine(LerpPlayer(cellPos));
        //transform.position = Board.boardInstance.CellLayer[cellPos.x, cellPos.y].transform.position;
        Board.boardInstance.DehighlightCells();
        //board.DehighlightCells();
        
        //Turn(currentPlayer);
    }

    IEnumerator LerpPlayer(Vector2Int cellPos)
    {
        float t = 0;
        while (t < 3)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position,
                Board.boardInstance.CellLayer[cellPos.x, cellPos.y].transform.position + Vector3.up, t);
            //transform.position += Vector3.up * 1;
            yield return new WaitForEndOfFrame();
        }
        //transform.position += Vector3.up * 5;
    }
}
