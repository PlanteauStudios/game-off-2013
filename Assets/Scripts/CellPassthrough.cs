using UnityEngine;
using System.Collections;

public class CellPassthrough : MonoBehaviour {
    private bool _passable = true;

    void OnTriggerEnter(Collider other) {
        if (_passable && other.gameObject.tag == "Wall") {
            _passable = false;
        }
    }
    public bool CanPass() {
        if (!_passable) Debug.Log("Not passapble");
        return _passable;
    }
}
