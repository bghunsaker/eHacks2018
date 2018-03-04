using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour {
	//Variables
	public float speed = 10.0f;
	public float jumpHeight = 20.0f;
    private AndroidJavaClass ajc;

	// Use this for initialization
	void Start () {
        ajc = new AndroidJavaClass("edu.cmu.pocketsphinx.demo.GooseGameActivity");
        

    }
	
	// Update is called once per frame
	void Update () {

        string s = ajc.CallStatic<string>("shareCommand");
        //      if (Input.GetKey(KeyCode.D))
        //	transform.Translate(new Vector3(1, 0, 0));

        //if (Input.GetKey(KeyCode.A))
        //	transform.Translate(new Vector3(-1, 0, 0));

        //if (Input.GetKey(KeyCode.S))
        //	transform.Translate(new Vector3(0, -1, 0));

        //if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        //	transform.Translate(new Vector3(0, 1, 0));

        if (s.Equals("go"))
            transform.Translate(new Vector3(1, 0, 0));

        else if (s.Equals("stop"))
            transform.Translate(new Vector3(-1, 0, 0));

        else if (s.Equals("duck"))
            transform.Translate(new Vector3(0, -1, 0));

        else if (s.Equals("jump"))
            transform.Translate(new Vector3(0, 1, 0));

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
}
