using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class generateUIforSubmitReport : MonoBehaviour 
{
    public Dropdown proyek;
    public GameObject real_proyek_value;
    public InputField tugas;
    public InputField deskripsi;
    public InputField lamanya;
    public Button back, submit;

    Dictionary<string, int> proyek_list = new Dictionary<string, int>();

    public string urlSubmitReport = "http://localhost/project/project_management/projectSubmit.php";
    string[] project_name;
    string namaProyek = "";
    string task_name = "";
    string task_description = "";
    string timecost = "";

    IEnumerator Start()
    {
        ButtonValue();
        WWW www = new WWW("http://localhost/project/project_management/test.php");
        yield return www;
        project_name = www.text.Split(';');
        for (int i = 0; i < project_name.Length - 1; i++)
        {
            KeepValueToProjectList(i);
        }
        EditDropdown();
    }
    void ButtonValue()
    {
        var input_task = tugas.GetComponent<InputField>();
        var input_desc = deskripsi.GetComponent<InputField>();
        var input_cost = lamanya.GetComponent<InputField>();

        var se_task = new InputField.SubmitEvent();
        var se_desc = new InputField.SubmitEvent();
        var se_cost = new InputField.SubmitEvent();

        se_task.AddListener(submit_task);
        se_desc.AddListener(submit_desc);
        se_cost.AddListener(submit_cost);

        input_task.onEndEdit = se_task;
        input_desc.onEndEdit = se_desc;
        input_cost.onEndEdit = se_cost;

        back.onClick.AddListener(Home);
        submit.onClick.AddListener(Submit);
    }
    void submit_task(string arg)
    {
        task_name = arg;
    }
    void submit_desc(string arg)
    {
        task_description = arg;
    }
    void submit_cost(string arg)
    {
        timecost = arg;
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("logged_in");
    }
    void Submit()
    {
        StartCoroutine(reportSubmit(Account.userInput, namaProyek, task_name, task_description, timecost));
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
    void KeepValueToProjectList(int index)
    {
        proyek_list.Add(GetValueProjectList(project_name[index], "Proyek:"), index);
    }
    void EditDropdown()
    {
        if (proyek != null)
        {
            proyek.ClearOptions();
            proyek.AddOptions(proyek_list.Keys.ToList());
            proyek.onValueChanged.AddListener(ChangeValue);
        }
    }
    void ChangeValue(int pos)
    {
        int realValue = proyek_list.Values.ElementAt(pos);
        namaProyek = real_proyek_value.GetComponent<Text>().text;
        print(realValue + "," + namaProyek);
    }
    IEnumerator reportSubmit(string member, string nameProject, string name, string description, string time)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", member);
        form.AddField("project_name", nameProject);
        form.AddField("task_name", name);
        form.AddField("task_description", description);
        form.AddField("task_timecost", time);

        WWW www = new WWW(urlSubmitReport, form);
        yield return www;
        if(www.text=="Your task has been recorded.")
        {
            print(www.text);
            name = description = time = "";
            //Application.LoadLevel(2);
            SceneManager.LoadSceneAsync("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
