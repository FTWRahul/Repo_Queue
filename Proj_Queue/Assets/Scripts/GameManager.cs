using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Board board;
    public BoardData boardData;

    public GameObject playerPrefab;
    public PlayerData playerData;

    void Start()
    {
        board.MakeBoard(boardData);

        Player player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();

        player.MakePlayer(playerData);
        
        board.PlacePlayer(player.gameObject, new Vector2Int(2, 0));
    }
}
