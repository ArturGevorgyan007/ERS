using Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace UI;

public class UserMenu
{
    // public UserMenu() {
    // }

    private HttpClient _http;
    public UserMenu() {
       _http = new HttpClient();
       _http.BaseAddress = new Uri("http://localhost:5054/");
    }
    // List<Ticket> TicketList = new List<Ticket>();

    public async Task Start(User u)
    {
        
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Welcome to Empoyee Profile");
        Console.WriteLine("-----------------------------------------------------------------\n");

        while (true) {
            Console.WriteLine("PLEASE ENTER THE NUMBER OF YOUR CHOICE\n");
            Console.WriteLine("0 -> Log out");
            Console.WriteLine("1 -> Creat a ticket");
            Console.WriteLine("2 -> View submitted tickets");
            
            string input= Console.ReadLine()!;

            switch (input) {
                case "0":
                Console.WriteLine("Logged out! Back to the Main Menu\n");
                return;
                case "1":
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Please follow the steps to creat a ticket");
                Console.WriteLine("-----------------------------------------------------------------\n");
                // CreateTicket(u);
                break;
                case "2":
                // ViewTickets(u);
                string content = await _http.GetStringAsync($"user/tickets?username={u.Username}");
                Console.WriteLine(content);

                List<Ticket> tickets = JsonSerializer.Deserialize<List<Ticket>>(content);
                foreach (Ticket t in tickets)
                    Console.WriteLine(t);
                break;
                default:
                Console.WriteLine("Invalid entry");
                break;
            }
        }       
    }

    // private Ticket CreateTicket(User u) {

    //     Console.WriteLine("Creating ticket");
    //     Console.Write("Please enter expense description: ");
    //     string? Description = Console.ReadLine()!;
    //     Console.Write("Please enter expense amount: ");
    //     decimal Amount = Decimal.Parse(Console.ReadLine()!);

    //     // Ticket ticket = _repo.CreateNewTicket(u,Description,Amount);
    //     // u.listOfTickets.Add(ticket);

    //     return ticket;
    // }

    // private void ViewTickets(User u) {

    //     Console.WriteLine("Please see below all submitted tickets");
    //     // for (int i=0;i<u.listOfTickets.Count;i++) {
    //     //     Console.WriteLine($"Ticket {i+1}: Spent {u.listOfTickets[i].Amount} for {u.listOfTickets[i].Description}");
    //     // }
    //     // _repo.ViewTickets(u);
        
    // }
}

   