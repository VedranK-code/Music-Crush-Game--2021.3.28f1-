using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public const string PERMISSIONS_PREF_KEY = "PermissionsShown";
    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetInt(PERMISSIONS_PREF_KEY, 1);
        PlayerPrefs.Save();
    }
}
