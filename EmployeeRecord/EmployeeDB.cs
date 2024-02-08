using EmployeeDetails;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeRecord
{
    class EmployeeDB
    {
        private List<Employee> employees = new List<Employee>();
        private const string filePath = "employeeDetails.txt";
        public static void Main(string[] args)
        {
            EmployeeDB employeeRecord = new EmployeeDB();
            Console.WriteLine("Employee Record");

            while (true)
            {
                Console.WriteLine("\n 1. Add Employee\n 2. Delete Employee\n 3. Show Employee Record\n " +
                    "4. Search Employee Details by ID\n 5. Search Employee Details by Name\n 6. Update Employee Record\n 7. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        employeeRecord.AddEmployee();
                        break;
                    case 2:
                        employeeRecord.DeleteEmployee();
                        break;
                    case 3:
                        employeeRecord.ShowEmployeeRecord();
                        break;
                    case 4:
                        employeeRecord.SearchEmployeeById();
                        break;
                    case 5:
                        employeeRecord.SearchEmployeeByName();
                        break;
                    case 6:
                        employeeRecord.UpdateEmployeeRecord();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n");
                        break;
                }
            }
        }
        private void AddEmployee()
        {
            Employee employee = new Employee();

            Console.Write("Enter Employee ID: ");
            employee.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Employee Name: ");
            employee.Name = Console.ReadLine();

            Console.Write("Enter Department: ");
            employee.Department = Console.ReadLine();

            Console.Write("Enter Date of Birth (DD-MM-YYYY): ");
            employee.DateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Salary: ");
            employee.Salary = Convert.ToDouble(Console.ReadLine());

            employees.Add(employee);
            SaveToFile();

            Console.WriteLine("Employee added successfully!");
        }
        private void SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var employee in employees)
                {
                    writer.WriteLine($"{employee.Id}, {employee.Name}, {employee.Department}, {employee.DateOfBirth}, {employee.Salary}");
                }
            }
        }
        private void DeleteEmployee()
        {
            Console.WriteLine("Enter Employee ID: ");
            int removeEmployeeId = int.Parse(Console.ReadLine());

            Employee employeeToRemove = employees.FirstOrDefault(e => e.Id == removeEmployeeId);

            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                SaveToFile();
                Console.WriteLine($"Employee with ID {removeEmployeeId} removed successfully.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        public void ShowEmployeeRecord()
        {
            if (File.Exists(filePath))
            {
                List<Employee> employees = new List<Employee>();
                string[] details = File.ReadAllLines(filePath);

                foreach (var detail in details)
                {
                    string[] data = detail.Split(',');

                    Employee employee = new Employee
                    {
                        Id = int.Parse(data[0]),
                        Name = data[1],
                        Department = data[2],
                        DateOfBirth = DateTime.Parse(data[3]),
                        Salary = double.Parse(data[4])
                    };
                    employees.Add(employee);
                }
                Console.WriteLine("Employee Details:");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Department: {emp.Department}, DOB: {emp.DateOfBirth.ToShortDateString()}, Salary: {emp.Salary}");
                }
            }
            else
            {
                Console.WriteLine("No employee details found.");
            }
        }
        public void SearchEmployeeById()
        {
            Console.WriteLine("Enter Employee ID: ");
            int employeeId = int.Parse(Console.ReadLine());

            Employee employee = employees.Find(e => e.Id == employeeId);
            DisplayEmployeeDetails(employee);
        }
        public void SearchEmployeeByName()
        {
            Console.WriteLine("Enter Employee Name: ");
            string employeeName = Console.ReadLine();

            Employee employee = employees.Find(e => e.Name == employeeName);
            DisplayEmployeeDetails(employee);
        }
        private void UpdateEmployeeRecord()
        {
            LoadFromFile();

            Console.Write("Enter Employee ID to update: ");
            int employeeIdToUpdate = Convert.ToInt32(Console.ReadLine());

            Employee employeeToUpdate = employees.Find(e => e.Id == employeeIdToUpdate);

            if (employeeToUpdate != null)
            {
                Console.Write("Enter new Salary: ");
                employeeToUpdate.Salary = Convert.ToDouble(Console.ReadLine());

                SaveToFile();
                Console.WriteLine("Employee record updated successfully!");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
        private void LoadFromFile()
        {
            employees.Clear();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    Employee employee = new Employee
                    {
                        Id = Convert.ToInt32(parts[0]),
                        Name = parts[1],
                        Department = parts[2],
                        DateOfBirth = DateTime.Parse(parts[3]),
                        Salary = Convert.ToDouble(parts[4])
                    };
                    employees.Add(employee);
                }
            }
        }
        public void DisplayEmployeeDetails(Employee employee)
        {
            if (employee != null)
            {
                Console.WriteLine($"Employee Found - ID: {employee.Id}, Name: {employee.Name}, Department: {employee.Department}, DOB: {employee.DateOfBirth.ToShortDateString()}, Salary: {employee.Salary}");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }
    }
}