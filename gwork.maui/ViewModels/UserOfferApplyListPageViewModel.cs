using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using System.Collections.ObjectModel;

namespace gwork.maui.ViewModels
{
    public partial class UserOfferApplyListPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<UserOfferApplyListItem>? userOfferApplyListItems;

        private UserOfferApplyDatabase userOfferApplyDatabase = new();
        private UserDatabase userDatabase = new();
        private OfferDatabase offerDatabase = new();

        public UserOfferApplyListPageViewModel()
        {
            GetUserOfferApplyList();
        }

        private async void GetUserOfferApplyList()
        {
            var userOfferApplies = await userOfferApplyDatabase.GetUserOffersAppliesAsync();

            var userOfferApplyListItems = new ObservableCollection<UserOfferApplyListItem>();
            foreach (var userOfferApply in userOfferApplies)
            {
                var userOfferApplyListItem = new UserOfferApplyListItem();
                userOfferApplyListItem.User = await userDatabase.GetUserAsync(userOfferApply.UserId);
                userOfferApplyListItem.Offer = await offerDatabase.GetOfferAsync(userOfferApply.OfferId);

                userOfferApplyListItems.Add(userOfferApplyListItem);
            }

            UserOfferApplyListItems = userOfferApplyListItems;
        }

        [RelayCommand]
        private async Task AcceptApply(object commandParameter)
        {
            await Shell.Current.DisplayAlert("Informacja", "Zaakceptowano aplikację o pracę", "OK");
            GetUserOfferApplyList();
        }

        [RelayCommand]
        private async Task DeclineApply(object commandParameter)
        {
            var userOfferApplyListItem = commandParameter as UserOfferApplyListItem;
            if (userOfferApplyListItem != null)
            {
                var result = await Shell.Current.DisplayAlert("Ostrzeżenie", "Czy na pewno chcesz odrzucić aplikację?", "Tak", "Nie");
                if (result)
                {
                    var userId = userOfferApplyListItem.User.Id;
                    var offerId = userOfferApplyListItem.Offer.Id;

                    await userOfferApplyDatabase.DeleteUserOfferApplyAsync(await userOfferApplyDatabase.GetUserOfferApplyAsync(userId, offerId));
                    await Shell.Current.DisplayAlert("Informacja", "Odrzucono aplikację o pracę", "OK");
                    GetUserOfferApplyList();
                }
            }
        }
    }
}
