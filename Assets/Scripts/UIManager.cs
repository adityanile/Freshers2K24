using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject entry;
    public Transform parent;

    public TextMeshProUGUI msgUI;

    public TMP_InputField uPrn;

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
        
        if(s != null)
        {
            WebManager.instance.MarkRegistered(s, () =>
            {
                // Here we will mark the student to the list
                GameObject inst = Instantiate(entry, parent);
                inst.transform.SetAsFirstSibling();

                uPrn.text = "";

                EntryManager em = inst.GetComponent<EntryManager>();
                em.SetEntryData(s);
            });
        }
        else
        {
            uPrn.text = "";
            UIManager.instance.ShowPopUp("No Data Found", Color.red);
        }
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
