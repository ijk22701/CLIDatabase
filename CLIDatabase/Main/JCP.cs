class JCP
{
    private List<Item> items = new List<Item>();
    bool validRemoval = false;

    public JCP(List<Item> newItems)
    {
        items = newItems;
    }

    internal void RunApp()
    {
        PrintIntro();
        PickItems();
    }



    private void PrintIntro()
    {
        Console.WriteLine();
        Console.WriteLine("Welcome to the job price calculator!\n" +
            "Pick items from the database and the quantity, the calculator\n" +
            "will give you the stats on that project (cost, price, margin, hours etc)\n");
        Console.WriteLine();
        Console.WriteLine("Here is the database:");
        foreach (Item item in items)
        {
            Console.WriteLine(item.ToString());

        }
        Console.WriteLine();
    }

    private void PickItems()
    {

        bool repeat = true;

        while (repeat)
        {
            Console.WriteLine("===========Menu===========");
            Console.WriteLine("(1) - Add To List");
            Console.WriteLine("(2) - Remove from the list");
            Console.WriteLine("(3) - View The List");
            Console.WriteLine("(4) - Calculate");

            int selection = CheckIfInt();
            switch (selection)
            {
                case 1:
                    AddToList();
                    break;
                case 2:
                    RemoveFromList();
                    break;
                case 3:
                    ViewList();
                    break;
                case 4:
                    CalculateJob();
                    Console.WriteLine("***\n***");
                    Console.WriteLine("Please note that the list does not save!");
                    Console.WriteLine("***\n***");
                    Console.WriteLine("Would you like to make changes to this list? (1 for yes, 2 for no)");
                    int response = CheckIfInt();
                    switch (response)
                    {
                        case 1:
                            repeat = true;
                            break;
                        case 2:
                            repeat = false;
                            break;
                        default:
                            break;

                    }
                    break;
                default:
                    break;
            }
        }
    }

    private void ViewList()
    {
        foreach (var item in items)
        {
            if (item.DesiredQuantity > 0)
            {
                Console.WriteLine(item.ToStringForJPC());
            }
        }
    }

    private void CalculateJob()
    {
        Console.WriteLine("The output is between the lines ");
        ViewList();

        double totalCost = 0;
        double totalPrice = 0;
        double totalHours = 0;


        foreach (var item in items)
        {
            totalCost += (item.Cost * item.DesiredQuantity);
            totalPrice += (item.Price * item.DesiredQuantity);
            totalHours += (item.Hours * item.DesiredQuantity);
        }

        Console.WriteLine("=======================");
        Console.WriteLine("Total cost: " + totalCost);
        Console.WriteLine("Total price: " + totalPrice);
        Console.WriteLine("Total Hours: " + totalHours);
        Console.WriteLine("=======================");

    }

    private void AddToList()
    {
        Item current = FindItem();
        Console.WriteLine($"How many of this item would you like to add? {current.DesiredQuantity} of this" +
            $" item currently");
        current.DesiredQuantity += CheckIfInt();
    }

    private void RemoveFromList()
    {
        foreach (var item in items)
        {
            if (item.DesiredQuantity > 0)
            {
                validRemoval = true;
            }
        }
        if (validRemoval == true)
        {
            Item current = FindItemForRemoval();
            bool valid = false;
            while (!valid)
            {
                Console.WriteLine($"How many of this item would you like to remove? {current.DesiredQuantity} of this" +
                     $" item currently");
                int response = CheckIfInt();
                if (response <= current.DesiredQuantity)
                {
                    valid = true;
                    current.DesiredQuantity -= response;
                }
                else
                {
                    Console.WriteLine("Cannot remove more items than currently exist!");
                }
            }
            validRemoval = false;
        }
        else
        {
            Console.WriteLine("The list does not have any items that can be removed");
        }

    }



    private int CheckIfInt()
    {
        bool valid = false;
        while (!valid)
        {
            string response = Console.ReadLine();
            bool conversion = int.TryParse(response, out int number);
            if (conversion == true && number > -1)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Input is not an integer or int is negative, please try again");
            }
        }

        return -1;
    }

    private Item FindItem()
    {
        int ID;
        PrintIDs();

        Console.WriteLine("Enter the ID of the item from the list above to select it");
        bool found = false;
        while (!found)
        {
            ID = CheckIfInt();
            foreach (var item in items)
            {
                if (item.id == ID)
                {
                    Console.WriteLine("ID found, item is: " + item.name);
                    return item;
                }
            }
            Console.WriteLine($"No item found with id {ID}, please try again");
        }
        return null;
    }

    private Item FindItemForRemoval()
    {
        int ID;
        PrintIDsForRemoval();

        Console.WriteLine("Enter the ID of the item from the list above to select it");
        bool found = false;
        while (!found)
        {
            ID = CheckIfInt();
            foreach (var item in items)
            {
                if (item.id == ID)
                {
                    Console.WriteLine("ID found, item is: " + item.name);
                    return item;
                }
            }
            Console.WriteLine($"No item found with id {ID}, please try again");
        }
        return null;
    }

    private void PrintIDsForRemoval()
    {
        foreach (var item in items)
        {
            if (item.DesiredQuantity > 0)
            {
                Console.WriteLine(item.ToStringForSearch());
            }
        }
    }

    private void PrintIDs()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item.ToStringForSearch());
        }
    }
}