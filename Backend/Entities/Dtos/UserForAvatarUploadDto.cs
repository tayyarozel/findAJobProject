using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserForAvatarUploadDto:IDto
    {
        public int UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
