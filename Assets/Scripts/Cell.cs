using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CellType
{
    UNKNOWN, EMPTY, CITY, CAPITAL, MOUNTAINS
}

public class Cell
{
    // ö�����Ͷ���
    public enum CellType { UNKNOWN, EMPTY, CITY, CAPITAL, MOUNTAINS }

    // �����ֶ�

    // ˽���ֶ�
    private CellType type_;
    private int owner_;
    private int armySize_;
    private bool isSelecting_ = false;

    // Ĭ�Ϲ��캯��
    public Cell()
    {
        this.type_ = CellType.UNKNOWN;
        this.owner_ = 0;
        this.armySize_ = 0;
    }

    // �������Ĺ��캯��
    public Cell(CellType type, int owner, int armySize)
    {
        this.type_ = type;
        this.owner_ = owner;
        this.armySize_ = armySize;
    }

    // ��������װ�ֶ�
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
