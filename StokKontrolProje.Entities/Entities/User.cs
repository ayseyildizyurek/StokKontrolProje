﻿using StokKontrolProje.Entities.Entities.Enums;

namespace StokKontrolProje.Entities.Entities
{
	public class User:BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public List<Order> Orders { get; set; }
        public User()
        {
            Orders = new List<Order>();
        }
    }
}
