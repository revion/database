using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class generateUIforSignRequest : MonoBehaviour
{
    public Dropdown project;
    public GameObject real_project_value,
        start_year,
        start_month,
        start_day,
        end_year,
        end_month,
        end_day;
    public Button back, submit;
    public string urlLeaveRequest = "http://localhost/project/project_management/leaveRequest.php";

    Dictionary<string, int> project_list = new Dictionary<string, int>();

    string[] tmpProject;
    string project_name = "";
    string start_date = "";
    string come_date = "";
    IEnumerator Start()
    {
        ButtonValue();
        WWW www = new WWW("http://localhost/project/project_management/test.php");
        yield return www;
        tmpProject = www.text.Split(';');
        for (int i = 0; i < tmpProject.Length - 1; i++)
        {
            KeepValueProjectList(i);
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
        if ((start_year.GetComponent<Text>().text == null || start_year.GetComponent<Text>().text == "") ||
            (start_month.GetComponent<Text>().text == null || start_month.GetComponent<Text>().text == "") ||
            (start_day.GetComponent<Text>().text == null || start_day.GetComponent<Text>().text == ""))
        {
            //INSERT ERROR HERE
        }
        else
        {
            start_date=start_year+"-"+start_month+"-"+start_day;
        }
        if ((end_year.GetComponent<Text>().text == null || end_year.GetComponent<Text>().text == "") ||
            (end_month.GetComponent<Text>().text == null || end_month.GetComponent<Text>().text == "") ||
            (end_day.GetComponent<Text>().text == null || end_day.GetComponent<Text>().text == ""))
        {
            //INSERT ERROR HERE
        }
        else
        {
            come_date=end_year+"-"+end_month+"-"+end_day;
        }
        StartCoroutine(leaveRequest(Account.userInput, project_name, start_date, come_date));
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
        project_list.Add(GetValueProjectList(tmpProject[index], "Proyek:"), index);
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
    IEnumerator leaveRequest(string member, string nameProject, string date, string date_back)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", member);
        form.AddField("project_name", nameProject);
        form.AddField("start_date", date);
        form.AddField("come_date", date_back);

        WWW www = new WWW(urlLeaveRequest, form);
        yield return www;
        if(www.text=="Your request has been fulfilled. Hope you can come back soon!")
        {
            print(www.text);
            date = date_back = "";
            //Application.LoadLevel(2);
            SceneManager.LoadSceneAsync("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
