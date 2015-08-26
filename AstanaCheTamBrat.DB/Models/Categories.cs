using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstanaCheTamBrat.DB.Models
{
    public class Category
    {
        public Guid category_id;
        public string category_name;
        public Guid? category_parent;
        public List<Category> sub_categories;
    }
    public class Categories
    {

        public static List<Category> categories(Guid? parent_id, bool show_sub = false)
        {

            List<Category> result = new List<Category>();

            var _result = DataBase.entity.products_categories.Where(x => x.category_parent == parent_id).ToList<products_categories>();

            foreach (products_categories c in _result)
            {
                result.Add(new Category() { 
                    category_id = c.category_id, 
                    category_name = c.category_name, 
                    category_parent = c.category_parent,
                    sub_categories = show_sub == true ? categories(c.category_id, show_sub) : new List<Category>()
                });
            }

            return result;

        }

        public static List<Category> MyCategories(Guid? category_id, Guid provider_id)
        {

            List<Category> result = new List<Category>();
            List<products_categories> WasResult = new List<products_categories>();

            var _result = DataBase.entity.providers.First(x=> x.provider_id == provider_id).products;


            foreach (products p in _result)
            {
                if (!WasResult.Contains(p.products_categories)) WasResult.Add(p.products_categories);
                
                if (p.products_categories.category_parent != null)
                {

                    products_categories parent1 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == p.products_categories.category_parent);

                    if (!WasResult.Contains(parent1)) WasResult.Add(parent1);

                    if (parent1.category_parent != null)
                    {
               
                        products_categories parent2 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent1.category_parent);

                        if (!WasResult.Contains(parent2)) WasResult.Add(parent2);

                        if (parent2.category_parent != null)
                        {

                            products_categories parent3 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent2.category_parent);

                            if (!WasResult.Contains(parent3)) WasResult.Add(parent3);

                            if (parent3.category_parent != null)
                            {

                                products_categories parent4 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent3.category_parent);

                                if (!WasResult.Contains(parent4)) WasResult.Add(parent4);

                                if (parent4.category_parent != null)
                                {

                                    products_categories parent5 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent4.category_parent);

                                    if (!WasResult.Contains(parent5)) WasResult.Add(parent5);

                                    if (parent5.category_parent != null)
                                    {

                                        products_categories parent6 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent5.category_parent);

                                        if (!WasResult.Contains(parent6)) WasResult.Add(parent6);
                                        
                                    }

                                }

                            }

                        }

                    }

                }
            }

            List<products_categories> myResults = WasResult.Where (x=> x.category_parent == null).ToList<products_categories>();

            for (int i = 0; i < myResults.Count(); i++)
            {

                Category cat = new Category()
                {
                    category_id = myResults[i].category_id,
                    category_name = myResults[i].category_name,
                    category_parent = myResults[i].category_parent,
                    sub_categories = new List<Category>()
                };
                Guid? tmp_guid = myResults[i].category_id;
                
                addSubToCat(out cat.sub_categories, myResults, cat.category_id);

                result.Add(cat);
                
            }



            List<Category> myCategoriesInTemp = MyCategoriesInTemp(category_id, provider_id);

            foreach (Category pc in myCategoriesInTemp)
            {
                if (result.Contains(pc) == false) result.Add(pc);
            }


            return result;

        }



        public static List<Category> MyCategoriesInTemp(Guid? category_id, Guid provider_id)
        {

            List<Category> result = new List<Category>();
            List<products_categories> WasResult = new List<products_categories>();

            var _result = DataBase.entity.products_temp.Where(x => x.provider_id == provider_id).ToList();


            foreach (products_temp p in _result)
            {

                products_categories pproducts_categories = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == p.category_id);

                if (!WasResult.Contains(pproducts_categories)) WasResult.Add(pproducts_categories);

                if (pproducts_categories.category_parent != null)
                {

                    products_categories parent1 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == pproducts_categories.category_parent);

                    if (!WasResult.Contains(parent1)) WasResult.Add(parent1);

                    if (parent1.category_parent != null)
                    {

                        products_categories parent2 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent1.category_parent);

                        if (!WasResult.Contains(parent2)) WasResult.Add(parent2);

                        if (parent2.category_parent != null)
                        {

                            products_categories parent3 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent2.category_parent);

                            if (!WasResult.Contains(parent3)) WasResult.Add(parent3);

                            if (parent3.category_parent != null)
                            {

                                products_categories parent4 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent3.category_parent);

                                if (!WasResult.Contains(parent4)) WasResult.Add(parent4);

                                if (parent4.category_parent != null)
                                {

                                    products_categories parent5 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent4.category_parent);

                                    if (!WasResult.Contains(parent5)) WasResult.Add(parent5);

                                    if (parent5.category_parent != null)
                                    {

                                        products_categories parent6 = DataBase.entity.products_categories.FirstOrDefault(x => x.category_id == parent5.category_parent);

                                        if (!WasResult.Contains(parent6)) WasResult.Add(parent6);

                                    }

                                }

                            }

                        }

                    }

                }
            }

            List<products_categories> myResults = WasResult.Where(x => x.category_parent == null).ToList<products_categories>();

            for (int i = 0; i < myResults.Count(); i++)
            {

                Category cat = new Category()
                {
                    category_id = myResults[i].category_id,
                    category_name = myResults[i].category_name,
                    category_parent = myResults[i].category_parent,
                    sub_categories = new List<Category>()
                };
                Guid? tmp_guid = myResults[i].category_id;

                addSubToCat(out cat.sub_categories, WasResult, cat.category_id);

                result.Add(cat);

            }











            return result;

        }



        public static void addSubToCat(out List<Category> current_cat,List<products_categories> sub_cats, Guid? parrent_id)
        {

            current_cat = new List<Category>();
            
            var _sub_cats = sub_cats.Where(x => x.category_parent == parrent_id);

            foreach (products_categories c in _sub_cats)
            {
                Category cat = new Category() { category_id = c.category_id, category_name = c.category_name, category_parent = c.category_parent };
               
                current_cat.Add(cat);

                addSubToCat(out cat.sub_categories, sub_cats, cat.category_id);

            }

        }

    }
}
