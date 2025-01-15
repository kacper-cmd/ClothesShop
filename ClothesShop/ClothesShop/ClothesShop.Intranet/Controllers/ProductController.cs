using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using Rotativa.AspNetCore;
using ClothesShop.Intranet.Services.Interfaces;

namespace ClothesShop.Intranet.Controllers
{
    public class ProductController : Controller
    {
        private readonly ClothesShopContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        readonly IBufferedFileUploadService _bufferedFileUploadService;

     
        public ProductController(ClothesShopContext context, IWebHostEnvironment hostEnvironment, IBufferedFileUploadService bufferedFileUploadService)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _bufferedFileUploadService = bufferedFileUploadService;
        }

        // GET: Product
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber,int? categoryFilter)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParam"] = String.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";
            ViewData["PriceSortParam"] = sortOrder == "price" ? "priceDesc" : "price";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;//for paging
            var products = from s in _context.Product
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "nameDesc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "priceDesc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            var categories = await _context.Category.Where(c => c.IsActive).ToListAsync();
            ViewBag.Categories = categories;

            if (categoryFilter.HasValue && categoryFilter.Value != 0)
            {
                products = products.Where(s => s.CategoryId == categoryFilter.Value);
            }
            ViewData["CategoryFilterDropDown"] = categoryFilter;

            var clothesShopContext = products.Include(p => p.Category);//as not tracking faster is read operation
            //return View(await clothesShopContext.AsNoTracking().ToListAsync());
            int pageSize = 3;
            return View(await PaginatedList<Product>.CreateAsync(clothesShopContext.AsNoTracking(),pageNumber ?? 1, pageSize));
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Price,ImageNameUrl,Description,IsActive,Color,CategoryId")] Product product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extension = Path.GetExtension(file.FileName).ToLower();

                    // Check if the extension is valid
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                    {
                        product.ImageNameUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Only JPG and PNG files are allowed.");
                        ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
                        // Provide any additional error handling or messages as needed
                        // You can return the view with the error message or take a different action
                        return View(product);
                    }
                }
                else
                {
                    product.ImageNameUrl = "noimage.png";
                }



                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price,ImageNameUrl,Description,IsActive,Color,CategoryId")] Product product)
        //{
        //    if (id != product.ProductId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(product);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ProductExists(product.ProductId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
        //    return View(product);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Price,ImageNameUrl,Description,IsActive,Color,CategoryId")] Product product, IFormFile? file)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extension = Path.GetExtension(file.FileName).ToLower();

                        // Check if the extension is valid
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            product.ImageNameUrl = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            string path = Path.Combine(wwwRootPath + "/images/", fileName);
                            using (var fileStream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Only JPG and PNG files are allowed.");
                            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
                            // Provide any additional error handling or messages as needed
                            // You can return the view with the error message or take a different action
                            return View(product);
                        }
                    }
                    else
                    {
                        product.ImageNameUrl = "noimage.png";
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ClothesShopContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            if (product == null)
            {
                return NotFound();
            }
            // Delete the associated photo file if it exists
            if (!string.IsNullOrEmpty(product.ImageNameUrl))
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string imagePath = Path.Combine(wwwRootPath, "images", product.ImageNameUrl);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> ListOfProducts()
        {

                var products =   _context.Product
                .Include(p=>p.Category)
                .Where(a => a.IsActive == true)
                .OrderByDescending(p => p.Price)
                .ToList();

                var pdf = new ViewAsPdf(products);
                var fileContent = await pdf.BuildFile(ControllerContext);

                // Create the PDFs directory if it doesn't exist
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "PDFs");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                // Save the PDF file to the directory
                var filePath = Path.Combine(directoryPath, "AllProducts.pdf");
                System.IO.File.WriteAllBytes(filePath, fileContent);
                // Return a file download response
                return File(fileContent, "application/pdf", "AllProducts.pdf");


            // return View(products);
            //return new ViewAsPdf("ListOfProducts",products)
            //{
            //    FileName = "Customers.pdf",
            //    PageMargins = { Left = 0, Right = 0 }, // In millimeters.
            //    PageWidth = 210, // In millimeters.
            //    PageHeight = 297
            //};

        }
        public IActionResult Export()
        {
            return new ViewAsPdf("Export")
            {
                FileName = "Customers.pdf",
                PageMargins = { Left = 0, Right = 0 }, // In millimeters.
                PageWidth = 80, // In millimeters.
                PageHeight = 200
            };
        }
    }
}
