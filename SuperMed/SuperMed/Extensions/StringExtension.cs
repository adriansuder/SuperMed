namespace SuperMed.Extensions
{
    public static class StringExtension
    {
        public static string Capitalize(this string input)
        {
            var ret = input.Trim().ToCharArray();

            if (input.Length == 1)
            {
                return input.ToUpper();
            }

            for (int i = 0; i < ret.Length-1; i++)
            {
                if (i == 0)
                {
                    ret[0] = ret[0].ToString().ToUpper()[0];
                }
                else if (ret[i-1] == '-')
                {
                    ret[i] = ret[i].ToString().ToUpper()[0];
                }
                else if (ret[i - 1] == ' ')
                {
                    ret[i] = ret[i].ToString().ToUpper()[0];
                }
                else
                {
                    ret[i] = ret[i].ToString().ToLower()[0];
                }
            }

            var output = "";

            foreach (var character in ret)
            {
                output += character;
            }

            return output;
        }
    }
}
