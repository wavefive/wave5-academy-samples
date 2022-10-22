namespace Wave5.AcademyServices.ConsoleApp;

public class StudentConsoleWriterProvider
{
    #region [ Fields ]
    private readonly IStudentLogicProvider _studentLogic;
    #endregion

    #region [ CTor ]
    public StudentConsoleWriterProvider(IStudentLogicProvider studentLogic) {
        this._studentLogic = studentLogic;
    }
    #endregion

    #region [ Public Methods - Get List ]
    public async Task WriteAllAsync() {
        var items = await this._studentLogic.GetAllAsync();
        this.WriteStudents("All Students", items);
    }

    public async Task WriteActiveAsync() {
        var items = await this._studentLogic.GetAllAsync();
        this.WriteStudents("Active Students", items);
    }

    public async Task WriteInActiveAsync() {
        var items = await this._studentLogic.GetAllAsync();
        this.WriteStudents("InActive Students", items);
    }
    #endregion

    #region [ Private Methods - Helpers ]
    private void WriteHeader(string title) {
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"{title}");
        Console.WriteLine("----------------------------------------");
    }

    private void WriteFooter() {
        Console.WriteLine();
    }

    private void WriteStudents(string title, List<Student> students) {
        this.WriteHeader(title);
        students.ForEach(x => this.WriteStudent(x));
        this.WriteFooter();
    }

    private void WriteStudent(Student student) {
        Console.WriteLine($"{student.GetDisplayName()}");
    }
    #endregion

}