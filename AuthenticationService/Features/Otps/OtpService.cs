using DatabaseService.AppContextModels;
using DatabaseService.DataModels.OtpLogs;
using MailService.MailSetting;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Extensions;
using Shared.Models.Otps;

namespace AuthenticationService.Features.Otps;

public class OtpService : IOtpService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ISocialMailService _mailService;

    public OtpService(AppDbContext context, ISocialMailService mailService, IConfiguration configuration)
    {
        _context = context;
        _mailService = mailService;
        _configuration = configuration;
    }

    public async Task<OtpResponseModel> GetOtp(OtpRequestModel request, CancellationToken ct)
    {
        OtpResponseModel model = new();

        if (request.Email.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email && !x.IsDeleted, ct);

        if (user is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        var otpCodeModel = StringsExtension.GenerateOtp();

        SocialEmailModel emailModel = new()
        {
            From = _configuration.GetSection("MailSetting:From").Value,
            To = user.Email,
            Subject = EmailConstants.ForgetPasswordOtp,
            Body = SocialMailTemplate.EmailTemplateForOtp(otpCodeModel.OtpCode, EmailConstants.ForgetPasswordOtp),
            Host = _configuration.GetSection("MailSetting:Host").Value,
            UserName = _configuration.GetSection("MailSetting:UserName").Value,
            Password = _configuration.GetSection("MailSetting:Password").Value
        };

        var success = await _mailService.SendAsync(emailModel, ct);

        if (!success)
        {
            model.Response.Set(ResponseConstants.E0001);
            return model;
        }

        OtpLog otpLog = new()
        {
            Otp = otpCodeModel.OtpCode,
            UserId = user.UserId,
            OtpExpires = DateTime.Now.AddMinutes(5)
        };

        await _context.OtpLogs.AddAsync(otpLog, ct);
        var result = await _context.SaveChangesAsync(ct);
        if (result <= 0)
        {
            model.Response.Set(ResponseConstants.E0000);
            return model;
        }

        model.OtpExpires = otpCodeModel.OptExpires;
        model.ExpireType = ExpireConstants.Minute;
        model.Response.Set(ResponseConstants.S0000);
        return model;
    }

    public async Task<ValidateOtpResponseModel> ValidateOtp(ValidateOtpRequestModel request, CancellationToken ct)
    {
        ValidateOtpResponseModel model = new();

        if (request.OtpCode.IsNullOrEmpty())
        {
            model.Response.Set(ResponseConstants.W0000);
            return model;
        }

        var otpData = await _context.OtpLogs.FirstOrDefaultAsync(x => x.Otp == request.OtpCode, ct);

        if (otpData is null)
        {
            model.Response.Set(ResponseConstants.W0002);
            return model;
        }

        var valid = otpData.OtpExpires > DateTime.Now;

        var response = valid ? ResponseConstants.S0000 : ResponseConstants.W0004;

        model.Response.Set(response);
        return model;
    }
}