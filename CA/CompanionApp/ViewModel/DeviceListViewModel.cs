using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class DeviceListViewModel : BaseViewModel
    {
        public DeviceListViewModel()
        {
            Title = "Device List";
        }

        public String SerialNumber
        {
            set
            {
                Task<IEnumerable<Model.Device>> task = Task.Run<IEnumerable<Model.Device>>(async () => await DataStore.SearchDevicesBySerialAsync(value));
                Devices = new ObservableCollection<Model.Device>(task.Result);
            }
        }

        public String ZtdId
        {
            set
            {
                Task<IEnumerable<Model.Device>> task = Task.Run<IEnumerable<Model.Device>>(async () => await DataStore.SearchDevicesByZtdIdAsync(value));
                Devices = new ObservableCollection<Model.Device>(task.Result);
            }
        }

        public ObservableCollection<Model.Device> Devices
        {
            get;
            set;
        }
    }
}
