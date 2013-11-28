using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
    public float _max_bob;
    private bool _up;
	// Use this for initialization
	void Start () {
       _up = true;
	}

	// Update is called once per frame
	void Update () {
        _up = rigidbody.position.y >= _max_bob ? false : true;
        if (_up)
            rigidbody.AddForce(new Vector3(0.0f, 10.0f, 0.0f));
        else
            rigidbody.AddForce(new Vector3(0.0f, .5f, 0.0f));
	}
}
