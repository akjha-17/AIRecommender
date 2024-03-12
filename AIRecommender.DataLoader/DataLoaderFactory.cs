using AIRecommender_DataLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace AIRecommender.DataLoader
{
    public class DataLoaderFactory
    {
        protected DataLoaderFactory()
        {

        }
        public static readonly DataLoaderFactory Instance = new DataLoaderFactory();

        // public static readonly DataLoaderFactory Instance = new DataLoaderFactory();

        public virtual IDataLoader GetDataLoader()
        {
            string className = ConfigurationManager.AppSettings["Loader"];
            Console.WriteLine(className);
            Type type = Type.GetType(className);
            return (IDataLoader)Activator.CreateInstance(type);


        }

    }
}
