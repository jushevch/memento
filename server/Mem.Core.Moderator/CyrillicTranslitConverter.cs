using System.Collections.Generic;
using System.Text;

namespace Mem.Core.Moderator
{
    public class CyrillicTranslitConverter : ITranslitConverter
    {
        private readonly StringBuilder sb = new ();

        private readonly Dictionary<char, char> dic = new ()
        {
            ['а'] = 'a',
            ['б'] = 'b',
            ['в'] = 'v',
            ['г'] = 'h',
            ['д'] = 'd',
            ['е'] = 'e',
            ['ё'] = '8',
            ['ж'] = 'g',
            ['з'] = 'z',
            ['и'] = 'i',
            ['й'] = 'j',
            ['к'] = 'k',
            ['л'] = 'l',
            ['м'] = 'm',
            ['н'] = 'n',
            ['о'] = 'o',
            ['п'] = 'p',
            ['р'] = 'r',
            ['с'] = 's',
            ['т'] = 't',
            ['у'] = 'u',
            ['ф'] = 'f',
            ['х'] = 'x',
            ['ц'] = 'c',
            ['ч'] = '4',
            ['ш'] = '6',
            ['щ'] = 'w',
            ['ъ'] = '_',
            ['ы'] = 'y',
            ['ь'] = '-',
            ['э'] = '3',
            ['ю'] = 'q',
            ['я'] = '9',
        };

        public string Convert(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return name;
            }

            this.sb.Length = 0;

            foreach (var letter in name.ToLowerInvariant())
            {
                this.sb.Append(this.dic.ContainsKey(letter) ? this.dic[letter] : letter);
            }

            return this.sb.ToString();
        }
    }
}
