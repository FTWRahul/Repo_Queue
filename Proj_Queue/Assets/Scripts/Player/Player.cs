﻿using System;
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
}
