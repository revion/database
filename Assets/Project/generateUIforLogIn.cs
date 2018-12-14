using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class generateUIforLogIn : MonoBehaviour
{
    public Text welcome_screen;
    public Button[] tombol;
    void Start()
    {
        welcome_screen.text = "Welcome, " + Account.userInput;
        for (int i = 0; i < tombol.Length; i++)
        {
            if (i == 0)
            {
                tombol[i].onClick.AddListener(sign_project);
            }
            else if (i == 1)
            {
                tombol[i].onClick.AddListener(sign_team);
            }
            else if (i == 2)
            {
                tombol[i].onClick.AddListener(view_project);
            }
            else if (i == 3)
            {
                tombol[i].onClick.AddListener(view_team);
            }
            else if (i == 4)
            {
                tombol[i].onClick.AddListener(sign_report);
            }
            else if (i == 5)
            {
                tombol[i].onClick.AddListener(sign_leave_request);
            }
            else if (i == 6)
            {
                tombol[i].onClick.AddListener(sign_reimbursement);
            }
            else if (i == 7)
            {
                tombol[i].onClick.AddListener(log_out);
            }
        }
    }
    void sign_project()
    {
        SceneManager.LoadScene("sign_project");
    }
    void sign_team()
    {
        SceneManager.LoadScene("submit_team");
    }
    void view_project()
    {

    }
    void view_team()
    {
        SceneManager.LoadScene("view_team");
    }
    void sign_report()
    {
        SceneManager.LoadScene("report");
    }
    void sign_leave_request()
    {
        SceneManager.LoadScene("sign_request");
    }
    void sign_reimbursement()
    {
        SceneManager.LoadScene("ganti_rugi");
    }
    void log_out()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene("main_menu");
    }
}
