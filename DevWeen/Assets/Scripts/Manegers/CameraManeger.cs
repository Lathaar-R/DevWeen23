using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraManeger : MonoBehaviour
{
    [SerializeField]private PlayerController playerController;
    private bool[] cinemachineVirtualCamerasActive;
    [SerializeField] private CinemachineVirtualCamera[] cinemachineVirtualCameras;
    [SerializeField] private Image[] changeEffectImages;
    private bool changeEffectActive = false;

    public CinemachineVirtualCamera[] CinemachineVirtualCameras => cinemachineVirtualCameras;
    public bool[] CinemachineVirtualCamerasActive => cinemachineVirtualCamerasActive;

    // Start is called before the first frame update
    void Start()
    {
        //playerController = FindObjectOfType<PlayerController>();
        cinemachineVirtualCamerasActive = new bool[cinemachineVirtualCameras.Length];

        //set camera 2 active
        cinemachineVirtualCameras[2].gameObject.SetActive(true);
        cinemachineVirtualCamerasActive[2] = true;

        for(int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
        {
            if(i != 2)
            {
                cinemachineVirtualCameras[i].gameObject.SetActive(false);
                cinemachineVirtualCamerasActive[i] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameMenagerScript.Instance.IsRunning) return;
//        Debug.Log("Update");


        //Debug.Log(playerController.transform.position.x);
        if (playerController.transform.position.x < 6 && playerController.transform.position.x > -6)
        {
            if (!cinemachineVirtualCamerasActive[2])
            {
                cinemachineVirtualCameras[2].gameObject.SetActive(true);
                changeEffectActive = true;
                cinemachineVirtualCamerasActive[2] = true;

                for (int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
                {
                    if (i != 2)
                    {
                        cinemachineVirtualCameras[i].gameObject.SetActive(false);
                        cinemachineVirtualCamerasActive[i] = false;
                    }
                }
            }
        }
        else if (playerController.transform.position.x > 6)
        {
            if (playerController.transform.position.y < 0)
            {
                if (!cinemachineVirtualCamerasActive[0])
                {
                    cinemachineVirtualCameras[0].gameObject.SetActive(true);
                    changeEffectActive = true;
                    cinemachineVirtualCamerasActive[0] = true;

                    for (int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
                    {
                        if (i != 0)
                        {
                            cinemachineVirtualCameras[i].gameObject.SetActive(false);
                            cinemachineVirtualCamerasActive[i] = false;
                        }
                    }
                }
            }
            else
            {
                if (!cinemachineVirtualCamerasActive[3])
                {
                    cinemachineVirtualCameras[3].gameObject.SetActive(true);
                    changeEffectActive = true;
                    cinemachineVirtualCamerasActive[3] = true;

                    for (int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
                    {
                        if (i != 3)
                        {
                            cinemachineVirtualCameras[i].gameObject.SetActive(false);
                            cinemachineVirtualCamerasActive[i] = false;
                        }
                    }
                }
            }
        }
        else
        {
            if (playerController.transform.position.y < 0)
            {
                if (!cinemachineVirtualCamerasActive[1])
                {
                    cinemachineVirtualCameras[1].gameObject.SetActive(true);
                    changeEffectActive = true;
                    cinemachineVirtualCamerasActive[1] = true;

                    for (int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
                    {
                        if (i != 1)
                        {
                            cinemachineVirtualCameras[i].gameObject.SetActive(false);
                            cinemachineVirtualCamerasActive[i] = false;
                        }
                    }
                }
            }
            else
            {
                if (!cinemachineVirtualCamerasActive[4])
                {
                    cinemachineVirtualCameras[4].gameObject.SetActive(true);
                    changeEffectActive = true;
                    cinemachineVirtualCamerasActive[4] = true;

                    for (int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
                    {
                        if (i != 4)
                        {
                            cinemachineVirtualCameras[i].gameObject.SetActive(false);
                            cinemachineVirtualCamerasActive[i] = false;
                        }
                    }
                }
            }
        }

        if(changeEffectActive)
        {
            StartCoroutine(ChangeEffect());
            GameMenagerScript.Instance.PlayAudio("whiteNoise");
        }


    }

    private IEnumerator ChangeEffect()
    {
        changeEffectActive = false;
        for(int i = 0; i < changeEffectImages.Length; i++)
        {
            changeEffectImages[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.04f);
            changeEffectImages[i].gameObject.SetActive(false);
        }

    
    }

    public CinemachineVirtualCamera GetActiveVirtualCamera()
    {
        for(int i = 0; i < cinemachineVirtualCamerasActive.Length; i++)
        {
            if (cinemachineVirtualCamerasActive[i])
                return cinemachineVirtualCameras[i];
        }

        return null;
    }


}
