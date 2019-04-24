using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class DeviceViewModel : BaseViewModel
    {
        public DeviceViewModel()
        {
            Title = "Device";
        }

        public Model.Device Device
        {
            get;
            set;
        }
    }
}
