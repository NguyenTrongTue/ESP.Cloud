using ESP.Cloud.BE.Core.DL;
using ESP.Cloud.BE.Email;
using ESP.Cloud.BE.Email.Interface;
using Microsoft.Extensions.Logging;
using Quartz;

namespace ESP.Cloud.BE.Jobs;

[DisallowConcurrentExecution]
public class LoggingBackgroundJob : IJob
{
    private readonly ILogger<LoggingBackgroundJob> _logger;
    private readonly IBookingDL _bookingDL;
    private readonly IEmail _email;

    public LoggingBackgroundJob(ILogger<LoggingBackgroundJob> logger, IBookingDL bookingDL, IEmail email)
    {
        _logger = logger;
        _bookingDL = bookingDL;
        _email = email;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var result = await _bookingDL.CheckBooking();
            result.ForEach(booking =>
            {
                var request = new EmailDto()
                {
                    Subject = "Thông báo",
                    To = booking.email,
                    Body = "Có bạn lịch sử xe nhé!"
                };

                _email.SendMail(request);
            });
            _logger.LogInformation("{UtcNow}", DateTime.UtcNow);
        }
        catch (Exception)
        {
            throw;
        }
    }
}