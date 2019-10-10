using UnityEngine;

public class CardExecutor : MonoBehaviour
{
    public Card CardInfo { get; private set; }
    [SerializeField] private CardDisplayer cardDisplayer;
    

    public void Init(Card card)
    {
        Debug.Log("Init card");
        CardInfo = card;
        cardDisplayer.Init(CardInfo);
    }

    public void ReceiveSelectedCell(Vector2Int cellPos)
    {
        foreach (var action in CardInfo.actions)
        {
            action.originCell = cellPos;
        }

        Board.boardInstance.gameManager.ReceiveSelectedCellEvent -= ReceiveSelectedCell;
    }
    
    public void DisplayAction()
    {
        foreach (var action in CardInfo.actions)
        {
            action.DisplayPossiblePattern(Board.boardInstance.GetPlayerPosition(Board.boardInstance.CurrentPlayer));
        }
    }
}