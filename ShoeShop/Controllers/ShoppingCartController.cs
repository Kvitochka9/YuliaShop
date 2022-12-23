using ShoeShop.Core.Models;
using ShoeShop.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShoeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartController(IShoeRepository shoeRepository, IShoppingCartService shoppingCart)
        {
            _shoeRepository = shoeRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            await _shoppingCart.GetShoppingCartItemsAsync();
            var shoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount,
            };


            return View(shoppingCartViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int shoeId)
        {
            var selectedShoe = await _shoeRepository.GetShoeById(shoeId);
            if (selectedShoe == null)
            {
                return NotFound();
            }

            await _shoppingCart.AddToCartAsync(selectedShoe);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromShoppingCart(int shoeId)
        {
            var selectedShoe = await _shoeRepository.GetShoeById(shoeId);
            if (selectedShoe == null)
            {
                return NotFound();
            }

            await _shoppingCart.RemoveFromCartAsync(selectedShoe);

            return RedirectToAction("Index");
        }

        ///////SIZE////////
        [HttpPost]
        public async Task<IActionResult> IncreaseSize(int shoeId)
        {
            var selectedShoe = await _shoeRepository.GetShoeById(shoeId);
            if (selectedShoe == null)
            {
                return NotFound();
            }

            await _shoppingCart.BiggerSizeAsync(selectedShoe);

            return RedirectToAction("Index");
        }
        ///////SIZE////////
        [HttpPost]
        public async Task<IActionResult> DecreaseSize(int shoeId)
        {
            var selectedShoe = await _shoeRepository.GetShoeById(shoeId);
            if (selectedShoe == null)
            {
                return NotFound();
            }

            await _shoppingCart.SmallerSizeAsync(selectedShoe);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllCart()
        {
            await _shoppingCart.ClearCartAsync();
            return RedirectToAction("Index");
        }
    }
}