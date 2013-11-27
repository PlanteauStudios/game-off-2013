using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    private Movement _movement_script;
    private Movement.Direction _direction;
    public GameObject _pacman, _floor;

    // Use this for initialization
    void Start () {
        _movement_script = GetComponent<Movement>();
        _direction = Movement.Direction.Up;
    }
    int Randomize() {
        int ran = Random.Range(0, 3);
        return ran;
    }
    void SwitchDirection(int dir) {
        if (dir > 3) dir = 0;
        Movement.Direction next_direction = (Movement.Direction)dir;//_direction;

        if (next_direction == _direction)
            SwitchDirection(dir + 1);
        else {
            _direction = next_direction;
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Floor") return;
    
        SwitchDirection(Randomize());
    }
    // Update is called once per frame
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        rigidbody.velocity = _movement_script.MoveDirection(_direction) * _speed;
        rot.eulerAngles = _movement_script.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
