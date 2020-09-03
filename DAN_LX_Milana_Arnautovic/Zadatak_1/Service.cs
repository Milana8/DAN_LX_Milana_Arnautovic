using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1
{
    class Service
    {
        public bool AddEmployee(vwEmployee employeeToAdd)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    tblEmployee employee = new tblEmployee
                    {
                        NameandSurname = employeeToAdd.NameandSurname,
                        DateOfBirth = employeeToAdd.DateOfBirth,
                        RegistrationNumber = employeeToAdd.RegistrationNumber,
                        JMBG = employeeToAdd.JMBG,
                        Gender = employeeToAdd.Gender,
                        PhoneNumber = employeeToAdd.PhoneNumber,
                        SectorID = employeeToAdd.SectorID,
                        LocationID = employeeToAdd.LocationID,
                        Manager = employeeToAdd.Manager
                    };
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    employeeToAdd.EmployeeID = employee.EmployeeID;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = (from x in context.vwEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public bool EditEmployee(vwEmployee employee)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    tblEmployee employeeToEdit = context.tblEmployees.Where(x => x.EmployeeID == employee.EmployeeID).FirstOrDefault();
                    employeeToEdit.NameandSurname = employee.NameandSurname;
                    employeeToEdit.DateOfBirth = employee.DateOfBirth;
                    employeeToEdit.JMBG = employee.JMBG;
                    employeeToEdit.RegistrationNumber = employee.RegistrationNumber;
                    employeeToEdit.Gender = employee.Gender;
                    employeeToEdit.PhoneNumber = employee.PhoneNumber;
                    employeeToEdit.SectorID = employee.SectorID;
                    employeeToEdit.LocationID = employee.LocationID;
                    employeeToEdit.Manager = employee.Manager;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public bool DeleteEmployee(int employeeID)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {

                    var employeeOfThisManager = context.tblEmployees.Where(x => x.Manager == employeeID).ToList();

                    if (employeeOfThisManager.Count() > 0)
                    {
                        foreach (var employee in employeeOfThisManager)
                        {
                            employee.Manager = null;
                        }
                    }

                    tblEmployee employeeToDelete = context.tblEmployees.Where(x => x.EmployeeID == employeeID).FirstOrDefault();

                    var peopleInSector = context.tblEmployees.Where(x => x.SectorID == employeeToDelete.SectorID).ToList();
                    if (peopleInSector.Count() == 1)
                    {
                        var sector = context.tblSectors.Where(x => x.SectorID == employeeToDelete.SectorID).FirstOrDefault();
                        context.tblSectors.Remove(sector);
                        context.SaveChanges();
                    }

                    context.tblEmployees.Remove(employeeToDelete);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public List<vwEmployee> GetAllManagers(vwEmployee employee)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = context.vwEmployees.Where(x => x.JMBG != employee.JMBG).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblEmployee> GetEmployees()
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    List<tblEmployee> employees = new List<tblEmployee>();
                    employees = (from x in context.tblEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void AddLocations()
        {
            try
            {
                string[] lines = File.ReadAllLines(@"../../Adresa.txt");
                List<string> list = new List<string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    tblLocation location = new tblLocation();
                    list = lines[i].Split(',').ToList();
                    location.Adress = list[0];
                    location.Place = list[1];
                    location.States = list[2];
                    using (DAN_LXEntities1 context = new DAN_LXEntities1())
                    {

                        if (context.tblLocations.Where(x => x.Adress == location.Adress && x.Place == location.Place && x.States == location.States).ToList().Count == 0)
                        {

                            context.tblLocations.Add(location);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public List<vwLocation> GetAllLocations()
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    List<vwLocation> locations = new List<vwLocation>();

                    locations = (from x in context.vwLocations select x).OrderBy(x => x.Adress).ToList();
                    return locations;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public void AddSector(string sectorToAdd)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    tblSector sector = new tblSector
                    {
                        SectorName = sectorToAdd
                    };
                    context.tblSectors.Add(sector);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        public bool IsSectorExists(string sectorName)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    tblSector sector = context.tblSectors.Where(x => x.SectorName == sectorName).FirstOrDefault();
                    if (sector != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }

        public int FindSector(string sectorName)
        {
            try
            {
                using (DAN_LXEntities1 context = new DAN_LXEntities1())
                {
                    return context.vwSectors.Where(x => x.SectorName == sectorName).Select(x => x.SectorID).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return 0;
            }
        }
    }
}




