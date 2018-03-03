using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        Debug.Log("starting");
#if UNITY_EDITOR_WIN
        Debug.Log("win1");
        Debug.Log("win2");
#endif
#if UNITY_EDITOR_WIN
        Debug.Log("a");
#endif
#if UNITY_EDITOR_OSX
        Debug.Log("BBBBBBB");
#endif
        AndroidJavaClass pluginClass = new AndroidJavaClass("edu.cmu.pocketsphinx.demo.PocketSphinxActivity");
        pluginClass.Call("onCreate", null);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
