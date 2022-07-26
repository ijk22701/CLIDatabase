internal class DB_App
{
    private List<Item> items = new List<Item>();
    private const string DB_DATAFILE = "DB_DATAFILE.txt";
    private bool exit;

    /**
     * <summary>Runs the app </summary>
     */

    internal void RunApp()
    {
        ReadInputFile();
        while (!exit)
        {
            SortList();
            Menu();
        }
    }

    /**
     * <summary>Menu for the program</summary>
     */
    private void Menu()
    {
        bool valid = false;
        while (!valid)
        {
            Console.WriteLine();
            Console.WriteLine("Please make a selection");
            Console.WriteLine("1 - View database");
            Console.WriteLine("2 - Update item");
            Console.WriteLine("3 - Add item");
            Console.WriteLine("4 - Remove item");
            Console.WriteLine("5 - Enter Job Price Calculator");
            Console.WriteLine("0 - exit");

            Console.WriteLine();

            switch (CheckIfInt())
            {
                case 1: //view database
                    PrintDatabase();
                    valid = true;
                    break;
                case 2: //update item
                    UpdateItem();
                    valid = true;
                    break;
                case 3: //add item
                    AddItem();
                    valid = true;
                    break;
                case 4: //remove item
                    DeleteItem();
                    valid = true;
                    break;
                case 5: //enter job price calculator
                    RunJobPriceCalculator();
                    valid = true;
                    break;
                case 0: //quit
                    Console.WriteLine("Would you like to save before quitting?");
                    Console.WriteLine("(1 for yes, 2 for no, 3 for cancel");
                    switch (CheckIfInt())
                    {
                        case 1:
                            WriteOutputFile();
                            exit = true;
                            valid = true;
                            break;
                        case 2:
                            exit = true;
                            valid = true;
                            break;
                        case 3:
                            exit = false;
                            valid = false; //these false statements aren't really required but I think it makes it more readable :)
                            break;
                        default:
                            Console.WriteLine("Invalid choice, returning to menu");
                            valid = false;
                            break;
                    }

                    break;
                default:
                    Console.WriteLine("Invalid response, please try again.");
                    break;
            }
        }
    }

    private void RunJobPriceCalculator()
    {
        JCP app = new JCP(items);
        app.RunApp();
    }

    /**
     * <summary>Add an item to the database</summary>
     */
    private void AddItem()
    {
        bool valid = false;
        string newName = "";
        int newID = 0;
        int newQuantity = 0;
        double hours = .01;
        double newPrice = 0;
        double newCost = 0;

        List<Item> itemsByID = items;
        itemsByID.Sort((a, b) => a.id.CompareTo(b.id));

        //This block below adds the name to the item

        bool unique = false;
        while (!unique)
        {
            Console.WriteLine("Enter a name for the item");
            newName = Console.ReadLine();
            unique = true;
            foreach (var item in items)
            {
                if (newName == item.name)
                {
                    Console.WriteLine($"Name is not unique, {item.name} already has ID {item.id}");
                    Console.WriteLine("Consider updating the existing item");
                }
            }
        }

        //this block below adds the ID to the item 

        foreach (var item in itemsByID)
        {
            Console.WriteLine(item.ToStringForSearch());
        }

        while (!valid)
        {

            Console.WriteLine("Enter an ID for the item (list of used IDs above, must be unique)");
            newID = CheckIfInt();

            valid = true; //if valid changes back to false through the tests, it will restart the loop
            foreach (var item in items)
            {
                if (newID == item.id)
                {
                    Console.WriteLine($"ID is not unique, {newID} matches \"{item.name}\"");
                    valid = false;
                }
            }
        }
        Console.WriteLine();

        //This block below adds the cost for the item

        Console.WriteLine("Enter the cost for the item:");
        valid = false;
        newCost = CheckIfDouble();


        //this block below adds the price for the item

        Console.WriteLine("Enter the price for the item:");
        newPrice = CheckIfDouble();

        //this block adds the hours it takes to install the item

        Console.WriteLine("Enter the approximate number of hours it takes to install the item");
        hours = CheckIfDouble();

        //this block adds the quantity of the item

        Console.WriteLine("Enter the quantity of the item");
        newQuantity = CheckIfInt();

        Item newItem = new Item(newName, newID, newCost, newPrice, hours, newQuantity);
        Console.WriteLine(newItem.ToString());
        items.Add(newItem);
    }

    private void DeleteItem()
    {
        Item current = FindItem();

        Console.WriteLine($"Are you sure you want to delete {current.name}?");
        Console.WriteLine("(1 for yes, 2 for no)");

        bool valid = false;
        int response = 0;
        while (!valid)
        {
            response = CheckIfInt();
            if (response != 1 && response != 2)
            {
                valid = false;
            }
            else
            {
                valid = true;
            }
        }
        switch (response)
        {
            case 1:
                items.Remove(current);
                break;
            case 2:
                break;
            default:
                Console.WriteLine("Invalid input, no action taken");
                break;
        }
    }

    /**
     * <summary>Find an item in the database by ID (prompts user)</summary>
     * <returns>the item the user requests</returns>
     */
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


    /**
     * <summary>Prints all items in the database</summary>
     */
    private void PrintDatabase()
    {
        foreach (Item item in items)
        {
            Console.WriteLine(item.ToString());
        }
    }

    /**
     * <summary>Prints the item and corresponding ID in the datatabase</summary>
     */
    private void PrintIDs()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item.ToStringForSearch());
        }
    }

    /**
     * <summary>Allows users to change the data of items in the database with the exception of ID</summary>
     * 
     */
    private void UpdateItem()
    {
        Item current = FindItem();

        Console.WriteLine();
        Console.WriteLine(current.ToString());
        Console.WriteLine();

        Console.WriteLine("What field(s) would you like to update?"); //ordered most likely to least likely to change
        Console.WriteLine("1 - Quantity");
        Console.WriteLine("2 - Cost");
        Console.WriteLine("3 - Price");
        Console.WriteLine("4 - Name");
        Console.WriteLine("5 - Hours");
        Console.WriteLine("6 - All");
        Console.WriteLine("7 - Go back");


        Console.WriteLine();
        bool valid = false; //If input is an option in the switch below
        while (!valid)
        {

            switch (CheckIfInt())
            {


                case 1: //quantity

                    Console.WriteLine("Enter the new quantity:");
                    current.Quantity = CheckIfInt();
                    valid = true;

                    break;
                case 2: //cost

                    Console.WriteLine("Enter the new cost: ");
                    current.Cost = CheckIfDouble();
                    valid = true;

                    break;
                case 3: //price

                    Console.WriteLine("Enter the new price: ");
                    current.Price = CheckIfDouble();
                    valid = true;

                    break;
                case 4: //name

                    Console.WriteLine("Enter the new name: ");
                    current.name = Console.ReadLine();
                    valid = true;

                    break;

                case 5: //name

                    Console.WriteLine("Enter the new amount of hours to install: ");
                    current.Hours = CheckIfDouble();
                    valid = true;

                    break;

                case 6: //all


                    Console.WriteLine("Enter the new quantity:");
                    current.Quantity = CheckIfInt();

                    Console.WriteLine("Enter the new cost: ");
                    current.Cost = CheckIfDouble();

                    Console.WriteLine("Enter the new price: ");
                    current.Price = CheckIfDouble(); ;

                    Console.WriteLine("Enter the new amount of hours to install: ");
                    current.Hours = CheckIfDouble();

                    Console.WriteLine("Would you like to change the name? (1 yes, 0 no)"); //name is annoying to change, so offering extra choice
                    int response = CheckIfInt();
                    bool valid2 = false;
                    while (!valid2)
                    {
                        switch (response)
                        {
                            case 1:
                                Console.WriteLine("Enter the new name: ");
                                current.name = Console.ReadLine();
                                valid2 = true;
                                break;
                            case 0:
                                Console.WriteLine("No changes made to name");
                                valid2 = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice, 1 for yes, 0 for no");
                                valid2 = false;
                                break;
                        }
                    }
                    valid = true;
                    break;

                case 7:
                    valid = true;
                    break;

                default:
                    Console.WriteLine("Invalid input, please try again");
                    valid = false;
                    break;
            }
        }
    }

    /**
     * <summary>Reads the input file of the program and stores the data into a list</summary>
     * 
     */
    internal void ReadInputFile()
    {
        /*
         * DATAFILE STRUCTURE GOES AS:
         * $^% (item signature, required but only used to identify item, not important otherwise)
         * NAME (of type string)
         * ID (unique number for that item)
         * COST (of type double)
         * PRICE (of type double)
         * HOURS (of type double)
         * QUANTITY (of type int)
         */

        StreamReader infile = new StreamReader(DB_DATAFILE);
        string itemSignature; //literally can be anything in the file, will do something like $^% to make unique

        while ((itemSignature = infile.ReadLine()) != null) //while itemsignature is not null
        {
            string name = infile.ReadLine();
            int id = int.Parse(infile.ReadLine());
            double cost = double.Parse(infile.ReadLine());
            double price = double.Parse(infile.ReadLine());
            double hours = double.Parse(infile.ReadLine());
            int quantity = int.Parse(infile.ReadLine());


            Item i = new Item(name, id, cost, price, hours, quantity);
            items.Add(i);
        }

        Console.WriteLine("Reading input file is complete...");
        infile.Close();

    }

    /**
     * <summary>Sorts the item list alphabetically by name</summary>
     */
    private void SortList()
    {
        items.Sort((a, b) => a.name.CompareTo(b.name));
    }

    /**
     * <summary>Writes the output file to the program</summary>
     */
    internal void WriteOutputFile()
    {
        StreamWriter outFile = new StreamWriter(DB_DATAFILE);

        foreach (var item in items)
        {
            outFile.WriteLine(item.ToStringForOutputFile());
        }
        outFile.Close();
    }

    /**
     * <summary>Checks if user submitted input is of type integer</summary>
     * <returns>An integer submitted by the user, -1 if invalid (for positive ints)</returns>
     */
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

    /**
     * <summary>Checks if user input is of type double</summary>
     * <returns>A double submitted by the user</returns>
     */
    private double CheckIfDouble()
    {
        bool valid = false;
        while (!valid)
        {
            string response = Console.ReadLine();
            bool conversion = double.TryParse(response, out double number);
            if (conversion == true && number > -1)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Input is not an double or double is positive, please try again");
            }
        }
        return -1;
    }
}
