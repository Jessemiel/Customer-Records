using BcsExamApp.Constants.enums;
using BcsExamApp.Interfaces;
using BcsExamApp.Model;
using BcsExamApp.Validations;
using BcsExamApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BcsExamApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;

        public MainPageViewModel(INavigationService navigationService,
            ICustomerService customerService)
            : base(navigationService)
        {
            _customerService = customerService;

            SearchCustomerCommand = new DelegateCommand(async () => await OnSearchCustomer());
            SelectCustomerCommand = new DelegateCommand<object>(async param => await OnSelectCustomer(param), CanSelectCustomer);

            SetValidations();
            InitializeData();
        }

        #region Properties     
        private ValidatableObject<string> _parkCode;
        public ValidatableObject<string> ParkCode
        {
            get => _parkCode;
            set => SetProperty(ref _parkCode, value);
        }

        private ValidatableObject<DateTime> _arrivingDate;
        public ValidatableObject<DateTime> ArrivingDate
        {
            get => _arrivingDate;
            set => SetProperty(ref _arrivingDate, value);
        }

        private ObservableCollection<Customer> _customers;
        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set => SetProperty(ref _customers, value);
        }
        #endregion

        #region Commands
        public ICommand SearchCustomerCommand { get; set; }
        public ICommand SelectCustomerCommand { get; set; }
        #endregion

        #region Methods / Functions / Navigations
        private bool CanSelectCustomer(object arg)
        {
            return arg != null;
        }
        private async Task OnSelectCustomer(object customerList)
        {
            var selectedCustomer = (customerList as ListView).SelectedItem as Customer;
            var param = new NavigationParameters()
            {
                { nameof(Customer), selectedCustomer }
            };
            await NavigationService.NavigateAsync(nameof(SelectedCustomerPage), param);
        }
        private async Task OnSearchCustomer()
        {
            if (IsBusy) return;
            IsBusy = true;

            ValidateFields();            
            if (!ParkCode.IsValid || !ArrivingDate.IsValid)
            {
                IsBusy = false;
                return;
            }

            var response = await _customerService.GetCustomer(ParkCode.Value, ArrivingDate.Value.ToString("yyyy-MM-dd"));
            ResponseMsg = "No Result(s) found.";
            Customers = new ObservableCollection<Customer>(response.Value);
            IsBusy = false;

            if (response.Status == Status.error)
            {
                ResponseMsg = response.Detail;
            }
            
        }
        private void InitializeData()
        {
            Title = "Customer Search";
            ArrivingDate.Value = DateTime.UtcNow;
        }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(ParkCode.Value != null && ParkCode.IsValid)
            {
                await OnSearchCustomer();
            }
        }
        private void SetValidations()
        {
            ParkCode = new ValidatableObject<string>();
            ParkCode.Rules.Add(new EmptyOrNullValidationRule<string> { ValidationMessage = "Park Code is required" });

            ArrivingDate = new ValidatableObject<DateTime>();
            ArrivingDate.Rules.Add(new EmptyOrNullValidationRule<DateTime> { ValidationMessage = "Arriving date is required" });
            //ArrivingDate.Rules.Add(new DateFormatValidation<DateTime> { ValidationMessage = "Arriving Date format must be yyyy-MM-dd" });
        }
        private void ValidateFields()
        {
            ParkCode.Validate();
            ArrivingDate.Validate();
        }
        #endregion
    }
}