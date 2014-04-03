using System;
using SignaturePad.ViewModels;
using Intersoft.Crosslight.ViewModels;
using Intersoft.Crosslight.Input;

namespace SignaturePad.Core
{
	public class SignatureViewModel : ViewModelBase
	{

		private string _title;
        private byte[] _signProperty;

        public byte[] SignProperty
        {
            get
            {
                return _signProperty;
            }
            set
            {
                if (_signProperty != value)
                {
                    _signProperty = value;
                    this.Title = "Sucessfully changed";
                    OnPropertyChanged ("SignProperty");
                }
            }
        }

		public string Title
		{
			get { return _title; }
			set
			{
				if (_title != value)
				{
					_title = value;
					OnPropertyChanged ("Title");
				}
			}
		}

        public DelegateCommand UpdateCommand { get; set; }

		public SignatureViewModel ()
		{
			this.Title="Hello Crosslight";
            this.UpdateCommand = new DelegateCommand(ExecuteUpdate);
		}


		private void ExecuteUpdate(object parameter)
		{
			this.Title = "Updated ";
		}
	}
}

