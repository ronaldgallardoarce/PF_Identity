using Identity.Application.Contracts;
using Identity.Application.Contracts.Models;

namespace Identity.Application.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string GenerateChangePasswordEmail()
        {
            return "Your password has been successfully changed";
        }

        public string GenerateRegisterUserEmail(string userName, int code, string email)
        {
            return $@"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Verify Your Email</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                margin: 0;
                                padding: 0;
                                background-color: #f5f5f5;
                            }}
                            .email-container {{
                                background-color: #ffffff;
                                max-width: 600px;
                                margin: 20px auto;
                                border-radius: 8px;
                                overflow: hidden;
                                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                            }}
                            .email-header {{
                                background-color: #ff6a00;
                                color: #ffffff;
                                text-align: center;
                                padding: 20px;
                            }}
                            .email-header img {{
                                max-width: 100px;
                                margin-bottom: 2px;
                            }}
                            .email-body {{
                                padding: 20px;
                                text-align: center;
                                color: #333333;
                            }}
                            .email-body h1 {{
                                margin: 0;
                                font-size: 24px;
                            }}
                            .email-body p {{
                                font-size: 16px;
                                margin: 10px 0;
                            }}
                            .email-body a {{
                                display: inline-block;
                                margin: 20px 0;
                                padding: 10px 20px;
                                background-color: #ff6a00;
                                color: #ffffff;
                                text-decoration: none;
                                font-size: 18px;
                                border-radius: 5px;
                            }}
                            .email-footer {{
                                background-color: #f5f5f5;
                                text-align: center;
                                padding: 10px;
                                font-size: 12px;
                                color: #888888;
                            }}
                            .email-footer a {{
                                color: #007bff;
                                text-decoration: none;
                            }}
                            .social-icons {{
                                margin: 10px 0;
                            }}
                            .social-icons img {{
                                width: 32px;
                                margin: 0 5px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='email-container'>
                            <div class='email-header'>
                                <img src='https://t3.ftcdn.net/jpg/04/87/55/66/360_F_487556662_gYeUPwaDaq2cUnO8OMaYhyRgbcPUevZe.jpg' alt='Email Icon'>
                                <h1>Verify your email address</h1>
                            </div>
                            <div class='email-body'>
                                <h1>Welcome {userName}</h1>
                                <p>This is your code to verify your account</p>
                                <p>{code}</p>
                                <p>Please click the button below to confirm your email address and activate your account.</p>
                                <a href='http://localhost:7187/api/Auth/ConfirmEmail?email={email}&code={code}'>CONFIRM EMAIL</a>
                                <p>If you received this in error, simply ignore this email and do not click the button.</p>
                            </div>
                            <div class='email-footer'>
                                <div class='social-icons'>
                                    <a href='#'><img src='https://cdn-icons-png.flaticon.com/256/124/124010.png' alt='Facebook'></a>
                                    <a href='#'><img src='https://static.vecteezy.com/system/resources/previews/018/930/695/non_2x/twitter-logo-twitter-icon-transparent-free-free-png.png' alt='Twitter'></a>
                                </div>
                                <p>Copyright © 2025, UPDS</p>
                                <p>If you do not wish to receive these emails, <a>unsubscribe here</a>.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
        }
    }
}
