using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float ROTATION_SPEED = 3f;

    private float rotationH;
    private float rotationV;
    private float distance;

    private Vector3 axisH;
    private Vector3 axisV;
    private Vector3 fromCameraToTarget;

    public Transform target;

    void Start() {
        fromCameraToTarget = transform.position - target.position;
        distance = fromCameraToTarget.magnitude;

        UpdateAxisH();
        UpdateAxisV();

        CameraRotation1(0f, 0f);
        transform.LookAt(target);
    }

	// Update is called once per frame
	void Update () {
        FollowTarget();
        if (Input.GetMouseButton(0)) {
            var mouseX = -Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
            CameraRotation1(mouseX, mouseY);
        }
	}

    void CameraRotation1(float mouseX, float mouseY) {
        rotationH = mouseX * ROTATION_SPEED;
        rotationV = mouseY * ROTATION_SPEED;

        //ResetCameraPosition();

        UpdateAxisH();
        Quaternion rotH = Quaternion.AngleAxis(rotationH, axisH);

        // Instead of updating axisV
        axisV = rotH * axisV;
        Quaternion rotV = Quaternion.AngleAxis(rotationV, axisV);
        fromCameraToTarget = transform.position - target.position;

        // Performing rotation
        fromCameraToTarget = rotV * rotH * fromCameraToTarget;
        transform.position = target.position + fromCameraToTarget;

        // Instead of LookAt(target)
        var q = Quaternion.FromToRotation(transform.localToWorldMatrix * new Vector4(0, 0, 1, 0), -fromCameraToTarget);
        transform.rotation = q * transform.rotation;
        var q2 = Quaternion.AngleAxis(transform.rotation.z, transform.localToWorldMatrix * new Vector4(0, 0, 1, 0));
        transform.rotation = q2 * transform.rotation;

        fromCameraToTarget = transform.position - target.position;
    }

    void UpdateAxisH() {
        axisH = (target.localToWorldMatrix * new Vector4(0, 0, 1, 0)).normalized;
    }

    void UpdateAxisV() {
        axisV = Vector3.Cross(fromCameraToTarget, axisH).normalized;
    }

    void ResetCameraPosition() {
        // rotation angle about H axis remains, rotation angle about V axis goes to zero
        var defaultAxis = Vector3.Cross(axisV, axisH);
        transform.position = target.position - defaultAxis * distance;
        transform.LookAt(target.position);
    }

    void FollowTarget() {
        transform.position = target.position + fromCameraToTarget.normalized * distance;
        //transform.LookAt(target);
    }
}
