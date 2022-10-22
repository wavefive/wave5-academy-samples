namespace Wave5.AcademyServices.Data;

public class SeedProvider
{
    #region [ Fields ]
    private List<Student> _students;
    #endregion

    #region [ Singleton ]
    private static readonly Lazy<SeedProvider> _current = new Lazy<SeedProvider>(() => new SeedProvider(), LazyThreadSafetyMode.PublicationOnly);
    public static SeedProvider Current {
        get { return _current.Value; }
    }
    #endregion

    #region [ CTor ]
    public SeedProvider() {
        this._students = new List<Student>();
    }
    #endregion

    #region [ Public Methods ]
    public List<Student> LoadStudents() {
        this._students.Clear();
        this._students.Add(new Student() { FirstName = "Thanh", LastName = "Nhat van" });
        this._students.Add(new Student("Tri", "Minh Nguyen"));
        this._students.Add(StudentFactory.Create("Tan", "Ha"));
        this._students.Add("Long", "Phi Nguyen");
        this._students.Add("Huong", "Pham");
        this._students.Add("Khai", "Ngo");
        return this._students;
    }
    #endregion
}