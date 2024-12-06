public class ChangeBetCommand
{
    private UserData data;

    public ChangeBetCommand(UserData data)
    {
        this.data = data;
    }

    public void Execute(ChangeBetSignal signal)
    {
        data.Bet = signal.Bet;
    }
}