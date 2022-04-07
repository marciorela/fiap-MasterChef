﻿using MasterChef.Domain.Dto;
using MasterChef.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MasterChef.Api.Controllers
{
    [Route("api/auth")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _config;
        public AuthenticateController(IConfiguration config)
        {
            _config = config;
        }

        private bool Atutenticado(Auth authParam)
        {
            if ((authParam.ClientId != null) && (authParam.Secret != null))
            {
                return
                    ((authParam.ClientId == this._config["Auth:ClientId"]) &&
                     (authParam.Secret == this._config["Auth:Secret"]));
            }

            return false;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] Auth authParam)
        {
            try
            {
                if (!Atutenticado(authParam))
                {
                    return Unauthorized("Dados não autenticados!");
                }

                var tempoExpiracao = Convert.ToInt32(this._config["Token:TempoExpiracaoTokenMinutos"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKeyBytes = Encoding.ASCII.GetBytes(this._config["Token:SecretKey"]);
                var Expires = DateTime.UtcNow.AddMinutes(tempoExpiracao);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Sid, authParam.Id.ToString()), }),
                    Expires = Expires,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new AuthResponse(token: tokenHandler.WriteToken(token), startedAt: DateTime.UtcNow, expiresIn: (int)TimeSpan.FromMinutes(tempoExpiracao).TotalSeconds));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
