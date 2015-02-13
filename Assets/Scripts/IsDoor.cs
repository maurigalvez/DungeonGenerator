using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IsDoor : MonoBehaviour
{
   public List<string> TouchTypes;
   public GameObject destination;                  // Destination of this door
   public Transform destScreen;                    // Screen of destination
   //public cameraScript camera;                     // Instance of camera
   //public bool isActive = false;                // True if it's active, false otherwise.
   public float offsetV;
   public float offsetH;
	// Use this for initialization
	void Start () 
   {
      init();
	}

   private void init()
   {

      // check if there's a destination
      if (!destination)
         Debug.LogError("Destination for door has not been set!!");
   }
	
   private void OnTriggerEnter2D(Collider2D other)
   {
      foreach (string s in TouchTypes)
      {
         if (other.gameObject.GetComponent(s))
         {           
            Vector3 newPosition = destination.transform.position;
            //---------------
            // transport to destination
            //----------------
            other.gameObject.transform.position = new Vector3(newPosition.x + offsetH, newPosition.y + offsetV, newPosition.z);
            // set camera
            //camera.setScreen(destScreen);
         }
      }
   }

   private void OnTriggerEnter(Collider other)
   {
      foreach (string s in TouchTypes)
      {
         if (other.gameObject.GetComponent(s))
         {
            Vector3 newPosition = destination.transform.position;
            //---------------
            // transport to destination
            //----------------
            other.gameObject.transform.position = new Vector3(newPosition.x + offsetH, newPosition.y + offsetV, newPosition.z);
            // set camera
            //camera.setScreen(destScreen);
         }
      }
   }

	// Update is called once per frame
	void Update () {
	
	}
}
