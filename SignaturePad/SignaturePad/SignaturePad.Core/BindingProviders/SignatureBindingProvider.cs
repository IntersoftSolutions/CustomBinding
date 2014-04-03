using System;
using Intersoft.Crosslight;

namespace SignaturePad.Core
{
	public class SignatureBindingProvider : BindingProvider
	{
		public SignatureBindingProvider ()
		{
			this.AddBinding ("SignaturePad", SignaturePadProperties.ButtonTitleProperty, "Title");
            this.AddBinding ("SignaturePad", SignaturePadProperties.SignProperty, new BindingDescription("SignProperty",BindingMode.TwoWay));

            this.AddBinding("UpdateButton", BindableProperties.CommandProperty, "UpdateCommand");
		}
	}
}

