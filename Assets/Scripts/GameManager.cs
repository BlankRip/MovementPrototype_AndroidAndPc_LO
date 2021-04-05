using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject fireButton;
    [SerializeField] AudioSource seSource;
    [SerializeField] AudioClip bulletSE, breakSE;
    [HideInInspector] public int potsInScene;
    [HideInInspector] public bool pause;
    [HideInInspector] private bool check;
    
    private void Awake() {
        if(instance == null)
            instance = this;
    #if UNITY_IOS || UNITY_ANDROID
        joystick.SetActive(true);
        fireButton.SetActive(true);
    #endif
    }

    private void Start() {
        check = false;
        pause = false;
        StartCoroutine(StartCheckAfter());
    }

    private void Update() {
        if(check) {
            if(potsInScene <= 0)
                SceneManager.LoadScene(2);
        }
    }

    public void PlayerBulletSE() {
        seSource.PlayOneShot(bulletSE);
    }

    public void PlayBreakSE() {
        seSource.PlayOneShot(breakSE);
    }

    private IEnumerator StartCheckAfter() {
        yield return new WaitForSeconds(5f);
        check = true;
    }
}
