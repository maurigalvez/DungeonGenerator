using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//===================
// ROOM GENERATOR
//===================
[System.Serializable]
public class RoomGenerator : MonoBehaviour 
{
   //==============
   // VARIABLES
   //==============    
   public int rows;                         // Number of rows
   public int columns;                      // Number of columns
   public float TileWidth;                  // Tile width
   public float TileHeight;                 // Tile height
   public float roomX;                      // Room's posiiton X
   public float roomY;                      // Room's position Y
   //===============
   // LAYERS
   //===============
   public FloorLayer floor;                 // Floor Layer
   public WallLayer wall;                   // Wall Layer
   public List<MiscLayer> others;           // Other Layers
   private GameObject room;                 // Instance of this room's game object
   //===============
   // START
   //===============
   void Start () 
   {        
        // check tileWidth and tileHeight
        if (TileWidth == 0) TileWidth = 1;
        if (TileHeight == 0) TileHeight = 1;
        // Create dungeon's GameObject
        room = new GameObject("Room");
        // generate room
        generateRoom();
   }
   //=============
   // UPDATE
   //=============
   void Update () {}
   //=============
   // GENERATE ROOMS
   //=============
   private void generateRoom()
   {
      // Generate x and y
      float x = -TileWidth * rows * 0.5f;
      float y = -TileHeight * columns * 0.5f;
      Debug.Log(x + "   " + y);
      // Initialize floor
      floor.init(x, y, columns, rows, TileWidth,TileHeight);
      floor.drawLayer();
      floor.setParent(room.transform);
      // Initialize walls
      wall.init(x, y, columns, rows, TileWidth, TileHeight);
      wall.drawLayer();
      wall.setParent(room.transform);  
      // Initialize other layers
      foreach(MiscLayer ml in others)
      {
          ml.init(x, y, columns, rows, TileWidth, TileHeight);
          ml.drawLayer();
          ml.setParent(room.transform); 
      }
      // Translate room
      room.transform.position = new Vector3(roomX + TileWidth*0.5f, roomY + TileHeight * 0.5f);
   }

}
