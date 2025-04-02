using UnityEngine;

public class SongNameScroller : MonoBehaviour
{
    public float scrollSpeed = 1.0f;

    void Update()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}

