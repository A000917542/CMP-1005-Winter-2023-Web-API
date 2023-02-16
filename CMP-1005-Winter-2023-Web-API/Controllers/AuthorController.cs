using CMP_1005_Winter_2023_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace CMP_1005_Winter_2023_Web_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private static ICollection<Author> Authors = new List<Author>() { new Author() { Id = 1, FullName = "Steven King" }, new Author() { Id = 2, FullName = "Brandon Sanderson" }, new Author() { Id = 3, FullName = "Michael Crichton" } };

        public AuthorController()
        {
        }

        [HttpGet]
        public ICollection<Author> Get()
        {
            return Authors;
        }

        [HttpGet("{id?}")]
        public Author? Get(int id)
        {
            return Authors.FirstOrDefault(a => a.Id == id);
        }

        [HttpPost]
        public void Post(Author author)
        {
            Authors.Add(author);
        }

        //[HttpDelete]
        //public void Delete(int id)
        //{
        //    if (Authors.Any(a => a.Id == id))
        //        Authors.Remove(Authors.First(a => a.Id == id));
        //}

        [HttpPut]
        public void Put(Author author)
        {
            if (Authors.Any(a => a.Id == author.Id))
            {
                var auth = Authors.First(a => a.Id == author.Id);
                auth.FullName = author.FullName;
            }
        }

        [HttpOptions]
        public void Options()
        {
            var type = this.GetType();
            var methods = type.GetMethods();
            var attributes = methods.SelectMany(m => m.GetCustomAttributes(false));
            var httpAttributes = attributes.Where(a => a.GetType()?.BaseType?.Name == "HttpMethodAttribute").SelectMany(a => ((HttpMethodAttribute)a).HttpMethods);
            var distinctAttributes = httpAttributes.Distinct();
            var res = distinctAttributes.ToList();

            Response.Headers.Add("Allow", String.Join(", ", res));
            Response.StatusCode = 204;
        }
    }
}
