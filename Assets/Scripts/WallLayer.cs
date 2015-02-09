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
    //===============
    // GENERATE
    //===============
    /// <summary>
    /// Generates walls tiles and adds them to tiles list.
    /// </summary>
	public override void generate()
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
        while (currentX <= x2)
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
            if (currentX == x2)
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
            tiles.Add(new Tile(this.x, currentY, leftWall, this.layerID));
            // add tile on right
            tiles.Add(new Tile(x2, currentY, rightWall, this.layerID));
            // increase currentY
            currentY += this.tHeight;
        }
       //Debug.Log(tiles.Count);
    }
}
