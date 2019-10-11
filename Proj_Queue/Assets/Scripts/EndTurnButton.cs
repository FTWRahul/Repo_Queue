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
        _gameManager.OnPressEndTurnButtonEvent();
    }
}
