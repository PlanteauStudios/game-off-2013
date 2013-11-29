using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    private Movement.Direction _direction;
    public GameObject _pacman, _floor;

    public int _delayed_start;
    private int _wait = 0;
    public GameObject _pen_mid, _pen_exit;
    public bool _starting_position = true, _starting_area = true;
    void Start () {
        _direction = Movement.Direction.Up;
    }

    void OnCollisionEnter(Collision other) {
        // Debug.Log("Bump");
        // rigidbody.velocity = Vector3.zero;
        if (other.collider.gameObject.tag == "Floor") return;
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pellet") return;
        if (other.gameObject.tag == "Pen Mid") {
            _starting_position = false;
        } else if (other.gameObject.tag == "Pen Exit") {
            Debug.Log("hit the exit");
            _starting_area = false;
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
                Debug.Log("Getaway from exit");
            } else if (_direction == Movement.Direction.Up) {
                _direction = Movement.Direction.Right;
                Debug.Log("Getaway from exit");
            }
        } else if (other.gameObject.tag == "Pen Gate") {
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
                Debug.Log("Getaway from exit");
            }
        } else {
            Debug.Log("bump");
            Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Pen Gate") {
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
                Debug.Log("Getaway from exit");
            }
        }
    }
    void FixedUpdate () {
        if (_wait <= _delayed_start) {
            ++_wait;
            return;
        }
        if (_starting_position) {
            _direction = transform.position.x < _pen_mid.transform.position.x ?
                Movement.Direction.Right : Movement.Direction.Left;
        } else if(_starting_area) {
            _direction = transform.position.z < _pen_exit.transform.position.z ?
                Movement.Direction.Up: Movement.Direction.Down;
                Debug.Log("away from teh starting area");
        }

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        // while (Movement.CollisionIn(_direction, transform.position))
        //     Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
        Debug.Log("Ghosty X " + rigidbody.velocity.x + ", Y, " +  rigidbody.velocity.y
            + " Z" + rigidbody.velocity.z);
    }
}
