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

		// if (Event.current.Equals(Event.KeyboardEvent(KeyCode.Space.ToString())) || Event.current.Equals(Event.KeyboardEvent(KeyCode.W.ToString())))
		// 	//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);

		// if(Input.GetKey(KeyCode.D)) {
            
		// }
		// if(Input.GetKey(KeyCode.A)) {
        //     transform.Translate(new Vector3(-1, 0, 0));
		// }

		// if(Input.GetKey(KeyCode.)) {
			
		// }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        //Level i to Level i+1
        if (coll.gameObject.name == "Platform14" || coll.gameObject.name == "Platform2.5") {
            //Camera & Character Move to Next Level Position
            for( int i = 0; i < 65; i++)
            {
                transform.Translate(Vector2.right);
            }
            
        }
        //Death Scenarios LVL2
        if (coll.gameObject.name == "Spikes2.1" || coll.gameObject.name == "Spikes2.2")
        {
            //transform.Translate(Vector2.left);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
}
