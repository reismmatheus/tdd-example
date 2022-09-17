﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddExample.Data.Context;
using TddExample.Data.Interface;
using TddExample.Domain;
using static TddExample.CrossCutting.Extensions.TokenExtensions;


namespace TddExample.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly TddExampleContext _context;
        public UserRepository(TddExampleContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return default;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return default;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return default;

            return user;
        }

        public async Task<User> AddAsync(string name, string email, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedIn = DateTime.UtcNow,
                Active = true
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public override Task<User?> AddAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
