using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CompanionApp.ViewModel
{
    public class InfoViewModel : BaseViewModel
    {
        Model.Info privateInfo;

        public InfoViewModel()
        {
            Title = "Info";

            Task<Model.Info> task = Task.Run<Model.Info>(async () => await DataStore.GetInfo());
            privateInfo = task.Result;
        }

        public object Info
        {
            get { return privateInfo; }
        }

    }
}
