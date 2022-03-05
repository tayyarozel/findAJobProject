using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class User:IEntity
    {
        public int Id { get; set; }
        public string GenderId { get; set; }
        public int CityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }

    }
}
