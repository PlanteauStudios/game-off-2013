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
        if (Input.anyKeyDown) {
            KeyCode key = Event.current.keyCode;
            if (key != _last_key) {
                _last_key = key;
                Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
                switch (_last_key) {
                    case KeyCode.LeftArrow:
                        movement.Set(-1, 0.0f, 0);
                        break;
                    case KeyCode.RightArrow:
                        movement.Set(1, 0.0f, 0);
                        break;
                    case KeyCode.UpArrow:
                        movement.Set(0.0f, 0.0f, 1.0f);
                        break;
                    case KeyCode.DownArrow:
                        movement.Set(0.0f, 0.0f, -1.0f);
                        break;
                }
                rigidbody.velocity = movement * _speed;
            }
        }
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
