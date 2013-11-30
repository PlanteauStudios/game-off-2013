﻿using UnityEngine;
using System.Collections;
public class Startup : MonoBehaviour {
    public GameObject _life_icons, _pacman_life_icons;
    public GameObject _score_title, _score_number;
	void OnGUI () {
        Transform[] icons = _life_icons.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach(Transform t in icons) {
            if (t.gameObject.tag == "Life") {
                GUITexture g = t.gameObject.GetComponent<GUITexture>();
                // g.transform.position = new Vector3(-130f + 20 * count, -35.5f, -35.5f);
                g.transform.position = new Vector3(0f, 0f, 0f);
                g.pixelInset = new Rect(Screen.width - (80 + count * 75), Screen.height - 75, 70, 70);//fullscreen;
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
                g.pixelInset = new Rect(Screen.width - (80 + count * 75), Screen.height - 160, 70, 70);//fullscreen;
                ++count;
            }
        }
	}
}