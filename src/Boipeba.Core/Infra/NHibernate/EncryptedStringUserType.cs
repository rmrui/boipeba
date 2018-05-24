using System;
using System.Data;
using Boipeba.Core.Infra.Services;
using NHibernate.SqlTypes;
using NHibernate.Type;

#pragma warning disable 1591

namespace Boipeba.Core.Infra.NHibernate
{
    /// <summary>
    /// NHibernate User Type para string criptografada
    /// </summary>
    public class EncryptedStringUserType : AbstractStringType
    {
        private readonly TripleDESEncryptionService encryptionService;

        public EncryptedStringUserType() : base(new StringSqlType())
        {
            encryptionService = new TripleDESEncryptionService();
        }

        public override string Name
        {
            get { return "EncryptedString"; }
        }

        public override void Set(IDbCommand cmd, object value, int index)
        {
            string encrypted = encryptionService.Encrypt(Convert.ToString(value));

            base.Set(cmd, encrypted, index);
        }

        public override object Get(IDataReader rs, int index)
        {
            string value = Convert.ToString(base.Get(rs, index));

            return encryptionService.Decrypt(value);
        }

        public override object Get(IDataReader rs, string name)
        {
            string value = Convert.ToString(base.Get(rs, name));

            return encryptionService.Decrypt(value);
        }
    }
}