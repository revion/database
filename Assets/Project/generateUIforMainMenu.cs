using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class generateUIforMainMenu : MonoBehaviour
{
    public Button sign_in;
    void Start()
    {
        sign_in.onClick.AddListener(Login);
    }
    void Login()
    {
        //Application.LoadLevel(1);
        SceneManager.LoadScene("form");
    }
}