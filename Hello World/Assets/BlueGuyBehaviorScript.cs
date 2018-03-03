using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlueGuyBehaviorScript : MonoBehaviour {
    string command = "";
    AndroidJavaClass jc = null;
    AndroidJavaObject jo = null;

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Character Collision detected with " + col.gameObject.name);

        if (col.gameObject.name =="dropBox1")
        {
            Debug.Log("Dead");
                transform.Translate(Vector2.left);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }
        if (col.gameObject.name == "cannonBall")
        {
            Debug.Log("Dead");
            transform.Translate(Vector2.left);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //Use this for initialization

   void Start () {
        AndroidJavaClass jc = new AndroidJavaClass("edu.cmu.pocketsphinx.demo.GooseGameActivity");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("instance");
    }

    // Update is called once per frame
    void Update () {
        command = jc.Call<string>("shareCommand");

        //forward - crouch
       // if (Input.GetKey(KeyCode.D))
       if(command.Equals("crouch"))
        {
            transform.Translate(new Vector3(1, 0, 0));
        }
        //backward - shoot
        //if (Input.GetKey(KeyCode.A))
        if(command.Equals("shoot"))
        {
            transform.Translate(new Vector3(-1, 0, 0));
        }
        //jump
        //if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        if(command.Equals("jump"))
        {
            transform.Translate(new Vector3(0, 1, 0));
        }
      //  if (Input.GetKey(KeyCode.P))
       // {
            //GameObject.Find("projectile").
       // }

    }

    
}
