using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform mesh;
    [SerializeField] Transform shotPoint;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float rotationSpeed = 10;
    private CharacterController cc;
    private float horizontalInput, verticalInput;

    private void Start() {
        cc = GetComponent<CharacterController>();
    }

    private void Update() {
        if(!GameManager.instance.pause) {
            horizontalInput = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            verticalInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space))
                Shoot();
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
