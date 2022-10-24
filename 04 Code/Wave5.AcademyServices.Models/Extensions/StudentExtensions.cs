using RCode;
using System.Collections.Generic;

namespace Wave5.AcademyServices;

public static class StudentExtensions
{
    #region [ Public Methods - Name ]
    public static string GetDisplayName(this Student student) {

        if (!string.IsNullOrEmpty(student.FirstName) && !string.IsNullOrEmpty(student.LastName)) {
            return $"{student.FirstName} {student.LastName}";

        } else if (string.IsNullOrEmpty(student.FirstName) && !string.IsNullOrEmpty(student.LastName)) {
            return student.FirstName;

        } else if (!string.IsNullOrEmpty(student.FirstName) && string.IsNullOrEmpty(student.LastName)) {
            return student.LastName;

        } else {
            return string.Empty;
        }
    }
    #endregion

    #region [ Public Methods - Lists ]
    public static void Add(this List<Student> list, string firstName, string lastName) { 
        list.Add(new Student(firstName, lastName));
    }
    #endregion
}
