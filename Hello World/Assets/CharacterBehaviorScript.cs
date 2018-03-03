using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1, 0, 0));
        }
      
    }
}
