using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using SignaturePad.Infrastructure;
using SignaturePad.ModelServices;
using SignaturePad.Models;
using Intersoft.Crosslight;
using Intersoft.Crosslight.Forms;
using Intersoft.Crosslight.Input;
using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class ItemEditorViewModel : EditorViewModelBase<Item>
    {
        #region Fields

        private IEnumerable<Category> _categories;

        #endregion

        #region Properties

        public DelegateCommand ActivateImagePickerCommand { get; set; }

        public IEnumerable<Category> Categories
        {
            get { return _categories; }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    OnPropertyChanged("Categories");
                }
            }
        }

        private ICategoryRepository CategoryRepository
        {
            get
            {
                if (Container.Current.CanResolve<ICategoryRepository>())
                    return Container.Current.Resolve<ICategoryRepository>();
                return new CategoryRepository(); // for designer support
            }
        }

        public DelegateCommand FinishImagePickerCommand { get; set; }

        private IItemRepository ItemRepository
        {
            get
            {
                if (Container.Current.CanResolve<IItemRepository>())
                    return Container.Current.Resolve<IItemRepository>();
                return new ItemRepository(); // for designer support
            }
        }

        public DelegateCommand ViewLargeImageCommand { get; set; }

        #endregion

        #region Constructors

        public ItemEditorViewModel()
        {
            this.Title = "Edit Item";
            this.Categories = this.CategoryRepository.GetAll();

            this.ActivateImagePickerCommand = new DelegateCommand(ExecuteActivateImagePicker);
            this.FinishImagePickerCommand = new DelegateCommand(ExecuteFinishImagePickerCommand);
            this.ViewLargeImageCommand = new DelegateCommand(ExecuteViewLargeImage, CanExecuteViewLargeImage);
        }

        #endregion

        #region Methods

        private bool CanExecuteViewLargeImage(object parameter)
        {
            if (this.IsNewItem || this.Item.LargeImage == null)
                return false;

            return true;
        }

        private void ExecuteActivateImagePicker(object parameter)
        {
            ImagePickerActivateParameter activateParameter = parameter as ImagePickerActivateParameter;
            if (activateParameter != null)
            {
                activateParameter.CustomCommands = new Dictionary<string, ICommand>();
                activateParameter.CustomCommands.Add("View Larger", this.ViewLargeImageCommand);
            }
        }

        protected override void ExecuteCancel(object parameter)
        {
            base.ExecuteCancel(parameter);

            // In real world scenario, roll back all changes to the original state.
        }

        private void ExecuteFinishImagePickerCommand(object parameter)
        {
            ImagePickerResultParameter resultParameter = parameter as ImagePickerResultParameter;

            if (resultParameter != null && resultParameter.Result != null)
                this.Item.LargeImage = resultParameter.Result.ImageData;
        }

        protected override void ExecuteSave(object parameter)
        {
            // This sample doesn't implement physical data save as it uses XML as datasource
            // In real world apps, save the data to either local storage, Azure or web service.

            // Validate the item associated to this ViewModel
            this.Validate();

            // Perform save if there are no validation errors
            if (!this.HasErrors)
            {
                if (this.IsDirty)
                {
                    if (this.IsNewItem)
                        this.ItemRepository.Insert(this.Item);
                    else
                    {
                        this.ItemRepository.Update(this.Item);
                        this.OnDataChanged(this.Item);
                    }

                    // show quick status
                    this.ToastPresenter.Show("Changes saved", ToastDisplayDuration.Immediate);
                }

                this.IsDirty = false;

                // In real world apps, you might want to save changes in batch instead of individual
                // this.ItemRepository.SaveChanges(null, null);

                this.NavigationService.Close(new NavigationResult(NavigationResultAction.Done));
            }
            else
                this.ShowErrorMessage();
        }

        private void ExecuteViewLargeImage(object parameter)
        {
            this.NavigationService.Navigate(new NavigationTarget(typeof(ItemDetailViewModel), "PhotoDetail", new NavigationParameter(this.Item)));
        }

        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);

            if (parameter.Data != null)
                this.Item = parameter.Data as Item;
            else
            {
                this.Item = new Item();
                this.Item.PurchaseDate = DateTime.Today;
                this.Item.Quantity = 1;
                this.Item.Category = this.Categories.ElementAt(0);

                this.IsNewItem = true;
                this.Title = "Add New Item";
            }
        }

        protected override void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            base.OnErrorsChanged(e);

            if (this.Context.Platform.OperatingSystem == OSKind.WinRT)
            {
                // workaround for WinRT combobox bug
                this.GetService<IViewService>().RunOnUIThread(
                    () => { this.Item.RaisePropertyChanged("Category"); });
            }
        }

        private void ShowErrorMessage()
        {
            // Specifically on Phone devices, present the error to users
            if (this.Context.Platform.OperatingSystem != OSKind.WinRT)
                this.MessagePresenter.Show(this.ErrorMessage);
        }

        #endregion
    }
}