using System;
using Amazon.DynamoDBv2.DocumentModel;
using CRMApi.Utils;
namespace CRMApi.AWS {
    public class DynamoDBModel {
        public DynamoDBModel(){}

        public DynamoDBModel(Document document) {
            foreach(var key in document.Keys) {
                var property = this.GetType().GetProperty(key);
                if (property != null) {
                    // Amazon.DynamoDBv2.DocumentModel.DynamoDBEntry doesn't implement the IConvertible
                    // interface so we have to whitelist type conversion here
                    if (property.PropertyType == typeof(string)) {
                        property.SetValue(this, (string)document[key]);
                    } else if(property.PropertyType == typeof(int)) {
                        property.SetValue(this, (int)document[key]);
                    } else if(property.PropertyType == typeof(long)) {
                        property.SetValue(this, (long)document[key]);
                    } else {
                        throw new Exception("failed to cast unknown document property type");
                    }
                }
            }
            
        }

        public Document ToDocument() {
            Document document = new Document();
            foreach (var property in this.GetType().GetProperties()) {
                document[property.Name] = CastHelper.Cast(
                                property.GetValue(this, null),
                                property.PropertyType
                );
            }
            return document;
        }
    }
}
