using System;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using SignalRClientWPF.Dto;

namespace SignalRClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Threading.Thread Thread { get; set; }
        public string Host = "http://localhost:8089/";

        public IHubProxy Proxy { get; set; }
        public HubConnection Connection { get; set; }

        public bool Active { get; set; }

        private string userId;

        public MainWindow()
        {
            InitializeComponent();
        }


        private async void ActionHeartbeatButtonClick(object sender, RoutedEventArgs e)
        {
            await SendHeartbeat();
        }

         private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            await Proxy.Invoke("AddFriend", userId, FriendUserIdTextBox.Text);
        }


        private async void ActionSendButtonClick(object sender, RoutedEventArgs e)
        {
           //  await SendMessage();
        }

        private async Task SendMessage()
        {
            await Proxy.Invoke("AddMessage",userId, FriendListBox.SelectedItem.ToString(), MessageTextBox.Text);
        }

        private async void ActionSendObjectButtonClick(object sender, RoutedEventArgs e)
        {
          await SendMessage();
        }

        private async Task AddUSer()
        {
            await Proxy.Invoke("AddUser", ClientNameTextBox.Text, MessageTextBox.Text);
        }

        private async Task SendHeartbeat()
        {
            await Proxy.Invoke("HeartBeatChange");
        }

        private async Task SendHelloObject()
        {
            HelloModel hello = new HelloModel { Age = Convert.ToInt32(HelloTextBox.Text), Name = HelloMollyTextBox.Text };
            await Proxy.Invoke("sendHelloObject", hello);
        }

        private async void ActionWindowLoaded(object sender, RoutedEventArgs e)
        {


        }

        private static async Task<CookieCollection> GetAuthCookie(string loginUrl, string user, string pass)
        {
            var postData = string.Format(
            "username={0}&password={1}",
            user, pass
            );
            var httpHandler = new HttpClientHandler { CookieContainer = new CookieContainer() };
            using (var httpClient = new HttpClient(httpHandler))
            {
                var response = await httpClient.PostAsync(
                loginUrl,
                new StringContent(postData, Encoding.UTF8,
                "application/x-www-form-urlencoded"
                )
                );
                var cookies = httpHandler.CookieContainer
                .GetCookies(new Uri(loginUrl));
                return cookies;
            }
        }

        private void OnSendData(string message)
        
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => {
                                                                            MessagesListBox.Items.Insert(0, message);
                                                                            AddFriendGrid.Visibility =
                                                                                Visibility.Collapsed;
                                                                            MessaginGrid.Visibility = Visibility.Visible;

            }));
        }
        private void OnSendData(string ContextID,string userId)
        {
           
            Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
            {
                FriendListBox.Items.Add(userId);
                AddFriendGrid.Visibility = Visibility.Collapsed;
                MessaginGrid.Visibility = Visibility.Visible;
            }));
        }

        private async void ActionMessageTextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
               //  await SendMessage();
                MessageTextBox.Text = "";
            }
        }

        private void SignInTextBlock_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SignupGrid.Visibility = Visibility.Collapsed;
            LoginGrid.Visibility = Visibility.Visible;
        }

        private void SignUpTextBlock_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SignupGrid.Visibility = Visibility.Visible;
            LoginGrid.Visibility = Visibility.Collapsed;
        }

        private void Go_OnClick(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordTextBox.Password;
            Active = true;

            GetConnection(userName, password);
        }

        private string nameOfLoggedInUser;

        private  void GetConnection(string userName, string password)
        {
            Thread = new System.Threading.Thread(() =>
            {
                var loginUrl1 = "http://localhost:8089";

                var loginUrl = "http://localhost:8089//account/remotelogin";
                var authCookie = GetAuthCookie(loginUrl, userName, password).Result;
                if (authCookie == null)
                {
                    nameOfLoggedInUser = string.Empty;
                    Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => errorTextBlock.Visibility = Visibility.Visible));

                    return;
                }
                userId = userName;
                Connection = new HubConnection(Host);
                Connection.CookieContainer = new CookieContainer();
                Connection.CookieContainer.Add(authCookie["userId"]);
                Connection.CookieContainer.Add(authCookie["userName"]);
                nameOfLoggedInUser = authCookie["userName"].Value.Replace("%20", " ");
                Proxy = Connection.CreateHubProxy("MyHub");
                Proxy.On<string, string>("addmessage",
                    (name, message) => OnSendData("Recieved addMessage: " + name + ": " + message));
                Proxy.On("heartbeat", () => OnSendData("Recieved heartbeat"));
                Proxy.On<HelloModel>("sendHelloObject",
                    hello => OnSendData("Recieved sendHelloObject " + hello.Name + " " + hello.Age));
                Proxy.On<string>("broadcast", OnSendData);
                Proxy.On<string>("SendPrivateMessage", OnSendData);
                Proxy.On<string,string>("SendFriendAdded", OnSendData);
                Connection.Start().Wait();
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() => SuccessfulLogin()));

                Proxy.Invoke("SendPrivateMessage", "Hi, I'm authenticated!");
                while (Active)
                {
                    System.Threading.Thread.Sleep(10);
                }
            }) { IsBackground = true };
            Thread.Start();
        }

        private bool SuccessfulLogin()
        {
            LoginGrid.Visibility = Visibility.Collapsed;
            errorTextBlock.Visibility = Visibility.Collapsed;
            HomeViewHeader.Visibility = Visibility.Collapsed;
            LoggedUserNameTextBlock.Text = string.Concat("Hi ", nameOfLoggedInUser);
            LoginViewPanel.Visibility = Visibility.Visible;
            MessaginGrid.Visibility = Visibility.Visible;
            return true;
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            UserNameTextBox.Text = string.Empty;
            PasswordTextBox.Password = string.Empty;
        }

        private void SignOut_OnClick(object sender, MouseButtonEventArgs e)
        {
            //var jk = Proxy.Invoke<int>("Sum", 2, 3).Result;

        }

        private void CancelSignUp_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private async Task AddUser()
        {
            await Proxy.Invoke("AddUser", new User
            {
                Password = PasswordTextBoxSignup.Password,
                UserName = UserNameTextBoxsignUP.Text,
                FirstName = FirstNameTextBoxsignUp.Text,
                LastName = LastNameTextBoxsignUp.Text
            });
        }

        private HubConnection Connection1;
        private async void Register_OnClick(object sender, RoutedEventArgs e)
        {

            var loginUrl1 = "http://www.contoso.com:8080/";

            Connection1 = new HubConnection(loginUrl1);

            Proxy = Connection1.CreateHubProxy("AddUserHub");
            Proxy.On<bool>("AddMessage",AfterRegistration);
            await Connection1.Start();
            await AddUser();
        }
        private  void AfterRegistration(bool isSucceed)
        {
            if (isSucceed)
            {
                Connection1.Stop();
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)(() =>
                {
                    SignupGrid.Visibility = Visibility.Collapsed;
                    SuccessFullRegistrationtxtBlock.Visibility = Visibility.Visible;
                    SuccessFullRegistrationtxtBlock.Text = "Registration successful. Login to continue.";
                    LoginGrid.Visibility = Visibility.Visible;
                    UserNameTextBox.Focus();
                    PasswordTextBox.Focus();
                }));
            }
           
        }

        public class User
        {
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
        }

        private void AddFriendTextBlock_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessaginGrid.Visibility = Visibility.Collapsed;
            AddFriendGrid.Visibility = Visibility.Visible;
        }

       
        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClickHandler(object sender, RoutedEventArgs e)
        {

        }

        private void FriendListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

       

       
    }
}
