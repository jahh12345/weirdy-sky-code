using UnityEngine;
using UnityEngine.UI;

public class appearimg : MonoBehaviour
{
    public Image imageToShow;
    public Button button;

    void Start()
    {
        imageToShow.gameObject.SetActive(false);
        button.onClick.AddListener(ToggleImage);
    }

    void ToggleImage()
    {
        imageToShow.gameObject.SetActive(!imageToShow.gameObject.activeSelf);
    }
}
