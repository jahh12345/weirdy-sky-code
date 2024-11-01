using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; 

public class findkey : MonoBehaviour
{
    public Image imageToShow1; 
    public Image imageToShow2; 
    public Button button1; 
    public Button button2; 
    public GameObject popup; 
    public float delay = 2.0f; 
    public float popupDisplayDuration = 5.0f; 

    void Start()
    {
        imageToShow1.gameObject.SetActive(false); 
        imageToShow2.gameObject.SetActive(false); 
        popup.SetActive(false); 

        button1.onClick.AddListener(ShowImage1); 
        button2.onClick.AddListener(ShowImage2); 
    }

    void ShowImage1()
    {
        imageToShow1.gameObject.SetActive(true); 
        CheckWinCondition(); 
    }

    void ShowImage2()
    {
        imageToShow2.gameObject.SetActive(true); 
        CheckWinCondition(); 
    }

    void CheckWinCondition()
    {
        if (imageToShow1.gameObject.activeSelf && imageToShow2.gameObject.activeSelf)
        {
            StartCoroutine(ShowPopupWithDelay()); 
        }
    }

    IEnumerator ShowPopupWithDelay()
    {
        
        yield return new WaitForSeconds(delay); 
        popup.SetActive(true); 
        yield return new WaitForSeconds(popupDisplayDuration); 
        SceneManager.LoadScene("home"); 
    }
}
