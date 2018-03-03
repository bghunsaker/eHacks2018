using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueGuyBehaviorScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Character Collision detected with " + col.gameObject.name);

        if (col.gameObject.name =="dropBox1")
        {
            
            Debug.Log("Dead");
            
                transform.Translate(Vector2.left);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            
        } 
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1, 0, 0));
        }
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            transform.Translate(new Vector3(0, 1, 0));
        }

    }

    
}
