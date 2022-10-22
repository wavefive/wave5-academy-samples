using RCode;

namespace Wave5.AcademyServices;

public class Student : BaseEntity
{
    #region [ CTor ]
    public Student() {

    }

    public Student(string firstName, string lastName) : this() {
        this.FirstName = firstName;
        this.LastName = lastName;
    }
    #endregion

    #region [ Properties ]
    public string FirstName { get; set; }

    public string LastName { get; set; }
    #endregion
}
