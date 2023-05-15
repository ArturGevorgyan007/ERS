using Models;
using System.Data.SqlClient;
using Serilog;
namespace DataAccess;


public class DBRepository : IRepository
{
    private readonly string _connectionString;
    public DBRepository(string connectionString) {
        _connectionString = connectionString;
    }

    public List<Ticket> ViewTickets(User user) {
        List<Ticket> ticketList = new  List<Ticket>();

        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString()); 

        // Click the "Connect" button
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM TICKET Where UserID = @id", connection);
        cmd.Parameters.AddWithValue("@id", user.ID);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int wId = (int) reader["ID"];
            string wDescription = (string) reader["DESCRIPTION"];
            decimal wAmount= (decimal) reader["AMOUNT"];
            int wStatus= (int) reader["STATUS"];
            string statusText="";
            if (wStatus==0)
                statusText="Pending";
            else if (wStatus==1)
                statusText="Approved";
            else if (wStatus==2)
                statusText="Rejected";
            // Console.WriteLine($"Ticket ID{wId}: Spent {wAmount:C2} for {wDescription.ToLower()}. Status is {statusText.ToLower()}");
            Ticket t = new Ticket(wId,user.ID,wDescription,wAmount,wStatus);
            ticketList.Add(t);
        }
        return ticketList;
    }

    public int UserLogin(User user) {
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString()); 

        // Click the "Connect" button
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            string wName = (string) reader["NAME"];
            string wPassword = (string) reader["PASSWORD"];
            int wisManager = (int) reader["ISMANAGER"];
            int wid = (int) reader["ID"];
            if(wName==user.Username && wPassword==user.Password) {
                return wisManager;
            }            
        }
        return -1;
    }

    public Ticket CreateNewTicket(User user, Ticket newTicket) {
        
        
        using SqlConnection conn = new SqlConnection(Secrets.getConnectionString());
        conn.Open();


        using SqlCommand cmd = new SqlCommand("INSERT INTO Ticket(Description, Amount, UserID, Status) OUTPUT INSERTED.Id Values (@wDescription, @wAmount, @wUserId, @wStatus)", conn);
        cmd.Parameters.AddWithValue("@wDescription", newTicket.Description);
        cmd.Parameters.AddWithValue("@wAmount", newTicket.Amount);
        cmd.Parameters.AddWithValue("@wUserId", user.ID);       
        cmd.Parameters.AddWithValue("@wStatus", newTicket.Status);
        Console.WriteLine($"The ticket is successfully created",  Console.ForegroundColor=ConsoleColor.Green);
        Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
        int createdId = (int) cmd.ExecuteScalar();
        newTicket.UserId=createdId;   
        user.listOfTickets.Add(newTicket);
        

        return newTicket;
    }

        public User CreateNewUser(User userToCreate) {
        try 
        {
            using SqlConnection conn = new SqlConnection(Secrets.getConnectionString());
            conn.Open();

            using SqlCommand cmd = new SqlCommand("INSERT INTO Users(Name, Password, IsManager) OUTPUT INSERTED.Id Values (@wName, @wPassword, @wIsManager)", conn);
            cmd.Parameters.AddWithValue("@wName", userToCreate.Username);
            cmd.Parameters.AddWithValue("@wPassword", userToCreate.Password);
            cmd.Parameters.AddWithValue("@wIsManager", userToCreate.IsManager);

            int createdId = (int) cmd.ExecuteScalar();

            //User user = new User(name,password,createdId,isManager);
            // Console.WriteLine($"User with the name {@wName} is successfully created",  Console.ForegroundColor=ConsoleColor.Green);
            // Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
            
            return userToCreate;

        }
        catch (SqlException ex) {
            Console.WriteLine($"Username or password format is not valid",  Console.ForegroundColor=ConsoleColor.Red);
            Console.WriteLine($"Username length must be at least 4 and Password length must be at least 8 and must contain 1 uppercase, 1 lowercase letter, 1 digit and 1 special character ",  Console.ForegroundColor=ConsoleColor.Yellow);
            Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
            Log.Error("Caught SQL Exception trying to create new user");
            // Log.Error("Caught SQL ExceptiConsole.WriteLine($"Username or password format is not valid",  Console.ForegroundColor=ConsoleColor.red);Console.WriteLine($"Username or password format is not valid",  Console.ForegroundColor=ConsoleColor.red);Console.WriteLine($"Username or password format is not valid",  Console.ForegroundColor=ConsoleColor.red);on trying to create new user {0}", ex);
            //throw ex;
            return new User();
        }
    }

    public List<User> ShowUsersList() {
        
        List<User> usersList = new List<User>();
        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString()); 

        // Click the "Connect" button
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM USERS", connection);
        
        using SqlDataReader reader = cmd.ExecuteReader();
       
        while(reader.Read()) {
            int wId = (int) reader["ID"];
            string wName = (string) reader["NAME"];
            //Console.WriteLine($"{wId} - {wName}");
            string wpassword = (string) reader["PASSWORD"];
            int wisManager = (int) reader["ISMANAGER"];
            usersList.Add(new User(wName,wpassword,wId,wisManager));
        }
        return usersList;
    }   

    public Ticket ApproveTicket(int t_id) {
        using SqlConnection conn = new SqlConnection(Secrets.getConnectionString());
        conn.Open();
        
        using SqlCommand cmd = new SqlCommand($"UPDATE Ticket SET STATUS=1 where ID={t_id}", conn);
        cmd.ExecuteReader();
        // cmd.Parameters.AddWithValue("@wt_id", t_id);
        // Console.WriteLine("@wt_id");
        Console.WriteLine($"Ticket {t_id} is approved",  Console.ForegroundColor=ConsoleColor.Green);
        Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
        conn.Close();
        conn.Open();
        using SqlCommand cmd1 = new SqlCommand($"SELECT * FROM TICKET WHERE ID={t_id}", conn);
        using SqlDataReader reader = cmd1.ExecuteReader();
        
        Ticket t = new Ticket();
        while(reader.Read()) {
            int wId = (int) reader["ID"];
            int wuserId = (int) reader["UserID"];
            string wDescription = (string) reader["DESCRIPTION"];
            decimal wAmount= (decimal) reader["AMOUNT"];
            int wstatus = (int) reader["STATUS"];
            t = new Ticket(wId,wuserId,wDescription,wAmount,wstatus);
        }
        return t;
    }
    

    public Ticket RejectTicket(int t_id) {
        using SqlConnection conn = new SqlConnection(Secrets.getConnectionString());
        conn.Open();
        
        using SqlCommand cmd = new SqlCommand($"UPDATE Ticket SET STATUS=2 where ID={t_id}", conn);
         cmd.ExecuteReader();
        // cmd.Parameters.AddWithValue("@wt_id", t_id);
        // Console.WriteLine("@wt_id");
        Console.WriteLine($"Ticket {t_id} is rejected",  Console.ForegroundColor=ConsoleColor.Green);
        Console.WriteLine("",Console.ForegroundColor=ConsoleColor.White);
        conn.Close();
        conn.Open();
        using SqlCommand cmd1 = new SqlCommand($"SELECT * FROM TICKET WHERE ID={t_id}", conn);
        using SqlDataReader reader = cmd1.ExecuteReader();
        
        Ticket t = new Ticket();
        while(reader.Read()) {
            int wId = (int) reader["ID"];
            int wuserId = (int) reader["UserID"];
            string wDescription = (string) reader["DESCRIPTION"];
            decimal wAmount= (decimal) reader["AMOUNT"];
            int wstatus = (int) reader["STATUS"];
            t = new Ticket(wId,wuserId,wDescription,wAmount,wstatus);
        }
        return t;
    }

    public void ViewAllTickets(User u) {

        using SqlConnection connection = new SqlConnection(Secrets.getConnectionString()); 

        // Click the "Connect" button
        connection.Open();

        using SqlCommand cmd = new SqlCommand("SELECT * FROM TICKET Where UserID != @id", connection);
        cmd.Parameters.AddWithValue("@id", u.ID);
        using SqlDataReader reader = cmd.ExecuteReader();
        
        while(reader.Read()) {
            int wId = (int) reader["ID"];
            string wDescription = (string) reader["DESCRIPTION"];
            decimal wAmount= (decimal) reader["AMOUNT"];
            int wStatus= (int) reader["STATUS"];
            string statusText="";
            if (wStatus==0)
                statusText="Pending";
            else if (wStatus==1)
                statusText="Approved";
            else if (wStatus==2)
                statusText="Rejected";
            Console.WriteLine($"Ticket ID{wId}: Spent {wAmount:C2} for {wDescription.ToLower()}. Status is {statusText.ToLower()}");
        }

    }
}
