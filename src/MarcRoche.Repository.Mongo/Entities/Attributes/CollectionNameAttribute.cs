using System;

namespace MarcRoche.Repository.Mongo.Entities.Attributes
{
    public class CollectionNameAttribute : Attribute
    {
        private string _collectionName;

        public CollectionNameAttribute(string collectionName)
        {
            _collectionName = collectionName;
        }

        public string Name 
        {
            get { return _collectionName; } 
        }
    }
}
