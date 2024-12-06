public class ShowAddMoneyPopupCommand
{
    private readonly AddMoneyPopup.Factory popupFactory;

    public ShowAddMoneyPopupCommand(AddMoneyPopup.Factory popupFactory)
    {
        this.popupFactory = popupFactory;
    }

    public void Execute(ShowAddMoneyPopupSignal signal)
    {
        popupFactory.Create();
    }
}