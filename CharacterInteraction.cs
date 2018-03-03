using System.Collections;
using System.Collections.Generic;
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
		if (Input.GetKey(KeyCode.D))
			transform.Translate(new Vector3(1, 0, 0));

		if (Input.GetKey(KeyCode.A))
			transform.Translate(new Vector3(-1, 0, 0));

		if (Input.GetKey(KeyCode.S))
			transform.Translate(new Vector3(0, -1, 0));

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
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
