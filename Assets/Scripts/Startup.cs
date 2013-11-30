using UnityEngine;
using System.Collections;
public class Startup : MonoBehaviour {
    public GameObject _life_icons, _pacman_life_icons;
    public GameObject _score_title, _score_number;
    public GameObject _pacman, _ghosts, _pills, _big_pills;
    public GameObject _player_start_pos;
    private bool _finished = false;
	void OnGUI () {
        Transform[] icons = _life_icons.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach(Transform t in icons) {
            if (t.gameObject.tag == "Life") {
                GUITexture g = t.gameObject.GetComponent<GUITexture>();
                // g.transform.position = new Vector3(-130f + 20 * count, -35.5f, -35.5f);
                g.transform.position = new Vector3(0f, 0f, 0f);
                g.pixelInset = new Rect(Screen.width - (60 + count * 75), Screen.height - 75, 50, 50);//fullscreen;5
                ++count;
            }
        }
        Transform[] pacman_icons = _pacman_life_icons.GetComponentsInChildren<Transform>();
        count = 0;
        foreach(Transform t in pacman_icons) {
            if (t.gameObject.tag == "Life") {
                GUITexture g = t.gameObject.GetComponent<GUITexture>();
                // g.transform.position = new Vector3(-130f + 20 * count, -35.5f, -35.5f);
                g.transform.position = new Vector3(0f, 0f, 0f);
                g.pixelInset = new Rect(Screen.width - (60 + count * 75), Screen.height - 160, 50, 50);//fullscreen;5
                ++count;
            }
        }
        if (_finished) {
            if (Input.anyKeyDown) {
                ResetAll();
            }
        }
	}
    public void SetFinished() {
        _finished = true;
    }
    public void StartChomp() {
        AudioSource chomper = GetComponent<AudioSource>();
        chomper.Play();
    }
    public void StopChomp() {
        AudioSource chomper = GetComponent<AudioSource>();
        chomper.Stop();
    }
    public void ResetAll() {
        Time.timeScale = 1f;
        PacManAI pacman_ai = _pacman.GetComponent<PacManAI>();
        pacman_ai.Reset();
        Transform[] ghosts = _ghosts.GetComponentsInChildren<Transform>();
        foreach (Transform t in ghosts) {
            if (t.gameObject.tag == "Ghost" || t.gameObject.tag == "Player") {
                GhostAI gai = t.gameObject.GetComponent<GhostAI>();
                gai.Reset();
                if (t.gameObject.tag == "Player") {
                    PlayerController pc = t.gameObject.GetComponent<PlayerController>();
                    pc.Reset();
                }
            }
        }
        Transform[] pills = _pills.GetComponentsInChildren<Transform>();
        foreach (Transform t in pills) {
            if (t.gameObject.tag == "Pellet") {
                t.gameObject.SetActive(true);
            }
        }
        Transform[] big_ills = _big_pills.GetComponentsInChildren<Transform>();
        foreach (Transform t in big_ills) {
            if (t.gameObject.tag == "Super Pellet") {
                t.gameObject.SetActive(true);
            }
        }
        _finished = false;
    }
}