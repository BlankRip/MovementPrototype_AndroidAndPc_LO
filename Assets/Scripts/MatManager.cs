using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatManager : MonoBehaviour
{
    public static MatManager instance;
    [SerializeField] List<Material> mats;
    private int length;

    private void Awake() {
        if(instance == null)
            instance = this;
        length = mats.Count;
    }

    public Material ReturnMat() {
        int picked = Random.Range(0, length * 10);
        picked = (int)((float)picked / 10f);

        return mats[picked];
    }
}
