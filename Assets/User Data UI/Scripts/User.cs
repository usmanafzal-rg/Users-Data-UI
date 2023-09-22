using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class User : MonoBehaviour
{
    public TextMeshProUGUI username;
    public TextMeshProUGUI email;
    public TextMeshProUGUI gender;
    public Button viewDetailsButton;

    public void OnViewDetailsClick()
    {
        if (Controller.instance)
        {
            Controller.instance.ViewDetails(this);
        }
    }
}
