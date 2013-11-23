using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NodeScorer : MonoBehaviour {
    private class Pair {
        public int first, second;
    }
    public GridManager _manager;
    public GameObject Cell;
    private List<List<GameObject> > _cells;

    public GameObject _target

    void Start() {
        _manager = GetComponent<GridManager>();
        _cells = _manager._cells;
    }
    Vector2 Pos(GameObject g) {
        return new Vector2(g.rigidbody.position.x, g.rigidbody.position.z);
    }
    GameObject GetCellAt(Vector2 location) {
        return GetCellAt(location, 0, _cells.Count, 0, _cells[_cells.Count].Count);
    }
    GameObject GetCellAt(Vector2 location, int x_low, int x_high, int y_high, int y_low) {
        int x_mid = (x_high - x_low) / 2,
            y_mid = (y_low - y_high) / 2;
        if (Pos(_cells[x_mid][y_mid]) == location) 
            return _cells[x_mid][y_mid];
        Pair xes = new Pair(), yes = new Pair();
        if (x_mid > location.x) {
            xes.first = x_low; xes.second = x_mid -1;
        }
        else {
            xes.first = x_mid + 1; xes.second = x_high;
        }
        if (y_mid > location.y){
            yes.first = y_low; yes.second = y_mid - 1;
        }
        else {
            yes.first = y_mid + 1; yes.second = y_high;
        }

        return GetCellAt( location, xes.first, xes.second, yes.first, yes.second);
    }
}