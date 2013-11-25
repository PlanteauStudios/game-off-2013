using UnityEngine;

public class CollabCell {
    public const int MIN_SCORE = 0;
    public const int MAX_SCORE = 1000;
    public const int IMPASS_SCORE = int.MinValue;
    public const int BASE_SCORE = 50;

    public bool _pass = true;

    public int _score = MIN_SCORE;
    public GameObject _cell;
    public CollabCell(GameObject cell) {
        _cell = cell;
    }
    public CollabCell(GameObject cell, int score) {
        _cell = cell;
        _score = score;
    }
    public CollabCell(GameObject cell, int score, bool pass) {
        _cell = cell;
        _score = score;
        _pass = pass;
    }
    public Vector3 Pos() {
        return _cell.rigidbody.position;
    }
    void DecrementScore() { --_score; }
    void IncrementScore() { ++_score; }
}