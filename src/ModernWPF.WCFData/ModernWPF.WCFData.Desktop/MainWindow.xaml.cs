namespace ModernWPF.WCFData.Desktop
{
    using System;
    using System.Collections.ObjectModel;
    using System.Data.Services.Client;
    using System.Linq;
    using System.Windows;
    using ModernWPF.WCFData.Desktop.SampleEmployeeOData;

    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<Employee> employees;

        private readonly Uri dataServiceUri = new Uri("http://localhost:22681/SampleODataService.svc/", UriKind.Absolute);

        private readonly SampleDatabaseContext context;

        public MainWindow()
        {
            this.InitializeComponent();

            this.context = new SampleDatabaseContext(this.dataServiceUri);
            this.employees = new ObservableCollection<Employee>();
            this.EmployeeList.ItemsSource = this.employees;
        }

        private void GetEmployees(object sender, RoutedEventArgs e)
        {
            this.LoadEmployees();
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.NameTextBox.Text))
            {
                MessageBox.Show("El empleado no tiene nombre.", "Nuevo Empleado");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.EmailTextBox.Text))
            {
                MessageBox.Show("El e-mail no es valido.", "Nuevo Empleado");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.AddressTextBox.Text))
            {
                MessageBox.Show("La dirección no es valida.", "Nuevo Empleado");
                return;
            }

            this.AddButton.IsEnabled = false;

            Employee employee = new Employee
            {
                Name = this.NameTextBox.Text,
                Email = this.EmailTextBox.Text,
                AddressLine1 = this.AddressTextBox.Text,
                IsActive = true
            };

            this.context.AddToEmployees(employee);
            this.employees.Add(employee);

            this.context.SaveChanges();

            this.AddButton.IsEnabled = true;
        }

        private void LoadEmployees()
        {
            this.GetButton.IsEnabled = false;
            this.employees.Clear();

            IQueryable<Employee> employeeQuery = from emp in this.context.Employees
                                                 where emp.IsActive
                                                 select emp;

            DataServiceCollection<Employee> employeeList = new DataServiceCollection<Employee>(employeeQuery);

            foreach (Employee employee in employeeList)
            {
                this.employees.Add(employee);
            }

            this.GetButton.IsEnabled = true;
        }
    }
}
