using AppCashback.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AppCashback.MongoDb
{
    internal static class Mapeamentos
    {
        public static void Registrar()
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));

            BsonClassMap.RegisterClassMap<Compra>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);

                cm.MapMember(c => c.Codigo).SetElementName("cod");
                cm.MapMember(c => c.Cpf).SetElementName("cpf");
                cm.MapMember(c => c.Data).SetElementName("data");
                cm.MapMember(c => c.Status).SetElementName("status");
                cm.MapMember(c => c.Valor).SetElementName("valor");
            });

            BsonClassMap.RegisterClassMap<Revendedor>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);

                cm.MapMember(c => c.Cpf).SetElementName("cpf");
                cm.MapMember(c => c.Email).SetElementName("email");
                cm.MapMember(c => c.Nome).SetElementName("nome");
                cm.MapMember(c => c.Senha).SetElementName("senha");
            });
        }
    }
}
