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
		Tshape,
		Corners,
		Center,
        Random,
	}
	//=================
	// VARIABLES
	//=================
    public int number;                                  // Number of enemies to be added
    public string layerName;                            // Name of layer
    public int id;
	public GameObject prefab;	                    	// List of objects to be present on mapLayer   
    //=================
    // GENERATE
    //=================
    public override void generate()
    {
        layer = new GameObject(layerName);       
        this.layerID = id + 1;

        setAtCorners();

        setAtCenter();
    }

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

    public void setAtCenter()
    {
        // center position
        float cX = this.x + (this.width * 0.5f) - this.tWidth * 0.5f;
        float cY = this.y + (this.height * 0.5f) - this.tHeight * 0.5f;
        // center
        tiles.Add(new Tile(cX, cY, prefab, this.layerID));
        // radius
        float r = 0.75f;
        float angle = 0.0f;
        //  
        while(angle < 270f)
        {
            float posX = cX + r * Mathf.Cos(angle);
            float posY = cY + r * Mathf.Sin(angle);
            // position tile
            tiles.Add(new Tile(posX, posY, prefab, this.layerID));
            angle += 90.0f;
        }
    }	
}
