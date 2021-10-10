using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;   //used to reference the Player object (object camera will be following)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // FixedUpdate is called when physics are applied or a physics event occurs
    void FixedUpdate()
    {
        // v Continuously updating the transform/location of camera based on Player object location v
        this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
    }
}
