using OrderProcessing.Models;
using System.Data;
using System.Globalization;

namespace OrderProcessing
{
	internal class Program
	{
		static Dictionary<int, Order> orders = new Dictionary<int, Order>();
		static int nextKey = 1;
		static void Main()
		{
			bool applicationRunning = true;
			while (applicationRunning)
			{
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
						CreateOrder();
						break;
					case "2":
						TransferOrderToWarehouse();
						break;
					case "3":
						TransferOrderToDelivery();
						break;
					case "4":
						ViewOrders();
						break;
					case "5":
						Console.WriteLine("Exiting the app....");
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("This option is not valid. Please try again...");
						Console.WriteLine();
						break;
				}
			}

		}
		static void CreateOrder()
		{
			Console.Clear();
			Order order = new Order();
			bool isValid = false;
			string input = null;
			Console.WriteLine("Creating order...");
			while (!isValid)
			{
				Console.Write("Enter product name: ");
				input = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(input))
				{
					Console.WriteLine("Product name cannot be empty");
				}
				else
				{
					order.ProductName = input;
					break;
				}
			}

			decimal totalPrice;

			while (!isValid)
			{
				Console.Write("Enter total price: ");
				isValid = decimal.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out totalPrice) && (totalPrice > 0);
				if (!isValid)
				{
					Console.WriteLine("The amount entered was less than zero or was not a number. Please enter a valid decimal number.");
				}
				order.OrderPrice = totalPrice;
			}
			Console.WriteLine();
			Console.WriteLine("Enter address: ");
			Console.Write("Street: ");
			order.Street = Console.ReadLine();
			Console.Write("Zipcode: ");
			order.Zipcode = Console.ReadLine();
			Console.Write("City: ");
			order.City = Console.ReadLine();

			isValid = false;
			Console.WriteLine();

			int clientType;
			Console.WriteLine("1. Company");
			Console.WriteLine("2. Individual");
			while (!isValid)
			{
				Console.Write("Enter client type: ");
				isValid = int.TryParse(Console.ReadLine(), out clientType) && (clientType == 2 || clientType == 1);
				if (!isValid)
				{
					Console.WriteLine("The entered client type was wrong, try again...");
				}
				order.ClientType = (ClientType)clientType;
			}
			isValid = false;
			Console.WriteLine();

			int paymentType;
			Console.WriteLine("1. Credit Card");
			Console.WriteLine("2. Payment at pickup");
			while (!isValid)
			{
				Console.Write("Enter payment type: ");
				isValid = int.TryParse(Console.ReadLine(), out paymentType);
				if (!isValid || paymentType > 2 || paymentType < 1)
				{
					Console.WriteLine("The entered payment type was wrong, try again...");
				}
				order.PaymentType = (PaymentType)paymentType;
			}

			if (string.IsNullOrWhiteSpace(order.Street) || string.IsNullOrEmpty(order.City) || string.IsNullOrEmpty(order.Zipcode))
			{
				order.Status = OrderStatus.Error;
				Console.WriteLine("Order cannot have empty address. Status changed to error.");
			}
			else
			{
				Console.WriteLine("Order created.");
			}
			orders.Add(nextKey++, order);
			Console.WriteLine("Press any key to back to menu");
			Console.ReadKey();
			Console.Clear();
			Main();
		}

		static void TransferOrderToWarehouse()
		{
			Console.Clear();
			bool isValid = false;
			int orderKey = 0;
			Order pickedOrder = null;
			Dictionary<int, Order> newOrders = orders.Where(order => order.Value.Status == OrderStatus.New).ToDictionary();
			foreach (KeyValuePair<int, Order> kvp in newOrders)
			{
				Order order = kvp.Value;
				Console.WriteLine($@"ID: {kvp.Key}, Product Name: {order.ProductName}, Total: {order.OrderPrice:C} Status: {order.Status}");
			}
			while (!isValid)
			{

				Console.WriteLine("Select the order for transfer to the warehouse: ");
				isValid = int.TryParse(Console.ReadLine(), out orderKey);
				if (!isValid || !(orders.TryGetValue(orderKey, out pickedOrder)))
				{
					Console.WriteLine("The order number entered is incorrect, please select again.");
				}
			}

			if (pickedOrder != null)
			{
				if (pickedOrder.OrderPrice < 2500 && pickedOrder.PaymentType == PaymentType.CashOnDelivery)
				{
					pickedOrder.Status = OrderStatus.ReturnedToCustomer;
					orders[orderKey] = pickedOrder;
					Console.WriteLine("Cannot send the order to the warehouse because the total is too low for this payment method." +
						"\nThe order has been returned to the client." +
						"\nPlease change the payment method.");
				}
				else
				{
					pickedOrder.Status = OrderStatus.InWarehouse;
					orders[orderKey] = pickedOrder;
					Console.WriteLine("The order was properly transferred to the warehouse.");
				}
			}

			Console.WriteLine("Press any key to back to menu");
			Console.ReadKey();
			Console.Clear();
			Main();
		}

		static void TransferOrderToDelivery()
		{
			Console.Clear();
			Dictionary<int, Order> ordersInWarehouse = orders.Where(order => order.Value.Status == OrderStatus.InWarehouse).ToDictionary();

			foreach (KeyValuePair<int, Order> kvp in ordersInWarehouse)
			{
				Order order = kvp.Value;
				Console.WriteLine($@"ID: {kvp.Key}, Product Name: {order.ProductName}, Total: {order.OrderPrice:C} Status: {order.Status}");
			}
			bool isValid = false;
			int orderKey = 0;
			Order pickedOrder = null;
			while (!isValid)
			{
				Console.WriteLine("Pick order to send: ");
				isValid = int.TryParse(Console.ReadLine(), out orderKey);
				if (!isValid || !(ordersInWarehouse.TryGetValue(orderKey, out pickedOrder)))
				{
					Console.WriteLine("The order number entered is incorrect, please enter again.");
				}
			}

			if (pickedOrder != null)
			{
				Console.WriteLine("Transfered to delivery...");
				Thread.Sleep(5000);
				pickedOrder.Status = OrderStatus.InDelivery;
				Console.WriteLine("Sended");
			}

			Console.WriteLine("Press any key to back to menu");
			Console.ReadKey();
			Console.Clear();
			Main();
		}

		static void ViewOrders()
		{
			Console.Clear();
			Console.WriteLine("Order list:");
			foreach (KeyValuePair<int, Order> kvp in orders)
			{
				Order order = kvp.Value;
				Console.WriteLine($@"ID: {kvp.Key}, Product Name: {order.ProductName}, Total: {order.OrderPrice:C} Status: {order.Status}");
			}

			Console.WriteLine("Press any key to back to menu");
			Console.ReadKey();
			Console.Clear();
			Main();
		}
	}
}
