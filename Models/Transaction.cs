namespace Models;
public class Transaction
{
    public int ID{get;}
    public int SenderAccountNumber{get;set;}
    public int ReceiverAccountNumber{get;set;}
    public Double Amount{get;set;}
}
