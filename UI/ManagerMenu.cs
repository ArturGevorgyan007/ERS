﻿using Models;

namespace UI;

public class ManagerMenu
{
    public ManagerMenu() {
        
        //TicketList = _repo.initTicketList();
    }
    List<Ticket> TicketList = new List<Ticket>();
    public void Start(User u)
    {
        
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Welcome to Managers Profile");
        Console.WriteLine("-----------------------------------------------------------------\n");

        while (true) {
            Console.WriteLine("PLEASE ENTER THE NUMBER OF YOUR CHOICE\n");
            Console.WriteLine("0 -> Log out");
            Console.WriteLine("1 -> Create a ticket");
            Console.WriteLine("2 -> View My submitted tickets");
            Console.WriteLine("3 -> Manage Users submitted tickets");
           
            
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
                break;
                case "3":
                Console.WriteLine("-----------------------------------------------------------------");
                Console.WriteLine("Please follow the steps to edit ticket status");
                Console.WriteLine("-----------------------------------------------------------------\n");
                // ViewAllTickets(u);
                Console.WriteLine("Please enter ticket ID to manage\n");
                int TicketToManage = int.Parse(Console.ReadLine()!);
                // TicketManageMenu tmm = new(_repo);
                // tmm.Start(u,TicketToManage);
                break;
                default:
                Console.WriteLine("Invalid entry");
                break;
            }
        }       
    }

//     private Ticket EditTicket(User u) {

//         Console.WriteLine("Creating ticket");
//         Console.Write("Please enter expense description: ");
//         string? Description = Console.ReadLine()!;
//         Console.Write("Please enter expense amount: ");
//         decimal Amount = Decimal.Parse(Console.ReadLine()!);

//         Ticket ticket = _repo.CreateNewTicket(u,Description,Amount);
//         u.listOfTickets.Add(ticket);

//         return ticket;
//     }

    private void ViewAllTickets(User u) {

        Console.WriteLine("Please see below all submitted tickets by all users");
        // foreach(User user in MainMenu.UserList) {
        //     if (u.Username==user.Username)
        //         continue;
        //     else
        //         foreach (Ticket t in user.listOfTickets) {
        //                 Console.WriteLine($"User: {user.Username}, Ticket: {t.ID}, Spent {t.Amount:C2} for {t.Description.ToLower()}, status is {t.StatusText.ToLower()}");
        //         }
        // }
            // _repo.ViewAllTickets(u);
        
        
    }

    private void ViewTickets(User u) {

        Console.WriteLine("Please see below all submitted tickets");
        // for (int i=0;i<u.listOfTickets.Count;i++) {
        //     Console.WriteLine($"Ticket {i+1}: Spent {u.listOfTickets[i].Amount} for {u.listOfTickets[i].Description}");
        // }
        // _repo.ViewTickets(u);
        
    }

    // private Ticket CreateTicket(User u) {

    //     Console.WriteLine("Creating ticket");
    //     Console.Write("Please enter expense description: ");
    //     string? Description = Console.ReadLine()!;
    //     Console.Write("Please enter expense amount: ");
    //     decimal Amount = Decimal.Parse(Console.ReadLine()!);

    //     // Ticket ticket = _repo.CreateNewTicket(u,Description,Amount);
    //     u.listOfTickets.Add(ticket);

    //     return ticket;
    // }
}
   