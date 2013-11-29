using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float _speed;
	public GUIText _count_text, _win_text;
	private int _count;
	private KeyCode _last_key;

    private Movement.Direction _direction;
	void Start() {
		_count = 0;
		_win_text.text = "";
        _direction = Movement.Direction.Stand;
	}
    void OnGUI() {
        Vector3 move = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        KeyCode key = _last_key;
        if (Input.anyKeyDown) {
            key = Event.current.keyCode;
        }
        _last_key = key;
        Movement.Direction d = Movement.Direction.Up;
        switch (_last_key) {
            case KeyCode.LeftArrow:
                d = Movement.Direction.Left;
                break;
            case KeyCode.RightArrow:
                d = Movement.Direction.Right;
                break;
            case KeyCode.UpArrow:
                d = Movement.Direction.Up;
                break;
            case KeyCode.DownArrow:
                d = Movement.Direction.Down;
                break;
        }
        if (!Movement.CollisionIn(d, transform.position)) {
            _direction = d;
        }
        move = Movement.MoveDirection(_direction);
        rot.eulerAngles = Movement.FaceDirection(_direction);
            rigidbody.velocity = move * _speed;
            rigidbody.rotation = rot;
    }



}
