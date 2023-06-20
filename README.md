# Dotnet-Mail-Sender.Api
This is a minimal api for sending e-mails with dotnet core 7.0
<hr />

## Configuration
To use it with Gmail you must follow these steps:    
1. Turn on 2 step verification in your account by following these steps [here](https://support.google.com/accounts/answer/185839);
2. Create and generate an app password by clicking [here](https://myaccount.google.com/apppasswords);
3. Edit appsettings.json replacing `username` with your e-mail and `password` with your new generated app password.

## Running on Docker
1. In order to run the application on docker you need to have a dev certificate, if you don't have one, create as follows:
```shell
PS C:\> dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <CERT_PASSWORD>
PS C:\> dev-certs https --trust
```   

2. Edit the _.env_ file with the <span style="color:orange"><CERT_PASSWORD></span> you put on step before
3. On the root folder, run: 
```shell 
PS C:\MailSender.Api> docker compose up
```