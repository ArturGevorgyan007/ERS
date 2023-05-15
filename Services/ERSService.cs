using Models;
using DataAccess;

namespace Services;
public class ERSService
{
    // Dependency Injection: is a design pattern where the dependency of a class is injected through the constructor instead of the class itself having a specific knowledge of its dependency, or instantiating itself
    // This example here is actually a combination of dependency injection and dependency inversion
    // This allows for more flexible change in implementation, also this pattern makes unit testing much simpler
    private readonly IRepository _repo;
    public ERSService(IRepository repo) {
        _repo = repo;
    }

    public List<User> ShowUsersList() {
        return _repo.ShowUsersList();
    }

    public User CreateNewUser(User userToCreate) {
        return _repo.CreateNewUser(userToCreate);
    }

    public int UserLogin(User user) {
        return _repo.UserLogin(user);
    }

    public Ticket CreateNewTicket(User user, Ticket newTicket) {
        return _repo.CreateNewTicket(user, newTicket);
    }

    public List<Ticket> ViewTickets(User user) {
        return _repo.ViewTickets(user);
    }

    public Ticket ApproveTicket(int ticketID) {
        return _repo.ApproveTicket(ticketID);
    }

    public Ticket RejectTicket(int ticketID) {
        return _repo.RejectTicket(ticketID);
    }
}
