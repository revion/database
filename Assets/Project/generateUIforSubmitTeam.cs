using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class generateUIforSubmitTeam : MonoBehaviour 
{
    public Dropdown namaProyek;
    public GameObject proyek_list;
    public InputField jabatan;
    public Button back, submit;
    public string urlSubmitTeam = "http://localhost/project/project_management/submitTeam.php";

    Dictionary<string, int> project_list = new Dictionary<string, int>();
    string[] project_name;
    string nameOfProject = "";
    string role = "";
    IEnumerator Start()
    {
        ButtonValue();
        WWW takeProject = new WWW("http://localhost/project/project_management/test.php");
        yield return takeProject;
        project_name = takeProject.text.Split(';');
        for (int i = 0; i < project_name.Length - 1; i++)
        {
            KeepValueToProjectList(i);
        }
        EditDropdown();
    }
    /// <summary>
    /// Membuat tombol dapat berinteraksi
    /// </summary>
    void ButtonValue()
    {
        var input_role = jabatan.GetComponent<InputField>();
        var se_role = new InputField.SubmitEvent();
        se_role.AddListener(submit_role);
        //KEEP 
        input_role.onEndEdit = se_role;
        //BUTTON FUNCTION
        back.onClick.AddListener(Home);
        submit.onClick.AddListener(Submit);
    }
    /// <summary>
    /// Penggantian value opsi
    /// </summary>
    void EditDropdown()
    {
        if (namaProyek != null)
        {
            namaProyek.ClearOptions();
            namaProyek.AddOptions(project_list.Keys.ToList());
            namaProyek.onValueChanged.AddListener(ChangeValue);
        }
    }
    /// <summary>
    /// Replace value opsi dengan list dictionary
    /// </summary>
    /// <param name="index">Index dari loop</param>
    void KeepValueToProjectList(int index)
    {
        project_list.Add(GetValueProjectList(project_name[index], "Proyek:"), index);
    }
    /// <summary>
    /// Mengambil nilai dari kata berdasarkan pemisahan dan pembuangan karakter tertentu
    /// </summary>
    /// <param name="data">Data dalam database yang dipisahkan berdasarkan kondisi contain</param>
    /// <param name="index">Pemisahan dari data</param>
    /// <returns></returns>
    string GetValueProjectList(string data,string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if (value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
    void ChangeValue(int pos)
    {
        int realValue = project_list.Values.ElementAt(pos);
        nameOfProject = proyek_list.GetComponent<Text>().text;
        print(realValue + "," + nameOfProject);
    }
    void submit_role(string arg)
    {
        role = arg;
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("logged_in");
    }
    void Submit()
    {
        StartCoroutine(submitTeam(Account.userInput, nameOfProject, role));
    }
    IEnumerator submitTeam(string member, string nameProject, string yourRole)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", member);
        form.AddField("project_name", nameProject);
        form.AddField("role", yourRole);

        WWW www = new WWW(urlSubmitTeam, form);
        yield return www;
        if (www.text == member+" has been signed.")
        {
            print(www.text);
            yourRole = "";
            //Application.LoadLevel(2);
            SceneManager.LoadSceneAsync("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
