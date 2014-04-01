using SignaturePad.ViewModels;
using Intersoft.Crosslight;

namespace SignaturePad
{
    public class ItemListBindingProvider : BindingProvider
    {
        #region Constructors

        public ItemListBindingProvider()
        {
            IApplicationContext context = this.GetService<IApplicationService>().GetContext();
            bool isAndroidTablet = context.Device.Kind == DeviceKind.Tablet && context.Platform.OperatingSystem == OSKind.Android;

            ItemBindingDescription itemBinding = new ItemBindingDescription()
            {
                DisplayMemberPath = "Name",
                DetailMemberPath = "Location",
                ImageMemberPath = "ThumbnailImage",
                ImagePlaceholder = "item_placeholder.png"
            };

            itemBinding.AddBinding("TextLabel", BindableProperties.StyleAttributesProperty, new BindingDescription("IsSold") { Converter = new TextLabelStyleConverter() });

            this.AddBinding("TableView", BindableProperties.ItemsSourceProperty, "Items");
            this.AddBinding("TableView", BindableProperties.ItemTemplateBindingProperty, itemBinding, true);
            this.AddBinding("TableView", BindableProperties.IsBatchUpdatingProperty, "IsBatchUpdating");
            this.AddBinding("TableView", BindableProperties.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.SelectedItemsProperty, "SelectedItems", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.IsEditingProperty, "IsEditing", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.AddItemCommandProperty, "AddCommand", BindingMode.TwoWay);
            this.AddBinding("TableView", BindableProperties.DeleteItemCommandProperty, "DeleteCommand", BindingMode.TwoWay);

            this.AddBinding("TableView", BindableProperties.DetailNavigationTargetProperty, new NavigationTarget(typeof(ItemDetailViewModel)), true);

            this.AddBinding("AddButton", BindableProperties.CommandProperty, "AddCommand");

            this.AddBinding("DeleteButton", BindableProperties.TextProperty, "DeleteText");
            this.AddBinding("DeleteButton", BindableProperties.CommandProperty, "DeleteCommand");

            if (isAndroidTablet)
            {
                this.AddBinding("DeleteButton", BindableProperties.CommandParameterProperty, "SelectedItem");
                this.AddBinding("EditButton", BindableProperties.CommandProperty, "EditCommand");
            }
            else
                this.AddBinding("DeleteButton", BindableProperties.CommandParameterProperty, "SelectedItems");

            this.AddBinding("MarkSoldButton", BindableProperties.TextProperty, "MarkSoldText");
            this.AddBinding("MarkSoldButton", BindableProperties.CommandProperty, "MarkSoldCommand");
            this.AddBinding("MarkSoldButton", BindableProperties.CommandParameterProperty, "SelectedItems");

            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "TotalItemsText");
        }

        #endregion
    }
}