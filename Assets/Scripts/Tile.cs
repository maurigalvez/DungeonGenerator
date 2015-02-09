using UnityEngine;
using System.Collections;
[System.Serializable]
/// <summary>
/// Tile - Class used to generate Tiles for layers
/// </summary>
public class Tile
{
	//================
	// VARIABLES
	//================
	private float x;								// position X
	private float y;								// position Y
   private int id;                        // Id of layer that tile belongs to
	private GameObject sprite;					// prefab of sprite
	private GameObject tileObject;		   // instance of tile gameObject
	//===============
	// CONSTRUCTOR
	//===============
	/// <summary>
	/// Initializes <see cref="Tile"/> instance with given parameters.
	/// </summary>
	/// <param name="_x">X position of Tile</param>
	/// <param name="_y">Y position of Tile</param>
	/// <param name="_sprite">Tile's sprite</param>
	/// <param name="_layerID">Depth or sprite's LayerID</param>
	public Tile(float _x, float _y, GameObject _sprite, int _layerID)
	{
		this.x = _x;
		this.y = _y;
		this.sprite = _sprite;
		// assign layerId to sprite
		this.id = _layerID;
	}
	//===============
	// GENERATE
	//===============
	/// <summary>
	/// Generates game object of this Tile.
	/// </summary>
	public void generate()
	{
		tileObject = (GameObject)GameObject.Instantiate(sprite,new Vector3(x,y,0),Quaternion.identity);
        tileObject.GetComponent<SpriteRenderer>().sortingOrder = this.id;
	}
	//===============
	// SET PARENT
	//===============
	/// <summary>
	/// Sets the parent of this Tile's gameobject in scene
	/// </summary>
	/// <param name="_p">Transform of parent of this Tile's GameObject</param>
	public void setParent(Transform _p)
	{
		tileObject.transform.parent = _p;
	}
}

