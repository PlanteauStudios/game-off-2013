using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float _speed;
	public GUIText _count_text, _win_text;
	private int _count;
	private KeyCode _last_key;


	void Start() {
		_count = 0;
		SetCountText();
		_win_text.text = "";
	}
    void OnGUI() {
        Vector3 movement = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        KeyCode key = _last_key;
        if (Input.anyKeyDown) {
            key = Event.current.keyCode;
        }
        _last_key = key;
        switch (_last_key) {
            case KeyCode.LeftArrow:
                movement = Movement.MoveDirection(Movement.Direction.Left);
                rot.eulerAngles = Movement.FaceDirection(Movement.Direction.Left);
                break;
            case KeyCode.RightArrow:
                movement = Movement.MoveDirection(Movement.Direction.Right);
                rot.eulerAngles = Movement.FaceDirection(Movement.Direction.Right);
                break;
            case KeyCode.UpArrow:
                movement = Movement.MoveDirection(Movement.Direction.Up);
                rot.eulerAngles = Movement.FaceDirection(Movement.Direction.Up);
                break;
            case KeyCode.DownArrow:
                movement = Movement.MoveDirection(Movement.Direction.Down);
                rot.eulerAngles = Movement.FaceDirection(Movement.Direction.Down);
                break;
        }
        rigidbody.velocity = movement * _speed;
        rigidbody.rotation = rot;
    }


	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "pickup") {
			other.gameObject.SetActive(false);
			++_count;
			SetCountText();
		}
	}

	void SetCountText() {
		_count_text.text = "Count: " + _count.ToString();
		if (_count >= 9) {
			_win_text.text = "You Win!";
		}
	}
}
