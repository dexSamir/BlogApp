﻿using System;
using BlogApp.Core.Entities;

namespace BlogApp.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
	Task<User?> GetUserByUsernameAsync(string name); 
}
