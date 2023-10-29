﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tarefas.API.DTO;

namespace Tarefas.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AutorizaController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IConfiguration _configuration;

		public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_configuration = configuration;
		}

		[HttpGet]
		public ActionResult<string> Get()
		{
			return "AutorizaController:: Acessado em : " + DateTime.Now;
		}
		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] UsuariosDTO model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
			}
			
			var user = new IdentityUser
			{
				UserName = model.email,
				Email = model.email,				
				EmailConfirmed = true 
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return BadRequest(result.Errors);
			}

			await _signInManager.SignInAsync(user, isPersistent: false);
			return Ok(model);
		}
		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] UsuariosDTO userInfo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
			}

			var result = await _signInManager.PasswordSignInAsync(userInfo.email, userInfo.Password,
									isPersistent: false, lockoutOnFailure: false);	
			
			if (result.Succeeded)
			{
				return Ok(GeraToken(userInfo));
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Credênciais Inválidas.");
				return BadRequest(ModelState);
			}
		}
		private UsuarioToken GeraToken(UsuariosDTO userInfo)
		{
			//Gera uma chave com base em algorítmo simétrico baseada na chave privada passada no AppSettings.json
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

			//Gera a assinatura digital do token usando a chave privada passada no AppSettings.json
			var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//Define o tempo de expiração do Token
			var expiracao = _configuration["TokenConfiguration:ExpireHours"];
			var _expiracao = DateTime.UtcNow.AddHours(double.Parse(expiracao));

			//classe que representa um token JWT e gera o token
			JwtSecurityToken token = new JwtSecurityToken(
				issuer: _configuration["TokenConfiguration:Audience"],
				audience: _configuration["TokenConfiguration:Audience"],
				expires: _expiracao,
				signingCredentials: credenciais);
			return new UsuarioToken()
			{
				Autheticated = true,
				Token = new JwtSecurityTokenHandler().WriteToken(token),
				Expiration = _expiracao,
				Message = "Token OK"
			};
		}
	}
}