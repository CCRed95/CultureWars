using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Ccr.MaterialDesign;
using Ccr.MaterialDesign.Controls.Icons;
using Ccr.MaterialDesign.MVVM;
using Ccr.MaterialDesign.Static;
using CultureWars.Manager.Models;

namespace CultureWars.Manager.ViewModels
{
	public static class ExpanderItemSelectionExtensions
	{
		internal static ExpanderItemSelection GenerateItem<TViewModel>(
			this (IconKind iconKind, string itemName, TViewModel viewModel, Swatch swatch) @this)
				where TViewModel
					: ViewModelBase
		{
			return new ExpanderItemSelection(
				@this.iconKind,
				@this.itemName,
				@this.viewModel,
				@this.swatch);
		}
	}


	public class RootViewModel
		: ViewModelBase
	{

		private ObservableCollection<ExpanderItemSelection> _magazineProcessingTasks;
		private ObservableCollection<ExpanderItemSelection> _naturalLanguageProcessingTasks;
		private ObservableCollection<ExpanderItemSelection> _speechRecognitionAnalyticsTasks;
		private ObservableCollection<ExpanderItemSelection> _literaryPublishingTasks;
		private ObservableCollection<ExpanderItemSelection> _mediaPublishingTasks;
		private ObservableCollection<ExpanderItemSelection> _entityLibraryItems;
		private ObservableCollection<ExpanderItemSelection> _contentLibraryItems;

		private ExpanderItemSelection _selectedMenuItem;



		public ObservableCollection<ExpanderItemSelection> MagazineProcessingTasks
		{
			get => _magazineProcessingTasks;
			set
			{
				_magazineProcessingTasks = value;
				NotifyOfPropertyChange(() => MagazineProcessingTasks);
			}
		}

		public ObservableCollection<ExpanderItemSelection> NaturalLanguageProcessingTasks
		{
			get => _naturalLanguageProcessingTasks;
			set
			{
				_naturalLanguageProcessingTasks = value;
				NotifyOfPropertyChange(() => NaturalLanguageProcessingTasks);
			}
		}

		public ObservableCollection<ExpanderItemSelection> SpeechRecognitionAnalyticsTasks
		{
			get => _speechRecognitionAnalyticsTasks;
			set
			{
				_speechRecognitionAnalyticsTasks = value;
				NotifyOfPropertyChange(() => SpeechRecognitionAnalyticsTasks);
			}
		}

		public ObservableCollection<ExpanderItemSelection> LiteraryPublishingTasks
		{
			get => _literaryPublishingTasks;
			set
			{
				_literaryPublishingTasks = value;
				NotifyOfPropertyChange(() => LiteraryPublishingTasks);
			}
		}

		public ObservableCollection<ExpanderItemSelection> MediaPublishingTasks
		{
			get => _mediaPublishingTasks;
			set
			{
				_mediaPublishingTasks = value;
				NotifyOfPropertyChange(() => MediaPublishingTasks);
			}
		}

		public ObservableCollection<ExpanderItemSelection> EntityLibraryItems
		{
			get => _entityLibraryItems;
			set
			{
				_entityLibraryItems = value;
				NotifyOfPropertyChange(() => EntityLibraryItems);
			}
		}

		public ObservableCollection<ExpanderItemSelection> ContentLibraryItems
		{
			get => _contentLibraryItems;
			set
			{
				_contentLibraryItems = value;
				NotifyOfPropertyChange(() => ContentLibraryItems);
			}
		}


		public ExpanderItemSelection SelectedMenuItem
		{
			get => _selectedMenuItem;
			set
			{
				_selectedMenuItem = value;
				NotifyOfPropertyChange(() => SelectedMenuItem);
			}
		}

		private TextSummarizerViewModel _textSummarizerView;
		public TextSummarizerViewModel TextSummarizerView
		{
			get => _textSummarizerView;
			set
			{
				_textSummarizerView = value;
				NotifyOfPropertyChange(() => TextSummarizerView);
			}
		}


		public RootViewModel()
		{
			MagazineProcessingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.Scanner, "OCR Scanner", new TextSummarizerViewModel(), PaletteResources.Pink).GenerateItem(),
				(IconKind.TagTextOutline, "OCR Rectifier",new TextSummarizerViewModel(), PaletteResources.Pink).GenerateItem(),
				(IconKind.FormatListBulleted, "Issue Structural Analyzer", new TextSummarizerViewModel(), PaletteResources.Pink).GenerateItem()
			};

			NaturalLanguageProcessingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.Information, "Text Summarizer", new TextSummarizerViewModel(), PaletteResources.Purple).GenerateItem(),
				(IconKind.FormatListNumbers, "Topic Extractor",new TextSummarizerViewModel(), PaletteResources.Purple).GenerateItem()
			};

			SpeechRecognitionAnalyticsTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.CommentTextOutline, "Transcript Generator", new TextSummarizerViewModel(), PaletteResources.Blue).GenerateItem(),
				(IconKind.ClosedCaption, "Closed Captions Fragmentor", new TextSummarizerViewModel(), PaletteResources.Blue).GenerateItem(),
				(IconKind.Transcribe, "Transcript Topic Extractor", new TextSummarizerViewModel(), PaletteResources.Blue).GenerateItem(),
			};

			LiteraryPublishingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.FilePdfBox, "DRM Protections Generator", new TextSummarizerViewModel(), PaletteResources.Cyan).GenerateItem(),
				(IconKind.Web, "Website Posts Generator", new TextSummarizerViewModel(), PaletteResources.Cyan).GenerateItem(),
				(IconKind.TextToSpeech, "Audiobook/TTS Generator",new TextSummarizerViewModel(), PaletteResources.Cyan).GenerateItem()
			};

			MediaPublishingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.InformationOutline, "ID3 Metadata Applicator", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.More, "Metadata Normalizer", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.Transfer, "Metadata Transformer", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.TagMultiple, "Topic and Tag Generation", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.Transcribe, "Transcript Generator", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.Web, "Website Post Generator", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.Audiobook, "Audio Podcast Exporter", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem(),
				(IconKind.Shuffle, "Media Converter", new TextSummarizerViewModel(), PaletteResources.Teal).GenerateItem()
			};

			EntityLibraryItems = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.TagOutline, "Tags", new TextSummarizerViewModel(), PaletteResources.Yellow).GenerateItem(),
				(IconKind.Pen, "Authors", new TextSummarizerViewModel(), PaletteResources.Yellow).GenerateItem(),
				(IconKind.Television, "Interviewers/Hosts", new TextSummarizerViewModel(), PaletteResources.Yellow).GenerateItem()
			};

			ContentLibraryItems = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.Book, "Books", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem(),
				(IconKind.BookMultiple, "Volumes", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem(),
				(IconKind.BookMultipleVariant, "Issues", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem(),
				(IconKind.FormatListBulleted, "Articles", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem(),
				(IconKind.Audiobook, "Audio Podcasts", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem(),
				(IconKind.FileVideo, "Videos and Interviews", new TextSummarizerViewModel(), PaletteResources.Orange).GenerateItem()
			};

			TextSummarizerView = new TextSummarizerViewModel();
		}



		public void OnViewUnloaded()
		{
			foreach (var item in MagazineProcessingTasks)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in NaturalLanguageProcessingTasks)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in SpeechRecognitionAnalyticsTasks)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in LiteraryPublishingTasks)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in MediaPublishingTasks)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in EntityLibraryItems)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			foreach (var item in ContentLibraryItems)
			{
				if (item.ViewModel is IDisposable disposable)
				{
					disposable?.Dispose();
				}
			}

			TextSummarizerView.Dispose();
		}
	}
}


/*
 			MediaProcessingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.FileVideo, "Video Conversion", PaletteResources.Pink).GenerateItem(), 
				(IconKind.FileVideo, "Video Conversion", PaletteResources.Purple).GenerateItem(),
				(IconKind.FileVideo, "Advanced Video Conversion", PaletteResources.DeepPurple).GenerateItem(),
				(IconKind.Audiobook, "Audio Conversion", PaletteResources.Indigo).GenerateItem(),
				(IconKind.Audiobook, "Advanced Audio Conversion", PaletteResources.Blue).GenerateItem(),
				(IconKind.VideoOff, "Export to Audio Podcast", PaletteResources.LightBlue).GenerateItem(),
				(IconKind.FileMultiple, "Batch Converter", PaletteResources.Cyan).GenerateItem(),
			};

			MetadataTaggingTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.Information, "ID3 / Windows Metadata", PaletteResources.Pink).GenerateItem(),
				(IconKind.FileMultiple, "Metadata Normalizer", PaletteResources.Purple).GenerateItem(),
				(IconKind.ReorderHorizontal, "Metadata Transformations", PaletteResources.DeepPurple).GenerateItem(),
				(IconKind.TagMultiple, "Tags and Topics", PaletteResources.Indigo).GenerateItem(),
				(IconKind.Transcribe, "Transcript Generator", PaletteResources.Blue).GenerateItem(),
				(IconKind.RadioTower, "Broadcasting Metadata", PaletteResources.LightBlue).GenerateItem(),
			};

			LibraryToolsTasks = new BindableCollection<ExpanderItemSelection>
			{
				(IconKind.Shovel, "Action", PaletteResources.Pink).GenerateItem(),
				(IconKind.Netflix, "Adventure", PaletteResources.Purple).GenerateItem(),
				(IconKind.Coffee, "Casual", PaletteResources.DeepPurple).GenerateItem(),
				(IconKind.ShieldOutline, "Strategy", PaletteResources.Indigo).GenerateItem(),
				(IconKind.LibraryBooks, "Intellectual", PaletteResources.Blue).GenerateItem(),
				(IconKind.Football, "Sport", PaletteResources.LightBlue).GenerateItem(),
			};
 */


