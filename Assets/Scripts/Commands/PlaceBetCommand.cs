public class PlaceBetCommand
{
    private UserData data;

    public PlaceBetCommand(UserData data)
    {
        this.data = data;
    }

    public void Execute()
    {
        data.Balance -= (int) (data.Bet * 100);
    }
}
