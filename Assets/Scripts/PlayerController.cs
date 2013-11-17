using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float _speed;
	public GUIText _count_text, _win_text;
	private int _count;
	private KeyCode _last_key;
    private enum Direction { Left, Right, Up, Down };
	void Start() {
		_count = 0;
		SetCountText();
		_win_text.text = "";
	}
    void OnGUI() {
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        KeyCode key = _last_key;
        if (Input.anyKeyDown) {
            key = Event.current.keyCode;
        }
        _last_key = key;
        switch (_last_key) {
            case KeyCode.LeftArrow:
                movement = MoveDirection(Direction.Left);
                rot.eulerAngles = FaceDirection(Direction.Left);
                break;
            case KeyCode.RightArrow:
                movement = MoveDirection(Direction.Right);
                rot.eulerAngles = FaceDirection(Direction.Right);
                break;
            case KeyCode.UpArrow:
                movement = MoveDirection(Direction.Up);
                rot.eulerAngles = FaceDirection(Direction.Up);
                break;
            case KeyCode.DownArrow:
                movement = MoveDirection(Direction.Down);
                rot.eulerAngles = FaceDirection(Direction.Down);
                break;
        }
        rigidbody.velocity = movement * _speed;
        rigidbody.rotation = rot;
    }
    Vector3 MoveDirection(Direction d) {
        switch (d) {
            case Direction.Left :
                return new Vector3(-1, 0.0f, 0);
            case Direction.Right :
                return new Vector3(1, 0.0f, 0);
            case Direction.Up :
                return new Vector3(0.0f, 0.0f, 1.0f);
            case Direction.Down :
                return new Vector3(0.0f, 0.0f, -1.0f);
        }
        return new Vector3(0.0f, 0.0f, 0.0f);
    }
    Vector3 FaceDirection(Direction d) {
        switch (d) {
            case Direction.Left :
                return new Vector3(270.0f,270.0f ,0.0f);
            case Direction.Right :
                return new Vector3(270.0f,90.0f, 0.0f);
            case Direction.Up :
                return new Vector3(270.0f,0.0f, 0.0f);
            case Direction.Down :
                return new Vector3(270.0f,180.0f, 0.0f);
        }
        return new Vector3(270.0f, 0.0f, 0.0f);
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
