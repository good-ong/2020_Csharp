using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Configuration;

namespace _20200206_nhibernate_gyudong
{
    public class SessionFactory
    {
        private static volatile ISessionFactory iSessionFactory;
        private static object syncRoot = new object();

        public static ISession OpenSession
        {
            get
            {
                if (iSessionFactory == null)
                {
                    lock (syncRoot)
                    {
                        if (iSessionFactory == null)
                        {
                            iSessionFactory = BuildSessionFactory();
                        }
                    }
                }
                return iSessionFactory.OpenSession();
            }
        }

        private static ISessionFactory BuildSessionFactory()
        {
            try
            {
                string connString = ConfigurationManager.AppSettings["connection_string"];

                return Fluently.Configure()
                    .Database(MySQLConfiguration.Standard
                    .ConnectionString(connString))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                    .ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                throw ex;
            }
        }

        // Create Session
        private static AutoPersistenceModel CreateMappings()
        {
            return AutoMap
                .Assembly(System.Reflection.Assembly.GetCallingAssembly())
                .Where(testc => testc.Namespace == "_20200206_nhibernate_gyudong.Model");
        }
        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        { 
        
        }
    }
}
