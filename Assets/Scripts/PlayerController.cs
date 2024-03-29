using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{

    private Cursor cursor;

    void Start()
    {
        cursor = new Cursor();
    }

    // Update is called once per frame
    void Update()
    {
        SelectCell();
        Move();
    }

    public void SelectCell()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = MapManager.Instance.GetCellPosWithWorldPos(worldPos);
            Debug.Log(MapManager.Instance.GetCellArmy(cellPos.x, cellPos.y));
            if (!MapManager.Instance.IsPositionInMap(cellPos))
            {
                return;
            }
            else if (cursor.Mode == 0)
            {
                cursor.SingleClick(cellPos);
            }
            else if (cursor.Mode == 1 && cursor.GetPosition() == cellPos)
            {
                cursor.DoubleClick();
            }
            else if (cursor.Mode == 1 && cursor.GetPosition() != cellPos)
            {
                cursor.SingleClick(cellPos);
            }
            else if (cursor.Mode == 2 && cursor.GetPosition() == cellPos)
            {
                CancelSelection();
            }
        }
    } // 选中格子

    public void CancelSelection()
    {
        cursor.Hide();
    } // 取消选中

    public void Move()
    {
        Vector3Int toPos = cursor.GetPosition();
        if (Input.GetKeyDown(KeyCode.W))
        {
            toPos.y += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            toPos.y -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            toPos.x -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            toPos.x += 1;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelSelection();
            return;
        } // 如果按下空格键，取消选中格子（隐藏光标）（Cancel selection (hide cursor) if space key is pressed）
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.PlayerClearActions();
            CancelSelection();
            return;
        }
        else
        {
            return;
        }
        if (cursor.Mode == 0 || !MapManager.Instance.IsPositionInMap(toPos))
        {
            CancelSelection();
            return;
        }
        Cell toCell = MapManager.Instance.GetCell(toPos);
        GameManager.Instance.PlayerAddAction(new Action(cursor.GetPosition(), toPos, GameManager.Instance.GetPlayerID(), cursor.Mode));
        cursor.SingleClick(toPos);
    } // 移动光标

}
