using UnityEngine;
using System.Collections;

public class PacManAI : MonoBehaviour {
    enum State { Explore, Hunt, Hide };
    public float _speed;

    public GUIText _score_text, _win_text;
    public int _score;
    private Movement.Direction _direction;
    public GameObject _player;

    private const int STARTING_SCORE = 50;
    private const int PELLET_POINTS = 5;
	// Use this for initialization
	void Start () {
        _direction = Movement.Direction.Up;
        _score = STARTING_SCORE;
        SetCountText();
	}

	// Update is called once per frame

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;
        Debug.Log("coll" + other.collider.gameObject.tag);
        if (other.collider.gameObject.tag != "Pellet") {
            Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        }
    }
    void OnTriggerEnter(Collider other) {
         if (other.gameObject.tag == "Pellet") {
            other.gameObject.SetActive(false);
            _score -= PELLET_POINTS;
            SetCountText();
        }
    }
    void SetCountText() {
        _score_text.text = "Score: " + _score.ToString();
        if (_score <= 0) {
            _win_text.text = "You Lose!";
        }
    }
    void Update () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
}
