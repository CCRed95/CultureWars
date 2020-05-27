using Ccr.MaterialDesign;
using Ccr.MaterialDesign.Controls.Icons;
using Ccr.MaterialDesign.MVVM;

namespace CultureWars.Manager.Models
{
	public class ExpanderItemSelection
	{
		public IconKind Icon { get; set; }

		public string Title { get; set; }

		public ViewModelBase ViewModel { get; set; }

		public Swatch Swatch { get; set; }


		public ExpanderItemSelection()
		{
		}

		public ExpanderItemSelection(
		  IconKind icon,
		  string title,
		  ViewModelBase viewModel,
		  Swatch swatch)
		{
			Icon = icon;
			Title = title;
			ViewModel = viewModel;
			Swatch = swatch;
		}
	}
}
