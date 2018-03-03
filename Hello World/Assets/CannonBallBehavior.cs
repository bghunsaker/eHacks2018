using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehavior : MonoBehaviour {
    public bool isUp = false;
    int counter = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        
        if (isUp == true)
        {
            
            for (int i = 0; i < 80; i++)
            {
                transform.Translate(new Vector3(0, -1, 0));
            }
            isUp = false;
            
        }
        if (isUp == false)
        {
            transform.Translate(Vector2.up);
            counter++;
            if (counter >= 80)
            {
                isUp = true;
                counter = 0;
            }
        }

    }
}
