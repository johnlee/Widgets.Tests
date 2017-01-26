namespace Widgets.Data
{
    public interface IRepository
    {
        IWidgetRepository Widgets { get; }
        IOrderRepository Orders { get; }
    }
}
