using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public void OnStartSpotTheDifferenceGame()
    {
        SceneManager.LoadScene("SpotTheDifferenceGame");
    }
}
