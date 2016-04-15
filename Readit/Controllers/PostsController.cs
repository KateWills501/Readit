using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Readit.Models;

namespace Readit.Controllers
{
    public class PostsController : Controller
    {
        private ReaditDbContext db = new ReaditDbContext();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.OrderByDescending(x => x.UpCount - x.DownCount).ToList();
            var postsVMs = posts.Select((p, i) => new PostVM()
            {
                Author = p.Author,
                ExternalLink = p.ExternalLink,
                TimeSinceCreation = Math.Round(DateTime.Now.Subtract(p.CreateDate).TotalHours),
                Score = p.UpCount - p.DownCount,
                Title = p.Title,
                PostId = p.Id,
                Rank = i + 1,
                NumOfComments = p.Comments.Count()
            }).ToList()
            ;
            return View(postsVMs);
        }


        public ActionResult CommentList(int postId)
        {
            Post post = db.Posts.Find(postId);
            if (post == null)
            {
                return HttpNotFound();
            }

            var comments = post.Comments.OrderByDescending(x => x.UpCount - x.DownCount).ToList();

            var commentVMs = comments.Select(p => new CommentVM()
            {
                Author = p.Author,
                TimeSinceCreation = Math.Round(DateTime.Now.Subtract(p.CreateDate).TotalHours),
                Score = p.UpCount - p.DownCount,
                CommentId = p.Id,
                Body = p.Body
            }).ToList();

            return PartialView(commentVMs);
        }

        [HttpPost]
        public ActionResult AddComment(Comment newComment, int postId)
        {
            newComment.CreateDate = DateTime.Now;
            newComment.Post = db.Posts.Find(postId);
            if (newComment.Post == null)
            {
                return RedirectToAction("Index");
            }

            db.Comments.Add(newComment);
            db.SaveChanges();
            return RedirectToAction("Index"); //TODO: Direct to Details/ID
        }


        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.TimeSinceCreation = Math.Round(DateTime.Now.Subtract(post.CreateDate).TotalHours);

            var model = new PostDetailVM()
            {
                Author = post.Author,
                ExternalLink = post.ExternalLink,
                TimeSinceCreation = Math.Round(DateTime.Now.Subtract(post.CreateDate).TotalHours),
                Title = post.Title,
                Body = post.Body,
                PostId = post.Id,
                NumOfComments = post.Comments.Count
            };

            return View(model);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Body,ExternalLink,Author")] Post post)
        {
            if (post.Body != null || post.ExternalLink != null)
            {
                if (ModelState.IsValid)
                {
                    post.CreateDate = DateTime.Now;
                    db.Posts.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(post);
        }

        [HttpPost]
        public ActionResult Vote(int postid, bool up)
        {
            var post = db.Posts.Find(postid);
            if (post == null)
            {
                return Content("nope");
            }
            if (up)
            {
                post.UpCount++;
            }
            else
            {
                post.DownCount++;
            }

            db.SaveChanges();
            return Content((post.UpCount - post.DownCount).ToString());
        }

        //// GET: Posts/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Title,Body,ExternalLink,CreateDate,Author")] Post post)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(post).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(post);
        //}

        //// GET: Posts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = db.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        //// POST: Posts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Post post = db.Posts.Find(id);
        //    db.Posts.Remove(post);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
