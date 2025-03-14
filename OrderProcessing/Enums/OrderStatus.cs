namespace OrderProcessing
{
	public enum OrderStatus
	{
		New,
		InWarehouse,
		InDelivery,
		ReturnedToCustomer,
		Error,
		Closed
	}
}