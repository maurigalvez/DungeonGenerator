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
   private Vector3 dungeonPosition;         // Position of dungeon in game world
   //==================
   // ROOMS PROPERTIES
   //==================
   public int RoomRows;                     // Number of rows
   public int RoomColumns;                  // Number of columns
   public float TileWidth;                  // Width of Tile
   public float TileHeight;                 // Height of Tile
   public float gapH;                       // Horizontal Gap between rooms
   public float gapV;                       // Vertical Gap between rooms
   public float doorOffset;                 // Offset between player and destination
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
      RoomWidth = RoomRows * (TileWidth);
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
      Room prevRoom = null;
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
            prevRoom = cRoom;
            roomsGenerated++;
         }
         else
         {
            prevPosition = prevGenerated.transform.position;
            //----------------------------
            // This is not the first room
            //----------------------------         
            // Obtain position of previous room
            roomPos = prevPosition;
            //-----------
            // Obtain a position where two rooms do not overlap
            //-----------
            while (checkOverlap(roomPos))
            {
               roomPos = prevPosition;
               // obtain direction
               int direction = Random.Range(1, 4);    
               switch (direction)
               {
                  // N
                  case 1:
                     // check if it's not in 3D
                     if (!switch3D)
                        roomPos += new Vector3(0, RoomHeight + gapV, 0);
                     else
                        roomPos += new Vector3(0, 0, RoomHeight + gapV);

                     // assign direction
                     cRoom.direction = Room.eRoomDirection.South;
                     break;              
                  // S
                  case 2:
                     // check if it's not in 3D
                     if (!switch3D)
                        roomPos -= new Vector3(0, RoomHeight + gapV, 0);
                     else
                        roomPos += new Vector3(0, 0, RoomHeight + gapV);

                     // assign direction
                     cRoom.direction = Room.eRoomDirection.North;
                     break;
                  // E
                  case 3:
                     roomPos += new Vector3(RoomWidth + gapH , 0, 0);
                     // assign direction
                     cRoom.direction = Room.eRoomDirection.East;
                     break;
                  // W
                  case 4:
                     roomPos -= new Vector3(RoomWidth  + gapH, 0, 0);
                     // assign direction
                     cRoom.direction = Room.eRoomDirection.West;
                     break;
               }
            } // check overlap
            // Assign new position to room's game object
            cRoom.getInstance().transform.position = roomPos;
            // connect with previous room
            prevRoom.connect(cRoom);
            // Initialize and set parent     
            cRoom.setParent(this.transform);
            // Set this room as prevGenerated
            prevGenerated = cRoom.getInstance();
            prevRoom = cRoom;
         }
         // Remove room
         roomsToGenerate.RemoveAt(current);
      }
   } 
   //===============
   // CHECK OVERLAP
   //===============
   /// <summary>
   /// Checks if given position overlaps with the position of other rooms
   /// </summary>
   /// <param name="point1">Position to check overlap.</param>
   /// <returns>Returns true if position overlaps with any of the rooms</returns>
   public bool checkOverlap(Vector3 point1)
   { 
      // Iterate over rooms
      foreach(Room r in rooms)
      { 
          if (Vector3.Distance(point1, r.getInstance().transform.position) <= 0)
            return true;
      }
      return false;
   }

}
