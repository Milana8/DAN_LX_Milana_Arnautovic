using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    class AddEmployeeViewModel : ViewModelBase
    {
        AddEmployeeView view;
        Service service = new Service();
        Validation validation = new Validation();

        public AddEmployeeViewModel(AddEmployeeView view)
        {
            this.view = view;
            LocationList = service.GetAllLocations();
            ManagerList = service.GetAllEmployees();
            SectorList = service.GetAllSectors();
            Employee = new vwEmployee();
        }
        private bool isUpdateEmployee;
        public bool IsUpdateEmployee
        {
            get
            {
                return isUpdateEmployee;
            }
            set
            {
                isUpdateEmployee = value;
            }
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
        private vwEmployee manager;

        public vwEmployee Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<vwEmployee> managerList;

        public List<vwEmployee> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private vwLocation location;

        public vwLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        private List<vwLocation> locationList;

        public List<vwLocation> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private vwSector sector;

        public vwSector Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }
        private List<vwSector> sectorList;

        public List<vwSector> SectorList
        {
            get
            {
                return sectorList;
            }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
            }
        }




        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }
        private void SaveExecute()
        {

            var result = MessageBox.Show("Are you sure you want to create this employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                try
                {
                    service.AddEmployee(Employee);
                    IsUpdateEmployee = true;
                    service.GetAllEmployees().ToList();
                    MessageBox.Show("You successfully created employee!", "Notification");
                    view.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception" + ex.Message.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Can save
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExecute()
        {

            return true;
        }



        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }

        private void CloseExecute()
        {
            view.Close();
        }
    }

}

