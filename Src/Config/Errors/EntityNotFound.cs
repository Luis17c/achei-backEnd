namespace Errors {
    public static class EntityNotFound {
        public static string Throw (string entityName) {
            return $"'{entityName}' not found";
        }
    }
}