using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Models;

namespace gwork.maui.ViewModels
{
    public partial class UserDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        User user = (User)App.LoggedUser.Clone();
    }
}
