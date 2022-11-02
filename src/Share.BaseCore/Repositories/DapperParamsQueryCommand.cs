namespace Share.BaseCore.Repositories
{
    public class DapperParamsQueryCommand
    {
        public string FieldName { get; set; }
        /// <summary>
        /// >; <; =; >=; NOTIN; IN... 
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// AND, OR
        /// </summary>
        public string SqlOperator { get; set; }
        public string ValueCompare { get; set; }
    }
}
