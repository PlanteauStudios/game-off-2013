using UnityEngine;
using System.Collections;
using System;
public class Movement {
    public enum Direction { Up = 0, Down = 1, Left = 2, Right = 3 };
    public static int Randomize() {
        int ran = UnityEngine.Random.Range(0, 3);
        return ran;
    }
    public static Vector3 MoveDirection(Direction d) {
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
    public static Vector3 FaceDirection(Direction d) {
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
    public static Direction MovingIn(Vector3 v) {
        bool hori = Math.Abs(v.x) >= Math.Abs(v.z);
        if (hori) {
            if (v.x > 0) return Direction.Right;
            return Direction.Left;
        }
        if (v.z > 0) return Direction.Up;
        return Direction.Down;
    }
    public static bool Reverseing(Direction before, Direction after) {
        bool ret = false;
        if ((before == Direction.Up && after == Direction.Down) ||
            (before == Direction.Left && after == Direction.Right))
            ret = true;
        if ((after == Direction.Up && before == Direction.Down) ||
             (after == Direction.Left && before == Direction.Right))
            ret = true;
        return ret;
    }
    public static void SwitchDirection(int dir, int depth, Vector3 current_position, ref Direction current_direction) {
        if (depth > 50) return;
        if (dir > 3) dir = 0;
        Direction next_direction = (Direction)dir;//current_direction;

        if (next_direction == current_direction || Reverseing(current_direction, next_direction) ||
            CollisionIn(next_direction, current_position)) {
            SwitchDirection(dir + 1, ++depth, current_position, ref current_direction);
        } else {
            current_direction = next_direction;
        }
    }
    public static bool CollisionIn(Direction dir, Vector3 current_position) {
        Vector3 attempted_direction = MoveDirection(dir);
        Ray ray = new Ray(current_position, attempted_direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,5f)) {
            return true;
        }
        return false;
    }
}
