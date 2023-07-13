using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.Hosting;

namespace _2023_MacNETCore_API.Models
{
    public class Employees
    {
        public Employees()
        {
        }

        [Key]
        public int id_num { get; set; }

        public string? fname { get; set; }

        public string? lname { get; set; }

        public int age { get; set; }

        public char? gender { get; set; }

        public DateTime? hire_date { get; set; }

        public Decimal salary { get; set; }

        public int Job_id { get; set; }

        public int Manager_ID { get; set; }

        public int Dept_id { get; set; }

        public string? phone { get; set; }

        public string? email { get; set; }

        public int ssn { get; set; }

        public int Address_id { get; set; }

        public int Loc_id { get; set; }

        // Lazy Load:

        [ForeignKey("Job_id")]
        public virtual Jobs? Job { get; set; }

        [ForeignKey("Manager_id")]
        public virtual Managers? Manager { get; set; }

        [ForeignKey("Dept_id")]
        public virtual Departments? Department { get; set; }

        [ForeignKey("Address_id")]
        public virtual Address? Address { get; set; }

        [ForeignKey("Loc_id")]
        public virtual Worklocation? Worklocation { get; set; }

    }
}
