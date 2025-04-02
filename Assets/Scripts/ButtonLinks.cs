using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLinks : MonoBehaviour
{
    public int link = 0;

    public void openLink()
    {   if(link==0)
        {
        Application.OpenURL("https://unity.com/legal/privacy-policy");
        }

        if(link==1)
        {
        Application.OpenURL("https://games.jaspero.co/privacy-policy");
        }

        if(link==2)
        {
        Application.OpenURL("https://policies.google.com/privacy");
        }
    }
}
