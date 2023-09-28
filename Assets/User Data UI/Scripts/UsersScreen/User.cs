using System;
using System.Collections;
using System.Collections.Generic;
using deVoid.UIFramework;
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
    public UsersScreen _screen;

    IEnumerator ViewDetails()
    {
        AudioManager audioManager = ServiceLocator.Instance.Get<AudioManager>();
        audioManager.PlayEffect(_screen.viewDetailButtonAudioclip);
        yield return new WaitForSeconds(0.4f);
        _screen.ShowUserDetailScreen(email.text);
    }
    public void OnViewDetailsClick()
    {
        StartCoroutine(ViewDetails());
    }
}
