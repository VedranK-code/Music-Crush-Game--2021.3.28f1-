using UnityEngine;

public class Unmute : MonoBehaviour
{
    void Start()
    {
        AudioListener.volume = 1f;
    }
}
