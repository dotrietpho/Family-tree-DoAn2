using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CayGiaPhaAPI.Class;

namespace CayGiaPhaAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class NodesController : ApiController
    {
        private Context db = new Context();

        // GET: api/Nodes
        public IQueryable<Node> GetNode()
        {
            // return db.Node.Include(x => x.persons).Include(x => x.childs.Select(y => y.childs)).Include(x => x.childs.Select(y => y.persons));
            return db.Node.Include(x => x.persons)
                .Include(x => x.childs.Select(y => y.childs))
                .Include(x => x.childs.Select(y => y.persons));
        }

        // GET: api/Nodes/5
        [ResponseType(typeof(Node))]
        public IHttpActionResult GetNode(int id)
        {
            var node = db.Node.Find(id);
            if (node == null)
            {
                return NotFound();
            }
            var nodes = db.Node.Include(x => x.persons)
                .Include(x => x.childs.Select(y => y.childs))
                .Include(x => x.childs.Select(y => y.persons)).ToList();
            var returnNode = nodes.FirstOrDefault(p => p.Id == id);
            return Ok(returnNode);
        }

        // PUT: api/Nodes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNode(int id, Node node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != node.Id)
            {
                return BadRequest();
            }

            db.Entry(node).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Nodes
        [ResponseType(typeof(Node))]
        public IHttpActionResult PostNode(Node node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Node.Add(node);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = node.Id }, node);
        }

        // DELETE: api/Nodes/5
        [ResponseType(typeof(Node))]
        public IHttpActionResult DeleteNode(int id)
        {
            Node node = db.Node.Find(id);
            if (node == null)
            {
                return NotFound();
            }

            db.Node.Remove(node);
            db.SaveChanges();

            return Ok(node);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NodeExists(int id)
        {
            return db.Node.Count(e => e.Id == id) > 0;
        }
        [HttpPut]
        [Route("api/AddChild/{currentId}")]
        public IHttpActionResult AddChild([FromUri]int currentId, [FromBody]Person child)
        {
            var current = db.Person.FirstOrDefault(p => p.personId == currentId);
            var node = db.Node.FirstOrDefault(p => p.Id == current.IdNodeCha);
            if (node == null)
            {
                return NotFound();
            }
            Node a = new Node(node.Id);
            db.Node.Add(a);
            db.SaveChanges();
            Person newPerson = new Person() { personName = child.personName, personGender = child.personGender, personImg = child.personImg, IdNodeCha = a.Id };

            if (string.IsNullOrWhiteSpace(newPerson.personImg))
                if (!string.IsNullOrWhiteSpace(newPerson.personGender))
                {
                    newPerson.personImg = "/UploadedFiles/" + newPerson.personGender + ".png";
                }
                else newPerson.personImg = "/UploadedFiles/male.png";

            db.Person.Add(newPerson);
            db.SaveChanges();
            return Ok(newPerson.personId);
        }

        [HttpPut]
        [Route("api/AddParent/{currentId}")]
        public IHttpActionResult AddParent([FromUri]int currentId, [FromBody]Person parent)
        {
            var current = db.Person.FirstOrDefault(p => p.personId == currentId);
            var node = db.Node.FirstOrDefault(p => p.Id == current.IdNodeCha);
            if (node == null)
            {
                return NotFound();
            }
            //Neu node cha da co person
            if (node.IdNodeCha != null)
            {
                //Them person vao node cha
                Person newPerson = new Person() { personName = parent.personName, personGender = parent.personGender, personImg = parent.personImg, IdNodeCha = node.IdNodeCha };
                if (string.IsNullOrWhiteSpace(newPerson.personImg))
                    if (!string.IsNullOrWhiteSpace(newPerson.personGender))
                    {
                        newPerson.personImg = "/UploadedFiles/" + newPerson.personGender + ".png";
                    }
                    else newPerson.personImg = "/UploadedFiles/male.png";
                db.Person.Add(newPerson);
                db.SaveChanges();
                return Ok(newPerson.personId);
            }
            //Khong co node cha (dang o node cao nhat)
            else
            {
                //Them node cha (them len tren)
                Node a = new Node();
                db.Node.Add(a);
                db.SaveChanges();
                //Them person vao node cha
                Person newPerson = new Person() { personName = parent.personName, personGender = parent.personGender, personImg = parent.personImg, IdNodeCha = a.IdNodeCha };
                if (string.IsNullOrWhiteSpace(newPerson.personImg))
                    if (!string.IsNullOrWhiteSpace(newPerson.personGender))
                    {
                        newPerson.personImg = "/UploadedFiles/" + newPerson.personGender + ".png";
                    }
                    else newPerson.personImg = "/UploadedFiles/male.png";
                db.Person.Add(newPerson);
                db.SaveChanges();
                return Ok(newPerson.personId);
            }
        }

        [HttpPut]
        [Route("api/AddPartner/{currentId}")]
        public IHttpActionResult AddPartner([FromUri]int currentId, [FromBody]Person partner)
        {
            var current = db.Person.FirstOrDefault(p => p.personId == currentId);
            var node = db.Node.FirstOrDefault(p => p.Id == current.IdNodeCha);
            if (node == null)
            {
                return NotFound();
            }
            Person newPerson = new Person() { personName = partner.personName, personGender = partner.personGender, personImg = partner.personImg, IdNodeCha = current.IdNodeCha };
            if (string.IsNullOrWhiteSpace(newPerson.personImg))
                if (!string.IsNullOrWhiteSpace(newPerson.personGender))
                {
                    newPerson.personImg = "/UploadedFiles/" + newPerson.personGender + ".png";
                }
                else newPerson.personImg = "/UploadedFiles/male.png";
            db.Person.Add(newPerson);
            db.SaveChanges();
            return Ok(newPerson.personId);
        }
    }
}