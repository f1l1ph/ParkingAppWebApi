﻿using ParkingAppWebApi.Models;

namespace ParkingAppWebApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}