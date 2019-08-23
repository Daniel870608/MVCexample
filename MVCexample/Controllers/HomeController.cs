using MVCexample.Models;
using MVCexample.Services;
using MVCexample.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCexample.Controllers
{
    public class HomeController : Controller
    {
        public GuestbookService guestbookService = new GuestbookService();
        public ActionResult Index(string Search = "",int page = 1)
        {
            Guestbook IndexViewModel = new Guestbook();
            IndexViewModel.ForPaging = new ForPaging(page);
            IndexViewModel.DataList = guestbookService.GetAllGuestbooks(IndexViewModel.ForPaging,Search);
            return View(IndexViewModel);
        }

        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Add(Gbook Data)
        {
            guestbookService.InsertNewGuestbook(Data);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            Gbook Data = guestbookService.GetDataById(Id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult Edit(Gbook EditData)
        {
            guestbookService.EditGuestbook(EditData);
            return RedirectToAction("Index");
        }

        public ActionResult Reply(int Id)
        {
            Gbook Data = guestbookService.GetDataById(Id);
            return View(Data);
        }
        [HttpPost]
        public ActionResult Reply(Gbook ReplyData)
        {
            guestbookService.ReplyGuestbook(ReplyData);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            guestbookService.DeleteGuestbook(Id);
            return RedirectToAction("Index");
        }
    }
}