using UnityEngine;
using System.Collections;
[System.Serializable]
/// <summary>
/// Wall layer - Class used to create Wall Layer
/// </summary>
public class WallLayer : Layer
{
    //================
    // WALL SPRITES
    //================
    public GameObject leftWall;                     // Left wall
    public GameObject rightWall;                    // Right wall
    public GameObject bottomWall;                   // Bottom Wall
    public GameObject topWall;                      // Top Wall
    public GameObject TLCorner;                     // Top-Left Corner
    public GameObject TRCorner;                     // Top-Right Corner
    public GameObject BLCorner;                     // Bottom-Left Corner
    public GameObject BRCorner;                     // Bottom-Right
    //=================
    // DOOR SPRITES
    //=================   
    public GameObject Door;                         // Door to be placed on top of the walls
    [System.NonSerialized]
    public Tile doorLeft;                          // If true a door will be placed on left wall
    public Tile doorRight;                         // If true a door will be placed on right wall
    public Tile doorTop;                           // If true a door will be placed on top wall
    public Tile doorBottom;                        // If true a door will be placed on bottom wall
    //===============
    // GENERATE 2D
    //===============
    /// <summary>
    /// Generates walls tiles and adds them to tiles list.
    /// </summary>
	public override void generate2D()
    {
        //-----------------
        // CHECK IF GAME OBJECTS ARE NOT SET
        //----------------
        if(!leftWall || !rightWall || !bottomWall || !topWall || !TLCorner || !TRCorner || !BLCorner || !BRCorner)
        {
            Debug.LogError("GameObject for one of the walls have not been set! Wall will not be drawn until all of them are set");
            return;
        }            

        layer = new GameObject("Walls");
        // set layerId to 1
        layerID = 1;       
        //=================
        // TOP AND BOTTOM
        //=================       
        float currentX = this.x;
        int colsAdded = 0;
        while (currentX <= x2)
        {
           
           if (colsAdded == columns / 2)
           {
              doorTop = new Tile(currentX, this.y, bottomWall, this.layerID);
              doorBottom = new Tile(currentX, this.y2, topWall, this.layerID);
              // add door at top
              tiles.Add(doorTop);
              // add door at bottom
              tiles.Add(doorBottom);
              colsAdded++;              
           }
           else
           {
              //---------------
              // Top and bottomleft corner
              //---------------
              if (currentX == this.x)
              {
                 // top left corner
                 tiles.Add(new Tile(currentX, y2, TLCorner, this.layerID));
                 // bottom left corner
                 tiles.Add(new Tile(currentX, this.y, BLCorner, this.layerID));
              }
              //---------------
              // Top and bottomright corner
              //---------------
              else if (currentX == x2)
              {
                 // top left corner
                 tiles.Add(new Tile(currentX, y2, TRCorner, this.layerID));
                 // bottom left corner
                 tiles.Add(new Tile(currentX, this.y, BRCorner, this.layerID));
              }
              else
              {
                 // add tile at top 
                 tiles.Add(new Tile(currentX, this.y, bottomWall, this.layerID));
                 // add tile at bottom
                 tiles.Add(new Tile(currentX, y2, topWall, this.layerID));
              }
           }
            // increase currentX
            currentX += this.tWidth;
            // increase cols Added
            colsAdded++;
        }
        //==================
        // LEFT AND RIGHT
        //==================
        float currentY = this.y + this.tHeight;
        int rowsAdded = 0;
        while (currentY <= y2 - this.tHeight)
        {
           if (rowsAdded == rows / 2)
           {
              doorLeft = new Tile(this.x, currentY, leftWall, this.layerID);
              doorRight = new Tile(this.x2, currentY, rightWall, this.layerID);
              // add left door
              tiles.Add(doorLeft);
              // add right door
              tiles.Add(doorRight);
           }
           else
           {
              // add tile on left
              tiles.Add(new Tile(this.x, currentY, leftWall, this.layerID));
              // add tile on right
              tiles.Add(new Tile(x2, currentY, rightWall, this.layerID));
           }
            // increase currentY
            currentY += this.tHeight;
           // increase rows Added
            rowsAdded++;
        }
        
    }
    //===============
    // GENERATE 3D
    //===============
    /// <summary>
    /// Generates walls tiles and adds them to tiles list.
    /// </summary>
    public override void generate3D()
    {
       //-----------------
       // CHECK IF GAME OBJECTS ARE NOT SET
       //----------------
       if (!leftWall || !rightWall || !bottomWall || !topWall || !TLCorner || !TRCorner || !BLCorner || !BRCorner)
       {
          Debug.LogError("GameObject for one of the walls have not been set! Wall will not be drawn until all of them are set");
          return;
       }
       layer = new GameObject("Walls");
       // Set Position on Y
       float height = tHeight * 0.75f;
       //=================
       // TOP AND BOTTOM
       //=================       
       float currentX = this.x;
       while (currentX <= x2)
       {
          //---------------
          // Top and bottomleft corner
          //---------------
          if (currentX == this.x)
          {
             // top left corner
             tiles.Add(new Tile(currentX, height , y2, TLCorner));
             // bottom left corner
             tiles.Add(new Tile(currentX, height, this.y, BLCorner));
          }
          //---------------
          // Top and bottomright corner
          //---------------
          if (currentX == x2)
          {
             // top left corner
             tiles.Add(new Tile(currentX, height,y2, TRCorner));
             // bottom left corner
             tiles.Add(new Tile(currentX, height,this.y, BRCorner));
          }
          else
          {
             // add tile at top 
             tiles.Add(new Tile(currentX, height, this.y, bottomWall));
             // add tile at bottom
             tiles.Add(new Tile(currentX, height,y2, topWall));
          }
          // increase currentX
          currentX += this.tWidth;
       }
       //==================
       // LEFT AND RIGHT
       //==================
       float currentY = this.y + this.tHeight;
       while (currentY <= y2 - this.tHeight)
       {
          // add tile on left
          tiles.Add(new Tile(this.x, height, currentY, leftWall));
          // add tile on right
          tiles.Add(new Tile(x2, height, currentY, rightWall));
          // increase currentY
          currentY += this.tHeight;
       }
    }
}
