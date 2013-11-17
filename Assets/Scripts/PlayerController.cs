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
		// _win_text.text = "";
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
                movement.Set(-1, 0.0f, 0);
                rot.eulerAngles = new Vector3(270.0f,270.0f ,0.0f);
                break;
            case KeyCode.RightArrow:
                movement.Set(1, 0.0f, 0);
                rot.eulerAngles = new Vector3(270.0f,90.0f, 0.0f);
                break;
            case KeyCode.UpArrow:
                movement.Set(0.0f, 0.0f, 1.0f);
                rot.eulerAngles = new Vector3(270.0f,0.0f, 0.0f);
                break;
            case KeyCode.DownArrow:
                movement.Set(0.0f, 0.0f, -1.0f);
                rot.eulerAngles = new Vector3(270.0f,180.0f, 0.0f);
                break;
        }
        rigidbody.velocity = movement * _speed;
        rigidbody.rotation = rot;
    }
	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		if (other.gameObject.tag == "pickup") {
			other.gameObject.SetActive(false);
			++_count;
			SetCountText();
		}
	}
	void SetCountText() {
		/*_count_text.text = "Count: " + _count.ToString();		
		if (_count >= 9) {
			_win_text.text = "You Win!";	
		}*/
	}
}
