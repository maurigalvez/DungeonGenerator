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
   public float gap;                                    // Space between objects
   public sortPlacement distribution;   
   private bool is3D;
   private float oHeight;
   //=================
   // GENERATE2D
   //=================
   public override void generate2D()
   {
      layer = new GameObject(layerName);       
      this.layerID = id + 1;
      is3D = false;
      distributeLayer();
      
   }
   //==================
   // GENERATE3D
   //==================
   public override void generate3D()
   {
      layer = new GameObject(layerName);  
      is3D = true;
      oHeight = tHeight * 0.75f;
      distributeLayer();
   }
   //==================
   // DISTRIBUTE LAYER
   //==================
   private void distributeLayer()
   {
      //------------------
      // Generate layer depending on chosen distribution
      //------------------
      switch (distribution)
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
      //--------
      // 3D
      //--------
      if (is3D)
      {
         
         // bottom left
         tiles.Add(new Tile(this.x + this.tWidth, oHeight, this.y + this.tHeight, prefab));
         // top left
         tiles.Add(new Tile(this.x + this.tWidth, oHeight, this.y2 - this.tHeight, prefab));
         // bottom right
         tiles.Add(new Tile(this.x2 - this.tWidth, oHeight, this.y + this.tHeight, prefab));
         // top right
         tiles.Add(new Tile(this.x2 - this.tWidth, oHeight, this.y2 - this.tHeight, prefab));
      }
      //--------
      // 2D
      //--------
      else
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
         // 3D
         if (is3D == true)
         {
            tiles.Add(new Tile(posX, oHeight,posY, prefab));
         }
         // 2D
         else
         {
            // position tile
            tiles.Add(new Tile(posX, posY, prefab, this.layerID));
         }
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
      // 3D
      if (is3D == true)
      {
         tiles.Add(new Tile(cX, oHeight, cY, prefab));
      }
      // 2D
      else
      {
         // Add to tiles
         tiles.Add(new Tile(cX, cY, prefab, layerID));
      }
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
      float cX = this.x + tWidth * 5f;
      float lastY = this.y2 - tHeight * 5f;
      float cY = this.y + tHeight *5f;
      //-------
      // TOP AND BOTTOM
      //--------
      while(cX < lastX)
      {
         //---------
         // 3D
         //---------
         if (is3D)
         {
            tiles.Add(new Tile(cX, oHeight, cY - tHeight, prefab));
            tiles.Add(new Tile(cX, oHeight, lastY, prefab));
         }
         //---------
         // 2D
         //---------
         else
         {
            tiles.Add(new Tile(cX, cY - tHeight, prefab, layerID));
            tiles.Add(new Tile(cX, lastY, prefab, layerID));
         }
         cX += tWidth * gap;
      }
      //-------
      // LEFT AND RIGHT
      //-------
      cX = this.x + tWidth * 5f;      
      while (cY < lastY)
      {
         //---------
         // 3D
         //---------
         if (is3D)
         {
            tiles.Add(new Tile(cX, oHeight, cY, prefab));
            tiles.Add(new Tile(lastX, oHeight, lastY, prefab));
         }
         //---------
         // 2D
         //---------
         else
         {
            tiles.Add(new Tile(cX, cY, prefab, layerID));
            tiles.Add(new Tile(lastX, cY, prefab, layerID));
         }
         cY += tHeight * gap;
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
         //---------
         // 3D
         //---------
         if (is3D)
         {
            if (cX < middleX - tWidth || cX > middleX + tWidth)
               tiles.Add(new Tile(cX, oHeight, middleY, prefab));
         }
         //---------
         // 2D
         //---------
         else
         {
            if (cX < middleX - tWidth || cX > middleX + tWidth)
               tiles.Add(new Tile(cX, middleY, prefab, layerID));
         }
         // increase currentX
         cX += tWidth * gap;
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
         //---------
         // 3D
         //---------
         if (is3D)
         {
            if (cY < middleY - tHeight || cY > middleY + tHeight)
               tiles.Add(new Tile(middleX,oHeight, cY, prefab));
         }
         //---------
         // 2D
         //---------
         else
         {
            if (cY < middleY - tHeight || cY > middleY + tHeight)
               tiles.Add(new Tile(middleX, cY, prefab, layerID));
         }
         // increase currentY
         cY += tHeight * gap;
      }
   }
}
