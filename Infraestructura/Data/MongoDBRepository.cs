﻿using MongoDB.Driver;

namespace Infraestructura.Data
{
    public class MongoDBRepository
    {
        public MongoClient client;

        public IMongoDatabase db;

        public MongoDBRepository(){
            client = new MongoClient("mongodb+srv://s76325953:s76325953@cluster0.acwg8vw.mongodb.net");
            db = client.GetDatabase("Briefcase");
        }
    }
}
