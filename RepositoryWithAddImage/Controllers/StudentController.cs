using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using RepositoryWithAddImage.Repository;
using RepositoryWithAddImage.ViewModel;

namespace RepositoryWithAddImage.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        this.studentRepository = studentRepository;
    }
    public async Task< ActionResult<StudentVm>> Index(CancellationToken cancellationToken)
    {
        return View(await studentRepository.GetAllAsync(cancellationToken));
    }
    public async Task<ActionResult<StudentVm>> Create(long id, CancellationToken cancellationToken)
    {
        if (id==0)
        {
            return View(new StudentVm());
        }
        else
        {
            var data=await studentRepository.GetByIdAsync(id, cancellationToken);
            return View(data);
        }
    }
    [HttpPost]
    public async Task<ActionResult<StudentVm>> Create(long id, StudentVm vm, CancellationToken cancellationToken,IFormFile photo)
    {
        if (id==0)
        {
            if (ModelState.IsValid)
            {
                if (photo!=null && photo.Length>0)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/student", photo.FileName);
                    using (var stream=new FileStream(path, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    vm.Image = $"{photo.FileName}";
                }
                await studentRepository.InsertAsync(vm, cancellationToken);
                return RedirectToAction("Index");
            }
        }
        else
        {
            if (photo != null && photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image/student", photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                vm.Image = $"{photo.FileName}";
            }
            await studentRepository.UpdateAsync(id, vm, cancellationToken);
            return RedirectToAction("Index");
        }
        return View(new StudentVm());
    }
    public async Task<ActionResult<StudentVm>> Delete(long id, CancellationToken cancellationToken)
    {
        if (id==0)
        {
            return RedirectToAction("Index");
        }
        else
        {
            await studentRepository.DeleteAsync(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
    public async Task<ActionResult<StudentVm>> Details(long id,CancellationToken cancellationToken)
    {
        if (id == 0) { return RedirectToAction("Index"); }
        else
        {
            var data=await studentRepository.GetByIdAsync(id, cancellationToken);
            return View(data);
        }
    }
}
