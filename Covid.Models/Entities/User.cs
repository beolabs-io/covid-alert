using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Covid.Models.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Key { get; set; }

        public IList<Alert> Alerts { get; set; }

        public User()
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] key = Encoding.GetEncoding("utf-8").GetBytes(Guid.NewGuid().ToString());
            this.Key = HexStringFromBytes(sha.ComputeHash(key));
        }

        private string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
