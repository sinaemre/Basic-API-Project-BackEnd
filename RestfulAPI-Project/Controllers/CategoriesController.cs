using ApplicationCore_API.DTO_s;
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
    }
}
