using UnityEngine;
using System.Collections;
[System.Serializable]
//=================
// ROOM - Class used to generate rooms for dungeon
//=================
public class Room  
{
	//================
	// VARIABLES
	//================
   private GameObject floor;                       // Floor Game Object
   private int xTiles;                            // Number of rows
   private int yTiles;                            // Number of columns
	private float x1,x2,y1,y2;                     // Position of corners
	private float width;                           // Width of room
	private float height;                          // Height of room   
   private int tileWidth;                         // Width of tile
   private int tileHeight;                        // Height of tile
	private Vector2 center;                        // Center point of room	
	private GameObject room;                       // Room's Game Object
	//=================
	// SET VALUES
	//=================
    // Sets the x and y position of the room and initializes other variables
	public Room(float _x, float _y, int _rows, int _cols, int _tWidth, int _tHeight, GameObject _floor)
	{
        //---------------
        // Obtain Tile values
        //---------------
        this.tileWidth = _tWidth;
        this.tileHeight = _tHeight;
        //---------------
        // Assign number of rows and columns
        //--------------
        this.xTiles = _rows;
        this.yTiles = _cols;
        //---------------
        // Assign room's with and height
        //---------------
        this.width = tileWidth * xTiles;
        this.height = tileHeight * yTiles;
        //--------------
        // Assign room's corners and center
        //--------------
		this.x1 = _x;
		this.x2 = _x + this.width;
		this.y1 = _y;
		this.y2 = _y + this.height;
		this.center = new Vector2(Mathf.Floor ((x1+x2)/2), Mathf.Floor((y1+y1)/2));
        //--------------
        // Assign room's floor 
        //--------------
        this.floor = _floor;
	}
	//==================
	// INTERSECTS
	//==================
	// Returns true if this room collides with given room
	public bool intersects(Room room)
	{
		return (x1 <= room.x2 && x2 >= room.x1 && y1 <= room.y2 && room.y2 >= room.y1);
	}
    //==================
    // GENERATE ROOM
    //==================
    public void generateRoom()
    {
        // Create room game object
        room = new GameObject("Room");
        // Instantiate floor tiles for room
        for (int i = 0; i < xTiles; i++)
        {
            for (int j = 0; j < yTiles; j++)
            {
                // Instantaite tile
                GameObject tile = (GameObject)GameObject.Instantiate(floor, new Vector3(this.x1 + i * tileWidth, this.y1 + j * tileHeight, 0), Quaternion.identity);
                // assign room object as parent of tile
                tile.transform.parent = room.transform;
            }// yTiles
        }// xTiles
    }
	//==================
	// GET ROOM
	//==================
	// Returns rooms GameObject
	public GameObject getRoom()
	{
		return room;
	}
    //==================
    // SET PARENT
    //==================
    // Parents this room gameObject to given transform
    public void setParent(Transform _p)
    {
        room.transform.parent = _p;
    }
}
