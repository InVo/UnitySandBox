using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour {

    private bool _walking = false;
    private Animator _animator;
    private const float SPEED = 0.1f;

    public bool Walking {
        get {
            return _animator.GetBool("ShouldWalk");
        }
        set {
            _walking = value;
            _animator.SetBool("ShouldWalk", _walking);
        }
    }

    void Awake() {
        _animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        Walking = true;
	}
	
	// Update is called once per frame
	void Update () {
        var v = Input.GetAxis("Vertical");
        if (v != 0) {
            if (!Walking) {
                Walking = true;
            }
            Vector3 walkingDirection = transform.TransformDirection(v, 0, 0).normalized;
            transform.position += walkingDirection * SPEED;
        }

        if (v == 0) {
            if (Walking) {
                Walking = false;
            }
        }


	}
}
