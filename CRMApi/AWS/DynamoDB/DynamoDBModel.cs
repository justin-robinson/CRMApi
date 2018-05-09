using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Amazon.DynamoDBv2.DocumentModel;
using CRMApi.Utils;
namespace CRMApi.AWS.DynamoDB {
    public abstract class DynamoDBModel {

        protected Table Table { get; set; }
        protected string TableName { get; set; }

        protected DynamoDBModel(){}

        protected DynamoDBModel(Document document) {
            FromDocument(document);
        }

        public void FromDocument(Document document) {
            foreach (var key in document.Keys) {
                var property = this.GetType().GetProperty(key);
                if (property != null) {
                    // Amazon.DynamoDBv2.DocumentModel.DynamoDBEntry doesn't implement the IConvertible
                    // interface so we have to whitelist type conversion here
                    if (property.PropertyType == typeof(string)) {
                        property.SetValue(this, (string)document[key]);
                    } else if (property.PropertyType == typeof(int)) {
                        property.SetValue(this, (int)document[key]);
                    } else if (property.PropertyType == typeof(long)) {
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

        public async Task<DynamoDBModel> Get(string id) {
            Table table = GetTable();
            FromDocument(await table.GetItemAsync(id));
            return this;
        }

        public async Task<DynamoDBModel> Save() {
            Table table = GetTable();
            await table.PutItemAsync(this.ToDocument());
            return this;
        }

        public async Task<List<DynamoDBModel>> GetAll() {
            Table table = GetTable();
            ScanFilter notDeleted = new ScanFilter();
            notDeleted.AddCondition("DeleteTime", ScanOperator.Equal, 0);
            Search results = table.Scan(notDeleted);
            List<DynamoDBModel> output = new List<DynamoDBModel>();
            List<Document> set;
            do {
                set = await results.GetNextSetAsync();
                set.ForEach(d => {
                    DynamoDBModel model = (DynamoDBModel)Activator.CreateInstance(this.GetType());
                    model.FromDocument(d);
                    output.Add(model);
                });
            } while (!results.IsDone);

            return output;
        }


        private Table GetTable() {
            if (this.Table == null) {
                try {
                    this.Table = Table.LoadTable (Client.client, this.TableName);
                } catch (Exception e) {
                    Console.WriteLine ($"\n Error: failed to load the {this.TableName} table; " + e.Message);
                }
            }


            return this.Table;
        }
    }
}
