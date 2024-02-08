using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image FadeOutImage;

    public void OnPlayeButtonPressed()
    {
        StartCoroutine(StartGame());
    }   

    private IEnumerator StartGame()
    {
        //fade out
        float elapsed = 0f;
        FadeOutImage.gameObject.SetActive(true);
        while(elapsed < 1f)
        {
            FadeOutImage.color = new Color(0, 0, 0, elapsed);
            elapsed += Time.deltaTime/2;
            yield return null;
        }
 
        yield return new WaitForSeconds(1f);

        //load scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }    


}
