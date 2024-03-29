using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

    private int playerID_ = 1;
    private string playerName_;
    private int armySize_;
    private Vector3Int capitalPos_;
    private List<Action> actions = new List<Action>();

    public Player(int playerID, string playerName, Vector3Int capitalPos)
    {
        this.playerID_ = playerID;
        this.playerName_ = playerName;
        this.capitalPos_ = capitalPos;
    }

    public int PlayerID
    {
        get { return playerID_; }
        set { playerID_ = value; }
    }

    public string PlayerName
    {
        get { return playerName_; }
        set { playerName_ = value; }
    }

    public int ArmySize
    {
        get { return armySize_; }
        set { armySize_ = value; }
    }

    public Vector3Int CapitalPos
    {
        get { return capitalPos_; }
        set { capitalPos_ = value; }
    }

    public int GetActionCount()
    {
        return actions.Count;
    }

    public void AddAction(Action action)
    {
        actions.Add(action);
    }

    public void ExecuteAction()
    {
        if (actions.Count == 0)
        {
            return;
        }
        actions[0].Move();
        actions.RemoveAt(0);
    }

    public void ClearActions()
    {
        actions.Clear();
    }


}
