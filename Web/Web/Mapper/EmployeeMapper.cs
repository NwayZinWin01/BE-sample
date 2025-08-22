using Data_Access.Entity;
using Infrastructure.utilities;
using Web.ViewModel;

namespace Web.Mapper
{
    public class EmployeeMapper
    {
        public Employee? MapViewModelToModel(Employee data,EmployeeViewModel vm)
        {
            if (data != null)
            {
                data.name=vm.name;
                data.age=vm.age;
                data.salary=vm.salary;
                data.position=vm.position;
                data.company=vm.company;
            }
            return data;

        }

        public EmployeeViewModel? MapModelToViewModel(Employee data,EmployeeViewModel vm)
        {
            if(data != null)
            {
                vm.id=data.id;
                vm.name=data.name;
                vm.age=data.age;
                vm.salary=data.salary;
                vm.position=data.position;
                vm.company=data.company;

            }
            return vm;
        }


        public PagedResult<EmployeeViewModel> MapModelToListViewModel(PagedResult<Employee> list)
        {
           
            PagedResult<EmployeeViewModel> vmlist = new PagedResult<EmployeeViewModel>();
            foreach (var data in list.data)
            {
                EmployeeViewModel vm = new EmployeeViewModel();
                vm.id = data.id;
                vm.name=data.name;
                vm.age=data.age;
                vm.salary=data.salary;
                vm.position = data.position;
                vm.company=data.company;
                vmlist.data.Add(vm);

            }
            return vmlist;
        }
    }
}
