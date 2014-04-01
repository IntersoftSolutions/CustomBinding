using System.Linq;
using SignaturePad.Infrastructure;
using SignaturePad.ModelServices;
using SignaturePad.Models;
using Intersoft.Crosslight;

namespace SignaturePad.Models
{
    public class CategoryGroup : GroupItem<Item>
    {
        #region Properties

        public Category Category { get; private set; }

        private ICategoryRepository CategoryRepository
        {
            get { return Container.Current.Resolve<ICategoryRepository>(); }
        }

        private IItemRepository ItemRepository
        {
            get { return Container.Current.Resolve<IItemRepository>(); }
        }

        #endregion

        #region Constructors

        public CategoryGroup(IGrouping<string, Item> groupItem)
            : base(groupItem)
        {
            this.Category = this.CategoryRepository.GetByName(groupItem.Key);
        }

        #endregion
    }
}