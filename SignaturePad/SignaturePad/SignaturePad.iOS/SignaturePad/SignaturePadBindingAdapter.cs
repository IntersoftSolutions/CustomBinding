using System;
using Intersoft.Crosslight.iOS;
using Intersoft.Crosslight;
using SignaturePad.Core;
using MonoTouch.UIKit;

namespace SignaturePad.iOS
{
	public class SignaturePadBindingAdapter : BindingAdapterBase<UISignaturePadView>
	{
		public SignaturePadBindingAdapter ()
		{
			this.AddSupportedProperty(SignaturePadProperties.ButtonTitleProperty);
            this.AddSupportedProperty(SignaturePadProperties.SignProperty);
		}

		public override void SetValue (UISignaturePadView padView, BindableProperty property, object value)
		{
			bool isHandled = false;

			if (property == SignaturePadProperties.ButtonTitleProperty) {
				if (value is string) {
					padView.TestLabel.Text = value as string;
				} 
				isHandled = true;
			}
            else if (property == SignaturePadProperties.SignProperty) {
                if (value is byte && value!=null) {
                    //padView.TestLabel.Text = "Successfully changes";
                } 
                isHandled = true;
            }
           

			if (!isHandled)
				base.SetValue (padView, property, value);
		}

        public override object GetValue(UISignaturePadView obj, BindableProperty property)
        {
            return base.GetValue(obj, property);
        }

	}
}

