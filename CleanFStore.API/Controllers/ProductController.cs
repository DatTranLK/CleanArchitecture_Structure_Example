using CleanFStore.Application;
using CleanFStore.Application.Features.Products.Commands.CreateNewProduct;
using CleanFStore.Application.Features.Products.Commands.DeleteProduct;
using CleanFStore.Application.Features.Products.Commands.UpdateProduct;
using CleanFStore.Application.Features.Products.Queries.GetProductsList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CleanFStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet(Name = "GetProducts")]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ProductsResponse>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ServiceResponse<IEnumerable<ProductsResponse>>>> GetProducts()
        {
            try
            {
                var query = new GetProductsListQuery();
                var res = await _mediator.Send(query);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: "+ex.Message);
            }
        }
        [HttpPost(Name = "CreateProducts")]
        [ProducesResponseType(typeof(ServiceResponse<int>), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<ServiceResponse<int>>> CreateProducts([FromBody]CreateNewProductCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPut(Name = "UpdateProducts")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateProducts([FromBody] UpdateProductCommand command)
        {
            try
            {
                var res = await _mediator.Send(command);
                if (res.Message != "Successfully")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpDelete("{id}", Name = "DeleteProducts")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ServiceResponse<string>>> DeleteProducts(int id)
        {
            try
            {
                var command = new DeleteProductCommand() { ProductId = id};
                var res = await _mediator.Send(command);
                if(res.Message != "Successfully")
                {
                    return StatusCode((int)res.StatusCode, res);
                }
                return NoContent();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
