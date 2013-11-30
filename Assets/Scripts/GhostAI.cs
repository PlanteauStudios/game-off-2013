using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    public GameObject _ghost_start;

    private Movement.Direction _direction;
    public GameObject _pacman, _floor;
    public int _delayed_start;
    private int _wait = 0;
    public GameObject _pen_mid, _pen_exit;

    public GameObject _normal_form, _vulnerable_form;
    private bool _vulnerable = false, _flash_on = false;
    public int VULNERABLE_TIMER;
    private int _vulnerable_timer = 0, _vulnerable_flash_counter = 0;

    public bool _starting_position = true, _starting_area = true;
    void Start () {
        _direction = Movement.Direction.Up;
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;

        if (other.collider.gameObject.tag == "Ghost")
            Movement.Reverse(ref _direction);
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Pellet") return;
        if (other.gameObject.tag == "Pen Mid") {
            _starting_position = false;
        } else if (other.gameObject.tag == "Pen Exit") {
            _starting_area = false;
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
            } else if (_direction == Movement.Direction.Up) {
                _direction = Movement.Direction.Right;
            }
        } else if (other.gameObject.tag == "Pen Gate") {
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
            }
        } else if (other.gameObject.tag == "Ghost") {
            Movement.Reverse(ref _direction);
        } else {
            Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        }
    }
    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Pen Gate") {
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
            }
        } else if (other.gameObject.tag == "Ghost") {
            Movement.Reverse(ref _direction);
        }
    }
    void FixedUpdate () {
        AnimateVulnerable();
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
        }

        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        // while (Movement.CollisionIn(_direction, transform.position))
        //     Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
    public void Reset() {
        _starting_position = true; _starting_area = true;
    }
    public void SetVulnerable() {
        _vulnerable = true;
        _vulnerable_timer =0;
        _normal_form.SetActive(false);
        _vulnerable_form.SetActive(true);
    }
    public bool IsVulnerable() {
        return _vulnerable;
    }
    void Update() {
        if (_vulnerable) {
            if (_vulnerable_timer <= VULNERABLE_TIMER) {
                ++_vulnerable_timer;
            } else {
                _vulnerable = false;
                _normal_form.SetActive(true);
                _vulnerable_form.SetActive(false);
            }
        }
    }
    public void AnimateVulnerable() {
    }

}
