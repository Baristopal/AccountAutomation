namespace Library.Entities.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class NoSqlConfigAttribute : Attribute
{
    public string BucketName { get; set; }
    public string CollectionName { get; set; }


    public NoSqlConfigAttribute(string bucketName,string collectionName)
    {
        BucketName = bucketName;
        CollectionName = collectionName;
    }
}
