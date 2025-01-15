using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using ClothesShop.PortalWWW.Helpers;
using ClothesShop.PortalWWW.Services.Common;
using ClothesShop.PortalWWW.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.PortalWWW.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ClothesShopContext _context;
        private readonly CommonDataService _commonDataService;


        //public ShopController(ILogger<HomeController> logger, ClothesShopContext context)
        //{
        //    _logger = logger;
        //    _context = context;
        //}
        public ShopController(ILogger<HomeController> logger, ClothesShopContext context,
            CommonDataService commonDataService)
        {
            _logger = logger;
            _context = context;
            _commonDataService = commonDataService;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
        //    var item = await _context.Product.Where(p => p.IsActive == true).ToListAsync();
        //    return View(item);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();

        //    int pageSize = 3;
        //    int totalProducts = await _context.Product.CountAsync(p => p.IsActive == true);
        //    int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

        //    var products = await _context.Product
        //        .Where(p => p.IsActive == true)
        //        .Skip((page - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToListAsync();

        //    var viewModel = new ProductsViewModel
        //    {
        //        Products = products,
        //        CurrentPage = page,
        //        TotalPages = totalPages,
        //        TotalProducts = totalProducts
        //    };

        //    return View(viewModel);
        //}
        [HttpGet]
        [AuthenticationFilter]
        public async Task<IActionResult> Index(int page = 1, string sortBy = "")
        {
            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();

            int pageSize = 3;
            var query = _context.Product.Where(p => p.IsActive == true);

            // Apply sorting if specified
            switch (sortBy)
            {
                case "lowToHigh":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "range1":
                    query = query.Where(p => p.Price >= 0 && p.Price <= 55);
                    break;
                case "range2":
                    query = query.Where(p => p.Price > 55 && p.Price <= 100);
                    break;
                // Add more sorting options if needed
                default:
                    query = query.OrderBy(p => p.ProductId); // Default sorting by product ID
                    break;
            }
            ViewBag.SortBy = sortBy;

            int totalProducts = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var viewModel = new ProductsViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalProducts = totalProducts // Assign the total number of products to the TotalProducts property
            };

            ViewBag.SortBy = sortBy; // Pass the selected sorting option to the view
            ViewBag.Subpages = new List<string> { "Shop" };

            return View(viewModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(FilterViewModel filter, string? priceRange, string? searchTerm )
        //{
        //    if (filter == null)
        //    {
        //        filter = new FilterViewModel();
        //    }
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
        //    // Populate the filter categories from the Category table
        //    List<Category> categories = _context.Category.ToList();
        //    filter.Categories = new SelectList(categories, "Id", "Name");

        //    // Populate the filter price ranges based on the minimum and maximum prices in the Product table
        //    decimal minPrice = _context.Product.Min(p => p.Price);
        //    decimal maxPrice = _context.Product.Max(p => p.Price);
        //    filter.MinPrice = minPrice;
        //    filter.MaxPrice = maxPrice;

        //    var query = _context.Product.AsQueryable();

        //    if (filter.CategoryId.HasValue)
        //    {
        //        query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
        //    }

        //    if (!string.IsNullOrEmpty(filter.Color))
        //    {
        //        query = query.Where(p => p.Color == filter.Color);
        //    }

        //    if (filter.MinPrice.HasValue)
        //    {
        //        query = query.Where(p => p.Price >= filter.MinPrice.Value);
        //    }

        //    if (filter.MaxPrice.HasValue)
        //    {
        //        query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        //    }
        //    // Apply price range filter if selected
        //    if (!string.IsNullOrEmpty(priceRange))
        //    {
        //        string[] priceRangeValues = priceRange.Split('-');
        //        if (priceRangeValues.Length == 2)
        //        {
        //            string rangeStartString = priceRangeValues[0].Trim().Substring(1); // Remove '$' and trim leading/trailing spaces
        //            string rangeEndString = priceRangeValues[1].Trim().Substring(1); // Remove '$' and trim leading/trailing spaces

        //            if (decimal.TryParse(rangeStartString, out decimal rangeStart) && decimal.TryParse(rangeEndString, out decimal rangeEnd))
        //            {
        //                query = query.Where(p => p.Price >= rangeStart && p.Price <= rangeEnd);
        //            }
        //        }
        //    }


        //    // Apply the search term filter
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        query = query.Where(p => p.Name.ToLower().Contains(searchTerm));

        //    }
        //    var products = query.ToList();


        //    ViewBag.SearchTerm = searchTerm ?? ""; // Pass the search term to the view


        //    return View(products);
        //}

        [HttpPost]
        public async Task<IActionResult> Index(FilterViewModel filter, string? priceRange, string? searchTerm, int page = 1)
        {
            if (filter == null)
            {
                filter = new FilterViewModel();
            } 
            TempData["SelectedCategory"] = filter.CategoryId;

            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();

            // Populate the filter categories from the Category table
            List<Category> categories = _context.Category.ToList();
            filter.Categories = new SelectList(categories, "Id", "Name");

            // Populate the filter price ranges based on the minimum and maximum prices in the Product table
            decimal minPrice = _context.Product.Min(p => p.Price);
            decimal maxPrice = _context.Product.Max(p => p.Price);
            filter.MinPrice = minPrice;
            filter.MaxPrice = maxPrice;

            var query = _context.Product.AsQueryable();

            if (filter.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);
            }

            if (!string.IsNullOrEmpty(filter.Color))
            {
                query = query.Where(p => p.Color == filter.Color);
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            // Apply price range filter if selected
            if (!string.IsNullOrEmpty(priceRange))
            {
                string[] priceRangeValues = priceRange.Split('-');
                if (priceRangeValues.Length == 2)
                {
                    string rangeStartString = priceRangeValues[0].Trim().Substring(1); // Remove '$' and trim leading/trailing spaces
                    string rangeEndString = priceRangeValues[1].Trim().Substring(1); // Remove '$' and trim leading/trailing spaces

                    if (decimal.TryParse(rangeStartString, out decimal rangeStart) && decimal.TryParse(rangeEndString, out decimal rangeEnd))
                    {
                        query = query.Where(p => p.Price >= rangeStart && p.Price <= rangeEnd);
                    }
                }
            }

            // Apply the search term filter
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm));
            }

            // Pagination
            int pageSize = 10; // Number of products to display per page
            int totalProducts = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var products = query.ToList();

            ViewBag.SearchTerm = searchTerm ?? ""; // Pass the search term to the view

            var viewModel = new ProductsViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages
            };
            ViewBag.Subpages = new List<string> { "Shop","Result" };

            // Pass the filtered products and the filter model to the view or perform further operations
            return View(viewModel);
        }
        //[HttpGet]
        //public async Task<IActionResult> ProductDetail(int? id)
        //{
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
        //    var item = await _context.Product.FindAsync(id);
        //    return View(item);
        //}
        [HttpGet]
        public async Task<IActionResult> ProductDetail(int? id)
        {
            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
            //var item = await _context.Product.FindAsync(id);
            var item = await _context.Product.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProductId == id);

            if (item == null)
            {
                return NotFound(); 
            }
            var breadcrumbLinks = new List<BreadcrumbLink>
            {
                new BreadcrumbLink { Text = "Home", Url = Url.Action("Index", "Home") },
                new BreadcrumbLink { Text = "Shop", Url = Url.Action("Index", "Shop") },
                new BreadcrumbLink { Text = "Product Details", Url = null }
            };
            var relatedProducts = await _context.Product
                .Where(p => p.CategoryId == item.CategoryId && p.ProductId != item.ProductId)
                .Take(4) 
                .ToListAsync();

            ViewBag.BreadcrumbLinks = breadcrumbLinks;
            ViewBag.RelatedProducts = relatedProducts;

            return View(item);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string? searchTerm)
        {
            ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
            ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
            var products = await _context.Product
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();

            return View(products);
        }
        //public async Task<ActionResult> Search(string searchTerm)
        //{
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
        //    // Perform search logic based on the provided search term
        //    var searchResults = _context.Product.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower())).ToList();


        //    return View("Index", searchResults);
        //}


        //public async Task<IActionResult> ProductDetail(int? id)
        //{
        //    ViewBag.ModelBanner = await _commonDataService.GetBannersAsync();
        //    ViewBag.ModelFooter = await _commonDataService.GetFootersAsync();
        //    var item = await _context.Product.FindAsync(id);
        //    return View(item);
        //}

        //public ActionResult Index(FilterViewModel filter)
        //{
        //    var model = new FilterViewModel();

        //    // Retrieve category options from the database
        //    var categories = db.Categories.ToList();
        //    ViewBag.Categories = new SelectList(categories, "CategoryId", "CategoryName");

        //    // Retrieve color options from the database
        //    var colors = db.Products.Select(p => p.Color).Distinct().ToList();
        //    ViewBag.Colors = new SelectList(colors);

        //    // Apply filters to the model based on the user's selection
        //    model.CategoryId = filter.CategoryId;
        //    model.Color = filter.Color;
        //    model.MinPrice = filter.MinPrice;
        //    model.MaxPrice = filter.MaxPrice;

        //    // Perform further operations or retrieve filtered data

        //    return View(model);
        //}


    }
}
