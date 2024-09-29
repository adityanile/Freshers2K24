using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager instance;

    public string[] rawData;
    public string path = "FY2K24";

    [SerializeField]
    private FY studentsData = new FY();

    public List<Student> registeredStudents = new List<Student>();

    void Start()
    {
        if(instance != null)
           Destroy(this);
            
        instance = this;
        
        ParseCSV();
    }

    private void Update()
    {
        // Keyboard Control
        if (Input.GetKeyDown(KeyCode.Return))
            UIManager.instance.OnClickEnter();
    }

    public Student ValidateStudent(string prn)
    {
        var s = studentsData.students.Find(s => s.prn == prn);
        return s == null ? null : s;
    }


    void ParseCSV()
    {
        var dataSet = Resources.Load(path);

        rawData = dataSet.ToString().Split(new char[] { '\n' });
        rawData[rawData.Length - 1] = null;

        foreach (var s in rawData)
        {
            if (s != null)
            {
                string[] fields = s.Split(new char[] { ',' });

                Student student = new Student
                {
                    srno = int.Parse(fields[0]),
                    division = fields[1],
                    prn = fields[2],
                    name = fields[4],
                    marked = false
                };
                studentsData.students.Add(student);
            }
        }
    }

}
