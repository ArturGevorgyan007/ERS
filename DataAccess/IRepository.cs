using Models;
namespace DataAccess;
public interface IRepository
{
    int UserLogin(User user);

    Ticket CreateNewTicket(User u, Ticket newTicket);

    User CreateNewUser(User userToCreate);

    List<Ticket> ViewTickets(User user);
    List<User> ShowUsersList();

    Ticket ApproveTicket(int t_id);

    Ticket RejectTicket(int t_id);

    void ViewAllTickets(User u);
}