namespace TravelBuddy.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
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
		#endregion
        
		#region Properties
		public ObservableCollection<Country> Countries
		{
			get { return this.countries; }
			set { SetValue(ref this.countries, value); }
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
			var response = await this.apiService.GetList<Country>(
				"https://restcountries.eu",
				"/rest",
				"/v2/all");

			if (!response.IsSuccess)
			{
				await Application.Current.MainPage.DisplayAlert(
					"Error",
					response.Message,
					"Accept");
				
				return;
			}

			var list = (List<Country>)response.Result;

         
			this.Countries = new ObservableCollection<Country>(list);
		}
		#endregion

	}
}
