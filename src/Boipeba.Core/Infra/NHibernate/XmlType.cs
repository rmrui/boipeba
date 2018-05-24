using System;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using NHibernate.SqlTypes;
using NHibernate.Type;

#pragma warning disable 1591

namespace Boipeba.Core.Infra.NHibernate
{
    /// <summary>
    /// NHibernate User Type para string criptografada
    /// </summary>
    public class XmlType<T> : AbstractStringType where T: class, new()
    {
        public XmlType() : base(new XmlSqlType())
        {
            
        }

        public override string Name => "Xml";

        public override void Set(IDbCommand cmd, object value, int index)
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(T));

                serializer.Serialize(stringwriter, value);
                var item = stringwriter.ToString();

                base.Set(cmd, item, index);
            }
        }

        public override object Get(IDataReader rs, int index)
        {
            var value = Convert.ToString(base.Get(rs, index));

            using (var stringReader = new StringReader(value))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stringReader) as T;
            }
        }

        public override object Get(IDataReader rs, string name)
        {
            var value = Convert.ToString(base.Get(rs, name));

            using (var stringReader = new StringReader(value))
            {
                var serializer = new XmlSerializer(typeof(T));
                return serializer.Deserialize(stringReader) as T;
            }
        }
    }
}