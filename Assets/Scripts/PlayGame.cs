using UnityEngine;
using System.Collections;

public class PlayGame : MonoBehaviour {
    public Texture btnTexture;
    void OnGUI() {
        if (Input.anyKeyDown)
            Application.LoadLevel(1);
    }
    void OnMouseDown() {
        Application.LoadLevel(1);
    }
}
