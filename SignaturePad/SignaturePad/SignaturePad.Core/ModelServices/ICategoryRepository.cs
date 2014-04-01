using SignaturePad.Models;

namespace SignaturePad.ModelServices
{
    public interface ICategoryRepository : IDataRepository<Category, int>
    {
        #region Methods

        Category GetByName(string name);

        #endregion
    }
}