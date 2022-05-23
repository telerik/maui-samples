using System.Text;

namespace QSF
{
    public static class Extensions
    {
        public static string InsertSpacesInPascalCase(this string pascalCaseInput)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pascalCaseInput.Length; i++)
            {
                if (i > 0 && char.IsUpper(pascalCaseInput[i]))
                {
                    sb.Append(' ');
                }

                sb.Append(pascalCaseInput[i]);
            }

            return sb.ToString();
        }
    }
}
