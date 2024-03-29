using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellType
{
    UNKNOWN, EMPTY, CITY, CAPITAL, MOUNTAINS
}

public class Cell
{
    // 枚举类型定义
    public enum CellType { UNKNOWN, EMPTY, CITY, CAPITAL, MOUNTAINS }

    // 公有字段

    // 私有字段
    private CellType type_;
    private int owner_;
    private int armySize_;
    private bool isSelecting_ = false;

    // 默认构造函数
    public Cell()
    {
        this.type_ = CellType.UNKNOWN;
        this.owner_ = 0;
        this.armySize_ = 0;
    }

    // 带参数的构造函数
    public Cell(CellType type, int owner, int armySize)
    {
        this.type_ = type;
        this.owner_ = owner;
        this.armySize_ = armySize;
    }

    // 属性来封装字段
    public CellType Type
    {
        get { return type_; }
        set { type_ = value; }
    }

    public int Owner
    {
        get { return owner_; }
        set { owner_ = value; }
    }

    public int ArmySize
    {
        get { return armySize_; }
        set { armySize_ = value; }
    }

    public bool IsSelecting
    {
        get { return isSelecting_; }
        set { isSelecting_ = value; }
    }
}
