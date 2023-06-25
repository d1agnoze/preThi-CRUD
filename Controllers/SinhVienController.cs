using preThi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace preThi.Controllers
{
    public class SinhVienController : Controller
    {
        private DataClasses1DataContext db = new DataClasses1DataContext(global::System.Configuration.ConfigurationManager.ConnectionStrings["demo_sqlsvConnectionString"].ConnectionString); // Replace with your DataContext instantiation logic
        // GET: SinhVien
        [Authorize]
        public ActionResult Index()
        {
            int PageSize = 1;
            var GetSearch = Request.QueryString["search"];
            if (GetSearch == null)
            {
                GetSearch = "";
            }
            var Result = db.tbl_SinhViens.Where(x => x.HoTen.Contains(GetSearch)).ToList();
            int TotalPage = (int)Math.Ceiling((double)Result.Count / PageSize);
            var page = Convert.ToInt32(Request.QueryString["page"]);

            if (page <= 0)
                page = 1; 
            if (page>TotalPage)
                page = TotalPage;
            var Output = Result.Skip((page - 1) * PageSize).Take(PageSize).ToList();
            ViewBag.Model = Output;
            ViewBag.TotalPage = TotalPage;
            ViewBag.PageSize = PageSize;
            ViewBag.Search = GetSearch;
            return View();
        }

        // GET: SinhVien/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SinhVien/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SinhVien/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create(tbl_SinhVien sv)
        {
            try
            {
                db.tbl_SinhViens.InsertOnSubmit(sv);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Ex = ex.Message;
                return View();
            }
        }

        // GET: SinhVien/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            id = id.Trim();

            var FindUser = db.tbl_SinhViens.Where(sv => sv.MSSV == id).FirstOrDefault();
            if (FindUser !=null)
            {
                return View(FindUser);
            }
            return RedirectToAction("Index","Sinh Vien");
        }

        // POST: SinhVien/Edit/5
        [HttpPost]
        public ActionResult Edit(tbl_SinhVien sv)
        {   
            try
            {   var checkUser = db.tbl_SinhViens.FirstOrDefault(x => x.MSSV == sv.MSSV);
                if (checkUser != null)
                {   checkUser.HoTen = sv.HoTen;
                    checkUser.DiaChi = sv.DiaChi;
                    checkUser.KhoaHoc = sv.KhoaHoc;
                    checkUser.LopQuanLy = sv.LopQuanLy;
                    checkUser.NgaySinh = sv.NgaySinh;
                    checkUser.GioiTinh = sv.GioiTinh;
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch(Exception Ex)
            {
                ViewBag.Ex = Ex;
                return View(sv.MSSV);
            }
        }

        // GET: SinhVien/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            try
            {   var checkUser = db.tbl_SinhViens.FirstOrDefault(x => x.MSSV==id);
                if (checkUser != null)
                {
                    db.tbl_SinhViens.DeleteOnSubmit(checkUser);
                    db.SubmitChanges();
                }
                else { throw new Exception("Sinh Vien unavailable"); }
            }
            catch (Exception ex)
            {
                ViewBag.Ex = ex.Message;
            }
            return RedirectToAction("Index");
        }

        // POST: SinhVien/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {   
                return View();
            }
        }
    }
}
