
/////////////////////////////////////////////////////////////////////////
//
/// LINQ Grouping
///  - with groupby, linq return a list of dictionary items
///  - each item in the list is a dictionary with a key and a list of items
///  - the key is the value of the field you are grouping by
///  - the list of items is items that match the key
///  
/// - besides the list of items you can also add aggregate properties like count, et,c to the dictionary
///   --------------------------------------      
///            |  item1
///   - key1   |  item2
///            |  item3
///   --------------------------------------
///            |  item4
///   - key2   |  item5
///            |  item6
///   -------------------------------------- 
/// 
/// OR add aggregate properties like count, sum, etc
///   --------------------------------------      
///   - key1    |  item2
///   - count   |  item3
///   - sum     |  item4
///   --------------------------------------             
///
/// - resources
///   https://visualstudiomagazine.com/articles/2017/02/01/grouping-results-in-linq.aspx
/// 
/////////////////////////////////////////////////////////////////////////

using System.Linq;
using System.Collections.Generic;

/////////////////////////////////////////////////////////////////////////
#region setup data
var customers = new List<Customer>
{
    new Customer { CustomerId = 1,  Name = "Bob Lesman", City = "Chicago" },
    new Customer {  CustomerId = 2,Name = "Joe Stevens", City = "Chicago" },
    new Customer { CustomerId = 3, Name = "Merry Smith", City = "Chicago" },
    new Customer { CustomerId = 4, Name = "Sue Lin", City = "New York" },
    new Customer { CustomerId = 5, Name = "Jose Gonzalez", City = "New York" },
    new Customer { CustomerId = 6, Name = "Nathan Jones", City = "New York" },
    new Customer { CustomerId = 7, Name = "Jane Doe", City = "Seattle" },
    new Customer { CustomerId = 8, Name = "Sammy Adams", City = "Seattle" },
    new Customer { CustomerId = 9, Name = "Ed Wards", City = "Seattle" }
};

var orders = new List<Order>
{
    new Order { OrderId = 1, CustomerId = 1},
    new Order { OrderId = 2, CustomerId = 4 },
    new Order { OrderId = 3, CustomerId = 6 }
};

// create a list of line items
var lineItems = new List<LineItem>
{
    new LineItem { OrderId = 1, Item = "Onion", Category = "Vegetable" },
    new LineItem { OrderId = 1, Item = "Apple", Category = "Fruit" },
    new LineItem { OrderId = 1, Item = "Orange", Category = "Fruit" },
    new LineItem { OrderId = 1, Item = "Banana", Category = "Fruit" },

    new LineItem { OrderId = 2, Item = "Milk", Category = "Dairy" },
    new LineItem { OrderId = 2, Item = "Chicken", Category = "Meat" },
    new LineItem { OrderId = 2, Item = "Orange", Category = "Fruit" },

    new LineItem { OrderId = 3, Item = "Lettuce", Category = "Vegetable" },
    new LineItem { OrderId = 3, Item = "Papaya", Category = "Fruit" },
    new LineItem { OrderId = 3, Item = "Egg", Category = "Diary" },
    new LineItem { OrderId = 3, Item = "Beef", Category = "Meat" }
};

#endregion
/////////////////////////////////////////////////////////////////////////

// group orders by product category

var invoice1 = from d in lineItems

                   // 1. first perform necessary joins
               join o in orders on d.OrderId equals o.OrderId
               join c in customers on o.CustomerId equals c.CustomerId

               // 2. select the fields you want to be in the details
               select
                new
                {
                    o.OrderId,
                    ProductCategory = d.Category,
                    ItemName = d.Item,
                    CustomerName = c.Name
                } into details

               // 3. now perform the grouping of the details
               group details by details.ProductCategory
               into g

               // 4. now select the fields you want to be in the summary
               select g
               /*select new 
               { 
                Category = g.Key,
                Items = g.ToList(), 
                NumItems=g.Count() 
                }*/;


foreach (var g in invoice1)
{
    System.Console.WriteLine($"Group: {g.Key}, Count: {g.Count()}");
    foreach (var item in g)
    {
        System.Console.WriteLine($" {item.OrderId} - {item.ProductCategory} {item.ItemName} - {item.CustomerName}");
    }
}
/*
var cols = lineItems
    .GroupBy(x=>x.Category)
    .
*/
/////////////////////////////////////////////////////////////////////////
#region classes
internal class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
}

internal class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
}

class LineItem
{
    public int OrderId { get; set; }
    public string Item { get; set; }

    public string Category { get; set; }
}

#endregion classes
/////////////////////////////////////////////////////////////////////////

