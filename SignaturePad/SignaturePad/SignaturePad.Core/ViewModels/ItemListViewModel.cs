using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SignaturePad.Infrastructure;
using SignaturePad.ModelServices;
using SignaturePad.Models;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class ItemListViewModel : EditableListViewModelBase<Item>
    {
        #region Properties

        public string DeleteText
        {
            get
            {
                if (this.SelectedItems == null || this.SelectedItems.Count() == 0)
                    return "Delete";
                else
                    return "Delete (" + this.SelectedItems.Count() + ")";
            }
        }

        public DelegateCommand MarkSoldCommand { get; set; }

        public string MarkSoldText
        {
            get
            {
                if (this.SelectedItems == null || this.SelectedItems.Count() == 0)
                    return "Mark as Sold";
                else
                    return "Mark as Sold (" + this.SelectedItems.Count() + ")";
            }
        }

        public DelegateCommand NavigateGroupCommand { get; set; }

        private IItemRepository Repository
        {
            get
            {
                if (Container.Current.CanResolve<IItemRepository>())
                    return Container.Current.Resolve<IItemRepository>();
                else
                    return new ItemRepository(); // for designer support
            }
        }

        public string TitleText
        {
            get { return "Inventories (" + this.Items.Count() + ")"; }
        }

        public string TotalItemsText
        {
            get
            {
                if (this.Items.Count() == 0)
                    return "No items.";
                else if (this.Items.Count() == 1)
                    return "1 item";
                else
                    return this.Items.Count() + " items";
            }
        }

        #endregion

        #region Constructors

        public ItemListViewModel()
        {
            // source items, should be plain items, not sorted or filtered
            this.SourceItems = this.Repository.GetAll().ToObservable();

            // set group items
            this.RefreshGroupItems();

            // commands
            this.NavigateGroupCommand = new DelegateCommand(ExecuteNavigateGroup);
            this.MarkSoldCommand = new DelegateCommand(ExecuteMarkSold, CanExecuteMarkSold);
        }

        #endregion

        #region Methods

        protected override bool CanExecuteDelete(object parameter)
        {
            if (parameter is Item)
                return true;
            else if (parameter is IEnumerable<Item>)
                return ((IEnumerable<Item>)parameter).Count() > 0;

            return false;
        }

        protected override bool CanExecuteEdit(object parameter)
        {
            return (this.SelectedItem != null || (parameter != null && parameter is Item));
        }

        private bool CanExecuteMarkSold(object parameter)
        {
            if (parameter is Item)
                return true;
            else if (parameter is IEnumerable<Item>)
                return ((IEnumerable<Item>)parameter).Count() > 0;

            return false;
        }

        protected override void ExecuteAdd(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(
                new NavigationParameter()
                {
                    NavigationMode = NavigationMode.Modal,
                    EnsureNavigationContext = true,
                    ModalPresentationStyle = ModalPresentationStyle.FormSheet,
                    CommandId = "Add"
                });
        }

        protected override void ExecuteDelete(object parameter)
        {
            if (parameter is Item)
                this.Repository.Delete((Item)parameter);
            else if (parameter is IEnumerable<Item>)
                this.Repository.Delete((IEnumerable<Item>)parameter);

            // this.SelectedItem = null;

            if (this.SelectedItems != null)
                this.SelectedItems.Clear();
        }

        protected override void ExecuteEdit(object parameter)
        {
            if (this.SelectedItem != null)
                this.NavigationService.Navigate<ItemEditorViewModel>(new NavigationParameter(this.SelectedItem));
        }

        private void ExecuteMarkSold(object parameter)
        {
            if (parameter is IEnumerable<Item>)
            {
                var items = parameter as IEnumerable<Item>;

                foreach (Item item in items)
                {
                    item.IsSold = true;
                    item.SoldDate = DateTime.Today;

                    this.OnDataChanged(item);
                }
            }

            if (this.SelectedItems != null)
                this.SelectedItems.Clear();
        }

        private void ExecuteNavigateGroup(object parameter)
        {
            this.NavigationService.Navigate<GroupDetailViewModel>(new NavigationParameter(parameter));
        }

        public override void Filter(string query, string scope)
        {
            if (scope == "Name")
                this.FilterItems = this.Items.Where(o => o.Name.ToLowerInvariant().StartsWith(query.ToLowerInvariant())).ToList();
            else if (scope == "Location")
                this.FilterItems = this.Items.Where(o => o.Location.ToLowerInvariant().StartsWith(query.ToLowerInvariant())).ToList();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.RefreshGroupItems();
            this.OnPropertyChanged("TotalItemsText");
            this.OnPropertyChanged("TitleText");

            // WinRT requires GroupItems notification in another UI thread
            if (this.GetService<IApplicationService>().GetContext().Platform.OperatingSystem == OSKind.WinRT)
                this.GetService<IViewService>().RunOnUIThread(() => this.OnPropertyChanged("GroupItems"));
        }

        protected override void OnSelectedItemChanged(Item newItem)
        {
            this.EditCommand.RaiseCanExecuteChanged();
            this.DeleteCommand.RaiseCanExecuteChanged();
            this.MarkSoldCommand.RaiseCanExecuteChanged();
        }

        protected override void OnSelectedItemsCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            this.OnPropertyChanged("DeleteText");
            this.OnPropertyChanged("MarkSoldText");

            this.DeleteCommand.RaiseCanExecuteChanged();
            this.MarkSoldCommand.RaiseCanExecuteChanged();
        }

        protected override void OnSourceItemsChanged(ICollection<Item> items)
        {
            if (items != null)
                this.Items = items.OrderBy(o => o.Name);
            else
                this.Items = null;

            this.RefreshGroupItems();
        }

        public override void RefreshGroupItems()
        {
            // Uncomment the following line to display items in plain list
            if (this.Items != null)
                this.GroupItems = this.Items.OrderBy(o => o.Category.Name).GroupBy(o => o.Category.Name).Select(o => new CategoryGroup(o)).ToList();
        }

        #endregion
    }
}