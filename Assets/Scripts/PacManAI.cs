using UnityEngine;
using System.Collections;

public class PacManAI : MonoBehaviour {
    enum State { Explore, Hunt, Hide };

    public float _speed;

    public GUIText _score_text, _win_text;
    public int _score;

    public GameObject _life_icons;
    public int _lives;

    private Movement.Direction _direction;
    public GameObject _player;
    public GameObject _pacman_start;
    public GameObject _ghosts;

    public int _delayed_start;
    private int _wait = 0;

    private const int STARTING_SCORE = 0;
    private const int PELLET_POINTS = 5;
    private const int ROBOT_GHOST_POINTS = 50;
    private const int PERSON_GHOST_POINTS = 0;

    private bool _open_mouth = false;
    private int _mouth_flip_counter = 0;
    public GameObject _open_face;
    public GameObject _closed_face;


	void Start () {
        _direction = Movement.Direction.Up;
        _score = STARTING_SCORE;
        SetCountText();
	}


    void OnCollisionEnter(Collision other) {
        if (other.collider.gameObject.tag == "Floor") return;
        if (other.collider.gameObject.tag != "Pellet") {
            string other_tag = other.collider.gameObject.tag;
            if (other_tag == "Ghost" || other_tag == "Player") {
                GhostAI g_ai = other.collider.gameObject.GetComponent<GhostAI>();
                if (g_ai.IsVulnerable()) {
                    _score -= other_tag == "Ghost" ? ROBOT_GHOST_POINTS : PERSON_GHOST_POINTS;
                    if (other_tag == "Player") {
                        PlayerController pc = other.collider.gameObject.GetComponent<PlayerController>();
                        pc.LoseLife();
                    }
                    AudioSource death_sound = other.collider.gameObject.GetComponent<AudioSource>();
                    death_sound.Play();
                    other.transform.position = g_ai._ghost_start.transform.position;
                    g_ai.Reset();
                } else {
                    _score -= other_tag == "Ghost" ? ROBOT_GHOST_POINTS : PERSON_GHOST_POINTS;
                    transform.position = _pacman_start.transform.position;
                    if (other_tag == "Player") LoseLife();
                }
                    SetCountText();
            }
        }
    }
    void OnTriggerEnter(Collider other) {
         if (other.gameObject.tag == "Pellet") {
            other.gameObject.SetActive(false);
            _score += PELLET_POINTS;
            SetCountText();
        } else if (other.gameObject.tag == "Super Pellet") {
            Transform[] ghosts = _ghosts.GetComponentsInChildren<Transform>();
            foreach (Transform g in ghosts) {
                if (g.gameObject.tag != "Ghost" && g.gameObject.tag != "Player") continue;
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

    void LoseLife() {
        if (_lives > 0) {
            Transform[] icons = _life_icons.GetComponentsInChildren<Transform>();
            for (int i = icons.Length - 1; i >= 0; --i) {
                if (icons[i].gameObject.tag == "Life") {
                    if (icons[i].gameObject.activeInHierarchy) {
                        icons[i].gameObject.SetActive(false);
                        break;
                    }
                }
            }
            AudioSource death_sound = GetComponent<AudioSource>();
            death_sound.Play();
            --_lives;
        }
    }
    void SetCountText() {
        if (_score < 0) _score = 0;
        _score_text.text = _score.ToString();

        if (_lives <= 0) {
            _win_text.text = "You Win!\r\nPacman Only Got " + _score.ToString() + " points!";
            Time.timeScale = 0f;
        } else {
            _win_text.text = "";
        }
    }
    void FixedUpdate () {
        if (_wait <= _delayed_start) {
            ++_wait;
            return;
        }
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
