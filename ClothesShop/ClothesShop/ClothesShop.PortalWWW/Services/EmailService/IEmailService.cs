﻿
namespace ClothesShop.PortalWWW.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}
