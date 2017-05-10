using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreator : MonoBehaviour {
    public float cellsize;
    public float width;
    public float height;
    public int maxRoomSize;
    public int minRoomSize;
    public int maxCorridorSize; //最大走廊长度
    public int DungeonTimes;
    public char[,] map;
    public List<Rect> rooms;
    public GameObject cell;
    public Sprite floor;
    public Sprite wall;
    public Sprite door;
    public Texture guiroom;
    // Use this for initialization
    void Start () {
        Camera.main.transform.position = new Vector3(width / 2, height / 2)*cellsize;
        InitializeDungeon((int)width,(int)height);

        GenerateRoom(new room((int)width/2, (int)height/2, 3,3));
        rooms.Add(new Rect(width/2-3-1,height/2-3-1,2*3+1,2*3+1));
        while(DungeonTimes--!=0)
        {
            Dungeon(rooms[Random.Range(0,rooms.Count-1)]);
        }
        loadingDungeon(map);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void InitializeDungeon(int sizex, int sizey)
    {
        this.width = sizex;
        this.height = sizey;
        map = new char[sizex,sizey];
    }
    void Dungeon(Rect nowRoom)
    {
        int dir = Random.Range(0, 4);
        int corridor = Random.Range(0, maxCorridorSize);
        Rect randomRect = new Rect();
        randomRect.width = (int)Random.Range(minRoomSize, maxRoomSize)*2+1;
        randomRect.height = (int)Random.Range(minRoomSize, maxRoomSize)*2+1;
        switch (dir)
        {
            case 0://上
                randomRect.x = Random.Range(nowRoom.x + 1, nowRoom.x + width - 1);
                randomRect.y = nowRoom.yMax+corridor;
                break;
            case 1://下
                randomRect.x = Random.Range(nowRoom.x + 1, nowRoom.x + width - 1);
                randomRect.y = nowRoom.yMin - corridor - randomRect.height;
                break;
            case 2://右
                randomRect.y = Random.Range(nowRoom.y + 1, nowRoom.y + height - 1);
                randomRect.x = nowRoom.xMax + corridor;
                break;
            case 3://左
                randomRect.y = Random.Range(nowRoom.y + 1, nowRoom.y + height - 1);
                randomRect.x = nowRoom.xMin - corridor - randomRect.width;
                break;
            default:
                break;
        }
        if(canCreatRoom(randomRect)&&randomRect.xMin>=0&& randomRect.xMax <= width&&randomRect.yMin>=0&& randomRect.yMax<=height)
        {
            rooms.Add(randomRect);
            Debug.Log(dir.ToString() + ' ' + randomRect);
        }

    }
    void GenerateRoom(room room)
    {
        for (int i = room.x-room.width; i <= room.x+room.width; i++)
        {
            for (int j = room.y-room.height; j <= room.y+room.height; j++)
            {
                if(i == room.x - room.width|| i == room.x + room.width||j == room.y - room.height|| j == room.y + room.height)
                {
                    map[i, j] = (char)spriteType.wall;
                }
                else
                {
                    map[i, j] = (char)spriteType.floor;
                }
            }
        }
    }
    bool canCreatRoom(Rect room)
    {
        foreach (var _room in rooms)
        {
            if (_room.Overlaps(room))
            {
                return false;
            }
        }
        return true;
    }
    room randomRoom()
    {
        room room = new room();
        room.width = Random.Range(minRoomSize, maxRoomSize);
        room.height = Random.Range(minRoomSize, maxRoomSize);
        return room;
    }


    void loadingDungeon(char[,] map)
    {
        Sprite sp; 
        for(int i = 0;i<width; i++)
        {
            for(int j = 0;j<height;j++)
            {
                switch (map[i,j])
                {
                    case (char)spriteType.floor:
                        sp = floor;
                        break;
                    case (char)spriteType.wall:
                        sp = wall;
                        break;
                    case (char)spriteType.door:
                        sp = door;
                        break;
                    default:
                        continue;
                }
                GameObject _cell = Instantiate(cell, new Vector2(i, j) * cellsize, Quaternion.identity);
                _cell.GetComponent<SpriteRenderer>().sprite = sp;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(new Vector3(width/2,height/2),new Vector3(width,height));
        foreach (var rect in rooms)
        {
            Gizmos.DrawWireCube(rect.center,rect.size);
        }
        
    }
}
