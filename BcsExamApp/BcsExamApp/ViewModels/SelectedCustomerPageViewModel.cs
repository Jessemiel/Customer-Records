using BcsExamApp.Constants.enums;
using BcsExamApp.Interfaces;
using BcsExamApp.Model;
using BcsExamApp.Validations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BcsExamApp.ViewModels
{
    public class SelectedCustomerPageViewModel : ViewModelBase
    {

        #region Fields
        private Customer _customerInfo;
        private ValidatableObject<string> _email;
        private Response _customerResponse;
        private readonly ICustomerService _customerService;
        private readonly IPageDialogService _pageDialogService;
        #endregion
        public SelectedCustomerPageViewModel(INavigationService navigationService,
            ICustomerService customerService, 
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _customerService = customerService;
            _pageDialogService = pageDialogService;

            PostReservationCommand = new DelegateCommand(async () => await OnPostReservation());

            SetValidations();
            InitializeData();
        }
        #region Properties        
        public Customer CustomerInfo
        {
            get => _customerInfo;
            set => SetProperty(ref _customerInfo, value);
        }
        public ValidatableObject<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }        
        public Response CustomerResponse
        {
            get => _customerResponse;
            set => SetProperty(ref _customerResponse, value);
        }
        #endregion

        #region Commands
        public DelegateCommand PostReservationCommand { get; set; }
        #endregion

        #region Methods / Functions / Navigations
        private async Task OnPostReservation()
        {
            if (IsBusy) return;
            IsBusy = true;

            ValidateFields();
            ResponseMsg = string.Empty;
            if (!Email.IsValid)
            {
                IsBusy = false;
                return;
            }

            CustomerResponse.UserEmail = Email.Value;
            CustomerResponse.ResId = CustomerInfo.ReservationId;
                        
            var res = await _customerService.PostResponse(CustomerResponse);            
            if (res.Status == Status.success)
            {
                await NavigationService.GoBackAsync();
            }
            else
            {
                ResponseMsg = res.Detail;
            }
            IsBusy = false;

        }
        private void InitializeData()
        {
            Title = "Post a reservation";
            CustomerInfo = new Customer();
            CustomerResponse = new Response();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(parameters.TryGetValue<Customer>(nameof(Customer), out var customer))
            {
                CustomerInfo = customer;
            }
        }
        private void SetValidations()
        {
            Email = new ValidatableObject<string>();
            Email.Rules.Add(new EmptyOrNullValidationRule<string> { ValidationMessage = "Email is required" });
            Email.Rules.Add(new EmailValidationRule<string> { ValidationMessage = "Invalid Email" });
        }
        private void ValidateFields()
        {
            Email.Validate();
        }
        #endregion

    }
}
