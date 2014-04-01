using SignaturePad.Infrastructure;
using SignaturePad.ModelServices;
using SignaturePad.Models;
using Intersoft.Crosslight;
using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class CategoryListViewModel : ListViewModelBase<Category>
    {
        #region Properties

        private ICategoryRepository Repository
        {
            get
            {
                if (Container.Current.CanResolve<ICategoryRepository>())
                    return Container.Current.Resolve<ICategoryRepository>();
                else
                    return new CategoryRepository(); // for designer support
            }
        }

        #endregion

        #region Constructors

        public CategoryListViewModel()
        {
            this.SourceItems = this.Repository.GetAll().ToObservable();
        }

        #endregion
    }
}