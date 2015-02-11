using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Misc layer - Class used to create layer with objects and enemies
/// </summary>
[System.Serializable]
public class MiscLayer : Layer
{
   //=================
   // OBJECT PLACEMENT 
   //=================
   public enum sortPlacement
   {
      none,
      Cross,
      Corners,
      Center,
      Circle,
      Rectangle,
   }
   //=================
   // VARIABLES
   //=================
   public int number;                                   // Number of enemies to be added
   public string layerName;                             // Name of layer
   public int id;                                       // Id of MiscLayer
   public GameObject prefab;	                          // Prefab for tiles of this layer
   public float radius;                                 // Radius used for circle
   public sortPlacement distribution;
   //=================
   // GENERATE2D
   //=================
   public override void generate2D()
   {
      layer = new GameObject(layerName);       
      this.layerID = id + 1;
      //------------------
      // Generate layer depending on chosen distribution
      //------------------
      switch(distribution)
      {
         case sortPlacement.Cross:
            CrossDistribution();
         break;

         case sortPlacement.Center:
            setAtCenter();
         break;

         case sortPlacement.Corners:
            setAtCorners();
         break;

         case sortPlacement.Rectangle:
            RectangleDistribution();
         break;

         case sortPlacement.Circle:
            CircleDistribution();
         break;

         default:
            Debug.LogError("Select a valid sort distribution to generate layer properly!");
         break;
      }  
   }
   //==================
   // SET AT CORNERS
   //==================
   /// <summary>
   /// Positions the tiles at the four corners of the room!
   /// </summary>
   public void setAtCorners()
   {
      // bottom left
      tiles.Add(new Tile(this.x + this.tWidth, this.y + this.tHeight, prefab, this.layerID));
      // top left
      tiles.Add(new Tile(this.x + this.tWidth, this.y2 - this.tHeight, prefab, this.layerID));
      // bottom right
      tiles.Add(new Tile(this.x2 - this.tWidth, this.y + this.tHeight, prefab, this.layerID));
      // top right
      tiles.Add(new Tile(this.x2 - this.tWidth, this.y2 - this.tHeight, prefab, this.layerID));       
   }  
   //================
   // CIRCLE DISTRIBUTION
   //================
   /// <summary>
   /// Positions the tiles in a circular fashipn at the center of the room
   /// </summary>
   public void CircleDistribution()
   {
      // center position
      float cX = this.x + (this.width * 0.5f) - this.tWidth * 0.5f;
      float cY = this.y + (this.height * 0.5f) - this.tHeight * 0.5f;
      float angle = 0.0f;
      //  
      while(angle < 270f)
      {
         float posX = cX + radius * Mathf.Cos(angle);
         float posY = cY + radius * Mathf.Sin(angle);
         // position tile
         tiles.Add(new Tile(posX, posY, prefab, this.layerID));
         angle += 45.0f;
      }
   }	
   //====================
   // SET AT CENTER
   //====================
   /// <summary>
   /// Positions one instance of the prefab at the center of the room.
   /// </summary>
   public void setAtCenter()
   {
      // center position
      float cX = this.x + (this.width * 0.5f) - this.tWidth * 0.5f;
      float cY = this.y + (this.height * 0.5f) - this.tHeight * 0.5f;
      // Add to tiles
      tiles.Add(new Tile(cX, cY, prefab, layerID));
   }
   //======================
   // RECTANGLE DISTRIBUTION
   //======================
   public void RectangleDistribution()
   {
      //----------------
      // Calculate initialX and finalX
      //----------------
      float lastX = this.x2 - tWidth * 5f;
      float cX = this.x + tWidth * 6.25f;
      float lastY = this.y2 - tHeight * 5f;
      float cY = this.y + tHeight *5f;

      while(cX < lastX)
      {
         tiles.Add(new Tile(cX, cY-tHeight, prefab, layerID));
         tiles.Add(new Tile(cX, lastY, prefab, layerID));
         cX += tWidth * 2.0f;
      }
      cX = this.x + tWidth * 5f;
      ///cY _
      while (cY < lastY)
      {
         tiles.Add(new Tile(cX, cY, prefab, layerID));
         tiles.Add(new Tile(lastX,cY, prefab, layerID));
         cY += tHeight * 2.0f;
      }
   }
   //====================
   // CROSS DISTRIBUTION
   //====================
   /// <summary>
   /// Positions prefab in a cross distribution except for the center of the room.
   /// </summary>
   public void CrossDistribution()
   {
      //-----------------
      // Calculate middle Point
      //-----------------
      float middleX = this.x + (this.width * 0.5f) - this.tWidth * 0.5f;
      float middleY = this.y + (this.height * 0.5f) - this.tHeight * 0.5f;
      //----------------
      // Calculate initialX and finalX
      //----------------
      float initialX = this.x + tWidth * 4f;
      float lastX = this.x2 - tWidth * 4f;
      float cX = initialX;
      //---------------
      // ADD OBJECTS ON X
      //---------------
      while (cX <= lastX)
      {
         if (cX < middleX - tWidth || cX > middleX + tWidth)
            tiles.Add(new Tile(cX, middleY, prefab, layerID));
         // increase currentX
         cX += tWidth;
      }
      //----------------
      // Calculate initialY and finalY
      //----------------
      float initialY = this.y + tHeight * 4f;
      float lastY = this.y2 - tHeight * 4f;
      float cY = initialY;
      //---------------
      // ADD OBJECTS ON Y
      //---------------
      while (cY <= lastY)
      {
         if(cY < middleY - tHeight || cY > middleY + tHeight)
            tiles.Add(new Tile(middleX, cY, prefab, layerID));
         // increase currentY
         cY += tHeight;
      }
   }
}
