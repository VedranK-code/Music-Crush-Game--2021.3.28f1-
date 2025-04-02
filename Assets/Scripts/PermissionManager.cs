using UnityEngine;
using UnityEngine.SceneManagement;

public class PermissionManager : MonoBehaviour
{
    public const string PERMISSIONS_PREF_KEY = "PermissionsShown";

    private void Start()
    {
        // Check if the permission scenes have been shown before
        if (!PlayerPrefs.HasKey(PERMISSIONS_PREF_KEY))
        {
            // Show the permission scenes
            SceneManager.LoadScene("Permission Screen");

            // Set the preference to indicate that the permission scenes have been shown
           //PlayerPrefs.SetInt(PERMISSIONS_PREF_KEY, 1);
            //PlayerPrefs.Save();
        }
        else
        {
            
        }
    }
}
