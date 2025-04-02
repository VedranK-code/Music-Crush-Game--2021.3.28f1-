using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonController : MonoBehaviour
{
    public Button button;
    public Toggle[] toggles;

    private int togglesOnCount;

    private void Start()
    {
        // Disable the button initially
        button.interactable = false;

        // Subscribe to the toggles' OnValueChanged event
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            togglesOnCount++;
        }
        else
        {
            togglesOnCount--;
        }

        // Enable the button when all three toggles are turned on
        button.interactable = togglesOnCount >= 3;
    }
}
