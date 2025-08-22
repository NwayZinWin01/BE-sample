using Data_Access.Entity;
using Data_Access.Interface;
using Infrastructure.utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.common;
using Web.Mapper;
using Web.ViewModel;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController :BaseController
    {
        IEmployeeRepository _repo;
        EmployeeMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository) 
        {
           _repo = employeeRepository;
            _mapper = new EmployeeMapper();
        
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
           PagedResult<EmployeeViewModel> result = new PagedResult<EmployeeViewModel>();
            result = GetAll();
            return Json(result);
        }

        private PagedResult<EmployeeViewModel> GetAll()
        {
            QueryOptions<Employee> queryOptions =GetQueryOptions<Employee>();
            PagedResult<Employee> data =_repo.GetPagedResults(queryOptions);
            PagedResult<EmployeeViewModel> vm=_mapper.MapModelToListViewModel(data);
            return vm;
        }

        [HttpPost]
        [Route("SaveOrUpdate")]
        public IActionResult SaveOrUpdate(EmployeeViewModel vm)
        {
            CommandResult<Employee> result = new CommandResult<Employee>();
            if (vm.id > 0)
            {
              var  data = _repo.Get(vm.id);
                data = _mapper.MapViewModelToModel(data, vm);
                result=_repo.Save(data);

            }
            else
            {
                Employee data = new Employee();
                data = _mapper.MapViewModelToModel(data, vm);
                result=_repo.Save(data);

            }

            return Json(result);
        }

      
    }
}
