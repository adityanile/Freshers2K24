using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntryManager : MonoBehaviour
{
    public TextMeshProUGUI name;
    private Student student;

    public void SetEntryData(Student s)
    {
        student = s;
        name.text = s.ToString();
    }
}

