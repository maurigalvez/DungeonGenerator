using UnityEngine;
using System.Collections;
[System.Serializable]
/// <summary>
/// Floor layer - Class used to create Floor layer
/// </summary>
public class FloorLayer : Layer
{
    public GameObject floor;
   //================
   // GENERATE 2D
   //================
   /// <summary>
   /// Generates Floor tiles and adds them to tiles list
   /// </summary>
   public override void generate2D()
   {
      // Create new layer object
      layer = new GameObject("Floor");
      // set layerId to 0
      layerID = 0;
      //--------------------
      // Generate Tiles
      //--------------------
	   // rows
	   for (int i = 0; i < columns; i++)
		   // columns
		   for (int j = 0; j < rows; j++)
			   // create tile
			   tiles.Add (new Tile(this.x + j * tWidth,this.y + i* tHeight, floor,this.layerID));	
   }
   //================
   // GENERATE 3D
   //================
   /// <summary>
   /// Generates Floor tiles and adds them to tiles list
   /// </summary> 
   public override void generate3D()
   {
      // Create new layer object
      layer = new GameObject("Floor");
      // Assign a y value
      float ypos = 0.0f;
      //--------------------
      // Generate Tiles
      //--------------------
      // rows
      for (int i = 0; i < columns; i++)
         // columns
         for (int j = 0; j < rows; j++)
            // create tile
            tiles.Add(new Tile(this.x + j * tWidth, ypos, this.y + i * tHeight, floor));	      
   }
	
}
