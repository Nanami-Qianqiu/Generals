using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor
{
    private Vector3Int cursorPosition_;
    private int mode_;

    public Cursor()
    {
        cursorPosition_ = new Vector3Int(0, 0, 0);
        mode_ = 0;
    }


    public int Mode
    {
        get { return mode_; }
        set { mode_ = value; }
    }


    public void SingleClick(Vector3Int position)
    {
        Hide();
        cursorPosition_ = position;
        mode_ = 1;
        Show();
    }

    public Vector3Int GetPosition()
    {
        return cursorPosition_;
    }

    public void Hide()
    {
        mode_ = 0;
        MapManager.Instance.SetCellIsSelecting(cursorPosition_.x, cursorPosition_.y, false);
        MapManager.Instance.ClearCursor();
    }

    public void Show()
    {
        MapManager.Instance.SetCellIsSelecting(cursorPosition_.x, cursorPosition_.y, true);
        MapManager.Instance.DrawCursor(cursorPosition_, mode_);
    }

    public void DoubleClick()
    {
        mode_ = 2;
        Show();
    }

}
