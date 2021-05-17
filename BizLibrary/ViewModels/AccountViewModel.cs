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
using HashGenerators;
using BizLibrary.Repositories.Interfaces;
using BizLibrary.Repositories.Implementations;
using HashGenerators;
using System;

namespace MusicShop.ViewModels
{
	public class AccountViewModel : INotifyPropertyChanged
    {
        IAccountRepository _accountRepo = new AccountRepository();
        IRoleRepository _roleRepo = new RoleRepository();

        enum Role_Id
        {
            Client = 1,
            Admin = 2
        };

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

        #region Into Registration Logic
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
                            Password = MD5Generator.ProduceMD5Hash(ValidatePassword()),
                            Email = ValidateEmail(),
                            Phone = ValidatePhone(),
                            RoleId = (int)Role_Id.Client
                        };
                        var isSuchLogin = _accountRepo.GetAllAccounts().Where(a => a.Login == Login).FirstOrDefault();
						if (isSuchLogin !=  null)
						{
                            throw new Exception("Such Login Already Exist");
						}
                        
                        _accountRepo.AddAccount(newAcc);
                        MessageBox.Show($"Your account has been registered!", "Success", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (System.Exception err)
					{
                        MessageBox.Show($"{err.InnerException?.Message ?? err.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
					}
                });
			}
		}
        #endregion

        #region Into Loginization Logic

        private int tryCounter = 3;

        private ICommand _execLoginization;
        public ICommand ExecLoginization
        {
			get
			{
                return _execLoginization ?? new RelayCommand(obj =>
                {
					try
					{
						if (string.IsNullOrWhiteSpace(_login))
						{
                            throw new System.Exception("Please enter the login!");
						}
                        if (string.IsNullOrWhiteSpace(_password))
                        {
                            throw new System.Exception("Please enter the password!");
                        }

                        string hashedPasswordToCheck = MD5Generator.ProduceMD5Hash(_password);

                        Account accountByLogin = _accountRepo.GetAllAccounts().Where(acc => acc.Login == _login).FirstOrDefault();
						if (accountByLogin == null)
						{
                            throw new System.Exception("User with this login has not been found!");
						}
                        if (accountByLogin.Password != hashedPasswordToCheck)
                        {
                            throw new System.Exception($"Password is incorrect! Chances: {--tryCounter}");
                        }


                        var entryRole = _roleRepo.GetAllRoles().Where(role => role.Id == accountByLogin.RoleId).FirstOrDefault();
						if (entryRole == null)
						{
                            throw new System.Exception("Cannot proccess user with invalid role! Please add such role.");
						}



						switch (entryRole.Id)
						{
                            case (int)Role_Id.Client:
                                MessageBox.Show($"Welcome {entryRole.Name}!", "Client", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            case (int)Role_Id.Admin:
                                MessageBox.Show($"Welcome {entryRole.Name}!", "Admin", MessageBoxButton.OK, MessageBoxImage.Information);
                                break;
                            default:
                                throw new System.Exception("Cannot proccess user with invalid role! Please add such role.");
						}

					}
					catch (System.Exception err)
                    {
                        MessageBox.Show($"{err.InnerException?.Message ?? err.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
					{
						if (tryCounter == 0)
						{
                            MessageBox.Show("The amount of tryes 0. App will be closed", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
					}
                });
			}
		}
        #endregion

        #region Reg Validation

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
            Regex regExp = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?!.*(.)\1\1)[a-zA-Z0-9@]{6,12}$");
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

        #region init buttons logic
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
        #endregion
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
