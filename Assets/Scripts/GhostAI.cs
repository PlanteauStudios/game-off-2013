using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    private Movement.Direction _direction;
    public GameObject _pacman, _floor;

    void Start () {
        _direction = Movement.Direction.Up;
    }

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;
        Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
    }
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
