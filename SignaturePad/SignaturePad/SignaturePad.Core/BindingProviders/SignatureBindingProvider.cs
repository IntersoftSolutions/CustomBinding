using System;
using Intersoft.Crosslight;

namespace SignaturePad.Core
{
	public class SignatureBindingProvider : BindingProvider
	{
		public SignatureBindingProvider ()
		{
			this.AddBinding ("SignaturePad", SignaturePadProperties.ButtonTitleProperty, "Title");
			this.AddBinding("UpdateButton", BindableProperties.CommandProperty, "UpdateCommand");
		}
	}
}

