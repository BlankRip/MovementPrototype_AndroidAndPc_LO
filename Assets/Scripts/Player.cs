using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform mesh;
    [SerializeField] Transform shotPoint;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] Joystick joystick;
    [SerializeField] [Range(0, 1)] float moveThreshold = 0.5f;

    private CharacterController cc;
    private float horizontalInput, verticalInput;

    private void Start() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        if(!GameManager.instance.pause) {
        #if UNITY_IOS || UNITY_ANDROID
            if(joystick.Horizontal > moveThreshold)
                horizontalInput = moveSpeed * Time.deltaTime;
            else if(joystick.Horizontal < -moveThreshold)
                horizontalInput = -moveSpeed * Time.deltaTime;
            else
                horizontalInput = 0;

            if(joystick.Vertical > moveThreshold)
                verticalInput = moveSpeed * Time.deltaTime;
            else if(joystick.Vertical < -moveThreshold)
                verticalInput = -moveSpeed * Time.deltaTime;
            else
                verticalInput = 0;
        #else
            horizontalInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            verticalInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
        #endif
        } else {
            horizontalInput = verticalInput = 0;
        }

        Movement();
    }

    public void Shoot() {
        ObjectPooler.instance.SpawnPoolObj("Bullet", shotPoint.position, mesh.rotation);
    }

    private void Movement() {
        Vector3 move = (transform.forward * verticalInput + transform.right * horizontalInput);
        cc.Move(move);

        if(move != Vector3.zero)
            mesh.rotation = Quaternion.Slerp(mesh.rotation, Quaternion.LookRotation(move, Vector3.up), Time.deltaTime * rotationSpeed);
    }
}
