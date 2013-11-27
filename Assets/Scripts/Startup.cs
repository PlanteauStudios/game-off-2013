using UnityEngine;
using System.Collections;

public class Startup : MonoBehaviour {
    public float height;
    public Vector3[] vertexList;
    public GameObject _pacman;
    private bool initialize;
	// Use this for initialization
	void Start () {
        initialize = false;
	}

	// Update is called once per frame
	void Update () {
        if (!initialize) {
            GenerateMaze();
            initialize = true;
        }
        // NodeScorer ns = GetComponent<NodeScorer>();
        // ns.GenerateScores(_pacman);
	}
    void GenerateMaze() {
        // Debug.Log("GenerateMaze");
        GridManager grid = GetComponent<GridManager>();
        // Debug.Log("Building grid");
        grid.Build();
        // Debug.Log("Building grid complet");
        NodeScorer ns = GetComponent<NodeScorer>();
        // Debug.Log("Initializing NodeScorer");
        ns.Initialize(ref grid);
        // Debug.Log("Initializing NodeScorer Complete");
        // Debug.Log("Generating Scores");
        ns.GenerateScores(_pacman);
        // Debug.Log("Generating Scores Complete");
    }
}