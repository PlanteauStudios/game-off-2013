using UnityEngine;
using System.Collections;

public class CellPassthrough : MonoBehaviour {
    private bool _passable = true;
	
    void OnTriggerEnter(Collider other) {
        if (_passable && other.gameObject.tag == "Wall") {
            _passable = false;
            Debug.Log("Not a _passable cell");
        }
    }
    public bool CanPass() { return _passable; }
}
