﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetBet.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public decimal Balance { get; set; }

        
    }
}