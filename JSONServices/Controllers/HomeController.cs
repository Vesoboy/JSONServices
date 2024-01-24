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

    /// <summary>
    /// GET запрос, возвращает список товаров
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("api/Get")]
    public IActionResult GetProducts()
    {
        return Json(products);
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
}


