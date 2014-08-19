using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class PACKETLoginRequest
    {
        public String Username;
        public String Password;
        public String Email;
        public PACKETLoginRequest(String Username, String Password, String Email)
        {
            this.Username = Username;
            this.Password = Password;
            this.Email = Email;

        }



    }
    public class PacketDataRequest
    {

        public PacketHeader header;
        public PACKETDataRequest payload;


    }
 public class PACKETDataRequest
    {
        public string Type;

        public PACKETDataRequest(String Type)
        {
         this.Type = Type;
        }


 }

    [Serializable]
    public class PacketHeader
    {
        public PacketHeader(String TYPE)
        {
            PTYPE = TYPE;
        }
        public String PTYPE;
    }


