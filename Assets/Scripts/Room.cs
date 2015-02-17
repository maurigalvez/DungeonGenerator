using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//===================
// ROOM GENERATOR
//===================
[System.Serializable]
public class Room
{
   public enum eRoomDirection
   {
      North,
      South,
      West,
      East
   }
   //==============
   // ROOM PROPERTIES
   //==============    
   public GameObject customRoom;             // Custom Room to be placed instead of this one
   public int roomId;                        // Number of room 
   public eRoomDirection direction;          // Direction of this room
   private int rows;                         // Number of rows
   private int columns;                      // Number of columns
   private float TileWidth;                  // Tile width
   private float TileHeight;                 // Tile height
   private Vector3 position;                 // Room position
   private bool switch3d;                    // True if 3D feature is on, otherwise is off.   
   private bool inPosition;                  // True if room has been set to a specific position, False otherwise
   //===============
   // LAYER PROPERTIES
   //===============   
   public FloorLayer floor;                 // Floor Layer
   public WallLayer wall;                   // Wall Layer
   public List<MiscLayer> others;           // Other Layers
   private GameObject room;                 // Instance of this room's game object
   //===============
   // INIT
   //===============
   public void init (int _row, int _col, float _tWidth, float _tHeight, bool _3dswitch, Vector3 _pos) 
   {       
      inPosition = false;
      // Check if there's a custom Room
      if (customRoom)
      {
         Debug.Log("Custom room generated at position " + position.ToString());
         // Set customRoom at set positions
         customRoom.transform.position = position;
      }
      else
      {
         // set values
         switch3d = _3dswitch;
         rows = _row;
         columns = _col;
         TileWidth = _tWidth;
         TileHeight = _tHeight;
         position = _pos;
         // Create dungeon's GameObject
         room = new GameObject("Room");         
         // generate room
         generateRoom();
      }
   }
   //=============
   // GENERATE ROOMS
   //=============
   private void generateRoom()
   {
      // Generate x and y
      float x = -TileWidth * rows * 0.5f;
      float y = -TileHeight * columns * 0.5f;
      // Translate room
      if (!switch3d)
         room.transform.position = new Vector3(position.x + TileWidth * 0.5f, position.y + TileHeight * 0.5f, position.z);
      else
         room.transform.position = new Vector3(position.x + TileWidth * 0.5f, position.y, position.z + TileHeight * 0.5f);
      //Debug.Log(x + "   " + y);
      // Initialize floor
      floor.init(x, y, columns, rows, TileWidth,TileHeight,switch3d);
      floor.setParent(room.transform);
    
      // Initialize walls
      wall.init(x, y, columns, rows, TileWidth, TileHeight,switch3d);
      wall.setParent(room.transform);
      // Initialize other layers
      foreach(MiscLayer ml in others)
      {
          ml.init(x, y, columns, rows, TileWidth, TileHeight, switch3d);     
          ml.setParent(room.transform); 
      }
     
   }
   //================
   // SET PARENT
   //===============
   /// <summary>
   /// Sets parent of this room to given transform
   /// </summary>
   /// <param name="_p">Parent of layer</param>
   public void setParent(Transform _p)
   {      
      this.room.transform.parent = _p;
   }
   //================
   // CONNECT
   //================
   public void connect(Room otherRoom)
   {
      IsDoor door1 = null;
      IsDoor door2 = null;
      switch(otherRoom.direction)
      {
         case eRoomDirection.North:
            // Check if it's in 2D
            if(!this.switch3d)
            {
               this.wall.doorTop.change2DSprite(wall.Door);   
               otherRoom.wall.doorBottom.change2DSprite(otherRoom.wall.Door);              
            }

            // Obtain door components           
            door1 = this.wall.doorTop.getInstance().GetComponent<IsDoor>();
            door2 = otherRoom.wall.doorBottom.getInstance().GetComponent<IsDoor>();    
         break;

         case eRoomDirection.East:
            // Check if it's in 2D
            if (!this.switch3d)
            {
               this.wall.doorRight.change2DSprite(wall.Door);
               this.wall.doorRight.rotate(new Vector3(0, 0, -90));
               otherRoom.wall.doorLeft.change2DSprite(otherRoom.wall.Door);
               otherRoom.wall.doorLeft.rotate(new Vector3(0, 0, 90));
            }

            // Obtain door components           
            door1 = this.wall.doorRight.getInstance().GetComponent<IsDoor>();
            door2 = otherRoom.wall.doorLeft.getInstance().GetComponent<IsDoor>();  
         break;

         case eRoomDirection.West:
            // Check if it's in 2D
            if (!this.switch3d)
            {
               this.wall.doorLeft.change2DSprite(wall.Door);
               this.wall.doorLeft.rotate(new Vector3(0, 0, 90));
               otherRoom.wall.doorRight.change2DSprite(otherRoom.wall.Door);
               otherRoom.wall.doorRight.rotate(new Vector3(0, 0, -90));
            }

            // Obtain door components           
            door1 = this.wall.doorLeft.getInstance().GetComponent<IsDoor>();
            door2 = otherRoom.wall.doorRight.getInstance().GetComponent<IsDoor>();  
         break;

         case eRoomDirection.South:
            // Check if it's in 2D
            if (!this.switch3d)
            {
               this.wall.doorBottom.change2DSprite(wall.Door);           
               otherRoom.wall.doorTop.change2DSprite(otherRoom.wall.Door);
            }

            // Obtain door components           
            door1 = this.wall.doorBottom.getInstance().GetComponent<IsDoor>();
            door2 = otherRoom.wall.doorTop.getInstance().GetComponent<IsDoor>(); 
         break;
      }// switch
      //--------
      // Assign doors properties
      //---------
      // check that both doors are not null
      if (door1!= null && door2!= null)
      {
         // Assign door destinations
         door1.destination = otherRoom.wall.doorBottom.getInstance();
         door2.destination = wall.doorTop.getInstance();
         // Assign destination screens
         door1.destScreen = otherRoom.getInstance().transform;
         door2.destScreen = this.room.transform;
      }
      else      
         Debug.LogError("Door GameObject doesn't have an IsDoor Component attached.");
      
   }
  
   //================
   // IS IN POSITION
   //================
   /// <summary>
   /// Determines whether room has been set on position in dungeon
   /// </summary>
   /// <returns>True if room is in position, otherwise it returns false.</returns>
   public bool isInPosition()
   {
      return inPosition;
   }
   //================
   // SET IN POSITION
   //================.
   /// <summary>
   /// Sets the room inPosition value to true.
   /// </summary>
   public void setInPosition()
   {
      inPosition = true;
   }

   public GameObject getInstance()
   {
      return this.room;
   }
}
