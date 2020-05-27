using System;
using System.Windows.Input;
using Ccr.MaterialDesign.MVVM;
using CultureWars.API.Smmry;
using CultureWars.API.Smmry.Domain;

namespace CultureWars.Manager.ViewModels
{
	public class TextSummarizerViewModel
		: ViewModelBase,
			IDisposable
	{
		private readonly SmmryAPI _smmryApi = new SmmryAPI();
		private const string _smmryApiKey = "6A7C2D467D";

		private string _smmryRequestWebsiteUrl;
		private int _smmrySentenceLength = 7;
		private double _charReductionPercentage;
		private string _smmryApiResponse;

		
		public string SmmryRequestWebsiteUrl
		{
			get => _smmryRequestWebsiteUrl;
			set
			{
				_smmryRequestWebsiteUrl = value;
				NotifyOfPropertyChange(() => SmmryRequestWebsiteUrl);
			}
		}

		public int SmmrySentenceLength
		{
			get => _smmrySentenceLength;
			set
			{
				//var _oldSmmrySentenceLength = _smmrySentenceLength;
				//if (_oldSmmrySentenceLength != value)
				//{
				_smmrySentenceLength = value;
					NotifyOfPropertyChange(() => SmmrySentenceLength);
					RequresAPIRequery = true;
				//}
				//if ( 
				//QuerySmmryApiCommand?.Execute(null);
			}
		}

		private bool _requresAPIRequery = false;
		public bool RequresAPIRequery
		{
			get => _requresAPIRequery;
			set
			{
				_requresAPIRequery = value;
				NotifyOfPropertyChange(() => RequresAPIRequery);
			}
		}

		
		public double CharReductionPercentage
		{
			get => _charReductionPercentage;
			set
			{
				_charReductionPercentage = value;
				NotifyOfPropertyChange(() => CharReductionPercentage);
			}
		}

		public string SmmryApiResponse
		{
			get => _smmryApiResponse;
			set
			{
				_smmryApiResponse = value;
				NotifyOfPropertyChange(() => SmmryApiResponse);
			}
		}


		public ICommand QuerySmmryApiCommand => new Command(
			async t =>
			{
				var parameters = new SmmryParameters
				{
					ApiKey = _smmryApiKey,
					Url = SmmryRequestWebsiteUrl,
					SentenceCount = SmmrySentenceLength,
				};

				var result = await _smmryApi.QueryAsync(parameters);
				var text = result.Content;
				SmmryApiResponse = text;
				CharReductionPercentage = double.Parse
					
					(result.ContentReduced.Replace("%", ""));
			});


		public void OnViewUnloaded()
		{
			Dispose();
		}
		
		/// <inheritdoc />
		public void Dispose()
		{
			_smmryApi?.Dispose();
		}
	}
}
