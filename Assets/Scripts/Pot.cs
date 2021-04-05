using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Renderer renderer;
    private Material myMat;

    private void Start() {
        float angleY = Random.Range(0, 360);
        transform.Rotate(0, 0, angleY);

        renderer = GetComponent<Renderer>();
        myMat = MatManager.instance.ReturnMat();
        renderer.material = myMat;

        GameManager.instance.potsInScene++;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Bullet")) {
            if(myMat == other.gameObject.GetComponent<Bullet>().myMat) {
                GameManager.instance.PlayBreakSE();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy() {
        GameManager.instance.potsInScene--;
    }
}
