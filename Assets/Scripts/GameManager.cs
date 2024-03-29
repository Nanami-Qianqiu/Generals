using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] private float gameSpeed = 1.0f;

    private Player player;
    private int timer = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = new Player(1, "Player1", new Vector3Int(0, 0, 0));
        MapManager.Instance.SetCell(0, 0, new Cell(Cell.CellType.CAPITAL, player.PlayerID, 10));
        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            timer++;
            GameTurn();
            ArmyDisplay.Instance.DrawArmy();
            yield return new WaitForSeconds(gameSpeed);
            ArmyDisplay.Instance.ClearArmy();
        }
    }

    public void PlayerAddAction(Action action)
    {
        player.AddAction(action);
    }

    public void PlayerClearActions()
    {
        player.ClearActions();
    }

    public int GetPlayerID()
    {
        return player.PlayerID;
    }

    public void GameTurn()
    {
        player.ExecuteAction();
        OneTurn();
    }

    public void OneTurn()
    {
        for (int x = 0; x < MapManager.Instance.GetWidth(); x++)
        {
            for (int y = 0; y < MapManager.Instance.GetHeight(); y++)
            {
                Cell cell = MapManager.Instance.GetCell(new Vector3Int(x, y));
                if (cell.Owner != 0 && (cell.Type == Cell.CellType.CAPITAL || cell.Type == Cell.CellType.CITY))
                {
                    cell.ArmySize += 1;
                    MapManager.Instance.SetCell(x, y, cell);
                }
            }
        }
    }

}
