using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Action
{

    public Action(Vector3Int from, Vector3Int to, int owner, int mode)
    {
        from_ = from;
        to_ = to;
        owner_ = owner;
        mode_ = mode;
    }

    private readonly int owner_;
    private Vector3Int from_;
    private Vector3Int to_;
    private int mode_;

    public int Owner
    {
        get { return owner_; }
    }

    public void Move()
    {
        Cell fromCell = MapManager.Instance.GetCell(from_);
        Cell toCell = MapManager.Instance.GetCell(to_);
        int attackArmySize = mode_ == 1 ? fromCell.ArmySize - 1 : fromCell.ArmySize / 2;
        if (fromCell.Owner != owner_)
        {
            return;
        }
        else if (attackArmySize == 0)
        {
            return;
        }
        else if (toCell.Owner == owner_)
        {
            toCell.ArmySize += attackArmySize;
            fromCell.ArmySize -= attackArmySize;
        }
        else
        {
            if (CanCapture(attackArmySize, toCell.ArmySize))
            {
                toCell.Owner = owner_;
                toCell.ArmySize = attackArmySize - toCell.ArmySize;
                fromCell.ArmySize -= attackArmySize;
            }
            else
            {
                toCell.ArmySize -= attackArmySize;
                fromCell.ArmySize -= attackArmySize;
            }
        }
        
        MapManager.Instance.SetCell(from_.x, from_.y, fromCell);
        MapManager.Instance.SetCell(to_.x, to_.y, toCell);
    }

    private bool CanCapture(int attackArmySize, int defendArmySize)
    {
        return attackArmySize > defendArmySize;
    }

}
