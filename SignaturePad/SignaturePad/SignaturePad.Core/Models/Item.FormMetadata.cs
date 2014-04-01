using Intersoft.Crosslight;
using Intersoft.Crosslight.Forms;
using SignaturePad.ViewModels;
using System;

namespace SignaturePad.Models
{
    [FormMetadataType(typeof(Item.FormMetadata))]
    partial class Item
    {
        [Form(Title = "{FormState} Item")]
        public class FormMetadata
        {
            [Section(Style = SectionLayoutStyle.ImageWithFields)]
            public static GeneralSection General;

            [Section("Item Details")]
            public static ItemDetailSection ItemDetail;

            [Section("Item Status")]
            [VisibilityBinding(Path = "IsNewItem", SourceType = BindingSourceType.ViewModel, ConverterType = typeof(BooleanNegateConverter))]
            public static SoldSection Sold;

            [Section]
            public static NotesSection Notes;
        }

        public class GeneralSection
        {
            [Editor(EditorType.Image)]
            [Image(Height = 83, Width = 80, Placeholder = "item_placeholder.png", Frame = "frame.png", FramePadding = 6, FrameShadowHeight = 3)]
            [ImagePicker(ImageResultMode = ImageResultMode.Both, ActivateCommand = "ActivateImagePickerCommand", PickerResultCommand = "FinishImagePickerCommand")]
            public static byte[] ThumbnailImage;

            [StringInput(Placeholder = "Product name")]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static string Name;

            [StringInput(Placeholder = "Price")]
            [Layout(Style = LayoutStyle.DetailOnly)]
            public static decimal Price;
        }

        public class ItemDetailSection
        {
            [Editor(EditorType.TextView)]
            [StringInput(Placeholder = "iPad 4 Mini, 64GB", MaxHeight = 68f, AutoResize = true)]
            public static string Description;

            [StringInput(Placeholder = "Living Room", AutoCorrection = AutoCorrectionType.No)]
            public static string Location;

            [Editor(EditorType.Selection)]
            [SelectedItemBinding(Path = "Category")]
            [Binding(Path = "Category.Name")]
            [SelectionInput(SelectionMode.Single, DisplayMemberPath = "Name", ListSourceType = typeof(CategoryListViewModel))]
            public static Category Category;

            /*[Editor(EditorType.Picker)]
            [Binding(Path = "Quantity")]
            [SelectionInput(DisplayMemberPath = "Title", ImageMemberPath = "Icon", ValueMemberPath = "Value", ListSourceMemberPath = "Options")]
            public static int Options;*/

            [Editor(EditorType.Date)]
            [Binding(StringFormat = "{0:d}")]
            [Display(Caption = "Purchase Date")]
            public static DateTime PurchaseDate;

            public static int Quantity;

            //[Editor(EditorType.Custom, CustomEditorIdentifier = "CustomEditorView")]
            //public static int CustomControl1;

            [Display(Caption = "Serial Number")]
            [StringInput(Placeholder = "Item Serial", AutoCorrection = AutoCorrectionType.No)]
            public static string SerialNumber;
        }

        public class ActionSection
        {
            [Editor(EditorType.Button)]
            [Binding(Path = "SaveCommand", SourceType = BindingSourceType.ViewModel)]
            [Button(Title = "Save", Style = ButtonStyle.Image, TextColor = "#ff000000", BackgroundImage = "greyButton.png greyButtonHighlight.png 6,6,6,6")]
            public static string OKButton;

            [Editor(EditorType.Button)]
            [Button(Title = "Delete", Style = ButtonStyle.Image, TextColor = "#ffffffff", TextShadow = "#ff000000 0,-1", BackgroundImage = "orangeButton.png orangeButtonHighlight.png 6,6,6,6")]
            public static string DeleteButton;
        }

        public class SoldSection
        {
            [Editor(EditorType.Switch)]
            [Display(Caption = "Is Sold")]
            public static bool IsSold;

            [Display(Caption = "Sold Date")]
            [Editor(EditorType.Date)]
            [VisibilityBinding(Path = "IsSold")]
            [Binding(StringFormat = "{0:d}")]
            public static DateTime SoldDate;
        }

        public class NotesSection
        {
            [Layout(Style = LayoutStyle.DetailOnly)]
            [Editor(EditorType.TextView)]
            [StringInput(Placeholder = "Notes", MinHeight = 68f, MaxHeight = 98f, AcceptLineBreak = true, AutoResize = true)]
            public static string Notes;
        }
    }
}
