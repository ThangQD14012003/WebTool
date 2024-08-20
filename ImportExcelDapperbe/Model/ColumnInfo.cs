namespace ImportExcelDapper.Model
{
    public class ColumnInfo
    {
        public string ColumnName { set; get; }  
        public string DataType { set; get;}
        public string IsNullable { set; get; }
        //public string DefaultConstraint { set; get; } 
        public bool IsIdentity { set; get; }
    }
}
