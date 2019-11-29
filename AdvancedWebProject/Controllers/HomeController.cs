using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvancedWebProject.Models;

namespace AdvancedWebProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ProductInfo> products = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {
                
                foreach(ProductInfo product in context.ProductInfo)
                {
                    var a = new ProductInfo();
                    a.ID = product.ID;
                    a.Name = product.Name;
                    a.Price = product.Price;
                    a.Category = product.Category;
                    a.Customer_ID = product.Customer_ID;
                    a.Description = product.Description;
                    products.Add(a);
                }
                ViewBag.productlist = products;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddUser()
        {

            return View();
        }

        public ActionResult AddUserToDB(FormCollection fomr)
        {
            using (AdvancedWebDevelopmentEntities1 context = new AdvancedWebDevelopmentEntities1())
            {
                CustomerData customer = new CustomerData();

                customer.Name = fomr["name"];
                customer.Surname = fomr["surname"];
                customer.Mail = fomr["mail"];
                customer.Phone = fomr["phone"];
                customer.Password = fomr["password"];
                context.CustomerData.Add(customer);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult SignIn(FormCollection fomr)
        {
            using (AdvancedWebDevelopmentEntities1 context = new AdvancedWebDevelopmentEntities1())
            {
                foreach (CustomerData customer in context.CustomerData)
                {
                    if (fomr["mail"] == customer.Mail)
                    {
                        if (fomr["password"] == customer.Password)
                        {
                            Session["signin"] = "1";
                            Session["UserID"] = customer.ID;
                            break;
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SignOut()
        {
            Session["signin"] = "0";
            Session["UserID"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult AddProduct()
        {
            if(Session["signin"] == "0" || Session["signin"]==null)
            {
                return RedirectToAction("AddUser");
            }
            else
            {
                return View();
            } 
        }

        public ActionResult AddProductToDB(FormCollection fomr)
        {
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {
                ProductInfo product = new ProductInfo();
                product.Name = fomr["name"];
                product.Price = Int32.Parse(fomr["price"]);
                product.Category = fomr["categories"];
                product.Customer_ID = Int32.Parse(Session["UserID"].ToString());
                product.Product_State = "OnSale";
                product.Description = fomr["description"];
                context.ProductInfo.Add(product);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Buy(int id)
        {
            var a = id;
            using (AdvancedWebDevelopmentEntities1 context = new AdvancedWebDevelopmentEntities1())
            {
                //Cart_Items cartitems = new Cart_Items();
                //cartitems.Product_ID = id;
                //context.Cart_Items.Add(cartitems);
                ProductInfo prod=context.ProductInfo.Find(id);
                prod.Buyer_ID = Int32.Parse(Session["UserID"].ToString());
                prod.Product_State = "OnCart";
                context.SaveChanges();
                //var prd = context.ProductInfo.Find(id);
                //context.ProductInfo.Remove(prd);
                //context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Cart()
        {
            List<ProductInfo> productlist = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities1 context = new AdvancedWebDevelopmentEntities1())
            {
            foreach(ProductInfo product in context.ProductInfo)
                {
                    if(product.Product_State.ToString() == "OnCart    ")
                    {
                        productlist.Add(product);
                    }
                }
            }
            ViewBag.productlistt = productlist;
            return View();
        }
        public ActionResult BuyAll()
        {
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {
                foreach(ProductInfo product in context.ProductInfo)
                {
                    if(product.Buyer_ID==Int32.Parse(Session["UserID"].ToString()))
                    {
                        if(product.Product_State == "OnCart    ")
                        {
                            context.ProductInfo.Remove(product);
                        }
                    }
                }
            }
                return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {
                ProductInfo info = context.ProductInfo.Find(id);
                ViewBag.prodinfo = info;
            }
            return View();
        }

        public ActionResult Technology()
        {
            List<ProductInfo> productss = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {

                foreach (ProductInfo product in context.ProductInfo)
                {
                    if(product.Category=="Technology")
                    {
                        var a = new ProductInfo();
                        a.ID = product.ID;
                        a.Name = product.Name;
                        a.Price = product.Price;
                        a.Category = product.Category;
                        a.Customer_ID = product.Customer_ID;
                        a.Description = product.Description;
                        productss.Add(a);
                    }
                }
                ViewBag.techlist = productss;
            }
            return View();
        }

        public ActionResult SelfCare()
        {
            List<ProductInfo> productss = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {

                foreach (ProductInfo product in context.ProductInfo)
                {
                    if (product.Category == "Self-Care")
                    {
                        var a = new ProductInfo();
                        a.ID = product.ID;
                        a.Name = product.Name;
                        a.Price = product.Price;
                        a.Category = product.Category;
                        a.Customer_ID = product.Customer_ID;
                        a.Description = product.Description;
                        productss.Add(a);
                    }
                }
                ViewBag.carelist = productss;
            }
            return View();
        }

        public ActionResult Clothing()
        {
            List<ProductInfo> productss = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {

                foreach (ProductInfo product in context.ProductInfo)
                {
                    if (product.Category == "Clothing")
                    {
                        var a = new ProductInfo();
                        a.ID = product.ID;
                        a.Name = product.Name;
                        a.Price = product.Price;
                        a.Category = product.Category;
                        a.Customer_ID = product.Customer_ID;
                        a.Description = product.Description;
                        productss.Add(a);
                    }
                }
                ViewBag.clothlist = productss;
            }
            return View();
        }

        public ActionResult Accessories()
        {
            List<ProductInfo> productss = new List<ProductInfo>();
            using (AdvancedWebDevelopmentEntities2 context = new AdvancedWebDevelopmentEntities2())
            {

                foreach (ProductInfo product in context.ProductInfo)
                {
                    if (product.Category == "Accessories")
                    {
                        var a = new ProductInfo();
                        a.ID = product.ID;
                        a.Name = product.Name;
                        a.Price = product.Price;
                        a.Category = product.Category;
                        a.Customer_ID = product.Customer_ID;
                        a.Description = product.Description;
                        productss.Add(a);
                    }
                }
                ViewBag.accessorieslist = productss;
            }
            return View();
        }
    }
}