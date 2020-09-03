using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWindowViewModel: ViewModelBase
    {
        MainWindow main;
        Service service = new Service();

        public MainWindowViewModel(MainWindow main)
        {
            this.main = main;
            EmployeeList = service.GetAllEmployees();

        }


        private vwEmployee employee;
        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        private List<vwEmployee> employeeList;
        public List<vwEmployee> EmployeeList
        {
            get { return employeeList; }
            set
            {
                employeeList = value;
                OnPropertyChanged("EmployeeList");
            }
        }


        private ICommand addEmployee;
        /// <summary>
        /// Add employee command
        /// </summary>
        public ICommand AddEmployee
        {
            get
            {
                if (addEmployee == null)
                {
                    addEmployee = new RelayCommand(param => AddEmployeeExecute(), param => CanAddEmployeeExecute());
                }
                return addEmployee;
            }
        }

        /// <summary>
        /// Add employee execute
        /// </summary>
        private void AddEmployeeExecute()
        {
            try
            {
                AddEmployeeView addEmployeeView = new AddEmployeeView();
                addEmployeeView.ShowDialog();
                if ((addEmployeeView.DataContext as AddEmployeeViewModel).IsUpdateEmployee == true)
                {
                    EmployeeList = service.GetAllEmployees().ToList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can add employee
        /// </summary>
        /// <returns></returns>
        private bool CanAddEmployeeExecute()
        {
            return true;
        }



        private ICommand editEmployee;
        /// <summary>
        /// Edit empolyee command
        /// </summary>
        public ICommand EditEmployee
        {
            get
            {
                if (editEmployee == null)
                {
                    editEmployee = new RelayCommand(param => EditEmployeeExecute(), param => CanEditEmployeeExecute());
                }
                return editEmployee;
            }
        }

        /// <summary>
        /// Can Edit Employee
        /// </summary>
        /// <returns></returns>
        public bool CanEditEmployeeExecute()
        {
            return true;
        }
        /// <summary>
        /// Edit employee execute
        /// </summary>
        public void EditEmployeeExecute()
        {
            try
            {
                EditEmployeeView editEmployeeView = new EditEmployeeView(Employee);
                editEmployeeView.ShowDialog();
                EmployeeList = service.GetAllEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private ICommand deleteEmployee;
        /// <summary>
        /// Delete employee command
        /// </summary>
        public ICommand DeleteEmployee
        {
            get
            {
                if (deleteEmployee == null)
                {
                    deleteEmployee = new RelayCommand(param => DeleteEmployeeExecute(), param => CanDeleteEmployeeExecute());
                }
                return deleteEmployee;
            }
        }
        /// <summary>
        /// Can delete employee
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteEmployeeExecute()
        {
            if (Employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Delete employee execute
        /// </summary>
        public void DeleteEmployeeExecute()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure that you want to delete the employee?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    if (Employee != null)
                    {
                        
                        service.DeleteEmployee(Employee.EmployeeID);
                        
                        EmployeeList = service.GetAllEmployees();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

    }
}
