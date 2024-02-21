using AutoMapper;
using RepositoryWithAddImage.DatabaseContext;
using RepositoryWithAddImage.Models;
using RepositoryWithAddImage.Service;
using RepositoryWithAddImage.ViewModel;

namespace RepositoryWithAddImage.Repository;

public class StudentRepository:RepositoryService<Student,StudentVm>, IStudentRepository
{
    public StudentRepository(StudentDbContext dbContext, IMapper mapper):base(dbContext, mapper)
    {
        
    }
}
