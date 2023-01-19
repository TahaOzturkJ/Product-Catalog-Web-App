using Project.BLL.DesignPatterns.GenericRepository.ConcRep;
using Project.ENTITIES.Models;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace Project.WEBUI.Controllers
{
    public class CatalogController : Controller
    {
        BrandRepository _bRep;
        CableTypeRepository _ctRep;
        ProductRepository _pRep;

        public CatalogController()
        {
            _bRep = new BrandRepository();
            _ctRep = new CableTypeRepository();
            _pRep = new ProductRepository();
        }

        #region Member

        // GET: Catalog
        public ActionResult MemberCatalog()
        {

            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }

            IndexVM ivm = new IndexVM
            {
                Products = _pRep.GetActives()
            };

            return View(ivm);
        }

        [HttpPost]
        public ActionResult MemberCatalog(IndexVM ivm,string model,string cavoRefNo,string oemRefNo)
        {
            int SelectedBrandID = ivm.Brand.ID;
            int SelectedCableTypeID = ivm.CableType.ID;
            string Model = model;
            string CavoRefNo = cavoRefNo;
            string OemRefNo = oemRefNo;

            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }

            if (SelectedBrandID==0 && SelectedCableTypeID==0)
            {
                if (Model=="" && CavoRefNo=="" && OemRefNo=="")
                {
                    ivm.Products=_pRep.GetActives();
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            
            }
            else if (SelectedBrandID!=0 && SelectedCableTypeID==0)
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x=>x.Brand.ID==SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }
            else if (SelectedBrandID==0 && SelectedCableTypeID!=0)
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x => x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }
            else
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x => x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }


            //ivm.Products = _pRep.Where(x => x.Brand.ID == SelectedBrandID && x.CableType.ID == SelectedCableTypeID);

            return View(ivm);
        }

        #endregion

        #region Admin

        // GET: Catalog
        public ActionResult AdminCatalog()
        {

            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }

            IndexVM ivm = new IndexVM
            {
                Products = _pRep.GetActives()
            };

            return View(ivm);
        }

        [HttpPost]
        public ActionResult AdminCatalog(IndexVM ivm, string model, string cavoRefNo, string oemRefNo)
        {
            int SelectedBrandID = ivm.Brand.ID;
            int SelectedCableTypeID = ivm.CableType.ID;
            string Model = model;
            string CavoRefNo = cavoRefNo;
            string OemRefNo = oemRefNo;

            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }

            if (SelectedBrandID == 0 && SelectedCableTypeID == 0)
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.GetActives();
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }

            }
            else if (SelectedBrandID != 0 && SelectedCableTypeID == 0)
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x => x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }
            else if (SelectedBrandID == 0 && SelectedCableTypeID != 0)
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x => x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }
            else
            {
                if (Model == "" && CavoRefNo == "" && OemRefNo == "")
                {
                    ivm.Products = _pRep.Where(x => x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                }
                else
                {
                    if (Model != "" && CavoRefNo == "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model == "" && CavoRefNo != "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo != "" && OemRefNo == "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else if (Model != "" && CavoRefNo == "" && OemRefNo != "")
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CableType.ID == SelectedCableTypeID && x.Brand.ID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                    else
                    {
                        ivm.Products = _pRep.Where(x => x.Model == Model && x.OemRefNo == OemRefNo && x.CavoRefNo == CavoRefNo && x.CableType.ID == SelectedCableTypeID && x.BrandID == SelectedBrandID && x.Status != ENTITIES.Enums.DataStatus.Deleted);
                    }
                }
            }


            //ivm.Products = _pRep.Where(x => x.Brand.ID == SelectedBrandID && x.CableType.ID == SelectedCableTypeID);

            return View(ivm);
        }

        public ActionResult AddProduct()
        {
            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
                 _pRep.Add(product);
                 return RedirectToAction("AdminCatalog");
        }

        public ActionResult UpdateProduct(int id)
        {
            var brandItems = _bRep.GetActives();
            var cabletypeItems = _ctRep.GetActives();
            if (brandItems != null)
            {
                ViewBag.brandData = brandItems;
            }
            if (cabletypeItems != null)
            {
                ViewBag.cabletypeData = cabletypeItems;
            }


            IndexVM ivm = new IndexVM
            {
                Product = _pRep.Find(id)
            };
            return View(ivm);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            if (product.Model.Trim() == "" || product.Positioning.Trim() == "" || product.CavoRefNo.Trim() == "" || product.ModelYear.Trim() == "" || product.Length.Trim() == "" || product.OemRefNo.Trim() == "" || product.BrandID == 0 || product.CableTypeID == 0)
            {
                ViewBag.Message("Lütfen Bütün Alanları Doldurun");
                return View();
            }
            else
            {
                _pRep.Update(product);
                return RedirectToAction("AdminCatalog");
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            _pRep.Delete(_pRep.Find(id));
            return RedirectToAction("AdminCatalog");
        }

        public ActionResult ImportFromExcel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase excelfile)
        {
            if (excelfile.ContentLength == 0 || excelfile is null)
            {
                ViewBag.Error = "Please select a excel file";
                return View("ImportFromExcel");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string path = Server.MapPath("~/Content" + excelfile.FileName);
                    //if (System.IO.File.Exists(path))
                    //{
                    //    System.IO.File.Delete(path);
                    //}
                    //excelfile.SaveAs(path);

                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = application.Worksheets[1];
                    Excel.Range range = worksheet.UsedRange;

                    // List<Product> listProducts = new List<Product>();

                    try
                    {
                        for (int row = 2; row < range.Rows.Count; row++)
                        {
                            Product p = new Product();
                            p.Model = ((Excel.Range)range.Cells[row, 1]).Text;
                            p.Positioning = ((Excel.Range)range.Cells[row, 2]).Text;
                            p.CavoRefNo = ((Excel.Range)range.Cells[row, 3]).Text;
                            p.ModelYear = ((Excel.Range)range.Cells[row, 4]).Text;
                            p.Length = ((Excel.Range)range.Cells[row, 5]).Text;
                            p.OemRefNo = ((Excel.Range)range.Cells[row, 6]).Text;
                            p.BrandID = Convert.ToInt32(((Excel.Range)range.Cells[row, 7]).Text);
                            p.CableTypeID = Convert.ToInt32(((Excel.Range)range.Cells[row, 8]).Text);
                            p.Status = (ENTITIES.Enums.DataStatus)Convert.ToInt32(((Excel.Range)range.Cells[row,9]).Text);
                            p.CreatedDate = Convert.ToDateTime(((Excel.Range)range.Cells[row,10]).Text);
                            // listProducts.Add(p);
                            _pRep.Add(p);
                            ViewBag.Error = "Veri Başarıyla Girildi.";
                        }
                    }
                    catch (System.Exception)
                    {
                        ViewBag.Error = "Hatalı Veri Girişi Yapıldı, Lütfen Sıralama Model,Positioning,CavoRefNo,ModelYear,Length,OemRefNo,BrandID,CableTypeID,DataStatus,CreatedDate Olacak Şekilde Excel Tablosu Giriniz...";
                    }
                    return RedirectToAction("AdminCatalog");

                }
                else
                {
                    ViewBag.Error = "File type is incorrect<br>";
                    return View("ImportFromExcel");
                }
            }
            
        }


        #endregion


    }
}