using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class test_purpose : MonoBehaviour
{
    public string[] proyek;
    public Dropdown ui;

    Dictionary<string, int> list = new Dictionary<string, int>();
    IEnumerator Start()
    {
        WWW list = new WWW("http://localhost/project/project_management/test.php");
        yield return list;
        print(list.text);
        proyek = list.text.Split(';');
        for (int i = 0; i < proyek.Length - 1; i++)
        {
            KeepValueToList(i);
        }
        EditDropdown();
    }
    void EditDropdown()
    {
        if (ui != null)
        {
            ui.ClearOptions();
            ui.AddOptions(list.Keys.ToList());
            ui.onValueChanged.AddListener(ChangeValue);
        }
    }
    string GetDataValue(string data,string index)
    {
        string value = data.Substring(data.IndexOf(index) + index.Length);
        if(value.Contains("|"))
        {
            value = value.Remove(value.IndexOf("|"));
        }
        return value;
    }
    void KeepValueToList(int index)
    {
        list.Add(GetDataValue(proyek[index], "Proyek:"), index);
    }
    void ChangeValue(int pos)
    {
        int realValue = list.Values.ElementAt(pos);
        print(realValue);
    }
}
