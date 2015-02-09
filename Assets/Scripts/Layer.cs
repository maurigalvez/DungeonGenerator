using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Layer - Class used to create Layers for TileMap
/// </summary>
public class Layer 
{	
	//=================
	// VARIABLES
	//=================
    protected GameObject layer;                                   // Instance of gameobject of this layer
    protected List<Tile> tiles = new List<Tile>();                // List of tiles that form layer
    protected int layerID;                                        // Layer ID - Depth of layer!
    protected float x;                                            // Position X of layer - Left Edge
    protected float y;                                            // Position Y of layer - Bottom Edge
    protected float x2;                                           // Right edge of layer
    protected float y2;                                           // Top edge of layer
    protected int rows;                                           // Number of tile rows
    protected int columns;                                        // Number of tile columns
    protected float width;                                        // Width of layer
    protected float height;                                       // Height of layer
    protected float tWidth;                                       // Tile Width
    protected float tHeight;                                      // Tile Height
	//==================
	// INITIALIZE
	//==================
	/// <summary>
	/// Initializes Layer with the specified _x, _y, _rows and _col.
	/// </summary>
	/// <param name="_x">X position of Layer</param>
	/// <param name="_y">Y position of Layer</param>
	/// <param name="_rows">Number of layer rows</param>
	/// <param name="_col">Number of layer columns</param>
    /// /// <param name="_tW">Width of tile</param>
    /// /// <param name="_tH">Height of tiles</param>
	public void init(float _x, float _y, int _rows, int _col, float _tW, float _tH)
	{
		this.x = _x;
		this.y = _y;
		this.rows = _rows;
		this.columns = _col;       
        this.tWidth = _tW;
        this.tHeight = _tH;
        this.width = rows * tWidth;
        this.height = columns * tHeight;
        this.x2 = this.x + (this.width) - tWidth;
        this.y2 = this.y + (this.height) - tHeight; 
        this.generate(); 
	}
    //==================
    // GENERATE
    //=================
    virtual public void generate(){}
    //=================
    // DRAW LAYER
    //=================
	/// <summary>
	/// Draws the layer on scene
	/// </summary>
    public void drawLayer()
    {
        // Iterate over the tiles
        foreach (Tile t in tiles)
        {
            // include tile in scene
            t.generate();
            // parent to layer gameObject
            t.setParent(layer.transform);
        }
    }
    //================
    // SET PARENT
    //===============
    /// <summary>
    /// Sets parent of this layer to given transform
    /// </summary>
    /// <param name="_p">Parent of layer</param>
    public void setParent(Transform _p)
    {
        this.layer.transform.parent = _p;
    }
}
