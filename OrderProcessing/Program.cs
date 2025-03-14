using OrderProcessing.Services;

namespace OrderProcessing
{
	internal class Program
	{
		static void Main()
		{
			Program mainMenu = new Program();

			bool applicationRunning = true;
			while (applicationRunning)
			{
				applicationRunning = mainMenu.DisplayMenu();
			}

		}
		public bool DisplayMenu(string errorMessage = "")
		{
			OrderService order = new OrderService();
			Console.Clear();
			if (!string.IsNullOrWhiteSpace(errorMessage))
			{
				Console.WriteLine(errorMessage);
			}
			Console.WriteLine("1. Create a new order.");
			Console.WriteLine("2. Send order to the warehouse.");
			Console.WriteLine("3. Send order for delivery.");
			Console.WriteLine("4. Show all orders.");
			Console.WriteLine("5. Exit the application");
			Console.WriteLine();

			Console.Write("Select: ");
			var option = Console.ReadLine();

			switch (option)
			{
				case "1":
					order.CreateOrder();
					break;
				case "2":
					order.TransferOrderToWarehouse();
					break;
				case "3":
					order.TransferOrderToDelivery();
					break;
				case "4":
					order.ViewOrders();
					break;
				case "5":
					Console.WriteLine("Exiting the app....");
					return false;
				default:
					DisplayMenu("This option is not valid. Please try again...");
					break;
			}
			return true;
		}
	}
}
