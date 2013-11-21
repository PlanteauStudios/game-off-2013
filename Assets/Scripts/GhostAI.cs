using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    private Movement _movement_script;
    private Movement.Direction _direction;
    public GameObject _pacman;
    // Use this for initialization
    void Start () {
        _movement_script = GetComponent<Movement>();
        _direction = Movement.Direction.Up;
    }
    
    // Update is called once per frame
    void Update () {
        // Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        Vector3 direct = _pacman.rigidbody.position - rigidbody.position;

        Vector3 horizontal = new Vector3(direct.x, 0.0f, 0.0f), 
                vertical = new Vector3(0.0f, 0.0f, direct.z);
        Vector3 movement = horizontal.x < vertical.z ? horizontal : vertical;
        _direction = _movement_script.MovingIn(direct);
        rigidbody.velocity = _movement_script.MoveDirection(_direction) * _speed;
        rot.eulerAngles = _movement_script.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
