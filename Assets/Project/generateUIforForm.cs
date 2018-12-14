using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class generateUIforForm : MonoBehaviour 
{
    public Button back;
    public Button submit;
    public InputField user_name;
    public InputField pass_word;

    string urlLogin = "http://localhost/project/project_management/userLogin.php";
    string pass = "";
    void Start()
    {
        var input_username = user_name.GetComponent<InputField>();
        var input_password = pass_word.GetComponent<InputField>();
        var se_name = new InputField.SubmitEvent();
        var se_pass = new InputField.SubmitEvent();
        se_name.AddListener(submit_username);
        se_pass.AddListener(submit_password);
        input_username.onEndEdit = se_name;
        input_password.onEndEdit = se_pass;
        //BUTTON
        back.onClick.AddListener(Home);
        submit.onClick.AddListener(Login);
    }
    void submit_username(string arg)
    {
        Account.userInput = arg;
    }
    void submit_password(string arg)
    {
        pass = arg;
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("main_menu");
    }
    void Login()
    {
        StartCoroutine(loginUser(Account.userInput, pass));
    }
    
    IEnumerator loginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        WWW www = new WWW(urlLogin, form);
        yield return www;
        ApplicationModel.error = www.text;
        if (ApplicationModel.error == "" || ApplicationModel.error == null)
        {
            print(ApplicationModel.error);
            ApplicationModel.logged_in = true;
            //Application.LoadLevel(2);
            SceneManager.LoadScene("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
