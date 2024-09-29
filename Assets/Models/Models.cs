using System.Collections.Generic;

[System.Serializable]
public class FY
{
    public string year = "2024";
    public List<Student> students = new List<Student>();
}
[System.Serializable]
public class Student
{
    public int srno;
    public string prn;
    public string division;
    public string name;
    public bool marked;

    public override string ToString()
    {
        return $"PRN: {prn} Name: {name}";
    }

}
