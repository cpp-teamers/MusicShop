using ModelsLibrary.Models;
using MusicShop.Commands;
using System.ComponentModel;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;

namespace MusicShop.ViewModels
{
	public class AccountViewModel : INotifyPropertyChanged
    {
        private Account _defaultAcc = new Account()
        {
            Login = "Login",
            Password = "Password",
            Email = "Email",
            Phone = "Phone",
        };
        public Account DefaultAcc 
        {
			get
			{
                return _defaultAcc;
            }
            set
			{
                _defaultAcc = value;
                OnPropertyChanged(nameof(DefaultAcc));
			}
        }

        public string _login;
        public string Login
		{
			get { return _login; }
			set
			{
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
		}

        public string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        
        private ICommand _execRegistation;
        public ICommand ExecRegistation
        {
			get
			{
                return _execRegistation ?? new RelayCommand(obj =>
                {
					try
					{
                        Account newAcc = new Account()
                        {
                            Login = ValidateLogin(),
                            Password = ValidatePassword(),
                            Email = ValidateEmail(),
                            Phone = ValidatePhone()
                        };

                        //TODO account repo insert

                    }
                    catch (System.Exception err)
					{
                        MessageBox.Show($"{err.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
					}
                });
			}
		}

       
        private ICommand _turnOnLogin;
        public ICommand TurnOnLogin
        {
            get
            {
                return _turnOnLogin ?? new RelayCommand(obj =>
                {
                    Grid ContainElemsGrid = (obj as Grid);
                    
                    Border backButton = default; 
                    foreach (var item in ContainElemsGrid.Children)
					{

                        if (item is Border potentialBackButton && potentialBackButton.Name == "BackButton")
						{
                            backButton = potentialBackButton;
                            continue;
                        }

                        if (item is StackPanel contElemStack && contElemStack.Name == "LoginRegButtonPanel") 
                        {
                            contElemStack.Visibility = Visibility.Hidden;
                            continue;
                        } 

						if (item is DockPanel LoginP && LoginP.Name == "LoginPanel")
                        {
                            OutvisBackButton(backButton);
                            LoginP.Visibility = Visibility.Visible;
                            break;
                        }
					} 
                    
                });
            }
        }

       
        private ICommand _turnOnReg;
        public ICommand TurnOnReg
        {
            get
            {
                return _turnOnReg ?? new RelayCommand(obj =>
                {
                    Grid ContainElemsGrid = (obj as Grid);

                    Border backButton = default;
                    foreach (var item in ContainElemsGrid.Children)
                    {
                        if (item is Border potentialBackButton && potentialBackButton.Name == "BackButton")
                        {
                            backButton = potentialBackButton;
                            continue;
                        }

                        if (item is StackPanel contElemStack && contElemStack.Name == "LoginRegButtonPanel")
                        {
                            contElemStack.Visibility = Visibility.Hidden;
                            continue;
                        }

                        if (item is DockPanel RegP && RegP.Name == "RegisterPanel")
                        {
                            OutvisBackButton(backButton);
                            RegP.Visibility = Visibility.Visible;
                            break;
                        }
                    }

                });
            }
        }

        private ICommand _execBack;
        public ICommand ExecBack
		{
			get
			{
                return _execBack ?? new RelayCommand(obj =>
                {
                    Grid ContainElemsGrid = (obj as Grid);

                    StackPanel initButtons = default;
                    Border backButton = default;
                    
                    foreach (var item in ContainElemsGrid.Children)
                    {
                        if (item is Border potentialBackButton && potentialBackButton.Name == "BackButton")
                        {
                            backButton = potentialBackButton;
                            continue;
                        }

                        if (item is StackPanel potentialcontElemStack && potentialcontElemStack.Name == "LoginRegButtonPanel")
                        {
                            initButtons = potentialcontElemStack;
                            continue;
                        }

                        if (item is DockPanel potentialReg && potentialReg.Name == "RegisterPanel" && potentialReg.Visibility == Visibility.Visible)
                        {
                            InvisBackButton(backButton);
                            potentialReg.Visibility = Visibility.Hidden;
                            continue;
                        }

                        if (item is DockPanel potentialLog && potentialLog.Name == "LoginPanel" && potentialLog.Visibility == Visibility.Visible)
                        {
                            InvisBackButton(backButton);
                            potentialLog.Visibility = Visibility.Hidden;
                            break;
                        }
                    }

                    initButtons.Visibility = Visibility.Visible;
                });
			}
		}

		#region 'back' button manipulations

		private void OutvisBackButton(Border borderWithButton)
		{
            borderWithButton.Visibility = Visibility.Visible;
		}
        private void InvisBackButton(Border borderWithButton)
        {
            borderWithButton.Visibility = Visibility.Hidden;
        }
        #endregion
        #region Validation

        private string ValidateLogin()
		{
			if (string.IsNullOrWhiteSpace(_login))
			{
                throw new System.Exception("Login Field is Empty!");
            }
			if (_login.Length >= 100)
			{
                throw new System.Exception("Login is too large!");
			}

            Regex regExp = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z\d.-][A-Za-z\d.-]{0,19}");
			if (!regExp.IsMatch(_login))
			{
                throw new System.Exception("Login is invalid! Try again...");
            }
            return _login;
        }

        private string ValidatePassword()
        {
            Regex regExp = new Regex(@"^(?=i)(?=.*[a-z])(?=.*[0-9])(?=.*[@#_])[a-z][a-z0-9@#_]{6,}[a-z0-9]$");
            if (!regExp.IsMatch(_password))
            {
                throw new System.Exception("Password is invalid! Try again...");
            }
            return _password;
        }

        private string ValidateEmail()
        {
            MailAddress email = new MailAddress(_email);
            return email.Address;
        }

        private string ValidatePhone()
        {
            Regex regExp = new Regex(@"^([\+]?33[-]?|[0])?[1-9][0-9]{8}$");
            if (!regExp.IsMatch(_phone))
            {
                throw new System.Exception("Phone is invalid! Try again...");
            }
            return _phone;
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
