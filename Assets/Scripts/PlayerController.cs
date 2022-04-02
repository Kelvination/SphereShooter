using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start() {

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {

        // Player and Camera rotation
        if (canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }


        if(Input.GetMouseButtonDown(0)) {
            Vector3 origin = playerCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
            RaycastHit hit;
            
            if(Physics.Raycast(origin, playerCamera.transform.forward, out hit, 5000)) {

                var target = hit.collider.GetComponent<Target>();
                if (target != null) {
                    target.turnOff();
                } else {
                    var bullseye = hit.collider.GetComponent<Bullseye>();
                    bullseye.turnOff();
                }

            }
        }
    }
}