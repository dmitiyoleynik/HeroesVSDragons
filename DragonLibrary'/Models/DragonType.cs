using GraphQL.Types;

namespace DragonLibrary_.Models
{
    public class DragonType : ObjectGraphType<Dragon>
    {
        public DragonType()
        {
            Field(d => d.Id);
            Field(d => d.Name);
            Field(d => d.Hp);
            Field(d => d.MaxHp);
            Field<DateTimeGraphType>("Created");
        }
    }
}
