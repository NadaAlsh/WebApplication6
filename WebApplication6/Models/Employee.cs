﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication6.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CivilId { get; set; }

        public string Position { get; set; }
        [Required]
        public int BankBranchId { get; set; }
        [Required]
        public BankBranch BankBranch { get; set; }
    }
}

