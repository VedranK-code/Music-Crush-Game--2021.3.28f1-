using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Button myButton; // Assign this in the Inspector
    private const string PrefKey = "SceneConfirmed";

    void Start()
    {
        // Provjeri postoji li PlayerPref i ako postoji, odmah prebaci na "Loading"
        if (PlayerPrefs.HasKey(PrefKey))
        {
            SceneManager.LoadScene("Loading");
            return;
        }

        if (myButton != null)
        {
            myButton.onClick.AddListener(ConfirmScene);
        }
        else
        {
            Debug.LogError("Button not assigned in Inspector!");
        }
    }

    void ConfirmScene()
    {
        PlayerPrefs.SetInt(PrefKey, 1); // Sprema postavku
        PlayerPrefs.Save(); // Osigurava trajnost podataka
        SceneManager.LoadScene("Loading");
    }
}