using UnityEngine;
using UnityEngine.UI;

public class SpriteCycle : MonoBehaviour
{
    public Button targetButton;
    public Sprite[] sprites;
    private int currentIndex = 0;

    private void Start()
    {
        // Set the initial sprite
        targetButton.image.sprite = sprites[currentIndex];
    }

    public void OnButtonClick()
    {
        // Cycle to the next sprite
        currentIndex++;
        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
        }

        // Update the button's sprite with the new sprite
        targetButton.image.sprite = sprites[currentIndex];
    }
}