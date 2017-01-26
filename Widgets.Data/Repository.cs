namespace Widgets.Data
{
    public class Repository : IRepository
    {
        public IOrderRepository Orders
        {
            get
            {
                return new OrderRepository();
            }
        }

        public IWidgetRepository Widgets
        {
            get
            {
                return new WidgetRepository();
            }
        }
    }
}