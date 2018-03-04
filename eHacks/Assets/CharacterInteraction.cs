using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteraction : MonoBehaviour {
    //Variables
    Image myImageComponent;
    public GameObject player;
    public GameObject goose;
    private Vector3 offset;
    public Sprite duckPosition;
    public Sprite uprightPosition;
    public GameObject position = new GameObject();
    public float speed = 10.0f;
	public float jumpHeight = 20.0f;
    public int level = 0;


    // Use this for initialization
    void Start () {
        myImageComponent = GetComponent<Image>();
        player = GameObject.Find("deer");
        offset = transform.position - player.transform.position;
        //string gooseID = GameObject.
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

        if (Input.GetKey(KeyCode.F))
        {
        

        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //transform.Translate(new Vector3(0, -1, 0));
            //myImageComponent.sprite = duckPosition;
            GetComponent<SpriteRenderer>().sprite = duckPosition;
            
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<SpriteRenderer>().sprite = uprightPosition;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || (Input.GetTouch(0).phase == TouchPhase.Stationary))
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
            || coll.gameObject.name == "Platform3.9" || coll.gameObject.name == "Platform4.13"
            || coll.gameObject.name == "Platform5.Exit") {
            
            //Camera & Character Move to Next Level Position
            for( int i = 0; i < 68; i++)
            {
                //Teleport to next level
                transform.Translate(Vector2.right);
            }
            if (coll.gameObject.name == "Platform14") {
                level = 1;
                SceneManager.LoadScene(level);
                SceneManager.UnloadSceneAsync(level - 1);
            }
            if (coll.gameObject.name == "Platform2.5")
            {
                level = 2;
                SceneManager.LoadScene(level);
                SceneManager.UnloadSceneAsync(level - 1);
            }
            if (coll.gameObject.name == "Platform3.9")
            {
                level = 3;
                SceneManager.LoadScene(level);
                SceneManager.UnloadSceneAsync(level - 1);
            }
            if (coll.gameObject.name == "Platform4.13")
            {
                level = 4;
                SceneManager.LoadScene(level);
                SceneManager.UnloadSceneAsync(level - 1);
            }
            if (coll.gameObject.name == "Platform5.Exit") {
                level = 0;
                SceneManager.LoadScene(level);
                SceneManager.UnloadSceneAsync(4);
            }
            transform.Translate(5*Vector2.up);
        }
        //Death Scenarios LVL2
        if (
            //Spikes
            coll.gameObject.name.Contains("Spikes")
            
            //Geese
            || coll.gameObject.name.Contains("cgoose")
            )

        {
            //Sets the user back to the start of Level 1
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.UnloadSceneAsync(level);
            level = 0;
        }

    }
}


/* TRASH
 coll.gameObject.name == "Spikes2.1" || coll.gameObject.name == "Spikes2.2" 
            || coll.gameObject.name == "Spikes3.1" || coll.gameObject.name == "Spikes3.2" 
            || coll.gameObject.name == "Spikes3.3" || coll.gameObject.name == "Spikes3.4" 
            || coll.gameObject.name == "Spikes4.1" || coll.gameObject.name == "Spikes4.2" 
            || coll.gameObject.name == "Spikes4.3"
*/
