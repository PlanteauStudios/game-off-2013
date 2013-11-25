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
    
    // Update is called once per frame
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        Vector3 direct = _pacman.rigidbody.position - rigidbody.position;

        _direction = _floor.GetComponent<GridManager>()._cells.BestDirection(0,0);//_movement_script.MovingIn(direct);
        rigidbody.velocity = _movement_script.MoveDirection(_direction) * _speed;
        rot.eulerAngles = _movement_script.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
