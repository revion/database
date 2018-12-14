using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class generateUIforSignProject : MonoBehaviour
{
    public InputField projectName;
    public InputField projectLocation;
    public InputField projectCost;
    public InputField projectTimeline;
    public GameObject year, month, day;
    public Button submit, back;

    public string urlSignProject = "http://localhost/project/project_management/signProject.php";
    string project_name = "";
    string project_location = "";
    string project_date = "";
    string project_cost = "";
    string project_timeline = "";

    void Start()
    {
        var input_name = projectName.GetComponent<InputField>();
        var input_location = projectLocation.GetComponent<InputField>();
        var input_cost = projectCost.GetComponent<InputField>();
        var input_timeline = projectTimeline.GetComponent<InputField>();
        //SEND INPUT TO STRING
        var se_name = new InputField.SubmitEvent();
        var se_location = new InputField.SubmitEvent();
        var se_cost = new InputField.SubmitEvent();
        var se_timeline = new InputField.SubmitEvent();
        //GET STRING
        se_name.AddListener(submit_projectName);
        se_location.AddListener(submit_projectLocation);
        se_cost.AddListener(submit_projectCost);
        se_timeline.AddListener(submit_projectTimeline);
        //SAVE FROM STRING TO STRING
        input_name.onEndEdit = se_name;
        input_location.onEndEdit = se_location;
        input_cost.onEndEdit = se_cost;
        input_timeline.onEndEdit = se_timeline;
        //BUTTON SUBMIT
        back.onClick.AddListener(Home);
        submit.onClick.AddListener(submit_project);
    }
    void submit_projectName (string arg)
    {
        project_name = arg;
    }
    void submit_projectLocation(string arg)
    {
        project_location = arg;
    }
    void submit_projectCost(string arg)
    {
        project_cost = arg;
    }
    void submit_projectTimeline(string arg)
    {
        project_timeline = arg;
    }
    void Home()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene("logged_in");
    }
    void submit_project()
    {
        if ((year.GetComponent<Text>().text == null || year.GetComponent<Text>().text == "") ||
            (month.GetComponent<Text>().text == null || month.GetComponent<Text>().text == "") ||
            (day.GetComponent<Text>().text == null || day.GetComponent<Text>().text == ""))
        {
            //INSERT ERROR LINE
        }
        else
        {
            project_date = year.GetComponent<Text>().text + "-" + month.GetComponent<Text>().text + "-" + day.GetComponent<Text>().text;
        }
        StartCoroutine(signProject(project_name, project_location, project_date, project_cost, project_timeline));
    }
    
    IEnumerator signProject(string name, string location, string date, string cost, string timeline)
    {
        WWWForm form = new WWWForm();
        form.AddField("project_name", name);
        form.AddField("project_location", location);
        form.AddField("project_date", date);
        form.AddField("project_timeline", timeline);
        form.AddField("project_cost", cost);

        WWW www = new WWW(urlSignProject, form);
        yield return www;
        if (www.text == "Your project has been recorded.")
        {
            print(www.text);
            name = location = date = cost = "";
            //Application.LoadLevel(2);
            SceneManager.LoadSceneAsync("logged_in");
        }
        else
        {
            print(www.text);
        }
    }
}
