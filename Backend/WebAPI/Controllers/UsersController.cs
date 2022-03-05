using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        IUserService _userservice;

        public UsersController(IUserService userService)
        {
            _userservice = userService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userservice.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userservice.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userservice.Add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            var result = _userservice.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userservice.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("avatar/add")]
        public IActionResult AvatarAdd([FromForm] UserForAvatarUploadDto userForAvatarUploadDto)
        {
            var result = _userservice.AvatarAdd(userForAvatarUploadDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("avatar/update")]
        public IActionResult AvatarUpdate([FromForm]  UserForAvatarUploadDto userForAvatarUploadDto)
        {
            var result = _userservice.AvatarUpdate(userForAvatarUploadDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("avatar/delete")]
        public IActionResult AvatarDelete(UserForAvatarUploadDto userForAvatarUploadDto)
        {
            var result = _userservice.AvatarDelete(userForAvatarUploadDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("password")]
        public IActionResult PasswordUpdate(UserForPasswordUpdateDto userForPasswordUpdateDto)
        {
            var result = _userservice.PasswordUpdate(userForPasswordUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
