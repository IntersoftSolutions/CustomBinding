using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SignaturePad.Models;

namespace SignaturePad.ModelServices
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields

        private IEnumerable<Category> _items = null;

        #endregion

        #region Methods

        private Category CreateCategory(XElement x)
        {
            Category category = new Category()
            {
                Name = x.Element("Name").Value,
                Id = int.Parse(x.Element("Id").Value),
                Image = x.Element("Id").Value + ".jpg"
            };

            if (!string.IsNullOrEmpty(category.Image))
                category.LargeImage = typeof(ItemRepository).Assembly.GetManifestResourceStream("SignaturePad.Core.Assets.Images.Category." + category.Image).ToByte();


            return category;
        }

        #endregion

        #region ICategoryRepository Members

        public virtual Category Get(int id)
        {
            return this.GetAll().FirstOrDefault(o => o.Id == id);
        }

        public virtual Category GetByName(string name)
        {
            return this.GetAll().FirstOrDefault(o => o.Name == name);
        }

        public virtual IEnumerable<Category> GetAll()
        {
            if (_items == null)
            {
                XDocument doc = XDocument.Load(typeof(CategoryRepository).Assembly.GetManifestResourceStream("SignaturePad.Core.Assets.Data.Categories.xml"));

                var query = from x in doc.Descendants("Category")
                            select CreateCategory(x);

                _items = query.ToList();
            }

            return _items;
        }

        #endregion
    }
}