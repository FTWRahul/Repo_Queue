using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void EndTurn()
    {
        _gameManager.EndPlayerTurn();
    }
}
