using System.Collections.Generic;
using System.Linq;
using SignaturePad.ViewModels;
using Intersoft.Crosslight;

namespace SignaturePad.ViewModels
{
    public class NavigationViewModel : SampleListViewModelBase<NavigationItem>
    {
        #region Properties

        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (_aboutText != value)
                {
                    _aboutText = value;
                    OnPropertyChanged("AboutText");
                }
            }
        }


        private string _aboutText { get; set; }

        #endregion

        #region Constructors

        public NavigationViewModel()
        {
            List<NavigationItem> items = new List<NavigationItem>();

            items.Add(new NavigationItem("Simple Page", "About", typeof(SimpleViewModel)));
            items.Add(new NavigationItem("About This App", "About", typeof(AboutNavigationViewModel)));

            this.SourceItems = items;
            this.RefreshGroupItems();
        }

        #endregion

        #region Methods

        public override void RefreshGroupItems()
        {
            if (this.Items != null)
                this.GroupItems = this.Items.GroupBy(o => o.Group).Select(o => new GroupItem<NavigationItem>(o)).ToList();
        }

        #endregion
    }
}