# Dotnet-Mail-Sender.Api
This is a minimal api for sending e-mails with dotnet core 7.0
<hr />

To use it with Gmail you must follow these steps:    
1. Turn on 2 step verification in your account by following these steps [here](https://support.google.com/accounts/answer/185839);
2. Create and generate an app password by clicking [here](https://myaccount.google.com/apppasswords);
3. Edit appsettings.json replacing `username` with your e-mail and `password` with your new generated app password.

## Running with docker

### Dev certificate
In order to run the application on docker you need to have a dev certificate, if you don't have one, create as follows:
```shell
PS C:\> dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p <YOUR_CERT_PASSWORD>
PS C:\> dev-certs https --trust
```   

### Building image and running as container

```shell
PS C:\Path\To\MailSender.Api> docker build -t mailsender-dotnet-api -f MailSender.Api/Dockerfile .    
PS C:\MailSender.Api> docker run --rm -it -p 5001:443 -e ASPNETCORE_URLS=https://+ -e ASPNETCORE_HTTPS_PORT=5001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="<YOUR_CERT_PASSWORD>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -e ASPNETCORE_ENVIRONMENT=Development -v $env:APPDATA\Microsoft\UserSecrets:\root\.microsoft\usersecrets:ro -v $env:USERPROFILE\.aspnet\https:/https/ mailsender-dotnet-api
```

_ps._ Replace `<YOUR_CERT_PASSWORD>` with a real password.