using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCard : MonoBehaviour {

    public Animator zombieAnimator;

	void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().Play("FlipAnimation", -1, 0f);
        }
    }

    public void OnAnimationEnd() {
        //GetComponent<Animator>().Play("FlipAnimation", -1, 0f);
    }
}
