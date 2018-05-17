namespace TravelBuddy.ViewModels
{
	using System;
	using System.ComponentModel;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Xamarin.Forms;
    
	public class LoginViewModel : INotifyPropertyChanged
    { 
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		//private string email;
		private string password;
		//private bool isRunning;
		//private bool isEnabled;

		#region Properties
		public string Email
		{
			get;
			set;

		}

		public string Password
		{
			get
			{
				return this.password;
			}
			set
			{
				if(this.password != value)
				{
					this.password = value;
					PropertyChanged?.Invoke(
						this,
						new PropertyChangedEventArgs(nameof(this.Password)));
				}
			}
		}

		public bool IsRunning
		{
			get;
			set;
		}

		public bool IsRemembered
		{
			get;
			set;
		}

		public bool IsEnabled
        {
            get;
            set;
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

			if(this.Email != "cdcalderon@gmail.com" || this.Password != "12345") {
				await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect.",
                    "Accept");
				this.Password = string.Empty;
                return;
			}

			await Application.Current.MainPage.DisplayAlert(
                    "Ok",
                    "ohh Yeahhh It worked.",
                    "Accept");
            return;
		}
		#endregion

		#region Constructors
		public LoginViewModel()
		{
			this.IsRemembered = true;
			this.IsEnabled = true;
		}
		#endregion
	}
}
