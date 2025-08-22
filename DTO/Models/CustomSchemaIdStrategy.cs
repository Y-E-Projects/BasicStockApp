namespace DTO.Models
{
    public static class CustomSchemaIdStrategy
    {
        public static string GetSchemaId(Type modelType)
        {
            return modelType.FullName.Replace(".", "_").Replace("+", "_");
        }
    }
}