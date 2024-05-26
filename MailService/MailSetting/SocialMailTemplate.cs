namespace MailService.MailSetting;

public class SocialMailTemplate
{
    public static string EmailTemplateForOtp(string otpCode,string title)
    {
        return $$"""
                 <!DOCTYPE html>
                 <html lang="en">

                 <head>
                     <meta charset="UTF-8">
                     <meta name="viewport" content="width=device-width, initial-scale=1.0">
                     <title>Mail</title>
                     <style>
                         .mainContainer {
                             min-width: 1000px;
                             overflow: auto;
                             line-height: 2;
                             /* background-color: #FFF9D0; */
                         }
                         .subContainer{
                             margin: 50px auto;
                             width: 70%;
                             padding: 20px 0;
                         }
                         .headerContainer {
                             border-bottom: 1px solid #000;
                         }
                         .headerTitle{
                             font-size: 1.5rem;
                             color: #5AB2FF;
                             text-decoration: none;
                             font-weight: 600;
                         }
                         .otpBox {
                             background-color: #5AB2FF;
                             margin: 0 auto;
                             width: max-content;
                             padding: 0 10px;
                             color: #fff;
                             border-radius: 10px;
                         }
                         .line {
                             border: none;
                             border-top: 1px solid #000;
                         }
                         .footer {
                             float: right;
                             padding: 8px;
                             color: #aaa;
                             font-size: 0.8rem;
                             font-weight: 300;
                         }
                     </style>
                 </head>

                 <body>
                     <div class="mainContainer">
                         <div class="subContainer">
                             <div class="headerContainer">
                                 <a href="" class="headerTitle">{{title}}</a>
                             </div>
                             <p >Thank you for choosing our brand. Use the following OTP to complete your Sign Up. OTP will be expire in
                                 5 minutes</p>
                             <h2 class="otpBox">{{otpCode}}</h2>
                             <p>With Best Regards </br> Coffee!</p>
                             <hr class="line">
                             <div class="footer">
                                 <p>Brand Name</p>
                                 <p>Address</p>
                                 <p>Contact Number</p>
                             </div>
                 
                         </div>
                     </div>
                 </body>

                 </html>
                 """;
    }
}