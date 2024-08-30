# SA_Online_Mart
This is the Home Page
![Welcome to home page](https://github.com/user-attachments/assets/d8293902-cd35-4951-9f60-995d536fb80a)

Before making any purchases first Register and create your account.
![Register](https://github.com/user-attachments/assets/cf6675c0-695c-402b-aed3-3f8558d64bf9)

Add Items to the cart.   

![CartItems](https://github.com/user-attachments/assets/16439233-615a-44be-a61f-cb6a15566821)

After Clicking the Checkout link, you are redirected to the Checkout Page   

![CheckoutPage](https://github.com/user-attachments/assets/a43d3fba-0573-42dc-bc42-daa1692bbc20)

Click Pay With Card and you will be redirected to the Payment Gateway.   

Find a card number on the official Stripe Website.   

You can click Test Mode to be redirected to the Stripe Page.    

Copy the card number and paste it on the payment gateway.   

Enter any email or CVC number, those do not matter as much as this is for testing purposes.   

![PaymentGateway](https://github.com/user-attachments/assets/a34107a7-28d6-4b4a-8099-fd384e33a274)   


If payment is successful you will be redirected the Payment Complete Page.   

![PaymentComplete](https://github.com/user-attachments/assets/ec122909-af6f-47aa-9075-849a87394a36)

Click on Products on the header to be redirected to the Products Page.   

The Product Page is where you can add, delete, edit, and save products.   

![Manage Product](https://github.com/user-attachments/assets/dba8950a-ca06-4d78-9a68-3eaab8a58fbb)   


To add an image you must first create a category.   

So go to the category page by clicking the category in the header.   

Once you have created a category, you can update and delete the category.    

![ProductCategory](https://github.com/user-attachments/assets/353bf280-1394-4027-9f99-314473c70e89)   


### Setting up Database
**The name of the Database must be OnlineMart**
![onmart](https://github.com/user-attachments/assets/d9749d2b-8491-43f5-a987-ab9adfae533d)   


Inside Visual Studio Delete the Migrations Folder.   

![migrations](https://github.com/user-attachments/assets/503dd691-daf6-4ab0-b000-5eea5ddefb4b)   


After Deleting the Migrations folder Open a connection to the OnlineMart Database.   

![connection](https://github.com/user-attachments/assets/3d60fca9-107f-4246-a5be-e792821e515d)

- Open Nuget Manager Console
- Type Add-Migration InitialCreate

![migrate](https://github.com/user-attachments/assets/ab0ee366-f83d-4dd8-b623-952cb320f8aa)   


- After Build succeeded 
- Type Update-Database

This will automatically create the database table for you based on the ApplicationDbContext class.    

![update](https://github.com/user-attachments/assets/379e1861-7549-4629-8561-e18eba2f68e6)


