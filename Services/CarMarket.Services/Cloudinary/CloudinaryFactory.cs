﻿namespace CarMarket.Services.Cloudinary
{
    using CloudinaryDotNet;

    using Microsoft.Extensions.Configuration;

    public class CloudinaryFactory
    {
        public static Cloudinary GetInstance(IConfiguration configuration)
        {
            var cloud = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:ApiKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            Account account = new Account(cloud, apiKey, apiSecret);
            Cloudinary instance = new Cloudinary(account);
            return instance;
        }
    }
}
