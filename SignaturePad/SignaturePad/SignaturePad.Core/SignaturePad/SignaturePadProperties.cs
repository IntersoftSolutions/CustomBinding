using System;
using Intersoft.Crosslight;

namespace SignaturePad.Core
{
	public class SignaturePadProperties
	{
        public static readonly BindableProperty ButtonTitleProperty = BindableProperty.Register("ButtonTitle",typeof(string));
        public static readonly BindableProperty SignProperty = BindableProperty.Register("SignProperty",typeof(byte[]));

	}
}

