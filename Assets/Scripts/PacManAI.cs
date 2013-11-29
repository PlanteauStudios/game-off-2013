using UnityEngine;
using System.Collections;

public class PacManAI : MonoBehaviour {
    enum State { Explore, Hunt, Hide };

    public float _speed;

    public GUIText _score_text, _win_text;
    public int _score;
    private Movement.Direction _direction;
    public GameObject _player;
    public GameObject _pacman_start;
    public GameObject _ghosts;

    private const int STARTING_SCORE = 500;
    private const int PELLET_POINTS = 1;
    private const int ROBOT_GHOST_POINTS = 5;
    private const int PERSON_GHOST_POINTS = 10;

    private bool _open_mouth = false;
    private int _mouth_flip_counter = 0;
    public GameObject _open_face;
    public GameObject _closed_face;


	// Use this for initialization
	void Start () {
        _direction = Movement.Direction.Up;
        _score = STARTING_SCORE;
        SetCountText();
	}

	// Update is called once per frame

    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;
        if (other.collider.gameObject.tag != "Pellet") {
            string other_tag = other.collider.gameObject.tag;
            if (other_tag == "Ghost" || other_tag == "Player") {
                GhostAI g_ai = other.collider.gameObject.GetComponent<GhostAI>();
                if (g_ai.IsVulnerable()) {
                    _score -= other_tag == "Ghost" ? ROBOT_GHOST_POINTS : PERSON_GHOST_POINTS;
                    other.transform.position = g_ai._ghost_start.transform.position;
                } else {
                    _score += other_tag == "Ghost" ? ROBOT_GHOST_POINTS : PERSON_GHOST_POINTS;
                    transform.position = _pacman_start.transform.position;
                }
                    SetCountText();
            }
        }
    }
    void OnTriggerEnter(Collider other) {
         if (other.gameObject.tag == "Pellet") {
            other.gameObject.SetActive(false);
            _score -= PELLET_POINTS;
            SetCountText();
        } else if (other.gameObject.tag == "Super Pellet") {
            Transform[] ghosts = _ghosts.GetComponentsInChildren<Transform>();
            foreach (Transform g in ghosts) {
                if (g.gameObject.tag != "Ghost") continue;
                GhostAI g_ai = g.gameObject.GetComponent<GhostAI>();
                g_ai.SetVulnerable();
                other.gameObject.SetActive(false);
            }
        } else if (other.gameObject.tag == "Pen Gate") {
            if (_direction == Movement.Direction.Down) {
                _direction = Movement.Direction.Left;
            }
        } else {
                Movement.SwitchDirection(Movement.Randomize(), 0, transform.position, ref _direction);
        }
    }
    void SetCountText() {
        _score_text.text = "Score: " + _score.ToString();
        if (_score <= 0) {
            _win_text.text = "You Lose!";
        }
    }
    void FixedUpdate () {
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(270.0f, 0.0f, 0.0f);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
    }
    void Update() {
        ++_mouth_flip_counter;
        if (_mouth_flip_counter >= 5) {
            _closed_face.SetActive(!_open_mouth);
            _open_face.SetActive(_open_mouth);
            _open_mouth = !_open_mouth;
            _mouth_flip_counter = 0;
        }
    }
}
