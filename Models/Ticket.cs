using System.Text;
namespace Models;

public class Ticket
{
    private Decimal _amount;
    private string? _description;
    private int _id;

    private int _userId;
    private int _status = 0;

    private string _statusText="Pending";

    public Ticket()
    {
    }

    public Ticket(int id, string? description, Decimal amount)
    {
        _description=description;
        _amount=amount;
        _id=id;
    }

    public Ticket(int id, int userId, string? description, Decimal amount, int status)
    {
        _description=description;
        _amount=amount;
        _id=id;
        _userId=userId;
        _status=status;
        if (this._status==0)
                _statusText="Pending";
            else if (this._status==1)
                _statusText="Approved";
            else if (this._status==2)
                _statusText="Rejected";
    }

    public string? Description {
        set {
            _description=value;
        }
        get {
            return _description;
        }
    }
    public Decimal Amount {
        set {
            _amount=value;
        }
        get {
            return _amount;
        }
    }

    public int Status {
        set {
            _status=value;
        }
        get {
            return _status;
        }
    }

    public int ID {
        set {
            _id=value;
        }
        get {
            return _id;
        }
    }

    public int UserId {
        set {
            _userId=value;
        }
        get {
            return _userId;
        }
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"ID: {this.ID} | Spent {this.Amount:C2} for {this.Description.ToLower()} | Status is {this._statusText.ToLower()}");
        return sb.ToString();
    }
}