using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject entry;
    public Transform parent;

    public TMP_InputField uPrn;

    public void OnClickEnter()
    {
        string prn = uPrn.text;
        prn = prn.ToUpper();

        Student s = ApplicationManager.instance.ValidateStudent(prn);
        
        if(s != null)
        {
            // Here we will mark the student to the list
            GameObject inst = Instantiate(entry, parent);
            
            EntryManager em = inst.GetComponent<EntryManager>();
            em.SetEntryData(s);

        }
        else
        {
            uPrn.text = "";
            Debug.Log("Invalid Student");
        }
    }
}
