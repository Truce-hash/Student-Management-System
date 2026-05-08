using ASPNETCore_DB.Models;

namespace ASPNETCore_DB.Interfaces
{
    public interface IStudent
    {
        IQueryable<Student> GetStudents(string searchString, string sortOrder);
        Student Details(string id);
        Student ByEmail(string id); //added by Cruz to ref to the Create httppost in studcontroller
        Student Create(Student student);
        Student Edit(Student student);     
        bool Delete(Student student);
        bool IsExist(string id);
    }
}
