using MSSQLDataGenerator.BDLoader.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace MSSQLDataGenerator.BDLoader
{
    public class BDL
    {
        readonly Stopwatch timer = new Stopwatch();

        public void LoadDataInTables(DbContext context)
        {
            var dbType = context.GetType();
            var dbSetProperties = dbType.GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            LoadMasterData(context);
            LoadExactMasterData(context);
            LoadData(context);

        }


        public void LoadData(DbContext context)
        {
            var dbSetProperties = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            try
            {
                foreach (var property in dbSetProperties)
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];
                    if (entityType.GetCustomAttributes(typeof(BulkDataLoaderXRowsOfData), true).Length == 1)
                    {
                        var Data_BulkDataLoaderXRowsOfData = (BulkDataLoaderXRowsOfData)entityType.GetCustomAttributes(typeof(BulkDataLoaderXRowsOfData), true)[0];
                        var NumberOfRows = Data_BulkDataLoaderXRowsOfData.NumberOfRows;

                        var entityTypeName = entityType.FullName;
                        var entityTypeClass = Type.GetType(entityTypeName);
                        var entityInstance = Activator.CreateInstance(entityTypeClass);
                        var actualEntity = (dynamic)entityInstance;

                        var List = GenerateDummeyData(actualEntity, NumberOfRows);

                        var dbType = context.GetType();
                        var newDbContext = (DbContext)Activator.CreateInstance(dbType);

                        Console.WriteLine("\nstarted Inserting in to Table :" + entityTypeName);

                        timer.Start();

                        newDbContext.AddRange(List);
                        newDbContext.SaveChanges();

                        Console.WriteLine("Completed Inserting in to Table :" + entityTypeName +" time elapsed : " + timer.Elapsed.ToString()+"\n");

                        timer.Stop();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }



        public void LoadMasterData(DbContext context)
        {
            var dbSetProperties = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            try
            {
                foreach (var property in dbSetProperties)
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];
                    if (entityType.GetCustomAttributes(typeof(BulkDataLoaderMasterTable), true).Length == 1)
                    {
                        var entityTypeName = entityType.FullName;
                        var entityTypeClass = Type.GetType(entityTypeName);
                        var entityInstance = Activator.CreateInstance(entityTypeClass);
                        var actualEntity = (dynamic)entityInstance;

                        var List = GenerateMasterData(actualEntity);

                        var dbType = context.GetType();
                        var newDbContext = (DbContext)Activator.CreateInstance(dbType);

                        Console.WriteLine("\nstarted Inserting in to Table :" + entityTypeName);

                        timer.Start();

                        newDbContext.AddRange(List);
                        newDbContext.SaveChanges();

                        Console.WriteLine("Completed Inserting in to Table :" + entityTypeName + " time elapsed : " + timer.Elapsed.ToString() + "\n");

                        timer.Stop();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void LoadExactMasterData(DbContext context)
        {

            var dbSetProperties = context.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));

            try
            {
                foreach (var property in dbSetProperties)
                {
                    var entityType = property.PropertyType.GetGenericArguments()[0];
                    if (entityType.GetCustomAttributes(typeof(BulkDataLoaderExactTable), true).Length == 1)
                    {
                        var entityTypeName = entityType.FullName;
                        var entityTypeClass = Type.GetType(entityTypeName);
                        var entityInstance = Activator.CreateInstance(entityTypeClass);
                        var actualEntity = (dynamic)entityInstance;

                        var List = GenerateExactMasterData(actualEntity);

                        var dbType = context.GetType();
                        var newDbContext = (DbContext)Activator.CreateInstance(dbType);


                        Console.WriteLine("\nstarted Inserting in to Table :" + entityTypeName);

                        timer.Start();

                        newDbContext.AddRange(List);
                        newDbContext.SaveChanges();

                        Console.WriteLine("Completed Inserting in to Table :" + entityTypeName + " time elapsed : " + timer.Elapsed.ToString()+"\n");

                        timer.Stop();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }


        #region Private

        List<TEntity> GenerateDummeyData<TEntity>(TEntity entity, Int64 NumberOfRecords)
        {
            List<TEntity> entities = new List<TEntity>();
            Type type = typeof(TEntity);

            var indexAttribute = type.GetCustomAttributes(typeof(IndexAttribute), true)
         .OfType<IndexAttribute>();

            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < NumberOfRecords; i++)
            {
                object instance = Activator.CreateInstance(type);

                foreach (PropertyInfo property in properties)
                {
                    var BulkDataLoaderAttribute = property.GetCustomAttributes(typeof(BulkDataLoader), true);
                    if (BulkDataLoaderAttribute.Length == 1)
                    {
                        var BulkDataLoader = (BulkDataLoader)BulkDataLoaderAttribute[0];

                        Random random = new Random();
                        switch (Type.GetTypeCode(property.PropertyType))
                        {
                            case TypeCode.String:

                                if (BulkDataLoader.HasFormat)
                                {
                                    var Format = BulkDataLoader.Format;
                                    var FormatType = BulkDataLoader.FormatType;
                                    property.SetValue(instance, GenerateFormatData(Format, FormatType));
                                }
                                else
                                {
                                    var AllPossibleValues = BulkDataLoader.Values;
                                    property.SetValue(instance, AllPossibleValues[random.Next(AllPossibleValues.Count)]);
                                }

                                break;
                            case TypeCode.Int32:
                                property.SetValue(instance, random.Next(BulkDataLoader.MinValue, BulkDataLoader.MaxValue));

                                break;

                            case TypeCode.DateTime:
                                var FromDate = BulkDataLoader.FromDate;
                                var ToDate = BulkDataLoader.ToDate;
                                property.SetValue(instance, SelectRandomDate(FromDate, ToDate));

                                break;
                        }
                    }
                }
                entities.Add((TEntity)instance);
            }




            if (indexAttribute != null)
            {
                foreach (var attr in indexAttribute)
                {
                    var IsUnique = attr.IsUniqueHasValue && attr.IsUnique;
                    if (IsUnique)
                    {
                        var propName = attr.PropertyNames[0];

                        entities = entities.GroupBy(e => e.GetType().GetProperty(propName).GetValue(e)).Select(g => g.First()).ToList();
                    }
                }
            }
            
            return entities;
        }

        List<TEntity> GenerateMasterData<TEntity>(TEntity entity)
        {
            List<TEntity> entities = new List<TEntity>();
            Type type = typeof(TEntity);

            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var BulkDataLoaderAttribute = property.GetCustomAttributes(typeof(BulkDataLoader), true);
                if (BulkDataLoaderAttribute.Length == 1)
                {
                    var BulkDataLoader = (BulkDataLoader)BulkDataLoaderAttribute[0];
                    var AllValues = BulkDataLoader.Values;

                    for (int i = 0; i < AllValues.Count; i++)
                    {
                        object instance = Activator.CreateInstance(type);

                        property.SetValue(instance, AllValues[i]);

                        entities.Add((TEntity)instance);
                    }
                }
            }
            return entities;
        }

        List<TEntity> GenerateExactMasterData<TEntity>(TEntity entity)
        {
            List<TEntity> entities = new List<TEntity>();
            Type type = typeof(TEntity);

            PropertyInfo[] properties = type.GetProperties();


            var NumberOfRecords = GetNumberOfPropertyPassedInBulkDataLoader(properties);

            for (int i = 0; i < NumberOfRecords; i++)
            {
                object instance = Activator.CreateInstance(type);

                foreach (PropertyInfo property in properties)
                {
                    var BulkDataLoaderAttribute = property.GetCustomAttributes(typeof(BulkDataLoader), true);
                    if (BulkDataLoaderAttribute.Length == 1)
                    {
                        var BulkDataLoader = (BulkDataLoader)BulkDataLoaderAttribute[0];

                        switch (Type.GetTypeCode(property.PropertyType))
                        {
                            case TypeCode.String:
                                var AllPossibleValues = BulkDataLoader.Values;
                                property.SetValue(instance, AllPossibleValues[i]);

                                break;
                            case TypeCode.Int16:
                                var Int16Values = BulkDataLoader.Int16Values;
                                property.SetValue(instance, Int16Values[i]);

                                break;
                            case TypeCode.Int32:
                                var Int32Values = BulkDataLoader.Int32Values;
                                property.SetValue(instance, Int32Values[i]);

                                break;
                            case TypeCode.Int64:
                                var Int64Values = BulkDataLoader.Int64Values;
                                property.SetValue(instance, Int64Values[i]);

                                break;
                        }
                    }
                }

                entities.Add((TEntity)instance);
            }


            return entities;
        }

        int GetNumberOfPropertyPassedInBulkDataLoader(PropertyInfo[] properties)
        {
            foreach (PropertyInfo property in properties)
            {
                var BulkDataLoaderAttribute = property.GetCustomAttributes(typeof(BulkDataLoader), true);
                if (BulkDataLoaderAttribute.Length == 1)
                {
                    var BulkDataLoader = (BulkDataLoader)BulkDataLoaderAttribute[0];

                    switch (Type.GetTypeCode(property.PropertyType))
                    {
                        case TypeCode.String:
                            return BulkDataLoader.Values.Count;

                        case TypeCode.Int32:
                            return BulkDataLoader.Int32Values.Count;
                        case TypeCode.Int64:
                            return BulkDataLoader.Int64Values.Count;
                        case TypeCode.Int16:
                            return BulkDataLoader.Int16Values.Count;
                    }
                }
            }

            return 0;
        }

        string GenerateFormatData(string Format, DataFormatType FormatType)
        {
            if (Format != null)
            {
                StringBuilder result = new StringBuilder();
                bool inBrackets = false;
                foreach (char c in Format)
                {
                    if (c == '{')
                    {
                        inBrackets = true;
                    }
                    else if (c == '}')
                    {
                        inBrackets = false;
                    }
                    else if (c == 'X' && inBrackets)
                    {
                        char randomLetter = GetRandomChar(FormatType);
                        result.Append(randomLetter);
                    }
                    else
                    {
                        result.Append(c);
                    }
                }
                return result.ToString();

            }
            return null;
        }

        char GetRandomChar(DataFormatType FormatType)
        {
            Random random = new Random();
            string chars = null;
            switch (FormatType)
            {
                case DataFormatType.AlfaNumeric:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    break;
                case DataFormatType.Alfabetic:
                    chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                    break;
                case DataFormatType.Numeric:
                    chars = "0123456789";
                    break;
            }

            return chars[random.Next(chars.Length)];
        }


        DateTime SelectRandomDate(DateTime fromDate, DateTime toDate)
        {
            Random random = new Random();
            TimeSpan timeSpan = toDate - fromDate;
            TimeSpan randomSpan = new TimeSpan((long)(random.NextDouble() * timeSpan.Ticks));
            return fromDate + randomSpan;
        }


        #endregion Private
    }


}