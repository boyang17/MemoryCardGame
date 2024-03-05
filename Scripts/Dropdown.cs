using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public static int row = 2;
    public static int col = 3;
    public void GridSize(int index)
    {
        switch (index)
        {
            case 0:
                row = 2;
                col = 3;
                break;
            case 1:
                row = 2;
                col = 4;
                break;
            case 2:
                row = 2;
                col = 5;
                break;
            case 3:
                row = 3;
                col = 4;
                break;
            case 4:
                row = 4;
                col = 4;
                break;
            case 5:
                row = 4;
                col = 5;
                break;
        }
        Debug.Log(row);
        Debug.Log(col);
    }
}
