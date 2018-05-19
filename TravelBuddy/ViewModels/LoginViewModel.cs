namespace TravelBuddy.ViewModels
{
	
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using TravelBuddy.Views;
	using Xamarin.Forms;
    
	public class LoginViewModel : BaseViewModel
    { 
		      
		#region Attributes
		private string password;
		private bool isRunning;
		private bool isEnabled;
		private string email;
		#endregion

		#region Properties
		public string Email
		{
			get { return this.email; }
			set { SetValue(ref this.email, value); }
		}

		public string Password
		{
			get { return this.password; }
			set { SetValue(ref this.password, value); }
		}

		public bool IsRunning
		{
			get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
		}

		public bool IsRemembered
		{
			get;
			set;
		}

		public bool IsEnabled
        {
			get { return this.isEnabled; }
			set { SetValue(ref this.isEnabled, value); }
        }
		#endregion

        #region Commands
        public ICommand LoginCommand
		{
			get
			{
				return new RelayCommand(Login);
			}
		}



		private async void Login()
		{
			if (string.IsNullOrEmpty(this.Email))
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error", 
					"You must enter an email.", 
					"Accept");
				return;
			}

			if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an Password.",
                    "Accept");
                return;
            }

			this.IsRunning = true;
			this.isEnabled = false;

			if(this.Email != "cdcalderon@gmail.com" || this.Password != "12345") {
				this.IsRunning = false;
                this.isEnabled = true;
				await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect.",
                    "Accept");
				this.Password = string.Empty;
                return;
			}

			this.IsRunning = false;
			this.isEnabled = true;

			this.Email = string.Empty;
			this.Password = string.Empty;

			MainViewModel.GetInstance().Countries = new CountriesViewModel();
			await Application.Current.MainPage.Navigation.PushAsync(new CountriesPage());

		}
		#endregion

		#region Constructors
		public LoginViewModel()
		{
			this.IsRemembered = true;
			this.IsEnabled = true;

			this.Email = "cdcalderon@gmail.com";
			this.Password = "12345";
		}
		#endregion
	}
}
