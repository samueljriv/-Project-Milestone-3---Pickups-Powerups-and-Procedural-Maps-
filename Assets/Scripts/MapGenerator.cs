using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    public bool isMapOfTheDay;
    private int mapSeed;
    private Room[,] grid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    void Start()
    {
        if (isMapOfTheDay == true)
        {
            mapSeed = DateToInt(DateTime.Now.Date);
        }
        else
        {
            mapSeed = DateToInt(DateTime.Now);
        }
        UnityEngine.Random.InitState(mapSeed);
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        grid = new Room[cols, rows];

        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                tempRoomObj.transform.parent = this.transform;

                tempRoomObj.name = "Room_" + currentCol + "," + currentRow;
                Room tempRoom = tempRoomObj.GetComponent<Room>();
                if (currentRow == 0)
                {
                    Destroy(tempRoom.doorNorth);
                }
                else if (currentRow == rows - 1)
                {
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }
                if (currentCol == 0)
                {
                    Destroy(tempRoom.doorEast);

                }
                else if (currentCol == cols - 1)
                {
                    Destroy(tempRoom.doorWest);
                }
                else
                {
                    Destroy(tempRoom.doorEast);
                    Destroy(tempRoom.doorWest);
                }

                grid[currentCol, currentRow] = tempRoom;
            }
        }
    }
}
