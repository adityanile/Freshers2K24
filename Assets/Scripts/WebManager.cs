using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    public static WebManager instance;

    private string url = "https://freshers2-k24.vercel.app";
    private string registerPath = "/api/registerStudent";
    private string testPath = "/api/test";

    void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    public void MarkRegistered(Student student, Action OnSuccess, Action OnFail)
    {
        StudentVM studentVM = new StudentVM
        {
            name = student.name,
            prn = student.prn
        };

        string json = JsonUtility.ToJson(studentVM);

        UIManager.instance.ShowPopUp("Loading...", Color.black);
        StartCoroutine(RegisterStudent(json, OnSuccess, OnFail));
    }

    IEnumerator RegisterStudent(string payload, Action OnSuccess, Action OnFail)
    {
        using (UnityWebRequest req = UnityWebRequest.Post(url + registerPath, payload, "application/json"))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                string res = req.downloadHandler.text;
                RegisterOM registerOM = JsonUtility.FromJson<RegisterOM>(res);

                if (registerOM.status == "success")
                {
                    // when user not in database then only add it to list
                    OnSuccess();
                    UIManager.instance.ShowPopUp(registerOM.msg, Color.green);
                }
                else
                {
                    if (registerOM.msg == "Already Registered")
                        UIManager.instance.ShowPopUp(registerOM.msg, Color.red);
                    else
                    {
                        UIManager.instance.ShowPopUp("Error Registering Online, Registering Locally", Color.red);
                        OnFail();
                    }
                }
            }
            else
            {
                UIManager.instance.ShowPopUp("Error Connecting Server, Registering Locally", Color.red);
                OnFail();
            }
        }
    }

    public IEnumerator TestConnection()
    {
        using (UnityWebRequest req = UnityWebRequest.Get(url + testPath))
        {
            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                string res = req.downloadHandler.text;
                RegisterOM registerOM = JsonUtility.FromJson<RegisterOM>(res);

                if (registerOM.status == "success")
                    UIManager.instance.ShowPopUp("Connection Successful", Color.green);
                else
                    UIManager.instance.ShowPopUp("Error Connecting Server", Color.red);
            }
            else
                UIManager.instance.ShowPopUp("Error Connecting Server", Color.red);
        }
    }
}
