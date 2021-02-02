using eShopLegacyMVC.Models;
using eShopLegacyMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopLegacyMVC.Controllers.Api
{
    public class CatalogApiController : ControllerBase
    {
        private ICatalogService service;

        public CatalogApiController(ICatalogService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("api/catalog/brands")]
        public virtual ActionResult GetBrands()
        {
            return Ok(service.GetCatalogBrands());
        }

        [HttpGet]
        [Route("api/catalog")]
        public virtual ActionResult Get()
        {
            return Ok(service.GetCatalogItemsPaginated(100, 0));
        }

        [HttpPost]
        [Route("api/catalog")]
        public virtual ActionResult Create([FromBody] CatalogItem catalogItem)
        {
            if (ModelState.IsValid)
            {
                service.CreateCatalogItem(catalogItem);
                return Ok(catalogItem);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("api/catalog/{id}")]
        public virtual ActionResult Update([FromBody] CatalogItem catalogItem, int id)
        {
            if (ModelState.IsValid)
            {
                var item = service.FindCatalogItem(id);
                if (item == null)
                {
                    return NotFound();
                }
                service.UpdateCatalogItem(catalogItem);
                return Ok(catalogItem);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("api/catalog/{id}")]
        public virtual ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var item = service.FindCatalogItem(id);
                if (item == null)
                {
                    return NotFound();
                }

                service.RemoveCatalogItem(item);

                return Ok();
            }
            return BadRequest();
        }
    }
}
