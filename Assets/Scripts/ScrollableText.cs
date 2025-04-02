using UnityEngine;
using TMPro;

public class ScrollableText : MonoBehaviour
{
    public float scrollSpeed = 50f; // Adjust the scroll speed as desired
    public float pauseDuration = 2f; // Adjust the pause duration as desired
    public float resetPauseDuration = 2f; // Adjust the reset pause duration as desired
    private RectTransform textRectTransform;
    private float textWidth;
    private float containerWidth;
    private bool isScrolling;
    private bool isResetting;
    private float pauseTimer;

    private Vector3 originalPosition;

    private void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
        TMP_Text textComponent = GetComponentInChildren<TMP_Text>();
        textWidth = textComponent.preferredWidth;

        containerWidth = textRectTransform.rect.width;
        originalPosition = textRectTransform.localPosition;

        isScrolling = true;
        isResetting = false;
        pauseTimer = 0f;
    }

    private void Update()
    {
        if (textWidth > containerWidth)
        {
            if (isScrolling)
            {
                Vector3 position = textRectTransform.localPosition;
                position.x -= scrollSpeed * Time.deltaTime;

                if (position.x + textWidth < containerWidth)
                {
                    isScrolling = false;
                    pauseTimer = pauseDuration;
                }

                textRectTransform.localPosition = position;
            }
            else if (!isResetting)
            {
                pauseTimer -= Time.deltaTime;

                if (pauseTimer <= 0)
                {
                    isResetting = true;
                    pauseTimer = resetPauseDuration;
                    textRectTransform.localPosition = originalPosition;
                }
            }
            else
            {
                pauseTimer -= Time.deltaTime;

                if (pauseTimer <= 0)
                {
                    isScrolling = true;
                    isResetting = false;
                }
            }
        }
    }
}
