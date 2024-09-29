using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class LocalStorageManager : MonoBehaviour
{
    public static LocalStorageManager instance;

    private string jsonPath;
   
    void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;

        jsonPath = Application.persistentDataPath + "/mainData.json";

        if (!File.Exists(jsonPath))
        {
            File.WriteAllText(jsonPath, "");
            Debug.Log("File Created");
        }
        else
        {
            // If any previous data then load it into application
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        LoadInitialData();
    }

    public void SaveSessionData()
    {
        LocalSaveVM saveDat = new LocalSaveVM();
        saveDat.registeredStudents = ApplicationManager.instance.registeredStudents.ToArray();

        string jsonstr =  JsonUtility.ToJson(saveDat,true);
        File.WriteAllText (jsonPath, jsonstr);
    }

    public void LoadInitialData()
    {
        string initialJson = File.ReadAllText(jsonPath);

        LocalSaveVM saveVM = JsonUtility.FromJson<LocalSaveVM>(initialJson);

        foreach(var s in saveVM.registeredStudents)
        {
            UIManager.instance.RegisterLocally(s);
        }
    }
}
