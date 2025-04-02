using UnityEngine;
using TMPro;

public class TextScrolling : MonoBehaviour
{
    public float scrollSpeed = 10f;  // Adjust the speed of scrolling

    private TMP_Text textComponent;
    private RectTransform rectTransform;
    private float contentWidth;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        CalculateContentWidth();
        StartCoroutine(ScrollText());
    }

    private void CalculateContentWidth()
    {
        TMP_TextInfo textInfo = textComponent.textInfo;
        contentWidth = textInfo.lineInfo[0].lineExtents.max.x - textInfo.lineInfo[0].lineExtents.min.x;
    }

    private System.Collections.IEnumerator ScrollText()
    {
        while (true)
        {
            if (contentWidth > rectTransform.rect.width)
            {
                float startPosition = rectTransform.rect.width / 2f + contentWidth / 2f;
                float endPosition = -contentWidth / 2f - rectTransform.rect.width / 2f;

                rectTransform.localPosition = new Vector3(startPosition, rectTransform.localPosition.y, rectTransform.localPosition.z);

                while (rectTransform.localPosition.x > endPosition)
                {
                    rectTransform.localPosition -= new Vector3(scrollSpeed * Time.deltaTime, 0f, 0f);
                    yield return null;
                }
            }

            yield return null;
        }
    }
}
