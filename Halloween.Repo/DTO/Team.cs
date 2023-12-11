using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halloween.Repo.DTO
{
    public class Team
    {
        [Key]
        public int Id {  get; set; }
        public string? TeamName { get; set; } = string.Empty;
        public List<SuperHero>? SuperHeros { get; set; }

    }
}
