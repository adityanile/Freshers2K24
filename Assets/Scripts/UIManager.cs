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

        }
        else
        {
            uPrn.text = "";
            Debug.Log("Invalid Student");
        }
    }
}
