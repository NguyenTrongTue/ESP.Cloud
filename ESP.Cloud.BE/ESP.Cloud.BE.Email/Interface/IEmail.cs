namespace ESP.Cloud.BE.Email.Interface
{
    public interface IEmail
    {
        void SendMail(EmailDto emailDto);
    }
}
