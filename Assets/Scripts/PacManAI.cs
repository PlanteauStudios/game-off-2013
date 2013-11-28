﻿using UnityEngine;
using System.Collections;

public class PacManAI : MonoBehaviour {
    enum State { Explore, Hunt, Hide };
    public float _speed;
    private Movement.Direction _direction;
    public GameObject _player;
	// Use this for initialization
	void Start () {
        _direction = Movement.Direction.Up;
	}

	// Update is called once per frame
	void Update () {
        Vector3 player_pos = _player.rigidbody.position;

        Vector3 direct = (player_pos - rigidbody.position) * .25f;
        _direction = Movement.MovingIn(direct);
        rigidbody.velocity = Movement.MoveDirection(_direction) * _speed;
        Quaternion rot = Quaternion.identity;
        rot.eulerAngles = Movement.FaceDirection(_direction);
        rigidbody.rotation = rot;
	}
}
