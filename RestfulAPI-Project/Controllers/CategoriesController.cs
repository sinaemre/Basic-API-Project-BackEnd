using ApplicationCore_API.DTO_s;
using ApplicationCore_API.Entities.Concrete;
using AutoMapper;
using Infrastructure_API.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestfulAPI_Project.Controllers
{
    //ProducesResponseTypes => Bir action methodu içerisinde birçok dönüş tipi ve yolu vardır. ProducesResponseTypes özniteliği kullanılarak farklı dönüş tiplerini Swagger gibi araçlar tarafından dökümantasyonların istemciler için daha açıklayıcı yanıtlar verbiliriz.
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    [Produces("application/json")]

    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        
        
        /// <summary>
        /// Kategori Listesini Yükle
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GetCategoryDTO>))]
        public IActionResult GetCategory()
        {
            var categories = _categoryRepo.GetCategories();
            List<GetCategoryDTO> model = new List<GetCategoryDTO>();

            foreach (var category in categories)
            {
                var entity = _mapper.Map<GetCategoryDTO>(category);
                model.Add(entity);
            }

            return Ok(model);
        }

        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">The Id of Category</param>
        /// <returns></returns>
        [HttpGet("{id?}", Name = "Get Category")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public IActionResult GetCategory(int id)
        {
            var category = _categoryRepo.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var model = _mapper.Map<GetCategoryDTO>(category);
                return Ok(model);
            }
        }

        /*
         * Action Methodlar
         * 
         * 1) FromBody => HTTP Request'inin içierisinde gönderilen parametreleri okumak için kullanılır.
         * 
         * 2) FromQuery => URL içerisinde gömülen parametreleri okumak için kullanılır.
         * 
         * 3) FromRoute => Enpoint URL'i içerisinde gönderilen parametreleri okumak için kullanılır. Yaygın olarak resource'a ait id bilgisini okurken kullanılır. 
         */

        /// <summary>
        /// Add The New Category
        /// </summary>
        /// <param name="model">In this process, Name and Description does required fields!</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] // Ekleme Başarılı
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Validasyonlara uyulmadı
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Bulunamadı
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // Server hatası
        public IActionResult CreateCategory([FromForm]CreateCategoryDTO model)
        {
            if (model == null)
            {
                return StatusCode(404, ModelState);
            }

            if (_categoryRepo.AnyCategory(model.Name))
            {
                ModelState.AddModelError("", "This category name is used! Choose another one!");
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(model);
            var result = _categoryRepo.CreateCategory(category);

            if (!result)
            {
                ModelState.AddModelError("", $"Something went wrong!\nCategory Name: {category.Name}\nDescription: {category.Description}");
                return StatusCode(500, ModelState);
            }

            return Ok(category);
        }


        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="model">In this process Id, Name and Description does required fields.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateCategory([FromForm] UpdateCategoryDTO model)
        {
            if (model == null) 
            {
                return BadRequest(ModelState);
            }

            var category = _mapper.Map<Category>(model);
            var result = _categoryRepo.UpdateCategory(category);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong...");
                return StatusCode(500, ModelState);
            }

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryRepo.GetCategory(id);
            if (category == null)
            {
                return NotFound(ModelState);
            }
            var result = _categoryRepo.DeleteCategory(id);
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong!");
            }
            return Ok("Kategori silindi!");
        }
    }
}
