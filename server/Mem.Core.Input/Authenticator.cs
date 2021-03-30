using System;

namespace Mem.Core.Input
{
    internal class Authenticator : InputHandler
    {
        private readonly ICharService service;

        public Authenticator(ICharService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));

            this.Action[UserStatus.Connected] = this.InitialPrompt;
            this.Action[UserStatus.GetLogin] = this.GetLogin;
            this.Action[UserStatus.GetPassword] = this.GetPassword;

            this.Prompt[UserStatus.GetLogin] = "Введите имя вашего персонажа, или 'создать'";
            this.Prompt[UserStatus.GetPassword] = "Введите пароль";
        }

        private void InitialPrompt(User user, string input)
        {
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetLogin]);
        }

        private void GetLogin(User user, string input)
        {
            if (input.Equals("создать", StringComparison.OrdinalIgnoreCase))
            {
                user.Status = UserStatus.Create;
                this.Next?.Handle(user, string.Empty);
            }
            else
            {
                this.ProcessLogin(user, input);
            }
        }

        private void ProcessLogin(User user, string input)
        {
            if (this.service.TryLoadCredentials(input, out var creds))
            {
                user.Credentials = creds;
                user.Output.Append(this.Prompt[user.Status = UserStatus.GetPassword]);
            }
            else
            {
                user.Output.Append("Персонаж с таким именем не найден.<br>");
                this.InitialPrompt(user, string.Empty);
            }
        }

        private void GetPassword(User user, string input)
        {
            if (user.Credentials.Password.Equals(input, StringComparison.Ordinal))
            {
                user.Status = UserStatus.Launch;
                this.Next?.Handle(user, string.Empty);
            }
            else
            {
                user.Output.Append("Неверный пароль.<br>");
                this.InitialPrompt(user, string.Empty);
            }
        }
    }
}
