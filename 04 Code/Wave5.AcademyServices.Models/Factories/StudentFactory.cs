namespace Wave5.AcademyServices;

public static class StudentFactory
{
    #region [ Public Methods - Create ]
    public static Student Create() {
        return new Student();
    }

    public static Student Create(string firstName, string lastName) {
        return new Student(firstName, lastName);
    }
    #endregion
}