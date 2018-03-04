using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour {
	//Variables
	public float speed = 10.0f;
	public float jumpHeight = 20.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int counter = 0;

		transform.Translate(new Vector3(.15f, 0, 0));
        
        if (Input.GetKey(KeyCode.A))
        {
            for (counter = 0; counter < 5; counter++)
            {
                transform.Translate(new Vector3(-.15f, 0, 0));
            }

        }

        if (Input.GetKey(KeyCode.S))
			transform.Translate(new Vector3(0, -1, 0));

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
			transform.Translate(new Vector3(0, .75f, 0));

        //var y = Input.GetAxis("Vertical");
        //var x = Input.GetAxis("Horizontal");

        //if (y < -18) {
        //    transform.Translate(new Vector3(0, 1, 0));
        //}
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Level i to Level i+1
        if (coll.gameObject.name == "Platform14" || coll.gameObject.name == "Platform2.5" 
            || coll.gameObject.name == "Platform3.9" || coll.gameObject.name == "Platform4.13") {
            //Camera & Character Move to Next Level Position
            for( int i = 0; i < 68; i++)
            {
                //Teleport to next level
                transform.Translate(Vector2.right);
            }
            transform.Translate(5*Vector2.up);
        }
        //Death Scenarios LVL2
        if (coll.gameObject.name == "Spikes2.1" || coll.gameObject.name == "Spikes2.2" 
            || coll.gameObject.name == "Spikes3.1" || coll.gameObject.name == "Spikes3.2" 
            || coll.gameObject.name == "Spikes3.3" || coll.gameObject.name == "Spikes3.4" 
            || coll.gameObject.name == "Spikes4.1" || coll.gameObject.name == "Spikes4.2" 
            || coll.gameObject.name == "Spikes4.3")
        {
            //Sets the user back to the start of Level 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
