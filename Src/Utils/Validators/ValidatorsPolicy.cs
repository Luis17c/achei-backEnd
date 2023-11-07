namespace Utils {
    namespace Validators {
        static public class ValidatorsPolicy {
            public static string maximumLength(string fieldName, int maximumLength) {
                return $"O campo '{fieldName}' excedeu o limite de {maximumLength} caracteres.";
            }
            public static string minimumLength(string fieldName, int minimumLength) {
                return $"O campo '{fieldName}' precisa ter no mínimo {minimumLength} caracteres";
            }
            public static string notEmpty(string fieldName) {
                return $"O campo '{fieldName}' é obrigatório.";
            }
            public static string wrongFormat(string fieldName, string rightFormat) {
                return $"O campo '{fieldName}' não é um {rightFormat}.";
            }
        }
    }
}