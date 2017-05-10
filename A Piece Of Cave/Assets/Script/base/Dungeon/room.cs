using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class room{

    public int x; //中心点x
    public int y;//中心点y
    public int width = 1;//总宽度的一半
    public int height = 1;

    public room()
    {

    }
    public room(int _x,int _y,int _width,int _height)
    {
        x = _x;
        y = _y;
        width = _width;
        height = _height;
    }
}
