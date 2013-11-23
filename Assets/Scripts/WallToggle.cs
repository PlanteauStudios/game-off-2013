using UnityEngine;
using System.Collections;

public class WallToggle : MonoBehaviour {
    public bool toggler;
    public int frequency;
    private int timer;
    private float min_distance;
    public GameObject player, wall;
	// Use this for initialization
	void Start () {
        timer = 0;
        min_distance = 5;
	}
	
	// Update is called once per frame
	void Update () {
	    if (toggler) {
            if (timer >= frequency) {
                if (Vector3.Distance(player.transform.position, wall.transform.position) > min_distance) {
                    wall.renderer.enabled = !wall.renderer.enabled;
                    wall.collider.enabled = !wall.collider.enabled;
                }
                timer = 0;
            }
            ++timer;
        }
	}
}
