using PlayListApp.Models;
using PlayListApp.Models.Enums;
using PlayListApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PlayListApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController() {
            _context = new ApplicationDbContext();
        }

        public HomeController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public ActionResult Index(int pageIndex = 1)
        {
            int pageSize = 10;
            var singers = _context.Singers.Include("Songs").OrderBy(s => s.Name).Skip((pageIndex -1) * pageSize).Take(pageSize);

            var SingersSongs = new List<SingerSongsViewModel>();

            foreach (var singer in singers)
            {
                if (singer.Songs.Count() == 0)
                {
                    var model = new SingerSongsViewModel()
                    {
                        SingerName = singer.Name,
                        SingerId = singer.Id,
                        Image = singer.Image
                    };
                    SingersSongs.Add(model);
                }
                else
                {
                    var model = new SingerSongsViewModel()
                    {
                        SongName = singer.Songs.FirstOrDefault().Name,
                        SongType = singer.Songs.FirstOrDefault().Type.ToString(),
                        SingerName = singer.Name,
                        SingerId = singer.Id,
                        Image = singer.Image
                    };
                    SingersSongs.Add(model);
                }
            }
            ViewBag.count = _context.Singers.Count();

            return View(SingersSongs);
        }

        public ActionResult Create()
        {
            var model = new Singer();

            return View("SingerViewModel" , model);
        }

        [HttpPost]
        public ActionResult Create(Singer model)
        {
            if (!ModelState.IsValid)
                return View("SingerViewModel" , model);

            byte[] imagebytes = null;
            if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(model.ImageFile.InputStream))
                {
                    imagebytes = binaryReader.ReadBytes(model.ImageFile.ContentLength);
                }
            }
            var singer = new Singer()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Image = imagebytes
            };

            _context.Singers.Add(singer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Update(Guid SingerId)
        {
            var singer = _context.Singers.FirstOrDefault(x => x.Id == SingerId);

            return View("SingerViewModel", singer);

        }

        [HttpPost]
        public ActionResult Update(Singer singer)
        {
            if (!ModelState.IsValid)
                return View("SingerViewModel", singer);

            byte[] imagebytes = null;
            if (singer.ImageFile != null && singer.ImageFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(singer.ImageFile.InputStream))
                {
                    imagebytes = binaryReader.ReadBytes(singer.ImageFile.ContentLength);
                }
            }
            singer.Image = imagebytes;

            _context.Singers.AddOrUpdate(singer);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public ActionResult Delete(Guid SingerId)
        {
            var singer = _context.Singers.Find(SingerId);
            if (singer != null)
            {
                _context.Singers.Remove(singer);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
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
    }
}