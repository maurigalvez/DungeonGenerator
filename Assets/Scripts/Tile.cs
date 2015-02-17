using UnityEngine;
using System.Collections;
[System.Serializable]
/// <summary>
/// Tile - Class used to generate Tiles for layers
/// </summary>
public class Tile
{
	//================
	// TILE PROPERTIES
	//================
	private float x;								// position X
	private float y;								// position Y
   private float z;                       // position Z (used only if it's a 3DTile)
   private int id;                        // Id of layer that tile belongs to
	private GameObject sprite;					// prefab of sprite
	private GameObject tileObject;		   // instance of tile gameObject
   private SpriteRenderer renderer;
   private Transform tileTransform;
	//===============
	// 2D CONSTRUCTOR
	//===============
	/// <summary>
	/// Initializes a 2D <see cref="Tile"/> instance with given parameters.
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
   // 3D CONSTRUCTOR
   //===============
   /// <summary>
   /// Initializes a 2D <see cref="Tile"/> instance with given parameters.
   /// </summary>
   /// <param name="_x">X position of Tile</param>
   /// <param name="_y">Y position of Tile</param>
   /// <param name="_z">Z position of Tile</param>
   /// <param name="_TileObj">Tile Game Object</param>
   public Tile(float _x, float _y, float _z, GameObject _tileObj)
   {
      this.x = _x;
      this.y = _y;
      this.z = _z;
      this.sprite = _tileObj;
   }
	//===============
	// GENERATE2D
	//===============
	/// <summary>
	/// Generates game object of this Tile on 2D
	/// </summary>
	public void generate2D()
	{
		tileObject = (GameObject)GameObject.Instantiate(sprite,new Vector3(x,y,0),Quaternion.identity);
      tileTransform = tileObject.transform;
      renderer = tileObject.GetComponent<SpriteRenderer>();
      if (renderer)
         renderer.sortingOrder = this.id;
      else
         Debug.LogError("Given Sprite object does not contain a Sprite Renderer Component!!");
	}
   //==============
   // GENERATE3D
	//==============
   /// <summary>
   /// Generates game object of this Tile on 3D
   /// </summary>
   public void generate3D()   
   {
      tileObject = (GameObject)GameObject.Instantiate(sprite, new Vector3(x, y,z), Quaternion.identity);
      tileTransform = tileObject.transform;
   }
   public void change2DSprite(GameObject _newSprite)
   {
      renderer.sprite = _newSprite.GetComponent<SpriteRenderer>().sprite;
   }
   //==============
   // FLIP HORIZONTAL
   //==============
   public void flipHorizontal()
   {
      tileTransform.localScale = new Vector3(-tileTransform.localScale.x, tileTransform.localScale.y, tileTransform.localScale.z) ;
   }
   //==============
   // FLIP VERTICAL
   //==============
   public void flipVertical()
   {
      tileTransform.localScale = new Vector3(tileTransform.localScale.x, -tileTransform.localScale.y, tileTransform.localScale.z);
   }
   //==============
   // FLIP ROTATE 
   //==============
   public void rotate(Vector3 _rotation)
   {
      tileTransform.rotation = Quaternion.Euler(_rotation);
   }
   //==============
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
   //===============
   // GET INSTANCE
   //================
   public GameObject getInstance()
   {
      return this.tileObject;
   }
}

