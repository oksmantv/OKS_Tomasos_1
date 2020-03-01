using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OKS_Tomasos.Models;
using OKS_Tomasos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKS_Tomasos.Services.SessionService
{
    public class SessionData
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionData(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Kund GetSessionKund()
        {
            var valuesJSON = _httpContextAccessor.HttpContext.Session.GetString("UserAccount");
            return JsonConvert.DeserializeObject<Kund>(valuesJSON);
        }

        public int GetSessionKundId()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
        }

        public void SetSessionKund(Kund K)
        {
            var jsonList = JsonConvert.SerializeObject(K);
            _httpContextAccessor.HttpContext.Session.SetString("UserAccount", jsonList);

        }

        public void SetSessionKundId(int K)
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserID", K.ToString());

        }

        public void SetSessionLoggedIn()
        {
            _httpContextAccessor.HttpContext.Session.SetString("UserLoggedIn", "1");
        }

        public List<Matratt> GetCart()
        { 
            var Json = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            if (Json != null)
                return JsonConvert.DeserializeObject<List<Matratt>>(Json);
            else
                return new List<Matratt>();

        }

        public void AddToCart(int id,IRepository _Connection)
        {

            List<Matratt> shoppingCart;
            var selectedProduct = _Connection.GetMatratt(id);

            if (_httpContextAccessor.HttpContext.Session.GetString("Cart") == null)
            {
                shoppingCart = new List<Matratt>();
            }
            else
            {
                var valuesJSON = _httpContextAccessor.HttpContext.Session.GetString("Cart");
                shoppingCart = JsonConvert.DeserializeObject<List<Matratt>>(valuesJSON);
            }

            shoppingCart.Add(selectedProduct);
            var jsonList = JsonConvert.SerializeObject(shoppingCart);

            _httpContextAccessor.HttpContext.Session.SetString("Cart", jsonList);

        }

    }
}
