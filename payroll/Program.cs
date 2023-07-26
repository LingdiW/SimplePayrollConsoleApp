using System;
using System.Globalization;

namespace PayrollSystem
{
    //Create an employee struct
    public struct Employee
    {
        public int employeeID;
        public string firstName;
        public string lastName;
        public double annualIncome;
        public double kiwiSaver;
        public double fortnightPayroll;
        public double hourlyWage;
    }
    class Program
    {
        // Main method
        public static void Main(string[] args)
        {
            //Create an array named employees and the value of it is the return value of the ReadEmployeeDetails function.
            Employee[] employees = ReadEmployeeDetails("payroll.txt");
            Console.WriteLine("NewKiwi Garage payroll system");
            
            bool exit = false;
            while (exit == false)//The while loop will make sure the menu will display everytime except exiting.
            {
                //This is the menu
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("1. Fortnight payroll calculation");
                Console.WriteLine("2. Sort and display the employee records");
                Console.WriteLine("3. Search for an employee");
                Console.WriteLine("4. Save into text file");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your option number: ");
                int option = int.Parse(Console.ReadLine());
                Console.WriteLine();

                //switch statement
                switch (option)
                {
                    case 1:
                        FortnightPayrollCalculation(employees);
                        DisplayEmployees(employees);
                        break;
                    case 2:
                        SortEmployeeRecords(employees);
                        DisplayEmployees(employees);
                        break;
                    case 3:
                        // ask user to input an employee ID
                        Console.Write("Enter employee ID: ");
                        int id = int.Parse(Console.ReadLine());
                        SearchEmployee(employees, id);
                        break;
                    case 4:
                        SaveEmployeeDetails(employees);
                        Console.WriteLine("Employee payroll details are saved successfully.");
                        break;
                    case 0:
                        // when exit = true, the program will stop
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option number. Please enter your option number again.");
                        break;
                }
            }
        }

        //function for reading the employees details
        static Employee[] ReadEmployeeDetails(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            Employee[] employees = new Employee[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                Employee employee = new Employee();
                employee.employeeID = int.Parse(parts[0]);
                employee.firstName = parts[1];
                employee.lastName = parts[2];
                employee.annualIncome = double.Parse(parts[3]);
                employee.kiwiSaver = double.Parse(parts[4]);
                 
                employees[i] = employee;
            }
            return employees;
        }

        //Fortnight Payroll Calculation function
        static void FortnightPayrollCalculation(Employee[] employees)
        {
            double tax;
            double kiwisaver;

            for (int i = 0; i < employees.Length; i++)
            {
                if (employees[i].annualIncome <= 14000)//use the condition to calculate the tax
                {
                    tax = employees[i].annualIncome * 0.105;
                }
                else if (employees[i].annualIncome > 14000 && employees[i].annualIncome <= 48000)
                {
                    tax = 14000 * 0.105 + (employees[i].annualIncome - 14000) * 0.175;
                }
                else if (employees[i].annualIncome > 48000 && employees[i].annualIncome <= 70000)
                {
                    tax = 14000 * 0.105 + (48000 - 14000) * 0.175 + (employees[i].annualIncome - 48000) * 0.3;
                }
                else if (employees[i].annualIncome > 70000 && employees[i].annualIncome <= 180000)
                {
                    tax = 14000 * 0.105 + (48000 - 14000) * 0.175 + (70000 - 48000) * 0.3 + (employees[i].annualIncome - 70000) * 0.33;
                }
                else
                {
                    tax = 14000 * 0.105 + (48000 - 14000) * 0.175 + (70000 - 48000) * 0.3 + (180000 - 70000) * 0.33 + 
                        (employees[i].annualIncome - 180000) * 0.39;
                }
                kiwisaver = employees[i].annualIncome * employees[i].kiwiSaver;
                employees[i].hourlyWage = employees[i].annualIncome / 52 / 40;
                employees[i].fortnightPayroll = (employees[i].annualIncome - kiwisaver - tax)/52 * 2;
            }
        } 

        //bubble sort function
        static void SortEmployeeRecords(Employee[] employees)
        {
            for(int i = 0; i < employees.Length - 1; i++)
            {
                for(int j = 0; j < employees.Length -i-1; j++)
                {
                    if (employees[j].employeeID > employees[j+1].employeeID)
                    {
                        //swap employees[j] and employees[j+1]
                        Employee temp = employees[j];
                        employees[j] = employees[j+1];
                        employees[j+1] = temp;
                    }
                }
            }
       }

        //Search function
        static void SearchEmployee(Employee[] employees, int id)
        {
            foreach (Employee employee in employees)
            {
                if (employee.employeeID == id)
                {
                    Console.WriteLine("Employee ID\tFirst Name\tLast Name\tAnnual Income\tKiwiSaver\tFortnightPayroll\tHoutly Wage");
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t{3:C}\t{4:P}\t\t{5:C}\t\t{6:C}", 
                        employee.employeeID, employee.firstName, employee.lastName, employee.annualIncome, 
                        employee.kiwiSaver,employee.fortnightPayroll,employee.hourlyWage);
                    return;
                }
            }
            Console.WriteLine("Employee with ID {0} not found.", id);
        }

        // Save all employee payroll details into a text file
        static void SaveEmployeeDetails(Employee[] employees)
        {
            // create a new text file
            using (StreamWriter writer = new StreamWriter("weekly_payroll.txt"))
            {
                writer.WriteLine("Employee ID\tFirst Name\tLast Name\tAnnual Income\tKiwiSaver\tFortnightPayroll\tHourly Wage");
                foreach (Employee employee in employees)
                {
                    writer.WriteLine("{0}\t\t{1}\t\t{2}\t{3:C}\t{4:P}\t\t{5:C}\t\t{6:C}", 
                        employee.employeeID, employee.firstName, employee.lastName, employee.annualIncome, 
                        employee.kiwiSaver, employee.fortnightPayroll,employee.hourlyWage);
                }
            }
        }

        //Display function
        static void DisplayEmployees(Employee[] employees)
        {
            Console.WriteLine("Employee ID\tFirst Name\tLast Name\tAnnual Income\tKiwiSaver\tFortnightPayroll\tHourly Wage");
            foreach (Employee employee in employees)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t{3:C}\t{4:P}\t\t{5:C}\t\t{6:C}", 
                    employee.employeeID, employee.firstName, employee.lastName, employee.annualIncome, 
                    employee.kiwiSaver, employee.fortnightPayroll, employee.hourlyWage);
            }
        }
    }
}
