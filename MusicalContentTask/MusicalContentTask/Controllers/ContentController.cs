using MusicalContentTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Configuration;
using MusicalContentTask.ViewModels;
namespace MusicalContentTask.Controllers
{
    public class ContentController : Controller
    {
       
        public ActionResult start( )
        {
           
            return View();
        }
       
        // GET: Content
        public ActionResult uploadcontent()
        {
           
            string username =User.Identity.GetUserName();
            ApplicationDbContext context = new ApplicationDbContext();
            var userId = context.Registeredusers
                         .Where(m => m.Name == username)
                         .Select(m => m.Id)
                         .SingleOrDefault();
           ViewBag.id= userId;
            return View("UploadForm");
          
        }
        [HttpPost]
        public ActionResult savecontent(Content content,HttpPostedFileBase songimg, HttpPostedFileBase idimg,HttpPostedFileBase songmp3)
        {

            ApplicationDbContext context = new ApplicationDbContext();
            int studentid = content.Registereduserid;
            if (songimg != null && songimg.ContentLength > 0)
            {
                
                string filename = Path.GetFileName(songimg.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/UserImages/"), filename);
                songimg.SaveAs(imgpath);
            }
            if (idimg != null && songimg.ContentLength > 0)
            {
                string filename2 = Path.GetFileName(idimg.FileName);
                string imgpath2 = Path.Combine(Server.MapPath("~/UserImages/"), filename2);
                idimg.SaveAs(imgpath2);
            }
            
            if (songmp3 != null && songimg.ContentLength > 0)
            {
                string filename3 = Path.GetFileName(songmp3.FileName);
                string audiopath = Path.Combine(Server.MapPath("~/UserAudios/"), filename3);
                songmp3.SaveAs(audiopath);
            }
            content.contentimg = "~/UserImages/" + songimg.FileName;
            content.idimg = "~/UserImages/" + idimg.FileName;
            content.contentfile = "~/UserAudios/" + songmp3.FileName;
            context.Contents.Add(content);
            context.SaveChanges();
            ViewData["id"] = "The song" + content.songname + "is added successfully !";

            return RedirectToAction("uploadedcontent", "Content");
        }
        public ActionResult uploadedcontent()
        {
            
            string username = User.Identity.GetUserName();
            ApplicationDbContext context = new ApplicationDbContext();
            var userId = context.Registeredusers
                         .Where(m => m.Name == username)
                         .Select(m => m.Id)
                         .SingleOrDefault();

          var contentlist = from c in context.Contents
                          where c.Registereduserid == userId
                          select c;
                         
           ;
          if (!contentlist.Any())
          {
             
              return View("EmptyMusic");
          }
          return View(contentlist.ToList());
        }
        public ActionResult alluploadedcontent()
        {
         
            string username = User.Identity.GetUserName();
            ApplicationDbContext context = new ApplicationDbContext();
         
            var viewModel =
                      from c in context.Contents
                     join r in context.Registeredusers on c.Registereduserid equals r.Id
                     select new UserContent { contents =c , users=r };


            if (!viewModel.Any())
                     {

                         return View("EmptyMusic");
                     }
                     return View(viewModel);
         
        }
    }
}