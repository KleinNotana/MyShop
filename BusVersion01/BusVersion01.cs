using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Contract;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using Entity;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BusVersion01
{
    public class BusVersion01 : IBus
    {
        IDataAccess _data;
        public string Name => "BusVersion01";

        public string Description => "BusVersion01 - test";

        public void DeleteOrder(int DelId)
        {
            var orders = _data.GetOrder();
            var order = orders.Where(o => o.Id == DelId).FirstOrDefault();
            _data.DeleteOrder(order);

        }

        public void addOrder(Order1 addOrder)
        {
            _data.addOrder(addOrder);
        }

        public void DepensOn(IDataAccess data)
        {
            _data = data;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = _data.GetCustomer();
            return customers;
        }

        public List<dynamic> GetOrderByFilter(string dateFrom= "1/1/1900", string dateTo = "1/1/2100", int currentPage = 1, int itemPerPage = 10)
        {
            //throw new NotImplementedException();
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder();
            var list = from o in orders join c in _data.GetCustomer() on o.CustomerId equals c.Id where o.OrderDate >= datefrom && o.OrderDate <= dateto select new { Id = o.Id, Name = c.CustomerName, Date = o.OrderDate };
            int count = list.Count();
            var result = from o in list
                         select new { Id = o.Id, Name = o.Name, Date = o.Date.Value.Day.ToString()+"/"+ o.Date.Value.Month.ToString()+"/"+o.Date.Value.Year.ToString(), Total = count };
            result = result.Skip((currentPage - 1) * itemPerPage).Take(itemPerPage);
            return result.ToList<dynamic>();

                       
        }

        public List<Order1> GetOrders()
        {
            List<Order1> orders = _data.GetOrder();
            return orders;
        }

        public List<OrderDetail> GetOrdersDetailById(int orderid)
        {
            //throw new NotImplementedException();
            List<OrderDetail> orderDetails = _data.GetOrderDetail();
            var result = orderDetails.Where(o => o.OrderId == orderid).ToList();
            return result;
        }

        public List<Product> GetProducts()
        {
            return new List<Product>(_data.GetProducts());
        }

        public BindingList<dynamic> GetProductsByFilter(string name, string sortType, int priceFrom = -1, 
            int priceTo = -1, int currentPage = 1,int itemPerPage = 5, int categoryID = -1)
        {
           var products = _data.GetProducts();
            var detailOrders = _data.GetOrderDetail();
            // get list product and join with orderdetail group by productID ground by product.ID, Sold = sum orderdetail.amount
            var list = from p in products
                       join od in detailOrders on p.Id equals od.ProductId into g
                       from od in g.DefaultIfEmpty()
                       group od by new { p.Id, p.ProductName, p.Price, p.Amount, p.ImgPath, p.CategoryId, p.Description } into g
                       select new { Id = g.Key.Id, Name = g.Key.ProductName, Price = g.Key.Price, Stock = g.Key.Amount, ImgPath = g.Key.ImgPath,
                           Sold = g.Sum(od => od != null ? od.Amount : 0), CategoryId = g.Key.CategoryId, Description = g.Key.Description };

            if (name != "")
            {
                list = list.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (priceFrom != -1 && priceTo != -1)
            {
                list = list.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
            }

            if(categoryID != -1)
            {
                list = list.Where(p => p.CategoryId == categoryID);
            }

            if (sortType == "Price")
            {
                list = list.OrderBy(p => p.Price);
            }
            else if (sortType == "Name")
            {
                list = list.OrderBy(p => p.Name);
            }

            int count = list.Count();

            var result = from p in list
                     select new { Id = p.Id, ImgPath = p.ImgPath, Name = p.Name, Price = p.Price, Stock = p.Stock, Sold = p.Sold, Total = count };
            result = result.Skip((currentPage - 1) * itemPerPage).Take(itemPerPage);

            return new BindingList<dynamic>(result.ToList<dynamic>());
            
        }

        public async Task<bool> Login(string username, string password)
        {
            bool result = await _data.LoginAsync(username, password);
            return result;
        }

        public void saveChanges()
        {
            _data.saveChanges();
        }

        public void addOrderDetail(OrderDetail addOrderDetail)
        {
            _data.addOrderDetail(addOrderDetail);
            //update stock
            var product = _data.getProductById(addOrderDetail.ProductId);
            product.Amount -= addOrderDetail.Amount;
            _data.saveChanges();
        }


        public bool addProduct(Product addProduct)
        {
            bool result = true;

            _data.addProduct(addProduct);

            return result;
        }

        public BindingList<Category> GetCategories()
        {
            return new BindingList<Category>(_data.GetCategory());
            
           
        }

        public bool addCategory(Category category)
        {
            bool result = true;
            var categories = _data.GetCategory();
            var check = categories.Where(c => c.Name == category.Name).FirstOrDefault();

            if (category.Name != "" && check == null)
            {
                _data.addCategory(category);
            }
            else 
            {
                result = false;
            }

            return result;
        }

        public Product getProductById(int id)
        {
            return _data.getProductById(id);
        }

        public void updateProduct(Product updateProduct)
        {
            _data.updateProduct(updateProduct);
        }

        public Customer getCustomerByName(string name)
        {
            var customers = _data.GetCustomer();
            var result = customers.Where(c => c.CustomerName.ToLower().Contains(name.ToLower())).FirstOrDefault();
            return result;
        }

        public void addCustomer(Customer addCustomer)
        {
            _data.addCustomer(addCustomer);
        }

        public Order1 getOrderById(int id)
        {
            return _data.getOrderById(id);
        }

        public void deleteOrderDetail(OrderDetail deleteOrderDetail)
        {
            _data.deleteOrderDetail(deleteOrderDetail);

        }
    
        public void deleteProduct(int id)
        {
            _data.deleteProduct(id);
        }

        public Category getCategoryById(int id)
        {
            return _data.getCategoryById(id);
        }

        public void updateCategory(Category updateCategory)
        {
            _data.updateCategory(updateCategory);
        }

        public void deleteCategory(int id)
        {
            _data.deleteCategory(id);
        }

        public dynamic getDetailProduct(int id)
        {
            var prduct = _data.getProductById(id);

           if(prduct != null)
            {
                var soldProduct = _data.GetOrderDetail().Where(o => o.ProductId == id).Sum(o => o.Amount);
                Category category = _data.getCategoryById(prduct.CategoryId ?? 0);

                var result = new
                {
                    ProductName = prduct.ProductName,
                    Price = prduct.Price,
                    Description = prduct.Description,
                    ImgPath = prduct.ImgPath,
                    Amount = prduct.Amount,
                    CategoryName = category.Name,
                    Sold = soldProduct
                };

                return result;
            }

            return null;
        }

        public double getTotalPrice(int OrderId)
        {
            var orderDetails = _data.GetOrderDetail();
            var result = orderDetails.Where(o => o.OrderId == OrderId).Sum(o => o.Amount * o.Price);
            return (double)result;
        }

        public List<dynamic> GetMonthlyReport(string dateFrom, string dateTo , int mode)
        {
            var orderdetails = _data.GetOrderDetail();
            
            //get order fileter by date
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder().Where(o => o.OrderDate >= datefrom && o.OrderDate <= dateto);
            //get all order detail group by month
            if (mode == 0)
            {
                var list = from o in orders
                       join od in orderdetails on o.Id equals od.OrderId
                       group od by new { o.OrderDate.Value.Month, o.OrderDate.Value.Year } into g
                       select new { Time = g.Key.Month.ToString() + "/" + g.Key.Year.ToString(), Month = g.Key.Month, Year = g.Key.Year, Total = g.Sum(od => od.Amount * od.Price) };
                return list.ToList<dynamic>();
            }
            else
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           join p in _data.GetProducts() on od.ProductId equals p.Id
                           group od by new { o.OrderDate.Value.Month, o.OrderDate.Value.Year, p.ProductName } into g
                           select new { Name = g.Key.ProductName, Time = g.Key.Month.ToString() + "/" + g.Key.Year.ToString(), Month = g.Key.Month, Year = g.Key.Year, Total = g.Sum(od => od.Amount ) };
                return list.ToList<dynamic>();
            }
        }

        public List<dynamic> GetDailyReport(string dateFrom, string dateTo, int mode)
        {
            var orderdetails = _data.GetOrderDetail();

            //get order fileter by date
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder().Where(o => o.OrderDate >= datefrom && o.OrderDate <= dateto);
            //get all order detail group by day
            if(mode==0)
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           group od by new { o.OrderDate.Value.Day, o.OrderDate.Value.Month, o.OrderDate.Value.Year } into g
                           select new { Time = g.Key.Day.ToString() + "/" + g.Key.Month.ToString() + "/" + g.Key.Year.ToString(), Day = g.Key.Day, Month = g.Key.Month, Year = g.Key.Year, Total = g.Sum(od => od.Amount * od.Price) };
                return list.ToList<dynamic>();
            }
            else
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           join p in _data.GetProducts() on od.ProductId equals p.Id
                           group od by new { o.OrderDate.Value.Day, o.OrderDate.Value.Month, o.OrderDate.Value.Year, p.ProductName } into g
                           select new {Name=g.Key.ProductName, Time = g.Key.Day.ToString() + "/" + g.Key.Month.ToString() + "/" + g.Key.Year.ToString(), Day = g.Key.Day, Month = g.Key.Month, Year = g.Key.Year, Total = g.Sum(od => od.Amount ) };
                return list.ToList<dynamic>();
            }

        }
        public List<dynamic> GetYearlyReport(string dateFrom, string dateTo, int mode)
        {
            var orderdetails = _data.GetOrderDetail();

            //get order fileter by date
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder().Where(o => o.OrderDate >= datefrom && o.OrderDate <= dateto);
            //get all order detail group by year
            if(mode==0)
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           group od by new { o.OrderDate.Value.Year } into g
                           select new { Time = g.Key.Year.ToString(), Year = g.Key.Year, Total = g.Sum(od => od.Amount * od.Price) };
                return list.ToList<dynamic>();
            }
            else
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           join p in _data.GetProducts() on od.ProductId equals p.Id
                           group od by new { o.OrderDate.Value.Year, p.ProductName } into g
                           select new { Name = g.Key.ProductName, Time = g.Key.Year.ToString(), Year = g.Key.Year, Total = g.Sum(od => od.Amount ) };
                return list.ToList<dynamic>();
            }
        }
        public List<dynamic> GetWeeklyReport(string dateFrom, string dateTo, int mode)
        {
            var orderdetails = _data.GetOrderDetail();

            //get order fileter by date
            DateTime datefrom = DateTime.Parse(dateFrom);
            DateTime dateto = DateTime.Parse(dateTo);
            var orders = _data.GetOrder().Where(o => o.OrderDate >= datefrom && o.OrderDate <= dateto);
            //get min date
            var minDate = orders.Min(o => o.OrderDate);
            //get all order detail group by week
            if(mode==0)
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           join p in _data.GetProducts() on od.ProductId equals p.Id
                           group od by new { week = getWeek(minDate ?? DateTime.Now, o.OrderDate ?? DateTime.Now), o.OrderDate.Value.Year } into g
                           select new {  Time = "Week " + g.Key.week.ToString(), Week = g.Key.week.ToString(), Year = g.Key.Year, Total = g.Sum(od => od.Amount * od.Price) };
                return list.ToList<dynamic>();
            }
            else
            {
                var list = from o in orders
                           join od in orderdetails on o.Id equals od.OrderId
                           join p in _data.GetProducts() on od.ProductId equals p.Id
                           group od by new { week = getWeek(minDate ?? DateTime.Now, o.OrderDate ?? DateTime.Now), o.OrderDate.Value.Year, p.ProductName } into g
                           select new { Name = g.Key.ProductName, Time = "Week " + g.Key.week.ToString(), Week = g.Key.week.ToString(), Year = g.Key.Year, Total = g.Sum(od => od.Amount ) };
                return list.ToList<dynamic>();
                
            }
        }
        public int getWeek(DateTime startDay, DateTime endDay)
        {
               int week = 1;
            for (DateTime date = startDay; date <= endDay; date = date.AddDays(1))
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    week++;
                }
            }
            return week;
        }

        public BindingList<dynamic> getOutOfStockProducts()
        {
            var products = _data.GetProducts();
            var category = _data.GetCategory();
            var result = from p in products
                         join c in category on p.CategoryId equals c.Id
                         where p.Amount <= 5
                         select new { Id = p.Id, Name = p.ProductName, Category = c.Name, Price = p.Price, Stock = p.Amount };
            return new BindingList<dynamic>(result.ToList<dynamic>());
        }

        public dynamic getTotalSales()
        {
            var orderDetails = _data.GetOrderDetail().Sum(o => o.Amount * o.Price);
            var orders = _data.GetOrder();

            var sales = from o in orders
                        join d in _data.GetOrderDetail() on o.Id equals d.OrderId
                        select new { Id = o.Id, Date = o.OrderDate, Total = d.Amount * d.Price };

            var currentMonthSales = sales.Where(s => s.Date.Value.Month == DateTime.Today.Month).Sum(s => s.Total);
            var lastMonthSales = sales.Where(s => s.Date.Value.Month == DateTime.Today.Month - 1 ).Sum(s => s.Total);

            var Comment = "";

            if (lastMonthSales != 0)
            {
                var percent = (double) currentMonthSales / lastMonthSales * 100;

                //round percent to 2 decimal places
                percent = Math.Round((double)percent, 2);

                if (percent > 100)
                {
                    Comment = $"Increase by {percent - 100}% compared to last month. ";
                }
                else if (percent < 100)
                {
                    Comment = $"Decrease by {100 - percent}% compared to last month. "; ;
                }
                else
                {
                    Comment = "Equal to last month";
                }
            }

            var result = new
            {
                TotalSales = $"${currentMonthSales}",
                Comment = Comment
            };

            return result;
        }

        public dynamic getSellingProductAmount()
        {
            var products = _data.GetProducts();

            var totalSellingProducts = products.Sum(p => p.Amount);

            dynamic result = new
            {
                TotalSellingProducts = totalSellingProducts
            };

            return result;
        }

        public dynamic getSoldProductAmount()
        {
            var orderDetails = _data.GetOrderDetail();

            //count this month sold product
            var soldProducts = from od in orderDetails
                               join o in _data.GetOrder() on od.OrderId equals o.Id
                               where o.OrderDate.Value.Month == DateTime.Today.Month && o.OrderDate.Value.Year == DateTime.Today.Year
                               select new { Id = od.ProductId, Amount = od.Amount };

            var totalSoldProducts = soldProducts.Sum(s => s.Amount);

            var lastMonthSoldProducts = from od in orderDetails
                                        join o in _data.GetOrder() on od.OrderId equals o.Id
                                        where o.OrderDate.Value.Month == DateTime.Today.AddMonths(-1).Month && o.OrderDate.Value.Year == DateTime.Today.AddMonths(-1).Year
                                        select new { Id = od.ProductId, Amount = od.Amount };

            var lastMonthTotalSoldProducts = lastMonthSoldProducts.Sum(s => s.Amount);

            string Comment = "";

            if (lastMonthTotalSoldProducts != 0)
            {
                var percent = (double) totalSoldProducts / lastMonthTotalSoldProducts * 100;

                //round percent to 2 decimal places
                percent = Math.Round((double)percent, 2);

                if (percent > 100)
                {
                    Comment = $"Increase by {percent - 100}% compared to last month. ";
                }
                else if (percent < 100)
                {
                    Comment = $"Decrease by {100 - percent}% compared to last month. "; ;
                }
                else
                {
                    Comment = "Equal to last month";
                }
            }
            

            dynamic result = new
            {
                TotalSoldProducts = totalSoldProducts,
                Comment = Comment
            };

            return result;
        }

        public dynamic getTotalCustomers()
        {
            var customers = _data.GetCustomer();

            var totalCustomers = customers.Count();

            dynamic result = new
            {
                TotalCustomers = totalCustomers
            };

            return result;
        }

        public Customer getCustomerByPhone(string Phone)
        {
            var customers = _data.GetCustomer();
            var result = customers.Where(c => c.PhoneNumber == Phone).FirstOrDefault();
            return result;
        }

        public List<dynamic> getTopSaleProducts()
        {
            var orderDetails = _data.GetOrderDetail();
            var products = _data.GetProducts();

            var saleOfProducts = from od in orderDetails
                                  join p in products on od.ProductId equals p.Id
                                  group od by new { p.Id, p.ProductName } into g
                                  select new { Name = g.Key.ProductName, Sold = g.Sum(od => od.Amount) };

            var totalSaleProducts = saleOfProducts.Sum(t => t.Sold);

            var topSaleProducts = saleOfProducts.OrderByDescending(p => p.Sold).Take(5);

            var result = topSaleProducts.ToList<dynamic>();

            result.Add(new { Name = "Other", Sold = totalSaleProducts - topSaleProducts.Sum(p => p.Sold) });

            return result;
        }

        public List<dynamic> getCurrentDailySales()
        {
            var orders = _data.GetOrder();
            var orderDetails = _data.GetOrderDetail();

            var dailySales = from o in orders
                             join od in orderDetails on o.Id equals od.OrderId
                             group od by new { o.OrderDate } into g
                             select new { Date = g.Key.OrderDate, Income = g.Sum(od => od.Amount * od.Price) };

            var latestDays = dailySales.OrderByDescending(d => d.Date).Take(7);

            var query = from d in latestDays
                        orderby d.Date
                        select new { Date = d.Date.Value.Day.ToString() + "/" + d.Date.Value.Month.ToString() + "/" + d.Date.Value.Year.ToString(), Income = d.Income };

            var result = query.ToList<dynamic>();

            return result;
        }

        public bool importData(string filePath)
        {
            SpreadsheetDocument document;
            try
            {
                document = SpreadsheetDocument.Open(filePath, false);
            }
            catch (Exception)
            {
                return false;
            }

            var wbPart = document.WorkbookPart!;
            var sheets = wbPart.Workbook.Descendants<Sheet>();

            var categorySheet = sheets.FirstOrDefault(s => s.Name == "Category");
            var wsPart = (WorksheetPart)wbPart.GetPartById(categorySheet.Id);
            var rows = wsPart.Worksheet.Descendants<Row>();
            //skip hearder
            rows = rows.Skip(1);

            foreach ( var row in rows)
            {
                var cells = row.Descendants<Cell>();
                var category = new Category();
                foreach (var cell in cells)
                {
                    var cellValue = cell.InnerText;
                    if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                    {
                        var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        if (stringTable != null)
                        {
                            cellValue = stringTable.SharedStringTable.ElementAt(int.Parse(cellValue)).InnerText;
                        }
                    }

                    if (cell.CellReference == "A" + row.RowIndex)
                    {
                        category.Name = cellValue;
                    }
                    
                }
                addCategory(category);
            }

            var productSheet = sheets.FirstOrDefault(s => s.Name == "Product");
            wsPart = (WorksheetPart)wbPart.GetPartById(productSheet.Id);
            rows = wsPart.Worksheet.Descendants<Row>();

            //skip hearder
            rows = rows.Skip(1);

            foreach ( var row in rows)
            {
                   var cells = row.Descendants<Cell>();
                var product = new Product();
                foreach (var cell in cells)
                {
                    var cellValue = cell.InnerText;
                    if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                    {
                        var stringTable = wbPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                        if (stringTable != null)
                        {
                            cellValue = stringTable.SharedStringTable.ElementAt(int.Parse(cellValue)).InnerText;
                        }
                    }

                    if (cell.CellReference == "A" + row.RowIndex)
                    {
                        product.ProductName = cellValue;
                    }
                    else if (cell.CellReference == "B" + row.RowIndex)
                    {
                        product.Price = int.Parse(cellValue);
                    }
                    else if (cell.CellReference == "C" + row.RowIndex)
                    {
                        product.Amount = int.Parse(cellValue);
                    }
                    else if (cell.CellReference == "D" + row.RowIndex)
                    {
                        product.ImgPath = cellValue;
                    }
                    else if (cell.CellReference == "E" + row.RowIndex)
                    {
                        product.Description = cellValue;
                    }
                    else if (cell.CellReference == "F" + row.RowIndex)
                    {
                        int categoryId = getCategoryID(cellValue);
                        product.CategoryId = categoryId;
                    }
                }
                addProduct(product);
            }

            return true;
            
        }

        public int getCategoryID(string name)
        {
            var category = _data.GetCategory().Where(c => c.Name == name).FirstOrDefault();
            if(category != null)
            {
                return category.Id;
            }
            else
            {
                return -1;
            }
        }
    }

}
