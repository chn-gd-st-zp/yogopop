namespace YogoPop.Component.MQ.RabbitMQ;

public interface IRabbitMQMessageEntity
{
    //
}

public class RabbitMQMessageEntity : IRabbitMQMessageEntity
{
    //
}

public static class RabbitMQExtend
{
    public static void Add(this List<IRabbitMQMessageEntity> messageEntities, IRabbitMQMessageEntity messageEntity)
    {
        messageEntities = messageEntities == null ? new List<IRabbitMQMessageEntity>() : messageEntities;
        messageEntities.Add(messageEntity);
    }
}