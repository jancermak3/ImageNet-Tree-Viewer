using ImageNet.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ImageNet.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ImageNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageNetItemsController : ControllerBase
    {
        private readonly IImageNetItemRepository _repository;
        private readonly ITreeTransformator _treeTransformator;
        private readonly IMapper _mapper;

        public ImageNetItemsController(IImageNetItemRepository repository, ITreeTransformator treeTransformator, IMapper mapper)
        {
            _repository = repository;
            _treeTransformator = treeTransformator;
            _mapper = mapper;
        }

        [HttpGet("tree")]
        public ActionResult<TreeNode> GetAsTree()
        {
            var items = _repository.GetAll();
            if (items.IsNullOrEmpty())
            {
                return NotFound("No data available.");
            }

            var tree = _treeTransformator.TransformToTree(items);
            return Ok(tree);
        }

        [HttpGet("linear")]
        public ActionResult<IEnumerable<ImageNetItemDto>> GetLinear()
        {
            var items = _repository.GetAll();
            if (items.IsNullOrEmpty())
            {
                return NotFound("No data available.");
            }
            
            var itemsDto = _mapper.Map<IEnumerable<ImageNetItemDto>>(items);
            return Ok(itemsDto);
        }
    }
}