using FluentNHibernate.Mapping;

namespace _20200206_nhibernate_gyudong.Mappings
{
    public class studentMapping : ClassMap<Model.student>
    {
        public studentMapping()
        {
            Id(x => x.grade);
            Map(x => x.cclass);
            Map(x => x.no);
            Map(x => x.name);
            Map(x => x.score);
            Table("student");
        }
    }
}
