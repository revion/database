using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class generateUIforViewTeam : MonoBehaviour
{
    public Button back, submit;
    public Dropdown project;
    public GameObject content;

    Dictionary<string, int> content_list = new Dictionary<string, int>();

    string[] project_list;
    string project_name = "";
    IEnumerator Start()
    {
        ButtonValue();
        WWW www = new WWW("http://localhost/project/project_management/test.php");
        yield return www;
        project_list = www.text.Split(';');
        for (int i = 0; i < project_list.Length - 1; i++)
        {
            KeepValueContentList(i);
        }
        EditDropdown();
    }
    void ButtonValue()
    {
        back.onClick.AddListener(Home);
        submit.onClick.AddListener(Submit);
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("logged_in");
    }
    void Submit()
    {
        StartCoroutine(deleteTeam(Account.userInput,project_name));
    }
    string GetValueContentList(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
    void KeepValueContentList(int index)
    {
        content_list.Add(GetValueContentList(project_list[index], "Proyek:"), index);
    }
    void EditDropdown()
    {
        if (project != null)
        {
            project.ClearOptions();
            project.AddOptions(content_list.Keys.ToList());
            project.onValueChanged.AddListener(ChangeValue);
        }
    }
    void ChangeValue(int pos)
    {
        int realValue = content_list.Values.ElementAt(pos);
        project_name = content.GetComponent<Text>().text;
        print(realValue + "," + project_name);
    }
    IEnumerator deleteTeam(string username, string projectName)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("project_name", projectName);

        WWW www = new WWW("http://localhost/project/project_management/deleteTim.php", form);
        yield return www;
        ApplicationModel.error = www.text;
        if (www.text == "Data has been deleted")
        {
            print(ApplicationModel.error);
            SceneManager.LoadScene("logged_in");
        }
        else
        {
            print(ApplicationModel.error);
        }
    }
}
