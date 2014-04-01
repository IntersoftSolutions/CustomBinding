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

			if (!isHandled)
				base.SetValue (padView, property, value);
		}
	}
}

