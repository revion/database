using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class generateUIforGantiRugi : MonoBehaviour 
{
    public Dropdown project;
    public GameObject real_project_value, year, month, day;
    public InputField name, total;
    public Button back, submit;

    public string urlGantiRugi = "http://localhost/project/project_management/gantiRugi.php";

    Dictionary<string, int> project_list = new Dictionary<string, int>();

    string[] nameProject;

    string project_name = "";
    string gr_name = "";
    string total_gr = "";
    string date_request = "";
    IEnumerator Start()
    {
        ButtonValue();
        WWW www = new WWW("http://localhost/project/project_management/test.php");
        yield return www;
        nameProject = www.text.Split(';');
        for (int i = 0; i < nameProject.Length - 1; i++)
        {
            KeepValueProjectList(i);
        }
        EditDropdown();
    }
    void ButtonValue()
    {
        var input_name = name.GetComponent<InputField>();
        var input_total = total.GetComponent<InputField>();

        var se_name = new InputField.SubmitEvent();
        var se_total = new InputField.SubmitEvent();

        se_name.AddListener(submit_name);
        se_total.AddListener(submit_total);

        input_name.onEndEdit = se_name;
        input_total.onEndEdit = se_total;
        back.onClick.AddListener(Home);
        submit.onClick.AddListener(Submit);
    }
    void submit_name(string arg)
    {
        gr_name = arg;
    }
    void submit_total(string arg)
    {
        total_gr = arg;
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("logged_in");
    }
    void Submit()
    {
        if ((year.GetComponent<Text>().text == null || year.GetComponent<Text>().text == "") ||
            (month.GetComponent<Text>().text == null || month.GetComponent<Text>().text == "") ||
            (day.GetComponent<Text>().text == null || day.GetComponent<Text>().text == ""))
        {
            //INSERT ERROR HERE
        }
        else
        {
            date_request = year.GetComponent<Text>().text + "-" + month.GetComponent<Text>().text + "-" + day.GetComponent<Text>().text;
        }
        StartCoroutine(reimbursementRequest(Account.userInput, project_name, gr_name, total_gr, date_request));
    }
    string GetValueProjectList(string data, string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
    void KeepValueProjectList(int index)
    {
        project_list.Add(GetValueProjectList(nameProject[index], "Proyek:"), index);
    }
    void EditDropdown()
    {
        if (project != null)
        {
            project.ClearOptions();
            project.AddOptions(project_list.Keys.ToList());
            project.onValueChanged.AddListener(ChangeValue);
        }
    }
    void ChangeValue(int pos)
    {
        int realValue = project_list.Values.ElementAt(pos);
        project_name = real_project_value.GetComponent<Text>().text;
        print(realValue + "," + project_name);
    }
    IEnumerator reimbursementRequest(string member, string nameProject, string name, string total, string date)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", member);
        form.AddField("project_name", nameProject);
        form.AddField("gr_name", name);
        form.AddField("total_gr", total);
        form.AddField("date_request", date);

        WWW www = new WWW(urlGantiRugi, form);
        yield return www;
        if (www.text == "Your request has been sent and will be fulfilled as soon as possible.")
        {
            print(www.text);
            name = total = date = "";
            //Application.LoadLevel(2);
            SceneManager.LoadSceneAsync("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
