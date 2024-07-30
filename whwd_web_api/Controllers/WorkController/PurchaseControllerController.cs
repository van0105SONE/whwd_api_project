using ApplicationCore.Dtos;
using ApplicationCore.Dtos.PurchaseDto;
using ApplicationCore.Filter;
using AutoMapper;
using Infrastructure.DataBaseContext;
using Infrastructure.Model.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace whwd_web_api.Controllers.WorkController
{
    [ApiController]
    public class PurchaseControllerController : Controller
    {
        public IMapper _mapper { get; set; }
        public DatabaseContexts databaseContexts { get; set; }
        public PurchaseControllerController(DatabaseContexts dbContext, IMapper mapper)
        {
            databaseContexts = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("createPurchase")]
        public IActionResult createResult([FromBody] PurchaseDto purchaseDto)
        {
            try
            {
                var cart = _mapper.Map<Carts>(purchaseDto);
                var user = databaseContexts.Users.FirstOrDefault(t => t.Id == purchaseDto.userId);
                cart.CreateBy = user;
                cart.status = "pending";
                databaseContexts.cart.Add(cart);
                databaseContexts.SaveChanges();
                List<CartItem> itemList = new List<CartItem>();

                foreach (var item in purchaseDto.items)
                {
                    CartItem cartItem = _mapper.Map<CartItem>(item);
                    cartItem.Id = Guid.NewGuid();
                    cartItem.Cart = cart;
                    databaseContexts.cartItems.Add(cartItem);
                    databaseContexts.SaveChanges();
                }

                return Ok(new MessageReponse<PurchaseResponseDto>()
                {
                    statusCode = 200,
                    message = "Successs",
                    isSuccess = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("approvePurchase/{id}")]
        public IActionResult approvePurchase(Guid id)
        {
            try
            {
                var cart = databaseContexts.cart.FirstOrDefault(t => t.Id == id);
                cart.status = "approve";
                databaseContexts.cart.Update(cart);
                databaseContexts.SaveChanges();

                var account =  databaseContexts.accounts.FirstOrDefault(t => t.AccountTypes.ToUpper() == "MAIN" && t.ProjectPlan.IsActive);
                account.Balance -= cart.totalPrice;
                account.WithdrawAmount += cart.totalPrice;
                databaseContexts.accounts.Update(account);
                databaseContexts.SaveChanges();

                return Ok(new MessageReponse<String>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("getPurchases")]
        public IActionResult getPurchases([FromQuery] BaseFilter filter)
        {
            try
            {
              var purchaseList =  databaseContexts.cart.Include(t => t.CreateBy).Include(t => t.Items).Skip((filter.page - 1) * filter.pageSize).Take(filter.pageSize).ToList();
                var jsonString = JsonConvert.SerializeObject(purchaseList, Formatting.Indented,
        new JsonSerializerSettings()
        {
            ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        }
    );
                return Ok(jsonString);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("deleteCart/{cartId}")]
        public IActionResult deleteCart(Guid cartId)
        {
            try
            {
                var cart = databaseContexts.cart.FirstOrDefault(t => t.Id == cartId);
                databaseContexts.cart.Remove(cart);
                databaseContexts.SaveChanges();
                return Ok(new MessageReponse<String>()
                {
                    statusCode = 200,
                    isSuccess = true,
                    message = "Success"
                });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete]
        [Route("deleteItem/{itemId}")]
        public IActionResult deleteItem(Guid itemId)
        {
            try
            {
               var cartItem =  databaseContexts.cartItems.FirstOrDefault(t => t.Id == itemId);
                databaseContexts.cartItems.Remove(cartItem);
                databaseContexts.SaveChanges();
               return Ok(new MessageReponse<String>() { 
                  statusCode = 200,
                  isSuccess = true,
                  message = "Success"
                  });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        

    }
}
