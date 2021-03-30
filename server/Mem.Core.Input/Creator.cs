using System;

namespace Mem.Core.Input
{
    internal class Creator : InputHandler
    {
        private readonly ICharService service;
        private readonly IMudConfiguration config;

        public Creator(ICharService service, IMudConfiguration config)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
            this.config = config ?? throw new ArgumentNullException(nameof(config));

            this.Action[UserStatus.Create] = this.Create;
            this.Action[UserStatus.GetNameNom] = this.GetNameNom;
            this.Action[UserStatus.GetNameGen] = this.GetNameGen;
            this.Action[UserStatus.GetNameDat] = this.GetNameDat;
            this.Action[UserStatus.GetNameAcc] = this.GetNameAcc;
            this.Action[UserStatus.GetNameIns] = this.GetNameIns;
            this.Action[UserStatus.GetNamePre] = this.GetNamePre;
            this.Action[UserStatus.GetGender] = this.GetGender;
            this.Action[UserStatus.CreatePassword] = this.CreatePassword;
            this.Action[UserStatus.ConfirmPassword] = this.ConfirmPassword;
            this.Action[UserStatus.GetEmail] = this.GetEmail;
            this.Action[UserStatus.ConfirmCheck] = this.ConfirmCheck;

            this.Prompt[UserStatus.GetNameNom] = "Введите имя персонажа в именительном падеже &ndash; здесь стоит кто?";
            this.Prompt[UserStatus.GetNameGen] = "Родительный &ndash; это вещи кого?";
            this.Prompt[UserStatus.GetNameDat] = "Дательный &ndash; дать денег кому?";
            this.Prompt[UserStatus.GetNameAcc] = "Винительный &ndash; позвать кого?";
            this.Prompt[UserStatus.GetNameIns] = "Творительный &ndash; сражаться с кем?";
            this.Prompt[UserStatus.GetNamePre] = "Предложный &ndash; думать о ком?";
            this.Prompt[UserStatus.GetGender] = "Укажите пол персонажа (м/ж/с)";
            this.Prompt[UserStatus.CreatePassword] = "Придумайте пароль";
            this.Prompt[UserStatus.ConfirmPassword] = "Повторите пароль для проверки";
            this.Prompt[UserStatus.GetEmail] = "Укажите электронный адрес на всякий случай";
            this.Prompt[UserStatus.ConfirmCheck] = "Проверьте, все ли правильно:";
        }

        private void Create(User user, string input)
        {
            user.Credentials = new NewChar();
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameNom]);
        }

        private void GetNameNom(User user, string input)
        {
            if (this.service.CharExists(input))
            {
                user.Output.Append("Персонаж с таким именем уже существует.<br>");
                user.Output.Append(this.Prompt[user.Status]);
            }
            else
            {
                input = NormalizeName(input);
                user.Credentials.Login = input;
                (user.Credentials as NewChar).Name.Nom = input;
                user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameGen]);
            }
        }

        private void GetNameGen(User user, string input)
        {
            (user.Credentials as NewChar).Name.Gen = NormalizeName(input);
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameDat]);
        }

        private void GetNameDat(User user, string input)
        {
            (user.Credentials as NewChar).Name.Dat = NormalizeName(input);
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameAcc]);
        }

        private void GetNameAcc(User user, string input)
        {
            (user.Credentials as NewChar).Name.Acc = NormalizeName(input);
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameIns]);
        }

        private void GetNameIns(User user, string input)
        {
            (user.Credentials as NewChar).Name.Ins = NormalizeName(input);
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetNamePre]);
        }

        private void GetNamePre(User user, string input)
        {
            (user.Credentials as NewChar).Name.Pre = NormalizeName(input);
            user.Output.Append(this.Prompt[user.Status = UserStatus.GetGender]);
        }

        private void GetGender(User user, string input)
        {
            var letter = char.ToLowerInvariant(input[0]);

            (user.Credentials as NewChar).Gender = letter switch
            {
                'м' => Gender.Male,
                'ж' => Gender.Female,
                'с' => Gender.Neutral,
                _ => Gender.None
            };

            if ((user.Credentials as NewChar).Gender != Gender.None)
            {
                user.Output.Append(this.Prompt[user.Status = UserStatus.CreatePassword]);
            }
            else
            {
                user.Output.Append(this.Prompt[user.Status]);
            }
        }

        private void CreatePassword(User user, string input)
        {
            user.Credentials.Password = input;
            user.Output.Append(this.Prompt[user.Status = UserStatus.ConfirmPassword]);
        }

        private void ConfirmPassword(User user, string input)
        {
            var sameInput = user.Credentials.Password.Equals(input, StringComparison.Ordinal);

            if (sameInput)
            {
                user.Output.Append(this.Prompt[user.Status = UserStatus.GetEmail]);
            }
            else
            {
                user.Output.Append("Пароли не совпадают.<br>");
                user.Output.Append(this.Prompt[user.Status = UserStatus.CreatePassword]);
            }
        }

        private void GetEmail(User user, string input)
        {
            user.Credentials.Email = input;
            this.PromptCheck(user);
        }

        private void PromptCheck(User user)
        {
            var creds = user.Credentials as NewChar;

            user.Output.Append(this.Prompt[user.Status = UserStatus.ConfirmCheck]);

            user.Output.Append($"<br>Здесь стоит {creds.Name.Nom}.<br>");
            user.Output.Append($"Это вещи {creds.Name.Gen}.<br>");
            user.Output.Append($"Дать денег {creds.Name.Dat}.<br>");
            user.Output.Append($"Позвать {creds.Name.Acc}.<br>");
            user.Output.Append($"Сражаться с {creds.Name.Ins}.<br>");
            user.Output.Append($"Думать о {creds.Name.Pre}.<br>");

            var gender = creds.Gender switch
            {
                Gender.Male => "мужской",
                Gender.Female => "женский",
                Gender.Neutral => "средний",
                _ => throw new InvalidOperationException()
            };

            user.Output.Append($"Пол персонажа {gender}.<br>");
            user.Output.Append($"Email: {creds.Email}<br>");

            user.Output.Append("Все верно (д/н)?");
        }

        private void ConfirmCheck(User user, string input)
        {
            var letter = char.ToLowerInvariant(input[0]);

            if (letter == 'д')
            {
                this.SaveChar(user);
                user.Status = UserStatus.Launch;
                this.Next?.Handle(user, string.Empty);
            }
            else if (letter == 'н')
            {
                user.Output.Append(this.Prompt[user.Status = UserStatus.GetNameNom]);
            }
        }

        private void SaveChar(User user) => this.service.SaveChar(new ProtoChar()
        {
            Credentials = user.Credentials,
            Mobile = new ProtoMobile()
            {
                Keywords = (user.Credentials as NewChar).Name.Nom.ToLowerInvariant(),
                Gender = (user.Credentials as NewChar).Gender,
                Name = (user.Credentials as NewChar).Name,
            },
            InRoomVnum = this.config.StartRoomVnum,
        });

        private static string NormalizeName(string name)
        {
            return $"{name.Substring(0, 1).ToUpperInvariant()}{name[1..].ToLowerInvariant()}";
        }
    }
}
