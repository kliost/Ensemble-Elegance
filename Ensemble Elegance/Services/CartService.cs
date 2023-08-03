using Ensemble_Elegance.Models;
using Microsoft.AspNetCore.Identity;
using Ensemble_Elegance.Extensions;
using System.Text.Json;
using System.Net.Mail;
using System.Net;

namespace Ensemble_Elegance.Services
{
    public interface ICartService
    {
        public void PushOrder(OrderModel order);

        public void AddToCartById(int productId);

        public List<CartProductModel> GetCart();

        public void UpdateQuantity(int productId, int newQuantity);

        public void DeleteFromCart(int productId);

        public void SendEmailTo(string email);

    }

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISession _session;
        public CartService(ApplicationDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _session = httpContextAccessor.HttpContext.Session;
        }

        // Adds product to cart using product's id 
        public void AddToCartById(int productId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();


            ProductModel? product = _context.Products.Where(x => x.Id == productId).FirstOrDefault();

            if (product != null)
            {
                CartProductModel cartProduct = new()
                {
                    productModel = product
                };
                var ExistingProductInCart = cart.Where(x => x.productModel.Id == cartProduct.productModel.Id).FirstOrDefault();
                if (ExistingProductInCart != null)
                {
                    ExistingProductInCart.Quantity++;
                }
                else
                {
                    cartProduct.Quantity = 1;
                    cart.Add(cartProduct);
                }
            }

            _session.SetObject("Cart", cart);
        }
        // Adds order to DataBase
        public void PushOrder(OrderModel order)
        {
            order.Status = OrderStatus.PendingConfirmation;
            var cart = GetCart();
            order.OrderListJson = JsonSerializer.Serialize(cart);
            _context.Orders.Add(order);
            _context.SaveChanges();
            SendEmailTo(order.CustomerEmail);
        }
        // Removes product from cart using id
        public void DeleteFromCart(int productId)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel? cartProduct = cart.Where(x => x.productModel.Id == productId).FirstOrDefault();
            if (cartProduct != null) cart.Remove(cartProduct);

        }
        // Gets cart from _session
        public List<CartProductModel> GetCart()
        {
            return _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
        }


        public void UpdateQuantity(int productId, int newQuantity)
        {
            List<CartProductModel> cart = _session.GetObject<List<CartProductModel>>("Cart") ?? new List<CartProductModel>();
            CartProductModel cartProduct = cart.FirstOrDefault(x => x.productModel.Id == productId);

            if (cartProduct != null)
            {
                cartProduct.Quantity = newQuantity;
            }
            _session.SetObject("Cart", cart);
        }

        public void SendEmailTo(string recipientEmailAddress)
        {
            string senderEmail = Environment.senderEmailAdress;
            string senderPassword = Environment.senderPassword;
            string recipientEmail = recipientEmailAddress;

            var mailMessage = new MailMessage(senderEmail, recipientEmail, "Esensemble Elegance", "Your order is accepted");
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword)
            };


            smtpClient.Send(mailMessage);


        }
    }
}
