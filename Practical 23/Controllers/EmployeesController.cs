using AutoMapper;
using Business_Access_Layer.AbstractFactory;
using Business_Access_Layer.Factory;
using Business_Access_Layer.Interface;
using Microsoft.AspNetCore.Mvc;
using Practical_23.Dto;
using Practical_23.Interface;
using Practical_23.Model;

namespace Practical_23.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetStudentsAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        [HttpGet("{id:int}", Name = "GetEmployeeByIdAsync")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<EmployeeDto>(employee));
        }

        [HttpGet("{id:int}/hours/{hours:int}")]
        public async Task<ActionResult<EmployeeDtoHourBonus>> GetBounsFromEmployeeId(int id, int hours)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }
            IDepartment? department = null;
            switch (employee.DepartmentId)
            {
                case Department.IT:
                    department = new ITFactory().CreateDepartment();
                    break;
                case Department.Admin:
                    department = new AdminFactory().CreateDepartment();
                    break;
                case Department.Sales:
                    department = new SalesFactory().CreateDepartment();
                    break;
                case Department.OnSite:
                    department = new OnSiteFactory().CreateDepartment();
                    break;
                default:
                    break;
            }
            var employeeHourBouns = _mapper.Map<EmployeeDtoHourBonus>(employee);
            employeeHourBouns.Hours = hours;
            if (department is not null)
            {
                employeeHourBouns.Bouns = department.CalculateOvertime(hours);
            }
            return Ok(employeeHourBouns);
        }

        [HttpGet("{id:int}/abstract/hours/{hours:int}")]
        public async Task<ActionResult<EmployeeDtoHourBonus>> GetBounsFromEmployeeIdUsingAbstract(int id, int hours)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee is null)
            {
                return NotFound();
            }

            IAbstractFactory? abstractFactory = null;
            IDepartment? department = null;
            switch (employee.DepartmentId)
            {
                case Department.IT:
                    abstractFactory = new ITAbstractFactory();
                    department = abstractFactory.CreateDepartment();
                    break;
                case Department.Admin:
                    abstractFactory = new AdminAbstractFactory();
                    department = abstractFactory.CreateDepartment();
                    break;
                case Department.Sales:
                    abstractFactory = new SalesAbstractFactory();
                    department = abstractFactory.CreateDepartment();
                    break;
                case Department.OnSite:
                    abstractFactory = new OnSiteAbstractFactory();
                    department = abstractFactory.CreateDepartment();
                    break;
                default:
                    break;
            }
            var employeeHourBouns = _mapper.Map<EmployeeDtoHourBonus>(employee);
            employeeHourBouns.Hours = hours;
            if (department is not null)
            {
                employeeHourBouns.Bouns = department.CalculateOvertime(hours);
            }
            return Ok(employeeHourBouns);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(CreateEmployeeDto createEmployee)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList());
                return UnprocessableEntity(ModelState);
            }
            var employeeToInsert = _mapper.Map<Employee>(createEmployee);
            await _employeeRepository.CreateEmployeeAsync(employeeToInsert);
            await _employeeRepository.SaveChangesAsync();
            var createdEmployeeToReturn = _mapper.Map<EmployeeDto>(employeeToInsert);

            return CreatedAtRoute("GetEmployeeByIdAsync", new { id = createdEmployeeToReturn.Id }, createdEmployeeToReturn);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployee)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employeeEntity is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList());
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(updateEmployee, employeeEntity);

            await _employeeRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployeeAsync(int id)
        {
            var employeeEntity = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employeeEntity is null)
            {
                return NotFound();
            }
            await _employeeRepository.DeleteEmployeeAsync(employeeEntity);
            await _employeeRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
