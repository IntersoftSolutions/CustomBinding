using Intersoft.Crosslight;

namespace SignaturePad
{
    public class SimpleBindingProvider : BindingProvider
    {
        #region Constructors

        public SimpleBindingProvider()
        {
            this.AddBinding("GreetingLabel", BindableProperties.TextProperty, "GreetingText");
            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "FooterText");
            this.AddBinding("Text1", BindableProperties.TextProperty, new BindingDescription("NewText", BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged));
            this.AddBinding("Button1", BindableProperties.CommandProperty, "ShowToastCommand");
        }

        #endregion
    }
}