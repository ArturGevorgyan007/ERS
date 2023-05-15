using Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;

namespace UI;

public class MainMenu
{
    private HttpClient _http;
    public MainMenu() {
       _http = new HttpClient();
       _http.BaseAddress = new Uri("http://localhost:5054/");
    }

    public static List<User> UserList = new List<User>();
    public static List<Ticket> TicketList = new List<Ticket>();
    

    public async Task StartAsync()
    {
        
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Welcome to the Expense Reimbursment System");
        Console.WriteLine("-----------------------------------------------------------------");
        //bool mainMenu = true;
        
        

        while (true) {
            Console.WriteLine("\nPLEASE ENTER THE NUMBER OF YOUR CHOICE\n");
            Console.WriteLine("0 -> Exit Application");
            Console.WriteLine("1 -> Login to My Account");
            Console.WriteLine("2 -> Register to New Account");
            Console.WriteLine("3 -> Show Users List");

            string input= Console.ReadLine()!;

            switch (input) {
                case "0":
                Console.WriteLine("Goodbye");
                Environment.Exit(0);
                break;
                case "1":
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Welcome to the Employee portal login");
                Console.WriteLine("-----------------------------------------------------------------\n");
                await UserLogin();
                Thread.Sleep(3000);
                break;
                case "2":
                CreateNewUser();
                break;
                case "3":
                // ShowUsersList();
                // HttpResponseMessage msg = await _http.GetAsync("users");
                // Console.WriteLine(await msg.Content.ReadAsStringAsync());
                string content = await _http.GetStringAsync("users");
                Console.WriteLine(content);

                List<User> users = JsonSerializer.Deserialize<List<User>>(content);
                foreach (User u in users)
                    Console.WriteLine(u);
                break;
                default:
                Console.WriteLine("Invalid entry");
                break;
            }
        }       
    }

    private async Task CreateNewUser() {
        User newUser = new User();
        Console.WriteLine("Creating New User");
        Console.Write("Please enter your name: ");
        string? UserName = Console.ReadLine()!;
        newUser.Username=UserName;
        Console.Write("Please enter your password: ");
        //string? Password = Console.ReadLine()!;
        string? Password = PasswordEntryMasking();
        newUser.Password=Password;
        Console.WriteLine("\nAre you a manager? ([1]-yes, [0]-no)");
        int isManager = int.Parse(Console.ReadLine()!);
        newUser.IsManager=isManager;
        if(isManager==0 || isManager==1) {
            JsonContent jsonContent = JsonContent.Create<User>(newUser);
            await _http.PostAsync("register", jsonContent);
        }
        else
            Console.WriteLine("Invalid entry!!!!!!!!!");
    }

    public async Task UserLogin() {
        User user = new User();
        Console.Write("Please enter your Username: ");
        string? UserName = Console.ReadLine()!;
        user.Username=UserName;
        Console.Write("Please enter your Password: ");
        string? Password = PasswordEntryMasking();
        user.Password=Password;

        JsonContent jsonContent = JsonContent.Create<User>(user);
        HttpResponseMessage msg = await _http.PostAsync("login", jsonContent);
        //await Task.Delay(2000);
        if ((await msg.Content.ReadAsStringAsync())=="0"){
                Console.WriteLine("\nLogged in successfully!\n");
                UserMenu userMenu = new UserMenu();
                userMenu.Start(user);
            }
            else if ((await msg.Content.ReadAsStringAsync())=="1"){
                Console.WriteLine("\nLogged in successfully as a Manager!\n");
                ManagerMenu managerMenu = new ManagerMenu();
                managerMenu.Start(user);
            }
        

                
        //     if(msg.IsSuccessStatusCode) {
        //         if(u.IsManager==0) {
        //             currentUser=u;
        //             UserMenu userMenu = new UserMenu(_repo);
        //             userMenu.Start(currentUser);
        //         }
        //         else {
        //             currentUser=u;
        //             ManagerMenu managerMenu = new ManagerMenu(_repo);
        //             managerMenu.Start(currentUser);
        //         }
        //         break;
        //     else
        //         Console.Write("Wrong Username or Password. Please try again.");
        // }
      
        // _repo.UserLogin(UserName, Password);

        // bool userFound = false;
        // bool passwordMatch = false;
        // User currentUser = new();

        // while(!userFound) {
        //     Console.Write("Please enter your Username: ");
        //     string? UserName = Console.ReadLine()!;
        //     foreach (User obj in UserList) {
        //         if (obj.Username==UserName) {
        //             currentUser=obj;           
        //             userFound=true;
        //             break;
        //         }
        //         else 
        //             continue; 
        //     }
        //     if(!userFound) {
        //         Console.WriteLine("Invalid Username",Console.ForegroundColor=ConsoleColor.Red);
        //         Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
        //     }
        // }

         
       
        // User currentUser = new();

        // foreach(User u in UserList) {
        //     if(u.Username==UserName && u.Password==Password) {
        //         if(u.IsManager==0) {
        //             currentUser=u;
        //             UserMenu userMenu = new UserMenu(_repo);
        //             userMenu.Start(currentUser);
        //         }
        //         else {
        //             currentUser=u;
        //             ManagerMenu managerMenu = new ManagerMenu(_repo);
        //             managerMenu.Start(currentUser);
        //         }
        //     }
        // }
        
    
    }

    // public void ShowUsersList() {
    //     List<User> usersList = _service.ShowUsersList();

    //     foreach (User u in usersList) {
    //         Console.WriteLine($"{u.ID} - {u.Username} - {u.IsManager}");
    //     }
    // }

    public string PasswordEntryMasking() {
        string? Password = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && Password.Length > 0)
            {
                Console.Write("\b \b");
                Password = Password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                Password += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        return Password;
    }
}