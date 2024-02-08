using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenagerScript : MonoBehaviour
{
    #region Variables and Properties
    //variaveis privadas
    private static GameMenagerScript instance;
    private int muzzleFlashIndex = 0;
    [SerializeField] private GameObject[] muzzleFlash;
    [SerializeField] private CameraManeger cameraManeger;
    [SerializeField] private GameObject player;
    [SerializeField] private Image FadeOutImage;
    [SerializeField] private GameObject[] tanks;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private String[] audioClipsNames; 
    [SerializeField] private AudioSource mainMusic;
     


    //variaveis publicas
    

    public bool IsRunning { get; set; } = false;
    public TextMeshProUGUI kills;
    public int Kills { get; set; } = 0;

    //propriedades publicas
    public static GameMenagerScript Instance => instance;

    #endregion

    private void Awake() {
        if(Instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        StartCoroutine(StartGame());
    }

    public void UpdateKills()
    {
        Kills++;
        kills.text = "Kills: " + Kills.ToString();
        //Debug.Log("Kills: " + Kills.ToString());
    }



    private IEnumerator StartGame()
    {
        //fade out
        FadeOutImage.gameObject.SetActive(true);
        float elapsed = 1f;
        while(elapsed > 0f)
        {
            FadeOutImage.color = new Color(0, 0, 0, elapsed);
            elapsed -= Time.deltaTime/2;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        PlayAudio("glass");
        

        //start the game
        
        player.SetActive(true);
        tanks[0].SetActive(false);
        tanks[1].SetActive(true);
        EnemySpawner.SetActive(true);
        IsRunning = true;
        FadeOutImage.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        StartCoroutine(EndGameCoroutine());
    }

    private IEnumerator EndGameCoroutine()
    {
        //get the middle of the screen

        kills.GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 2, -Screen.height / 2);
        kills.fontSize = 160;
        yield return new WaitForSeconds(5f);
        FadeOutImage.gameObject.SetActive(true);
        float elapsed = 0f;
        while(elapsed < 1f)
        {
            FadeOutImage.color = new Color(0, 0, 0, elapsed);
            elapsed += Time.deltaTime/2;
            yield return null;
        }

        
        yield return new WaitForSeconds(0.5f);
        //restart the whole game
        SceneManager.LoadScene(0);

    }

    public void PlayAudio(String name)
    {
        var index = Array.IndexOf(audioClipsNames ,name);

        if(index != -1)
        {
            for(int i = 0; i < audioSource.Length; i++)
            {
                if(audioSource[i].isPlaying)
                    continue;
                
                audioSource[i].clip = audioClips[index];
                audioSource[i].Play();
                //Debug.Log(i);
                break;
            }
        }
    }

    public void MuzzleFlash(Vector2 possition, Vector2 direction)
    {
        muzzleFlash[muzzleFlashIndex].SetActive(true);
        muzzleFlash[muzzleFlashIndex].transform.position = possition;
        muzzleFlash[muzzleFlashIndex].transform.up = direction;

        StartCoroutine(DisableMuzzleFlash(muzzleFlashIndex));
        muzzleFlashIndex = (muzzleFlashIndex + 1) % muzzleFlash.Length;
        
    }

    private IEnumerator DisableMuzzleFlash(int index)
    {
        yield return new WaitForSeconds(0.1f);
        muzzleFlash[index].SetActive(false);
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        //get active virtual camera
        CinemachineVirtualCamera virtualCamera = cameraManeger.GetActiveVirtualCamera();

        //get its body
        CinemachineCameraOffset cameraOffset = virtualCamera.GetComponent<CinemachineCameraOffset>();

        //get the original position
        Vector3 originalPosition = cameraOffset.m_Offset;


        //shake the camera
        float elapsed = 0f;

        while(elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            cameraOffset.m_Offset = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cameraOffset.m_Offset = Vector3.zero;
    }
}
