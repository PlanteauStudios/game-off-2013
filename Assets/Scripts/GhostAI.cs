using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
    public float _speed;
    private Movement _movement_script;
    private Movement.Direction _direction;
    public GameObject _pacman, _floor;

    void Start () {
        _movement_script = GetComponent<Movement>();
        _direction = Movement.Direction.Up;
    }
    int Randomize() {
        int ran = Random.Range(0, 3);
        return ran;
    }
    bool CollisionIn(Movement.Direction dir) {
        Vector3 attempted_direction = Movement.MoveDirection(dir);
        Ray ray = new Ray(transform.position, attempted_direction);
        RaycastHit hit;
        Bounds b = collider.bounds;

        if (Physics.Raycast(ray, out hit,5f)) {
            return true;
        }
        return false;
    }
    bool Reverseing(Movement.Direction before, Movement.Direction after) {
        bool ret = false;
        if ((before == Movement.Direction.Up && after == Movement.Direction.Down) ||
            (before == Movement.Direction.Left && after == Movement.Direction.Right))
            ret = true;
        if ((after == Movement.Direction.Up && before == Movement.Direction.Down) ||
             (after == Movement.Direction.Left && before == Movement.Direction.Right))
            ret = true;
        return ret;
    }
    void SwitchDirection(int dir) {
        if (dir > 3) dir = 0;
        Movement.Direction next_direction = (Movement.Direction)dir;//_direction;

        if (next_direction == _direction || Reverseing(_direction, next_direction) ||
            CollisionIn(next_direction)) {
            SwitchDirection(dir + 1);
        } else {
            _direction = next_direction;
        }
    }
    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;
        SwitchDirection(Randomize());
    }
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
