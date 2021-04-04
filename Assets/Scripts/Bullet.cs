using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed = 30;
    private Renderer renderer;
    [HideInInspector] public Material myMat;


    private void OnEnable() {
        if(renderer == null)
            renderer = GetComponent<Renderer>();
        myMat = MatManager.instance.ReturnMat();
        renderer.material = myMat;
    }

    private void Update() {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("ent");
        this.gameObject.SetActive(false);
    }
}
