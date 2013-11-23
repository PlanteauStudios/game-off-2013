using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
    private int _current_bob;
    public float _max_bob;
    private bool _up;
	// Use this for initialization
	void Start () {
	   _current_bob = 0;
       _up = true;
	}
	
	// Update is called once per frame
	void Update () {
        _up = rigidbody.position.y >= _max_bob ? false : true;
        if (_up)
            rigidbody.AddForce(new Vector3(0.0f, 10.0f, 0.0f));
        else 
            rigidbody.AddForce(new Vector3(0.0f, .5f, 0.0f));
        // Debug.Log("bob is " + _up + " because Y is " + rigidbody.position.y);

	}
}
