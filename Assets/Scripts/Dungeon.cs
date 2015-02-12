using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour 
{
   //==================
   // DUNGEON PROPERTIES
   //==================
   public bool switch3D;                    // True if 3D is on, otherwise 2D is on  
   public List<Room> rooms;                 // List of rooms of the dungeon
   public bool buildUp;                     // True if dungeon is to being built up, otherwise if will be builtDown
   private Vector3 dungeonPosition;         // Position of dungeon in game world
   //==================
   // ROOMS PROPERTIES
   //==================
   public int RoomRows;                     // Number of rows
   public int RoomColumns;                  // Number of columns
   public float TileWidth;                  // Width of Tile
   public float TileHeight;                 // Height of Tile
   private float RoomWidth;                 // Width of room
   private float RoomHeight;                // Height of room
	//==================
   // START
   //==================
   /// <summary>
   /// Initalize Dungeon
   /// </summary>
	void Start () 
   {
      // Set position of dungeon as this gameObject position
      dungeonPosition = this.transform.position;     
      // Check width and height
      if (TileWidth == 0) TileWidth = 1;
      if (TileHeight == 0) TileHeight = 1;
      // Check for rows and columns
      if(RoomRows == 0 || RoomColumns == 0)
      {
         Debug.LogWarning("Number of rows or columns has not been set!!");
         return;
      }     
      // Calculate Room Width and Height
      RoomWidth = RoomRows * TileWidth;
      RoomHeight = RoomColumns * TileHeight;
      // Generate dungeon
      generateDungeon();
	}	
	// Update is called once per frame
	void Update () {}
   //==================
   // GENERATE DUNGEON
   //==================
   /// <summary>
   /// Generates dungeon depending on selected layout
   /// </summary>
   private void generateDungeon()
   {
      // Generate rooms in list
      foreach (Room r in rooms)
      {
         r.init(RoomRows, RoomColumns, TileWidth, TileHeight, switch3D, this.transform.position);        
      }
      Vector3 roomPos = this.transform.position;
      Vector3 prevPosition;
      // Create a copy of rooms
      List<Room> roomsToGenerate = new List<Room>(rooms);
      // Room previously generated
      GameObject prevGenerated = null;
      int roomsGenerated = 0;
      // Continue while there are rrooms to generate
      while(roomsToGenerate.Count > 0)
      {
         int current = Random.Range(0, roomsToGenerate.Count - 1);
         // obtain current room
         Room cRoom = roomsToGenerate[current];
         // Check if there was a room previously generated
         if(prevGenerated == null)
         {
            //------------------------
            // This is the first room
            //------------------------
            // Initialize and set parent           
            cRoom.setParent(this.transform);
            // Set this room as prevGenerated
            prevGenerated = cRoom.getInstance();
            roomsGenerated++;
         }
         else
         {
            prevPosition = prevGenerated.transform.position;
            //----------------------------
            // This is not the first room
            //----------------------------
            int direction = Random.Range(1, 4);
            switch(direction)
            {
               // N
               case 1:
                  // Obtain position of previous room
                  roomPos = prevGenerated.transform.position;
                  roomPos += new Vector3(0, RoomHeight + TileHeight, 0);
                  break;
               // S
               case 2:
                  // Obtain position of previous room
                  roomPos = prevGenerated.transform.position;
                  roomPos -= new Vector3(0, RoomHeight + TileHeight, 0);
                  break;
               // E
               case 3:
                  // Obtain position of previous room
                  roomPos = prevGenerated.transform.position;
                  roomPos += new Vector3(RoomWidth, 0, 0);
                  break;
               // W
               case 4:
                  // Obtain position of previous room
                  roomPos = prevGenerated.transform.position;
                  roomPos -= new Vector3(RoomWidth,0, 0);
                  break;
            }            
            cRoom.getInstance().transform.position = roomPos;
            // Initialize and set parent     
            cRoom.setParent(this.transform);
            // Set this room as prevGenerated
            prevGenerated = cRoom.getInstance();
         }
         // Remove room
         roomsToGenerate.RemoveAt(current);
      }
   } 

}
