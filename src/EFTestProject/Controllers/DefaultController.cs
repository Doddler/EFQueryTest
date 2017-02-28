using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFTestProject.Data;
using EFTestProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EFTestProject.Controllers
{
	public class DefaultController : Controller
	{
		private readonly ApplicationDbContext context;

		public DefaultController(ApplicationDbContext context)
		{
			this.context = context;
		}

		public IActionResult Index()
		{
			if (!context.Members.Any())
			{
				Seed();
				return Content("Db seeded, reload to test");
			}
			
			//You'd expect with this query, you'd get a list ordered by Family
			//But that's not the case. 

			var q = context.Members.Include(m => m.LivingWith).AsQueryable();

			q = q.Where(m => m.IsMember || m.LivingWith.Any());
			q = q.OrderBy(m => m.Family);
			q = q.Skip(100);
			q = q.Take(100);

			var result = q.ToList();

			return View(result); //view simply outputs family ids
		}

		private void Seed()
		{
			var list = new List<Member>(); //hold onto this for a bit so we can update the relationships

			var rnd = new Random();
			var familyids = Enumerable.Range(0, 500).OrderBy(a => Guid.NewGuid()).ToList(); //randomize list of family ids

			for (var i = 0; i < 500; i++)
			{
				var m = new Member();
				
				m.Family = familyids[i]; //familyid is random

				m.IsMember = (rnd.Next(1, 10) < 8);

				if (i % 3 == 1) //every 3rd person is dead
					m.DeceasedDate = DateTime.Now;

				context.Add(m);

				list.Add(m);
			}

			context.SaveChanges(); //save our entities

			for (var i = 1; i < 500; i += 2) //we do a second loop cause EF doesn't seem to like setting foreign key on insert... I guess
			{
				list[i].LivesWith = list[i - 1]; //in theory, we set each one to live with the one before it. But the ids are all a bit weird.
			}

			context.SaveChanges();
		}
	}
}
