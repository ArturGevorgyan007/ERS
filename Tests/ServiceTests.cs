using Models;
using DataAccess;
using Services;

namespace Tests;

internal class FakeRepo : IRepository {
    public int UserLogin(User user) {
        throw new NotImplementedException();
    }

    public Ticket CreateNewTicket(User u, Ticket newTicket) {
         return new Ticket {
                Description = "Cinema Tickets",
                Amount = 50,
                UserId = 4,
                ID = 7,
            };
    }

    public User CreateNewUser(User userToCreate) {
         return new User {
            Username = "TestUser",
            Password = "P@55woRD0",
            ID = 1,
            IsManager = 1
         };
    }

    public List<User> initUserList() {
         throw new NotImplementedException();
    }

    public void initTicketList(List<User> uList) {
         throw new NotImplementedException();
    }

    // void ViewTickets(User u);

    public List<Ticket> ViewTickets(User user) {
          return new List<Ticket> {
            new Ticket {
                Description = "Bus Tickets",
                Amount = 5,
                UserId = 1,
                ID = 1,
            },
            new Ticket {
                Description = "Uber",
                Amount = 19,
                UserId = 2,
                ID = 3,
            },
            new Ticket {
                Description = "Tip",
                Amount = 4,
                UserId = 3,
                ID = 2,
            }
         };
    }
    public List<User> ShowUsersList() {
         return new List<User> {
            new User {
                Username = "UserTest1",
                Password = "R3v@tureTest1",
                ID = 1,
                IsManager = 1,
            },
            new User {
                Username = "UserTest2",
                Password = "R3v@tureTest2",
                ID = 2,
                IsManager = 0,
            },
            new User {
                Username = "UserTest3",
                Password = "R3v@tureTest3",
                ID = 1,
                IsManager = 3,
            }
         };
    }

    public Ticket ApproveTicket(int t_id) {
         throw new NotImplementedException();
    }

    public Ticket RejectTicket(int t_id) {
         throw new NotImplementedException();
    }

    public void ViewAllTickets(User u) {
         throw new NotImplementedException();
    }
}

public class ServiceTests {

    [Fact]

    public void ServiceShouldCreate() {
        ERSService service = new ERSService(new FakeRepo());
        Assert.NotNull(service);
    }
    [Fact]  
    public void ShowUsersListTest() {
        ERSService service = new ERSService(new FakeRepo());
        List<User> users = service.ShowUsersList();
        Assert.NotNull(users);
        Assert.Equal(3,users.Count);
        Assert.Equal(2,users[1].ID);
    }

     [Fact]  
    public void CreateNewTicketTest() {
        ERSService service = new ERSService(new FakeRepo());
        User u = new User();
        Ticket newTicket = new Ticket();
        Ticket ticket = service.CreateNewTicket(u,newTicket);
        Assert.NotNull(ticket);
        Assert.Equal(50,ticket.Amount);
    }

      [Fact]  
    public void CreateNewUserTest() {
        User u = new User();
        ERSService service = new ERSService(new FakeRepo());
        User user = service.CreateNewUser(u);
        Assert.NotNull(user);
        Assert.Equal("TestUser",user.Username);
    }
}