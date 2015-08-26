using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstanaCheTamBrat.DB.Models
{
    public class Product
    {
        public Guid product_id { get; set; }
        public string product_name { get; set; }
        public string product_shordescription { get; set; }
        public string product_fulldescription { get; set; }
        public int? product_price { get; set; }
        public bool? product_hit { get; set; }
        public bool? product_enabled { get; set; }
        public bool? product_approved { get; set; }
        public Guid? category_id { get; set; }
        public Guid? provider_id { get; set; }
        public string category_name { get; set; }
        public bool isInTemp = false;

        public bool isExist(Guid? product_id)
        {
            var tmp = DataBase.entity.products.FirstOrDefault(x => x.product_id == product_id);

            return (tmp != null);

        }

        public static List<Product> getProductsFromTempAndWork(Guid? provider_id, Guid? category_id)
        {
            List<Product> result = getProductsFromTemp(provider_id, category_id);

            List<Product> BaseResult = getProductsFromBase(provider_id, category_id);

            foreach (Product p in BaseResult)
            {
                if (result.Contains(p) == false)
                {
                    result.Add(p);
                }
            }
            return result;
        }
 
        public bool isExistInTem(Guid? product_id)
        {
            var tmp = DataBase.entity.products_temp.FirstOrDefault(x => x.product_id == product_id);

            return (tmp != null);

        }

        public void FromGuidInTemp(Guid? product_id)
        {
            var product_temp = DataBase.entity.products_temp.FirstOrDefault(x => x.product_id == product_id.Value);

            this.product_id = product_temp.product_id;
            this.product_name = product_temp.product_name;
            this.product_shordescription = product_temp.product_shordescription;
            this.product_fulldescription = product_temp.product_fulldescription;
            this.product_price = product_temp.product_price;
            this.product_hit = product_temp.product_hit;
            this.product_enabled = product_temp.product_enabled;
            this.product_approved = product_temp.product_approved;
            this.category_id = product_temp.category_id;
            this.provider_id = product_temp.provider_id;

            isInTemp = true;

            return;
        }

        public void FromGuid(Guid? product_id)
        {
            var product_temp = DataBase.entity.products_temp.FirstOrDefault(x => x.product_id == product_id.Value);
            if (product_temp != null)
            {

                this.product_id = product_temp.product_id;
                this.product_name = product_temp.product_name;
                this.product_shordescription = product_temp.product_shordescription;
                this.product_fulldescription = product_temp.product_fulldescription;
                this.product_price = product_temp.product_price;
                this.product_hit = product_temp.product_hit;
                this.product_enabled = product_temp.product_enabled;
                this.product_approved = product_temp.product_approved;
                this.category_id = product_temp.category_id;
                this.provider_id = product_temp.provider_id;

                if (this.category_id != null)
                    this.category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == this.category_id.Value).category_name;

                isInTemp = true;

                return;
            }
            else
            {

                var product = DataBase.entity.products.FirstOrDefault(x => x.product_id == product_id.Value);

                if (product == null) throw new Exception("Product not exits");

                this.product_id = product.product_id;
                this.product_name = product.product_name;
                this.product_shordescription = product.product_shordescription;
                this.product_fulldescription = product.product_fulldescription;
                this.product_price = product.product_price;
                this.product_hit = product.product_hit;
                this.product_enabled = product.product_enabled;
                this.product_approved = product.product_approved;
                this.category_id = product.category_id;
                this.provider_id = product.provider_id;

                if (this.category_id != null)
                    this.category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == this.category_id.Value).category_name;

                isInTemp = false;

                return;
            }

        }




        public static List<Product> getProductsFromBase(Guid? provider_id, Guid? category_id)
        {

            List<Product> result = new List<Product>();

            if (provider_id == null && category_id == null)
            {

                var _products = DataBase.entity.products;

                foreach (products prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id == null && category_id != null)
            {

                var _products = DataBase.entity.products.Where(x => x.category_id == category_id);

                foreach (products prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id != null && category_id == null)
            {

                var _products = DataBase.entity.products.Where(x => x.provider_id == provider_id);

                foreach (products prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id != null && category_id != null)
            {

                var _products = DataBase.entity.products.Where(x => x.category_id == category_id.Value  && x.provider_id == provider_id.Value);

                foreach (products prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            return result;
        }




        public static List<Product> getProductsFromTemp(Guid? provider_id, Guid? category_id)
        {

            List<Product> result = new List<Product>();

            if (provider_id == null && category_id == null)
            {

                var _products = DataBase.entity.products_temp;

                foreach (products_temp prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id == null && category_id != null)
            {

                var _products = DataBase.entity.products_temp.Where(x=>x.category_id==category_id);

                foreach (products_temp prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id != null && category_id == null)
            {

                var _products = DataBase.entity.products_temp.Where (x=>x.provider_id == provider_id);

                foreach (products_temp prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            if (provider_id != null && category_id != null)
            {

                var _products = DataBase.entity.products_temp.Where(x=> x.category_id == category_id && x.provider_id == provider_id);

                foreach (products_temp prod in _products)
                {
                    result.Add(new Product()
                    {
                        category_id = prod.category_id,
                        category_name = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == prod.category_id.Value).category_name,
                        product_approved = prod.product_approved,
                        product_enabled = prod.product_enabled,
                        product_fulldescription = prod.product_fulldescription,
                        product_hit = prod.product_hit,
                        product_id = prod.product_id,
                        product_name = prod.product_name,
                        product_price = prod.product_price,
                        product_shordescription = prod.product_shordescription,
                        provider_id = prod.provider_id,
                        isInTemp = true
                    });
                }

                return result;

            }

            return result;
        }

        public void MoveToMainTable()
        {
            if (isExistInTem(this.product_id))
            {



            }
        }

        public void Save(bool inTemp = false)
        {

            if (!inTemp)
            {
                products product;

                bool _isExist = false;

                if (isExist(product_id))
                {
                    product = DataBase.entity.products.FirstOrDefault(x => x.product_id == product_id);
                    _isExist = true;
                }
                else
                {
                    product = new products();
                }

                product.product_id = this.product_id;
                product.product_name = this.product_name;
                product.product_shordescription = this.product_shordescription;
                product.product_fulldescription = this.product_fulldescription;
                product.product_price = this.product_price;
                product.product_hit = this.product_hit;
                product.product_enabled = this.product_enabled;
                product.product_approved = this.product_approved;
                product.category_id = this.category_id;
                product.provider_id = this.provider_id;

                if (!_isExist)
                    DataBase.entity.products.Add(product);

                DataBase.entity.SaveChanges();

            }
            else
            {
                products_temp product;

                bool _isExist = false;

                if (isExistInTem(product_id))
                {
                    product = DataBase.entity.products_temp.FirstOrDefault(x => x.product_id == product_id);
                    _isExist = true;
                }
                else
                {
                    product = new products_temp();
                }

                if (!_isExist) product.product_id = this.product_id;
                product.product_name = this.product_name;
                product.product_shordescription = this.product_shordescription;
                product.product_fulldescription = this.product_fulldescription;
                product.product_price = this.product_price;
                product.product_hit = this.product_hit;
                product.product_enabled = this.product_enabled;
                product.product_approved = this.product_approved;
                product.category_id = this.category_id;
                product.provider_id = this.provider_id;

                if (!_isExist)
                    DataBase.entity.products_temp.Add(product);

                DataBase.entity.SaveChanges();
            }

        }

    }
}
