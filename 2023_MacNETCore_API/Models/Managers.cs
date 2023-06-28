using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _2023_MacNETCore_API.Models
{
    public class Managers
    {
        public Managers()
        {
        }

        [Key]
        public int? Manager_id { get; set; }
        public string? Manager_fname { get; set; }
        public string? Manager_lname { get; set; }
        public int? Manager_emp_no { get; set; }
        public int? Manager_Job_id { get; set; }
        public int? Manager_dept_id { get; set; }
    }
}

