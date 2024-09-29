using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    public static WebManager instance;

    private string url = "https://freshers2-k24.vercel.app";
    private string registerPath = "/api/registerStudent";

    void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    public void MarkRegistered(Student student, Action callback)
    {
        StudentVM studentVM = new StudentVM
        {
            name = student.name,
            prn = student.prn
        };

        string json = JsonUtility.ToJson(studentVM);
        StartCoroutine(RegisterStudent(json, callback));
    }

    IEnumerator RegisterStudent(string payload, Action callback)
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
                    callback();
                    UIManager.instance.ShowPopUp(registerOM.msg, Color.green);
                }
                else
                {
                    if (registerOM.msg == "Already Registered")
                        UIManager.instance.ShowPopUp(registerOM.msg, Color.red);
                    else
                        UIManager.instance.ShowPopUp("Error Registering User in Database", Color.red);
                }
            }
            else
            {
                UIManager.instance.ShowPopUp("Error Connecting Server", Color.red);
                Debug.Log("Use local DB");
            }
        }
    }
}
