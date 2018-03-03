using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingBoxBehavior : MonoBehaviour {
    public bool isUp = true;
    int counter = 0;
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //timescale rather than frames
    private void FixedUpdate()
    {
        if(isUp == true) {
          //  Debug.Log("Going Down");
            transform.Translate(Vector2.down);
        }
        if(isUp == false)
        {
            transform.Translate(Vector2.up);
            counter++;
            if (counter >= 50)
            {
                isUp = true;
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      
        isUp = false;
        counter = 0;
      //  Debug.Log("Box Collision detected");
    }
}
