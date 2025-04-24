using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using UserService.Data;
using UserService.Helpers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services.Impl
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;

        public AccountServiceImpl (IAccountRepository accountRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor, IMapper mapper, Cloudinary cloudinary)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = contextAccessor;
            _mapper = mapper;
            _cloudinary = cloudinary;
        }

        private async Task<string> GenerateJwtToken(string phoneNumber)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddMinutes(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> AssignRoleAsync(ApplicationUser user, string role)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            var result = await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        public async Task<bool> ChangePasswordAsync(string phoneNumber, string currentPassword, string newPassword)
        {
            return await _accountRepository.ChangePasswordAsync(phoneNumber, currentPassword, newPassword);
        }

        public async Task<IdentityResult> CreateAdminAsync(SignUpModel admin)
        {
            //var user = _mapper.Map<ApplicationUser>(admin);
            //var result = await _userManager.CreateAsync(user, admin.Password);
            //if (result.Succeeded)
            //{
            //    await _userManager.AddToRoleAsync(user, StaticEntities.UserRoles.Admin);
            //    await _accountRepository.CreateAdmin(admin);
            //}
            //return result;
            return await _accountRepository.CreateAdmin(admin);
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await _accountRepository.SignInAsync(model.PhoneNumber, model.Password);
            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var token = GenerateJwtToken(model.PhoneNumber);
            return await token;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            return await _accountRepository.SignUpAsync(model);
        }

        public async Task<UserModel> GetCurrentUserAsync()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User;

            if (userClaims == null || !userClaims.Identity.IsAuthenticated)
            {
                throw new Exception("User is not authenticated");
            }

            var phoneNumber = userClaims.FindFirst(ClaimTypes.MobilePhone)?.Value;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new Exception("Phone number is not found");
            }

            var user = await _accountRepository.GetCurrentUserAsync(phoneNumber);

            return user == null ? null : _mapper.Map<UserModel>(user);
        }

        public async Task<string> UpdateAvatarAsync(UserAvatar avatar)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == avatar.PhoneNumber);
            if (user == null) throw new Exception("User not found");
            
            if (avatar.Avatar == null || avatar.Avatar.Length == 0)
                throw new Exception("No file provided");

            await using var stream = avatar.Avatar.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(avatar.Avatar.FileName, stream),
                Folder = "avatars",
                PublicId = user.Id,
                Transformation = new Transformation()
            };
            
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.StatusCode != HttpStatusCode.OK)
                throw new Exception("Upload failed"); 
            
            var result = await _accountRepository.UpdateAvatarAsync(user.PhoneNumber, uploadResult.SecureUrl.AbsoluteUri);
            if (!result) throw new Exception("Update failed");
            
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
