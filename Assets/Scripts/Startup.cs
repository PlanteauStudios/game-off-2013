using UnityEngine;
using System.Collections;
public class Startup : MonoBehaviour {
    public GameObject _life_icons;
    private Rect fullscreen;
	void OnGUI () {
        fullscreen = new Rect (0, 0, Screen.width, Screen.height);
        Transform[] icons = _life_icons.GetComponentsInChildren<Transform>();
        int count = 0;
        foreach(Transform t in icons) {
            if (t.gameObject.tag == "Life") {
                GUITexture g = t.gameObject.GetComponent<GUITexture>();
                // g.transform.position = new Vector3(-130f + 20 * count, -35.5f, -35.5f);
                g.transform.position = new Vector3(0f, 0f, 0f);
                g.pixelInset = new Rect(Screen.width - (100 + count * 75), Screen.height - 75, 70, 70);//fullscreen;
                ++count;
            }
        }
	}
}