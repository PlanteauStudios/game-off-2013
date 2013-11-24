using UnityEngine;
using System.Collections.Generic;
public class CellGrid {
    public const int RADIUS = 5;
    public List<List<CollabCell> > _cells;

    public CellGrid() {
        _cells = new List<List<CollabCell> >();
    }
    public CellGrid(List<List<CollabCell> > cells) {
        _cells = cells;
    }

    public void Add(List<CollabCell> row) {
        _cells.Add(row);
    }
    public int Count() {
        return _cells.Count;
    }
    public void SetScore(int x, int y, Movement.Direction d, int score, int distance) {
        if (!_cells[x][y]._pass || distance >= RADIUS || _cells[x][y]._score >= score) return;

        _cells[x][y]._score = score;
        if (d != Movement.Direction.Right) SetScore(x + 1, y, Movement.Direction.Left, score - 1, distance + 1);
        if (d != Movement.Direction.Left) SetScore(x - 1, y, Movement.Direction.Right, score - 1, distance + 1);
        if (d != Movement.Direction.Up) SetScore(x, y - 1, Movement.Direction.Down, score - 1, distance + 1);
        if (d != Movement.Direction.Down) SetScore(x, y + 1, Movement.Direction.Up, score - 1, distance + 1);
    }
    public int ScoreInDirection(int x, int y, Movement.Direction d) {
        int x_dir = 0, y_dir = 0;
        switch (d) {
            case Movement.Direction.Left:
                x_dir = 1;
                break;
            case Movement.Direction.Right:
                x_dir = -1;
                break;
            case Movement.Direction.Up:
                y_dir = 1;
                break;
            case Movement.Direction.Down:
                y_dir = -1;
                break;
        }
        return _cells[x + x_dir][y + y_dir]._score;
    }

    //TODO this could be more 'elegant' as as loop or smtg
    public Movement.Direction BestDirection(int x, int y) {
        Movement.Direction d = Movement.Direction.Up;
        if (ScoreInDirection(x, y, Movement.Direction.Left) > 
            ScoreInDirection(x, y, Movement.Direction.Up))
            d = Movement.Direction.Left;
        if (ScoreInDirection(x, y, Movement.Direction.Down) > 
            ScoreInDirection(x, y, Movement.Direction.Left))
            d = Movement.Direction.Down;
        if (ScoreInDirection(x, y, Movement.Direction.Right) > 
            ScoreInDirection(x, y, Movement.Direction.Down))
            d = Movement.Direction.Right;
        return d;
    }
}