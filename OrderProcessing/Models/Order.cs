namespace OrderProcessing.Models
{
	class Order
	{
		public string ProductName { get; set; }
		public decimal OrderPrice { get; set; }
		public string Street { get; set; }
		public string City { get; set; }
		public string Zipcode { get; set; }
		public ClientType ClientType { get; set; }
		public PaymentType PaymentType { get; set; }
		public OrderStatus Status { get; set; }

		public Order()
		{
			Status = OrderStatus.New;
		}
	}

}
