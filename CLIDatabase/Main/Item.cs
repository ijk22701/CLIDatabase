internal class Item
{
    public string name { get; set; }
    private double cost; //how much the item costs
    private double price; //how much the item is sold for
    private int quantity; //number of that items
    private double hours; //hours to install
    private int desiredQuantity;
    public int id { get; set; }

    public Item()
    {

    }

    public Item(string newName, int newID, double newCost, double newPrice, double newHours, int newQuantity)
    {
        name = newName;
        id = newID;
        cost = newCost;
        price = newPrice;
        hours = newHours;
        quantity = newQuantity;
        desiredQuantity = 0;
    }

    public double Cost
    {
        get
        {
            return cost;
        }
        set
        {
            if (value > 0)
            {
                cost = value;
            }
            else
            {
                System.Console.WriteLine("Invalid cost, try again");
            }
        }
    }

    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            if (value > 0)
            {
                price = value;
            }
            else
            {
                System.Console.WriteLine("Invalid price, try again");
            }
        }
    }

    public double Hours
    {
        get
        {
            return hours;
        }
        set
        {
            if (value > 0)
            {
                hours = value;
            }
            else
            {
                System.Console.WriteLine("Must be greater than zero, please try again");
            }
        }
    }

    public int Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            if (value >= 0)
            {
                quantity = value;
            }
            else
            {
                System.Console.WriteLine("Invalid quantity, try again");
            }
        }
    }

    public int DesiredQuantity
    {
        get
        {
            return desiredQuantity;
        }
        set
        {
            if (value >= 0)
            {
                desiredQuantity = value;
            }
            else
            {
                System.Console.WriteLine("Invalid quantity, try again");
            }
        }
    }

    public override string ToString()
    {
        string filteredName = name;
        if (name.Length > 30)
        {
            filteredName = name.Substring(0, 27) + "...";
        }
        //this is insanely stupid but I can't get it to work the "right way"
        string s = string.Format("Name: {0, -30} ID:", filteredName);
        s += string.Format("{0, -6} Quantity:", id);
        s += string.Format("{0, -8} Cost: ", quantity);
        s += string.Format("{0, -8} Price: ", cost);
        s += string.Format("{0, -6} Hours: ", price);
        s += hours;
        return s;
    }

    public string ToStringForSearch()
    {
        string s = string.Format("ID: {0, -6}", id);
        return s += $"Name: {name}";
    }

    public virtual string ToStringForOutputFile()
    {
        string s = "";
        s += $"$^%\n";
        s += $"{name}\n";
        s += $"{id}\n";
        s += $"{cost}\n";
        s += $"{price}\n";
        s += $"{hours}\n";
        s += $"{quantity}";

        return s;
    }

    public string ToStringForJPC()
    {
        string filteredName = name;
        if (name.Length > 25)
        {
            filteredName = name.Substring(0, 22) + "...";
        }
        //this is insanely stupid but I can't get it to work the "right way"
        string s = string.Format("Name: {0, -25} ID:", filteredName);
        s += string.Format("{0, -4} Items used:", id);
        s += string.Format("{0, -4} Combined Cost: ", desiredQuantity);
        s += string.Format("{0, -8} Combined Price: ", cost * desiredQuantity);
        s += string.Format("{0, -6} Combined Hours: ", price * desiredQuantity);
        s += hours * desiredQuantity;
        return s;
    }
}