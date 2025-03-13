namespace OrderProcessing
{
	public enum OrderStatus
	{
		New,
		InWarehouse,
		InDelivery,
		Sended,
		ReturnedToCustomer,
		Error,
		Closed
	}
}