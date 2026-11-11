using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp2.Helper;
using MauiApp2.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp2.ViewModels
{
    public class SignInViewModel : ViewModelBase /*ObservableObject*/
    {
        private string? _userName;
        private string? _password;
        private bool _entryAsPassword;
        private bool _IsMessageLabelVisible;
        private string? _messageText;
        private Color? _MessageTextColor;
        private string? _TogglePasswordButtonText;
        private string? _passwordIconCode;

        private bool _iDebugMode => true;
        private DBMokup? _dbMokup;

        public string PasswordIconCode
        {
            get => _passwordIconCode!;
            set
            {
                if (_passwordIconCode != value)
                {
                    _passwordIconCode = value;
                    OnPropertyChanged(nameof(PasswordIconCode));
                }
            }
        }

        public string TogglePasswordButtonText
        {
            get => _TogglePasswordButtonText!;
            set
            {
                _TogglePasswordButtonText = value;
                OnPropertyChanged(nameof(TogglePasswordButtonText));
            }
        }
        public bool IsSignedInButtonEnabled
        {
            get => !string.IsNullOrEmpty(UserName)
                && !string.IsNullOrEmpty(UserPassword);
        }

        public bool IsMessageLabelVisible
        {
            get => _IsMessageLabelVisible;
            set
            {
                if (_IsMessageLabelVisible != value)
                {
                    _IsMessageLabelVisible = value;
                    OnPropertyChanged(nameof(IsMessageLabelVisible));
                }
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                    //OnPropertyChanged(nameof(IsSignedInButtonEnabled));
                    (SignInCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public string UserPassword
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(UserPassword));
                    //OnPropertyChanged(nameof(IsSignedInButtonEnabled));
                    (SignInCommand as Command)?.ChangeCanExecute();
                }
            }
        }

        public bool EntryAsPassword
        {
            get => _entryAsPassword;
            set
            {
                _entryAsPassword = value;
                OnPropertyChanged(nameof(EntryAsPassword));
            }
        }

        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged(nameof(MessageText));
            }
        }

        public Color MessageTextColor
        {
            get => _MessageTextColor;
            set
            {
                _MessageTextColor = value;
                OnPropertyChanged(nameof(MessageTextColor));
            }
        }

        public ICommand ShowPasswordCommand { get; }
        public ICommand SignInCommand { get; }
        public SignInViewModel()
        {
            EntryAsPassword = true;
            PasswordIconCode = FontHelper.CLOSED_EYE_ICON;
            IsMessageLabelVisible = false;
            MessageText = string.Empty;
            _dbMokup = new DBMokup();

            ShowPasswordCommand = new Command(TogglePassordButton);
            //SignInCommand = new Command(SignIn, CanSignIn);
            SignInCommand = new Command(SignIn, () =>
                !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword)));
        }

        //private bool CanSignIn()
        //{
        //    return !(string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(UserPassword));
        //}

        private void TogglePassordButton() //OnTugglePasswordButtonClicked()
        {
            EntryAsPassword = !EntryAsPassword;
            if (EntryAsPassword)
            {
                PasswordIconCode = FontHelper.CLOSED_EYE_ICON;
            }
            else
            {
                PasswordIconCode = FontHelper.OPEN_EYE_ICON;
            }
        }

        private void SignIn() //OnSignInButtonClicked()
        {
            if (_dbMokup!.isExist(UserName, UserPassword))
            {
                MessageText = MesseagesHelper.SignInSuccessMessage;
                MessageTextColor = Colors.Green;
            }
            else
            {
                MessageText = MesseagesHelper.SignInErrorMessage;
                MessageTextColor = Colors.Red;
            }

            IsMessageLabelVisible = true;
        }
    }
}
