using Microsoft.AspNetCore.Mvc;
using JSONServices.Models;

public class HomeController : Controller
{
    // Пример статической базы данных
    private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 100.49 },
        new Product { Id = 2, Name = "Product 2", Price = 130.49 }
    };
    private static List<Order> orders = new List<Order>();
    /// <summary>
    /// GET запрос, возвращает список товаров
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/GetProducts")]
    public IActionResult GetProducts()
    {
        return Json(products);
    }

    /// <summary>
    /// GET запрос, возвращает список заказов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/GetOrder")]
    public IActionResult GetOrder()
    {
        return Json(orders);
    }


    /// <summary>
    /// POST запрос с указанием значений в body
    /// </summary>
    /// <param name = "product"></param>
    /// пример:
    /// {
    ///     "Name": "2131",
    ///     "Price": 1
    /// }
    /// <returns></returns>
    [HttpPost]
    [Route("api/Add")]
    public IActionResult AddProduct([FromBody] Product product)
    {
        if (product != null)
        {
            product.Id = products.Count + 1;

            while (products.Any(p => p.Id == product.Id))
            {
                product.Id++;
            }

            products.Add(product);
            return Json(products);
        }
        return BadRequest("Invalid product data");
    }

    /// <summary>
    /// POST запрос с указанием параметра
    /// </summary>
    /// <param name="productId"></param>
    /// пример: api/delete?productId=1
    /// <returns></returns>
    [HttpPost]
    [Route("api/Delete")]
    public IActionResult DeleteProduct(int productId)
    {
        var productToRemove = products.Find(p => p.Id == productId);
        if (productToRemove != null)
        {
            products.Remove(productToRemove);
            return Json(products);
        }
        return NotFound();
    }

    /// <summary>
    /// POST запрос с указанием значений в body
    /// </summary>
    /// <param name = "order"></param>
    /// пример:
    /// {
    ///     "ProductIds": [1, 2]
    /// }
    ///
    /// <returns></returns>
    [HttpPost]
    [Route("api/CreateOrder")]
    public IActionResult CreateOrder([FromBody] OrderRequest orderRequest)
    {
        if (orderRequest != null && orderRequest.ProductIds != null && orderRequest.ProductIds.Any())
        {
            var order = new Order
            {
                Id = orders.Count + 1,
                ProductIds = orderRequest.ProductIds,
                Date = DateTime.Now 
            };
            while (orders.Any(s => s.Id == order.Id))
            {
                order.Id++;
            }
            orders.Add(order);
            return Json(orders);
        }
        return BadRequest("Invalid order data");
    }




    /// <summary>
    /// POST запрос для удаления заказа
    /// </summary>
    /// <param name="orderId"></param>
    /// пример: api/DeleteOrder?orderId=4
    /// <returns></returns>
    [HttpPost]
    [Route("api/DeleteOrder")]
    public IActionResult DeleteOrder(int orderId)
    {
        var orderToRemove = orders.Find(o => o.Id == orderId);
        if (orderToRemove != null)
        {
            orders.Remove(orderToRemove);
            return Json(orders);
        }
        return NotFound();
    }
}


