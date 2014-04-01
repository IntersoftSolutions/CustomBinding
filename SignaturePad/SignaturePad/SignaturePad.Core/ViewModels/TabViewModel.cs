using System.Collections.Generic;
using Intersoft.Crosslight;
using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class TabViewModel : MultiPageViewModelBase
    {
        #region Constructors

        public TabViewModel()
        {
            var items = new List<NavigationItem>();
            items.Add(new NavigationItem("Simple Page", typeof(SimpleViewModel)) { Image = "first.png" });
            items.Add(new NavigationItem("About", typeof(AboutNavigationViewModel)) { Image = "second.png" });

            this.Items = items.ToArray();
        }

        #endregion
    }
}