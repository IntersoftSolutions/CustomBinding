using Intersoft.Crosslight;

namespace SignaturePad
{
    public class ItemDetailBindingProvider : BindingProvider
    {
        #region Constructors

        public ItemDetailBindingProvider()
        {
            this.AddBinding("NameLabel", BindableProperties.TextProperty, "Item.Name");
            this.AddBinding("ImageView", BindableProperties.ImageProperty, "Item.LargeImage");
            this.AddBinding("DescriptionLabel", BindableProperties.TextProperty, "Item.Description");
            this.AddBinding("CategoryLabel", BindableProperties.TextProperty, "Item.Category.Name");
            this.AddBinding("PurchaseDateLabel", BindableProperties.TextProperty, new BindingDescription("Item.PurchaseDate") { StringFormat = "{0:d}" });
            this.AddBinding("LocationLabel", BindableProperties.TextProperty, "Item.Location");
            this.AddBinding("QuantityLabel", BindableProperties.TextProperty, "Item.Quantity");
            this.AddBinding("PriceLabel", BindableProperties.TextProperty, "Item.Price");
            this.AddBinding("SerialNumberLabel", BindableProperties.TextProperty, "Item.SerialNumber");
            this.AddBinding("NotesLabel", BindableProperties.TextProperty, "Item.Notes");
        }

        #endregion
    }
}