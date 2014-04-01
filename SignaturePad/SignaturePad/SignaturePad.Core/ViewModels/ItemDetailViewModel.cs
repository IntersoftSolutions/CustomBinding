using System.Linq;
using SignaturePad.Infrastructure;
using SignaturePad.ModelServices;
using SignaturePad.Models;
using Intersoft.Crosslight;
using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class ItemDetailViewModel : EditableDetailViewModelBase<Item>
    {
        #region Fields

        private GroupItem<Item> _group;

        #endregion

        #region Properties

        public GroupItem<Item> Group
        {
            get { return _group; }
            set
            {
                if (_group != value)
                {
                    _group = value;
                    OnPropertyChanged("Group");
                }
            }
        }

        /// <summary>
        /// Represents a property member that participates in the state saving during application suspension.
        /// Use the [StateAware] attribute to define members that will be saved and restored during application life cycle changes.
        /// </summary>
        [StateAware]
        public string ItemKey
        {
            get { return this.Item.Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    this.Item = this.ItemRepository.GetAll().First();
                else
                    this.Item = this.ItemRepository.Get(this.Item.Name);

                if (this.Item != null)
                    this.Group = this.ItemRepository.GetCategoryGroup(this.Item.CategoryId);
            }
        }

        private IItemRepository ItemRepository
        {
            get
            {
                if (Container.Current.CanResolve<IItemRepository>())
                    return Container.Current.Resolve<IItemRepository>();
                return new ItemRepository(); // for designer support
            }
        }

        #endregion

        #region Methods

        protected override void ExecuteAdd(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(
                new NavigationParameter
                {
                    NavigationMode = NavigationMode.Modal
                });
        }

        protected override void ExecuteEdit(object parameter)
        {
            this.NavigationService.Navigate<ItemEditorViewModel>(
                new NavigationParameter(this.Item)
                {
                    NavigationMode = NavigationMode.Modal
                });
        }

        public override void Navigated(NavigatedParameter parameter)
        {
            base.Navigated(parameter);

            if (parameter.Data != null)
            {
                this.Item = parameter.Data as Item;
                if (this.Item != null)
                    this.Group = this.ItemRepository.GetCategoryGroup(this.Item.CategoryId);
            }
        }

        #endregion
    }
}