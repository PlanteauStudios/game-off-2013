using UnityEngine;

public class CollabCell {
    public const int MIN_SCORE = 0;
    public const int MAX_SCORE = 1000;
    public const int IMPASS_SCORE = int.MinValue;

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
    void DecrementScore() { --_score; }
    void IncrementScore() { ++_score; }
}