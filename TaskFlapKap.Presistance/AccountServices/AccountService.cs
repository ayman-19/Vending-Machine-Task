using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Servicedto;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.Helper;

namespace TaskFlapKap.Presistance.AccountServices
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork dbContext;
		private readonly UserManager<User> userManager;
		private readonly Jwt _jwt;

		public AccountService(IUnitOfWork dbContext, UserManager<User> userManager, IOptions<Jwt> jwt)
		{
			this.dbContext = dbContext;
			this.userManager = userManager;
			_jwt = jwt.Value;
		}
		public async Task<UserResponse> LoginAsync(Login dto)
		{
			var user = await dbContext.Users.GetAsync(u => u.UserName!.Equals(dto.UserName));
			if (user is null || !await userManager.CheckPasswordAsync(user, dto.Password))
				return new UserResponse { Massage = "Email Or Password InValed!" };

			var createToken = await CreateAccessTokenAsync(user);
			return new UserResponse
			{
				Accesstoken = (new JwtSecurityTokenHandler().WriteToken(createToken)),
				Deposit = user.Deposit,
				IsAuthenticated = true,
				Role = (await userManager.GetRolesAsync(user)).First(),
				UserName = dto.UserName,
			};
		}

		public async Task<UserResponse> RegisterAsync(Register dto)
		{
			User user = new User
			{
				UserName = dto.UserName,
				Email = $"{dto.UserName}@gmail.com",
				Deposit = 0,
				Role = dto.Role,
			};

			var result = await userManager.CreateAsync(user, dto.Password);
			if (!result.Succeeded)
			{
				var errors = string.Empty;
				foreach (var error in result.Errors)
					errors += $"{error},";
				return new UserResponse { Massage = errors };
			}
			await dbContext.CommitAsync();

			await userManager.AddToRoleAsync(user, dto.Role.ToString());
			var getAccessToken = await CreateAccessTokenAsync(user);
			var token = new JwtSecurityTokenHandler().WriteToken(getAccessToken);


			return new UserResponse
			{
				Accesstoken = token,
				IsAuthenticated = true,
				UserName = dto.UserName,
				Role = dto.Role.ToString(),
			};
		}
		public async Task<UserResponse> UpdateAsync(string id, UpdateUserdto dto)
		{
			var user = await dbContext.Users.GetAsync(u => u.Id == id, astracking: false);
			user.Deposit = dto.Deposit;
			user.UserName = dto.UserName;
			user.Email = $"{user.Email}@gmail.com";
			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				var errors = string.Empty;
				foreach (var error in result.Errors)
					errors += $"{error},";
				return new UserResponse { Massage = errors };
			}
			await dbContext.CommitAsync();
			var getAccessToken = await CreateAccessTokenAsync(user);
			var token = new JwtSecurityTokenHandler().WriteToken(getAccessToken);
			return new UserResponse
			{
				Accesstoken = token,
				IsAuthenticated = true,
				Deposit = dto.Deposit,
				UserName = dto.UserName,
				Role = (await userManager.GetRolesAsync(user)).First()
			};
		}
		private async Task<JwtSecurityToken> CreateAccessTokenAsync(User user)
		{
			var userClaims = await userManager.GetClaimsAsync(user);
			var roleUser = await userManager.GetRolesAsync(user);
			var roleUserClaims = new List<Claim>();
			foreach (var role in roleUser)
				roleUserClaims.Add(new Claim(ClaimTypes.Role, role));

			var Claims = new List<Claim>
			{
					new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.PrimarySid,user.Id),
			};
			Claims.AddRange(roleUserClaims);
			Claims.AddRange(userClaims);
			var symetreckey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signCreadential = new SigningCredentials(symetreckey, SecurityAlgorithms.HmacSha256);
			return new JwtSecurityToken(issuer: _jwt.Issuer, audience: _jwt.Audience, claims: Claims, signingCredentials: signCreadential, expires: DateTime.Now.AddMonths((int)_jwt.AccessTokenExiretionDate));
		}
	}
}
