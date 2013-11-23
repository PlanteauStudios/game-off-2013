using UnityEngine;
using System.Collections;
using System;
public class Movement : MonoBehaviour { 
    public enum Direction { Left, Right, Up, Down };
    public Vector3 MoveDirection(Direction d) {
        switch (d) {
            case Direction.Left :
                return Vector3.left;
            case Direction.Right :
                return Vector3.right;
            case Direction.Up :
                return Vector3.forward;
            case Direction.Down :
                return Vector3.back;
        }
        return Vector3.zero;
    }
    public Vector3 FaceDirection(Direction d) {
        switch (d) {
            case Direction.Left :
                return new Vector3(270.0f,0.0f ,0.0f);
            case Direction.Right :
                return new Vector3(270.0f,180.0f, 0.0f);
            case Direction.Up :
                return new Vector3(270.0f,90.0f, 0.0f);
            case Direction.Down :
                return new Vector3(270.0f,270.0f, 0.0f);
        }
        return new Vector3(270.0f, 0.0f, 0.0f);
    }
    public Direction MovingIn(Vector3 v) {
        bool hori = Math.Abs(v.x) >= Math.Abs(v.z);
        if (hori) {
            if (v.x > 0) return Direction.Right;
            return Direction.Left;
        }
        if (v.z > 0) return Direction.Up;
        return Direction.Down;
    }

}
