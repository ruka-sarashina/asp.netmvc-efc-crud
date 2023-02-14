using ASPMVCCRUD.Data;
using ASPMVCCRUD.Models;
using ASPMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPMVCCRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDEMODbContext mVCDEMODbContext;

        public EmployeesController(MVCDEMODbContext mVCDEMODbContext)
        {
            this.mVCDEMODbContext = mVCDEMODbContext; // dengan membuat ini, bisa private readonly dan connect databse
        }

        // Membuat fungsi get untuk menampilkan ke layar
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var empolyees = await mVCDEMODbContext.Employees.ToListAsync();
            return View(empolyees);
        }


        // buat fungsi untuk menambahkan karyawan
        // setelah itu kita dpat menampilkan nya ke layar
        //sdsdaasd 
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        //properti ini akan require paramater
        //Add(AddEmployeeViewModel addEmployeeRequest), menerima method yang lalu diberi nama nya
        {
            //1. harus mengubah dari AddEmployeeViewModel -> AddEmployessController
            var employee = new Employee()
            {
                Id = Guid.NewGuid(), //karena ingin menetepkan nilai id, maka require langsung dari guid
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,

                //setelah membuat ini, saat nya memasukkan ke dlam database
            };

            await mVCDEMODbContext.Employees.AddAsync(employee);
            await mVCDEMODbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mVCDEMODbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if(employee != null)
            {
                // membuat variabel yang akan mengarahkan ke UpdateEmployeeViewModel
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id, 
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };
                return await Task.Run(() => View("View", viewModel));
            }

            return View(employee); 
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        { 
            // mengambil data dari databse terlebih dahalu
            var employee = await mVCDEMODbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            { 
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mVCDEMODbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDEMODbContext.Employees.FindAsync(model.Id);  

            if (employee != null)
            {
                mVCDEMODbContext.Employees.Remove(employee);

                await mVCDEMODbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("index");
        }
    }
} 
