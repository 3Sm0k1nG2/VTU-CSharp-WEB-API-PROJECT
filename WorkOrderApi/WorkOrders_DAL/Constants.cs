namespace WorkOrders_DAL
{
    public static class Constants
    {
        public const string SQL_CONNECTION_STRING_DEFAULT = "WorkOrders";
        public const string EXCEL_FILE_PATH = "D:\\VTU-Programming\\02. Second Year\\01. First Semester\\API\\Project\\External\\Resources\\sampledataworkorders.xlsx";
        
        // Error messages
        public const string ERROR_DUPLICATE_KEY_FOUND = "An error occurred while saving the entity changes. See the inner exception for details.";
        public const string ERROR_ENTITY_NOT_FOUND = "Entity was not found.";
        public const string ERROR_DTO_IS_EMPTY = "Dto cannot be empty.";
        public const string ERROR_FIELD_IS_EMPTY = "Field cannot be empty.";
        public const string ERROR_TECHS_COUNT_NOT_MATCHING = "Tech count does not match any in DB.";
    }
}
