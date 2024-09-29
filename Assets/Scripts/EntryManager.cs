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

        SetColor();
    }

    void SetColor()
    {
        if (student.marked)
            name.color = Color.green;
        else name.color = Color.red;
    }
}

