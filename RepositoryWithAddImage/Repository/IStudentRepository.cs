using RepositoryWithAddImage.Models;
using RepositoryWithAddImage.Service;
using RepositoryWithAddImage.ViewModel;

namespace RepositoryWithAddImage.Repository;

public interface IStudentRepository:IRepositoryService<Student,StudentVm>
{
}
