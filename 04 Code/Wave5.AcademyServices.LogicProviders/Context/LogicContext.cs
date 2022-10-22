namespace Wave5.AcademyServices;

public class LogicContext
{
    #region [ CTor ]
    public LogicContext(IStudentLogicProvider studentLogicProvider) {
        this.Students = studentLogicProvider;
    }
    #endregion

    #region [ Properties ]
    public IStudentLogicProvider Students { get; set; }
    #endregion
}
