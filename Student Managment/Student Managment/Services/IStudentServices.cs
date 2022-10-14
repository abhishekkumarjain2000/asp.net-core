using Student_Managment.Models;

namespace Student_Managment.Services
{
    public interface IStudentServices
    {
        public List<Student> GetStudentList();
        public string InsertStudent(Student student);
        public string UpdateStudent(Student student);
        public string DeleteStudent(int Student_Id);
    }
}