//private ObservableCollection<ExpanderItemSelection> _categoriesExpanderItems;
//public ObservableCollection<ExpanderItemSelection> CategoriesExpanderItems
//{
//	get => _categoriesExpanderItems;
//	set
//	{
//		_categoriesExpanderItems = value;
//		NotifyOfPropertyChange(() => CategoriesExpanderItems);
//	}
//}

//private ObservableCollection<ExpanderItemSelection>  _mediaProcessingTasks;
//public ObservableCollection<ExpanderItemSelection> MediaProcessingTasks
//{
//	get => _mediaProcessingTasks;
//	set
//	{
//		_mediaProcessingTasks = value;
//		NotifyOfPropertyChange(() => MediaProcessingTasks);
//	}
//}

//private ObservableCollection<ExpanderItemSelection>  _metadataTaggingTasks;
//public ObservableCollection<ExpanderItemSelection> MetadataTaggingTasks
//{
//	get => _metadataTaggingTasks;
//	set
//	{
//		_metadataTaggingTasks = value;
//		NotifyOfPropertyChange(() => MetadataTaggingTasks);
//	}
//}

//private ObservableCollection<ExpanderItemSelection>  _libraryToolsTasks;
//public ObservableCollection<ExpanderItemSelection> LibraryToolsTasks
//{
//	get => _libraryToolsTasks;
//	set
//	{
//		_libraryToolsTasks = value;
//		NotifyOfPropertyChange(() => LibraryToolsTasks);
//	}
//}

/*	MagazineProcessingTasks = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.Scanner, "OCR Scanner", PaletteResources.Red).GenerateItem(), 
			(IconKind.TagTextOutline, "OCR Rectifier", PaletteResources.Red).GenerateItem(),
			(IconKind.FormatListBulleted, "Issue Structural Analyzer", PaletteResources.Red).GenerateItem()
		};

		NaturalLanguageProcessingTasks = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.Information, "Text Summarizer", PaletteResources.Pink).GenerateItem(),
			(IconKind.FormatListNumbers, "Topic Extractor", PaletteResources.Pink).GenerateItem()
		};

		SpeechRecognitionAnalyticsTasks = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.CommentTextOutline, "Transcript Generator", PaletteResources.Purple).GenerateItem(),
			(IconKind.ClosedCaption, "Closed Captions Fragmentor", PaletteResources.Purple).GenerateItem(),
			(IconKind.Transcribe, "Transcript Topic Extractor", PaletteResources.Purple).GenerateItem(),
		};

		LiteraryPublishingTasks = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.FilePdfBox, "DRM Protections Generator", PaletteResources.DeepPurple).GenerateItem(),
			(IconKind.Web, "Website Posts Generator", PaletteResources.DeepPurple).GenerateItem(),
			(IconKind.TextToSpeech, "Audiobook/TTS Generator", PaletteResources.DeepPurple).GenerateItem()
		};

		MediaPublishingTasks = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.InformationOutline, "ID3 Metadata Applicator", PaletteResources.Indigo).GenerateItem(),
			(IconKind.More, "Metadata Normalizer", PaletteResources.Indigo).GenerateItem(),
			(IconKind.Transfer, "Metadata Transformer", PaletteResources.Indigo).GenerateItem(),
			(IconKind.TagMultiple, "Topic and Tag Generation", PaletteResources.Indigo).GenerateItem(),
			(IconKind.Transcribe, "Transcript Generator", PaletteResources.Indigo).GenerateItem(),
			(IconKind.Web, "Website Post Generator", PaletteResources.Indigo).GenerateItem(),
			(IconKind.Audiobook, "Audio Podcast Exporter", PaletteResources.Indigo).GenerateItem(),
			(IconKind.Shuffle, "Media Converter", PaletteResources.Indigo).GenerateItem()
		};

		EntityLibraryItems = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.TagOutline, "Tags", PaletteResources.Blue).GenerateItem(),
			(IconKind.Pen, "Authors", PaletteResources.Blue).GenerateItem(),
			(IconKind.Television, "Interviewers/Hosts", PaletteResources.Blue).GenerateItem()
		};

		ContentLibraryItems = new BindableCollection<ExpanderItemSelection>
		{
			(IconKind.Book, "Books", PaletteResources.LightBlue).GenerateItem(),
			(IconKind.BookMultiple, "Volumes", PaletteResources.LightBlue).GenerateItem(),
			(IconKind.BookMultipleVariant, "Issues", PaletteResources.LightBlue).GenerateItem(),
			(IconKind.FormatListBulleted, "Articles", PaletteResources.LightBlue).GenerateItem(),
			(IconKind.Audiobook, "Audio Podcasts", PaletteResources.LightBlue).GenerateItem(),
			(IconKind.FileVideo, "Videos and Interviews", PaletteResources.LightBlue).GenerateItem()
		};*/
