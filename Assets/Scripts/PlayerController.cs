using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float _speed;
	public GUIText _count_text, _win_text;
	private int _count;
	
	void Start() {
		_count = 0;
		SetCountText();
		_win_text.text = "";
	}
	
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal"),
			  moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.AddForce(movement * _speed * Time.deltaTime);
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
		_count_text.text = "Count: " + _count.ToString();		
		if (_count >= 9) {
			_win_text.text = "You Win!";	
		}
	}
}
