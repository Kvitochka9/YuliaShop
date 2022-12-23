using AutoMapper;
using ShoeShop.Core;
using ShoeShop.Core.Dto;
using ShoeShop.Core.Models;
using ShoeShop.Core.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShoeShop.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/admin/manageShoes")]
    public class AdminController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShoeRepository _shoeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(
            IOrderRepository orderRepository,
            IShoeRepository shoeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _shoeRepository = shoeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        [HttpGet("allOrders")]
        public async Task<IActionResult> AllOrders()
        {
            ViewBag.ActionTitle = "All Orders";
            var orders = await _orderRepository.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpGet("")]
        public async Task<IActionResult> ManageShoes()
        {
            var shoes = await _shoeRepository.GetAllShoesNameId();
            return View(shoes);
        }

        [HttpGet("add")]
        public async Task<IActionResult> AddShoe()
        {
            var category = await _categoryRepository.GetCategories();
            return View(new ShoeCreateUpdateViewModel
            {
                Categories = category
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShoe(ShoeDto shoeDto)
        {
            if (!ModelState.IsValid)
            {
                var category = await _categoryRepository.GetCategories();
                return View(new ShoeCreateUpdateViewModel
                {
                    ShoeDto = shoeDto,
                    Categories = category
                });
            }
            var shoe = _mapper.Map<ShoeDto, Shoe>(shoeDto);
            await _shoeRepository.AddShoeAsync(shoe);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("ManageShoes");
        }

        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditShoe(int id)
        {
            var shoe = await _shoeRepository.GetShoeById(id);
            var shoeDto = _mapper.Map<Shoe, ShoeDto>(shoe);
            var category = await _categoryRepository.GetCategories();

            return View(new ShoeCreateUpdateViewModel
            {
                Categories = category,
                ShoeDto = shoeDto
            });
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditShoe(int id, [FromForm]ShoeDto shoeDto)
        {
            if (!ModelState.IsValid)
            {
                var category = await _categoryRepository.GetCategories();
                return View(new ShoeCreateUpdateViewModel
                {
                    Categories = category,
                    ShoeDto = shoeDto
                });
            }
            var shoe = _mapper.Map<ShoeDto, Shoe>(shoeDto);
            shoe.Id = id;
            _shoeRepository.UpdateShoe(shoe);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction("ManageShoes");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            _shoeRepository.Delete(id);
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}