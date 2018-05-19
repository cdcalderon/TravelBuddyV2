namespace TravelBuddy.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Models;
	using Services;
	using Xamarin.Forms;

	public class CountriesViewModel : BaseViewModel
	{
		#region Services
		private ApiService apiService;

		#endregion

		#region Attributes
		private ObservableCollection<Country> countries;
		private bool isRefreshing;
		#endregion

		#region Properties
		public ObservableCollection<Country> Countries
		{
			get { return this.countries; }
			set { SetValue(ref this.countries, value); }
		}

		public bool IsRefreshing
		{
			get { return this.isRefreshing; }
			set { SetValue(ref this.isRefreshing, value); }
		}
		#endregion

		#region Constructors
		public CountriesViewModel()
		{
			this.apiService = new ApiService();
			this.LoadCountries();
		}
		#endregion

		#region Methods
		private async void LoadCountries()
		{
			this.isRefreshing = true;
			var connection = await this.apiService.CheckConnection();

			if (!connection.IsSuccess)
			{
				this.isRefreshing = false;
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					connection.Message,
					"Accept");
				await Application.Current.MainPage.Navigation.PopAsync();
				return;
			}

			var response = await this.apiService.GetList<Country>(
				"https://restcountries.eu",
				"/rest",
				"/v2/all");

			if (!response.IsSuccess)
			{
				this.isRefreshing = false;
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					response.Message,
					"Accept");
				await Application.Current.MainPage.Navigation.PopAsync();
				return;
			}

			var list = (List<Country>)response.Result;
			this.Countries = new ObservableCollection<Country>(list);
			this.isRefreshing = false;
		}
		#endregion

		public ICommand RefreshCommand
		{
			get
			{
				return new RelayCommand(LoadCountries);
			}
		}
	}
}
