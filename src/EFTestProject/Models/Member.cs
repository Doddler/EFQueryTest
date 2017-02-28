using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFTestProject.Models
{
    public class Member
    {
		public int Id { get; set; }
		public int Family { get; set; }
		public bool IsMember { get; set; }
		public DateTime? DeceasedDate { get; set; }
		public int? LivesWithId { get; set; }
		public Member LivesWith { get; set; }
		public ICollection<Member> LivingWith { get; set; }
	}
}
