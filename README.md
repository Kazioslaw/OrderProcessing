# OrderProcessing

## About

This project is a console application for order processing.

### Key Features

- **Create a new order**: Create a sample order
- **Send order to the warehouse**: Send order to the warehouse
- **Send order for delivery**: Forward the order for delivery
- **Show all orders**: Show list of all orders
- **Exit the application**: Exit the application

## Model details

#### Order

- **Product Name**: Name of the ordered product
- **Order Price**: The order total price
- **Address**
  - **Street**: Street and building number
  - **City**: City
  - **Zipcode**: Postal Code
- **Client Type**: Customer Type
- **Payment Type**: Payment Type
- **Status**: Order status

## Enum

#### ClientType

- **Company**
- **Individual**

#### Payment Type

- **Credit Card**
- **Cash at pickup**

#### Status

- **New**: Every order created is assigned this status
- **InWarehouse**: Status after transfer to the warehouse
- **InDelivery**: Status after transfer to shipping
- **ReturnedToCustomer**: Status given when the total price is less than required for payment by cash on pickup
- **Error**: Status given when address is not entered correctly
- **Closed**
