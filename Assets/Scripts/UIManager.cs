using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject entry;
    public Transform parent;

    public TextMeshProUGUI msgUI;

    public TMP_InputField uPrn;

    public List<GameObject> stdList = new List<GameObject>();

    private void Start()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    public void OnClickEnter()
    {
        string prn = uPrn.text;
        prn = prn.ToUpper();

        Student s = ApplicationManager.instance.ValidateStudent(prn);

        if (s != null)
        {
            WebManager.instance.MarkRegistered(s, OnSuccess: () =>
            {
                // Here we will mark the student to the list
                // when successful the marking online then mark locally

                // If we successfully marked online then set marked to true
                // else if updated locally then let marked = false

                s.marked = true;
                RegisterLocally(s);

            }, OnFail: () =>
            {
                RegisterLocally(s);
            });
        }
        else
        {
            uPrn.text = "";
            UIManager.instance.ShowPopUp("No Data Found", Color.red);
        }
    }

    public void OnClickClear()
    {
        ApplicationManager.instance.registeredStudents.Clear();

        foreach(var i in stdList)
        {
            Destroy(i.gameObject);
        }
        stdList.Clear();

        LocalStorageManager.instance.SaveSessionData();
    }

    public void OnClickTest()
    {
        StartCoroutine(WebManager.instance.TestConnection());
    }

    public void RegisterLocally(Student s)
    {
        GameObject inst = Instantiate(entry, parent);
        inst.transform.SetAsFirstSibling();

        stdList.Add(inst);
        ApplicationManager.instance.registeredStudents.Add(s);

        uPrn.text = "";

        EntryManager em = inst.GetComponent<EntryManager>();
        em.SetEntryData(s);

        // Save current state locally
        LocalStorageManager.instance.SaveSessionData();
    }


    public void ShowPopUp(string msg, Color color)
    {
        StartCoroutine(ShowMsg(msg, color));
    }

    IEnumerator ShowMsg(string msg, Color color)
    {
        msgUI.transform.parent.gameObject.SetActive(true);
        msgUI.text = msg;
        msgUI.color = color;

        yield return new WaitForSeconds(3);

        msgUI.transform.parent.gameObject.SetActive(false);
        msgUI.text = "";
        msgUI.color = Color.black;
    }
}
