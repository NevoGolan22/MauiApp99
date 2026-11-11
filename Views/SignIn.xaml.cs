namespace MauiApp2.Views;

using MauiApp2.Helper;
using MauiApp2.Service;
using MauiApp2.ViewModels;

public partial class SignIn : ContentPage
{
    

    public SignIn()
    {
        InitializeComponent();


        BindingContext = new SignInViewModel();

        //EntryAsPassword = true;

        //_dbMokup = new DBMokup();

        //if (_iDebugMode)
        //{
        //    UserName = "admin@mail.com";
        //    UserPassword = "admin";

    }

    
    
}