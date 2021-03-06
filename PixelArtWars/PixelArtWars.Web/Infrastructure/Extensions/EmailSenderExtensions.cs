using System.Text.Encodings.Web;
using System.Threading.Tasks;
using PixelArtWars.Services.Interfaces;

namespace PixelArtWars.Web.Infrastructure.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{link}'>link</a>");
        }
    }
}
